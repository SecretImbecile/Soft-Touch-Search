// <copyright file="Program.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearchIndexBuilder
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;
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

            // 2a. Transfer Chapters
            Console.WriteLine($"DONE");
            Console.Write("Transferring Chapters...".PadRight(PadRight));

            List<Chapter> chaptersExport;
            try
            {
                List<ChapterImport> chaptersImport = importContext.Chapters
                    .OrderBy(chapter => chapter.Number)
                    .ToList();

                chaptersExport = chaptersImport
                    .Select(chapter => new Chapter()
                    {
                        Id = chapter.Id,
                        Number = chapter.Number,
                        Title = chapter.Title,
                        Episodes = [],
                    })
                    .ToList();

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

            // 2b. Transfer Episodes
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
                    EpisodeImport episodeImport = importContext.Episodes
                        .Where(episode => episode.Id == id)
                        .Single();

                    Chapter matchingChapter = chaptersExport
                        .Where(chapter => chapter.Id == episodeImport.ChapterId)
                        .Single();

                    bool isFirstInChapter = importContext.Episodes
                        .Where(episode => episode.ChapterId == matchingChapter.Id)
                        .OrderBy(episode => episode.EpisodeNumber)
                        .FirstOrDefault()?
                        .Id == id;

                    bool isNonStory = exclusionRules
                        .Any(rule => rule.CheckEpisodeExcluded(episodeImport));

                    Episode episodeExport = new()
                    {
                        Id = episodeImport.Id,
                        ChapterId = episodeImport.ChapterId,
                        EpisodeNumber = episodeImport.EpisodeNumber,
                        Title = episodeImport.Title,
                        PublishDate = episodeImport.PublishDate,
                        UrlExternal = episodeImport.UrlExternal,
                        IsFirstEpisodeInChapter = isFirstInChapter,
                        IsNonStory = isNonStory,
                        UrlTapas = episodeImport.UrlTapas,
                        Chapter = matchingChapter,
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
    }
}