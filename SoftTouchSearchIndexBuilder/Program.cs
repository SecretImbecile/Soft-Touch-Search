// <copyright file="Program.cs" company="Jack Kelly">
// Copyright © 2024, 2025 Jack Kelly.
//
// Soft Touch Search is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the
// Free Software Foundation, either version 3 of the License, or
// (at your option)any later version.
//
// Soft Touch Search is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// Soft Touch Search. If not, see [https://www.gnu.org/licenses/].
// </copyright>

namespace SoftTouchSearchIndexBuilder
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using SoftTouchSearch.Data;
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Index;
    using SoftTouchSearchIndexBuilder.Classes;
    using SoftTouchSearchIndexBuilder.Configuration;

    /// <summary>
    /// Builds a Lucene.NET index for use by SoftTouchSearch.
    /// </summary>
    internal class Program
    {
        // Fields

        /// <summary>
        /// Padding size used in console output.
        /// </summary>
        private const int PadRight = 28;

        // Methods

        /// <summary>
        /// Builds a Lucene.NET index for use by SoftTouchSearch.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        internal static void Main(string[] args)
        {
            // Load configuration (arguments or secrets file)
            Console.Write("Starting...".PadRight(PadRight));

            IndexBuilderSettings settings;
            try
            {
                settings = LoadConfiguration(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.Error.WriteLine(ex.Message);
                throw;
            }

            // 1a. Create the export folders if they do not already exist
            if (!Directory.Exists(settings.ExportFolderPath))
            {
                Directory.CreateDirectory(settings.ExportFolderPath);
            }

            string indexDestinationPath = Path.Combine(settings.ExportFolderPath, settings.ExportIndexFolderName);
            if (!Directory.Exists(indexDestinationPath))
            {
                Directory.CreateDirectory(indexDestinationPath);
            }

            // 1b. Open the import and export database contexts.
            DbContextOptions<ImportDbContext> importOptions = new DbContextOptionsBuilder<ImportDbContext>()
                .UseSqlite($"Data Source={settings.ImportDbPath}")
                .Options;
            ImportDbContext importContext = new(importOptions);
            _ = importContext.Database
                .EnsureCreated();

            string dbDestinationPath = Path.Combine(settings.ExportFolderPath, settings.ExportDbFileName);
            DbContextOptions<SearchDbContext> exportOptions = new DbContextOptionsBuilder<SearchDbContext>()
                .UseSqlite($"Data Source={dbDestinationPath}")
                .Options;
            SearchDbContext exportContext = new(exportOptions);
            _ = exportContext.Database.EnsureDeleted();
            exportContext.Database.Migrate();

            // 2a. Transfer Thumbnails
            Console.WriteLine($"DONE");
            Console.Write("Transferring Thumbnails...".PadRight(PadRight));

            Dictionary<string, Thumbnail> thumbnailsExport;
            try
            {
                List<ThumbnailImport> thumbnailsImport = importContext.Thumbnails
                    .ToList();

                thumbnailsExport = [];
                foreach (ThumbnailImport import in thumbnailsImport)
                {
                    string hash = GenerateHash(import.Content);

                    if (!thumbnailsExport.ContainsKey(hash))
                    {
                        Thumbnail export = new()
                        {
                            Content = import.Content,
                            ContentType = import.ContentType,
                        };

                        thumbnailsExport.Add(hash, export);
                    }
                }

                exportContext.Thumbnails
                    .AddRange(thumbnailsExport.Values);

                exportContext
                    .SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.Error.WriteLine(ex.Message);
                throw;
            }

            // 2b. Transfer Chapters
            Console.WriteLine($"DONE");
            Console.Write("Transferring Chapters...".PadRight(PadRight));

            List<Chapter> chaptersExport;
            try
            {
                List<ChapterImport> chaptersImport = importContext.Chapters
                    .OrderBy(chapter => chapter.Number)
                    .Include(chapter => chapter.Episodes)
                    .ThenInclude(episode => episode.Thumbnail)
                    .ToList();

                chaptersExport = [];
                foreach (ChapterImport import in chaptersImport)
                {
                    int viewCount = importContext.Episodes
                        .Where(episode => episode.ChapterId == import.Id)
                        .Sum(episode => episode.Metadata.Views);

                    int likeCount = importContext.Episodes
                        .Where(episode => episode.ChapterId == import.Id)
                        .Sum(episode => episode.Metadata.Likes);

                    int commentCount = importContext.Episodes
                        .Where(episode => episode.ChapterId == import.Id)
                        .Sum(episode => episode.Metadata.Comments);

                    Chapter export = new()
                    {
                        Id = import.Id,
                        Number = import.Number,
                        Title = import.Title,
                        Episodes = [],
                        Metadata = new()
                        {
                            Views = viewCount,
                            Likes = likeCount,
                            Comments = commentCount,
                        },
                        ThumbnailGuid = MatchThumbnail(import, thumbnailsExport),
                    };

                    chaptersExport.Add(export);
                }

                exportContext.Chapters
                    .AddRange(chaptersExport);

                exportContext
                    .SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.Error.WriteLine(ex.Message);
                throw;
            }

            // 2c. Transfer Episodes
            Console.WriteLine($"DONE");
            Console.Write("Transferring Episodes...".PadRight(PadRight));

            List<Guid> episodeIds;
            try
            {
                List<ExclusionRule> exclusionRules = importContext.ExclusionRules
                    .ToList();

                // Don't fetch all the episodes at once (small memory optimisation)
                episodeIds = importContext.Episodes
                    .OrderBy(episode => episode.EpisodeNumber)
                    .Select(episode => episode.Id)
                    .ToList();

                foreach (Guid id in episodeIds)
                {
                    EpisodeImport import = importContext.Episodes
                        .Where(episode => episode.Id == id)
                        .Include(episode => episode.Thumbnail)
                        .Single();

                    Chapter matchingChapter = chaptersExport
                        .Where(chapter => chapter.Id == import.ChapterId)
                        .Single();

                    bool isFirstInChapter = importContext.Episodes
                        .Where(episode => episode.ChapterId == matchingChapter.Id)
                        .OrderBy(episode => episode.EpisodeNumber)
                        .FirstOrDefault()?
                        .Id == id;

                    bool isNonStory = exclusionRules
                        .Any(rule => rule.CheckEpisodeExcluded(import));

                    Episode episodeExport = new()
                    {
                        Id = import.Id,
                        ChapterId = import.ChapterId,
                        EpisodeNumber = import.EpisodeNumber,
                        Title = import.Title,
                        PublishDate = import.PublishDate,
                        UrlExternal = import.UrlExternal,
                        IsFirstEpisodeInChapter = isFirstInChapter,
                        IsNonStory = isNonStory,
                        UrlTapas = import.UrlTapas,
                        Chapter = matchingChapter,
                        Metadata = new()
                        {
                            Mature = import.Metadata.Mature,
                            Views = import.Metadata.Views,
                            Likes = import.Metadata.Likes,
                            Comments = import.Metadata.Comments,
                        },
                        ThumbnailGuid = MatchThumbnail(import, thumbnailsExport),
                    };

                    exportContext.Episodes
                        .Add(episodeExport);

                    exportContext
                        .SaveChanges();
                }

                GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.Error.WriteLine(ex.Message);
                throw;
            }

            // 3. Build search index
            Console.WriteLine($"DONE");
            Console.Write("Building Index...".PadRight(PadRight));

            try
            {
                string indexPath = Path.Combine(settings.ExportFolderPath, settings.ExportIndexFolderName);
                Lucene.Net.Store.Directory indexDirectory = Lucene.Net.Store.FSDirectory.Open(indexPath);

                Analyzer analyzer = new StandardAnalyzer(Constants.AppLuceneVersion);
                IndexWriterConfig indexConfig = new(Constants.AppLuceneVersion, analyzer);
                using IndexWriter indexWriter = new(indexDirectory, indexConfig);

                if (indexWriter.NumDocs > 0)
                {
                    // Remove any existing index contents
                    indexWriter.DeleteAll();
                    indexWriter.Commit();
                }

                foreach (Guid id in episodeIds)
                {
                    EpisodeImport episodeImport = importContext.Episodes
                        .Where(episode => episode.Id == id)
                        .Single();

                    Document document = IndexHelper
                        .ConvertEpisode(episodeImport);

                    indexWriter.AddDocument(document);
                }

                indexWriter.Flush(false, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.Error.WriteLine(ex.Message);
                throw;
            }

            // 4. Open export folder (Windows)
            Console.WriteLine($"DONE");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ProcessStartInfo processStartInfo = new()
                {
                    FileName = @"c:\windows\explorer.exe",
                    Arguments = settings.ExportFolderPath,
                    WindowStyle = ProcessWindowStyle.Normal,
                };
                Process.Start(processStartInfo);
            }
        }

        /// <summary>
        /// Load the application configuration from user secrets or command-line arguments.
        /// </summary>
        /// <param name="args">The command-line arguments provided to the application.</param>
        /// <returns>The bound <see cref="IndexBuilderSettings"/> model.</returns>
        /// <exception cref="InvalidOperationException">The "IndexBuilderSettings" configuration section does not exist.</exception>
        /// <exception cref="NullReferenceException">The "IndexBuilderSettings" configuration bound but a resulting setting was null.</exception>
        private static IndexBuilderSettings LoadConfiguration(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .AddCommandLine(args)
                .Build();

            IndexBuilderSettings settings = new();
            configuration
                .GetRequiredSection(nameof(IndexBuilderSettings))
                .Bind(settings);

            if (string.IsNullOrWhiteSpace(settings.ImportDbPath))
            {
                throw new NullReferenceException("Couldn't load configuration for ImportDbPath");
            }
            else if (string.IsNullOrWhiteSpace(settings.ExportFolderPath))
            {
                throw new NullReferenceException("Couldn't load configuration for ExportDbPath");
            }

            return settings;
        }

        /// <summary>
        /// Generate an MD5 hash for a thumbnail.
        /// </summary>
        /// <param name="content">The binary content of the thumbnail.</param>
        /// <returns>An <see langword="string"/> MD5 hash.</returns>
        private static string GenerateHash(byte[] content)
        {
            byte[] hash = MD5
                .HashData(content);

            return Convert
                .ToHexString(hash)
                .ToLower();
        }

        /// <summary>
        /// Find the thumbnail for an episode.
        /// </summary>
        private static Guid MatchThumbnail(EpisodeImport import, Dictionary<string, Thumbnail> thumbnails)
        {
            string hash = GenerateHash(import.Thumbnail.Content);
            return thumbnails[hash].Id;
        }

        /// <summary>
        /// Find the thumbnail for a chapter.
        /// </summary>
        /// <remarks>
        /// The chapter thumbnail is the same as the first episode in that chapter.
        /// </remarks>
        private static Guid MatchThumbnail(ChapterImport import, Dictionary<string, Thumbnail> thumbnails)
        {
            ThumbnailImport importThumbnail = import
                .Episodes
                .OrderBy(episode => episode.EpisodeNumber)
                .Select(episode => episode.Thumbnail)
                .First();

            string hash = GenerateHash(importThumbnail.Content);
            return thumbnails[hash].Id;
        }
    }
}