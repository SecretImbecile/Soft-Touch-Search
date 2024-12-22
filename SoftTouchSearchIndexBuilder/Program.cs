// <copyright file="Program.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearchIndexBuilder
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Lucene.Net.Documents;
    using Microsoft.EntityFrameworkCore;
    using SoftTouchSearch.Data;

    /// <summary>
    /// Builds a Lucene.NET index for use by SoftTouchSearch.
    /// </summary>
    internal class Program
    {
        // Fields

        /// <summary>
        /// Padding size used in console output.
        /// </summary>
        private const int PadRight = 25;

        // Methods

        /// <summary>
        /// Builds a Lucene.NET index for use by SoftTouchSearch.
        /// </summary>
        private static void Main(string[] args)
        {
            Stopwatch stopwatchTotal = Stopwatch.StartNew();

            // Verify arguments
            if (args.Length != 2)
            {
                throw new ArgumentException("Expected exactly 2 command-line arguments. (DB and export path)");
            }

            string dbPath = args[0];
            string exportPath = args[1];

            // Create the export folders if they do not exist
            if (!Directory.Exists(exportPath))
            {
                Directory.CreateDirectory(exportPath);
            }

            string indexDestinationPath = Path.Combine(exportPath, "softtouchsearch_index");
            if (!Directory.Exists(indexDestinationPath))
            {
                Directory.CreateDirectory(indexDestinationPath);
            }

            // 1. Copy the database to the export directory
            Console.Write("Copying Database...".PadRight(PadRight));

            string dbDestinationPath = Path.Combine(exportPath, "softtouchsearch.db");
            File.Copy(dbPath, dbDestinationPath, true);

            // 2. Get episodes from database
            Console.WriteLine($"DONE");
            Console.Write("Loading Episodes...".PadRight(PadRight));

            DbContextOptions<StoryDbContext> options = new DbContextOptionsBuilder<StoryDbContext>()
                .UseSqlite($"Data Source={dbDestinationPath}")
                .Options;
            StoryDbContext context = new(options);
            IndexBuilderMethods methods = new(context, indexDestinationPath);

            IEnumerable<Document> documents = methods.ConvertEpisodes();

            // 3. Build index
            Console.WriteLine($"DONE");
            Console.Write("Building Index...".PadRight(PadRight));

            methods.BuildIndex(documents);

            // 4. Open export folder (Windows)
            Console.WriteLine($"DONE");
            Console.WriteLine($"Finished indexing {documents.Count()} episodes in {stopwatchTotal.ElapsedMilliseconds} ms");
            stopwatchTotal.Stop();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ProcessStartInfo processStartInfo = new()
                {
                    FileName = @"c:\windows\explorer.exe",
                    Arguments = exportPath,
                    WindowStyle = ProcessWindowStyle.Normal,
                };
                Process.Start(processStartInfo);
            }
        }
    }
}