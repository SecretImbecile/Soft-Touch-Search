// <copyright file="IndexService.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Index.Services
{
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Store;
    using Lucene.Net.Util;

    /// <summary>
    /// Provides the search index for the Soft Touch Search.
    /// </summary>
    /// <param name="indexFilePath">Path to store the Lucene.NET Index file in.</param>
    public class IndexService : IIndexService
    {
        // Fields

        /// <summary>
        /// Gets or sets active index writer.
        /// </summary>
        private readonly Directory indexDirectory;

        /// <summary>
        /// Gets or sets active index writer.
        /// </summary>
        private readonly IndexWriter indexWriter;

        /// <summary>
        /// Whether the index has completed building.
        /// </summary>
        private bool indexBuilt = false;

        // Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexService"/> class.
        /// </summary>
        /// <param name="indexFilePath">File path of the Lucene.NET Index file.</param>
        public IndexService(string indexFilePath)
        {
            // Ensures index backward compatibility
            const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

            this.indexDirectory = FSDirectory.Open(indexFilePath);

            // Create an analyzer to process the text
            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            // Create an index writer
            var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
            this.indexWriter = new IndexWriter(this.indexDirectory, indexConfig);

            if (this.indexWriter.NumDocs > 0)
            {
                this.indexWriter.DeleteAll();
                this.indexWriter.Commit();
            }
        }

        // Properties

        /// <inheritdoc/>
        public bool IsIndexBuilt => this.indexBuilt;

        // Methods

        /// <inheritdoc/>
        public void AddToIndex(Document document)
        {
            try
            {
                this.indexWriter.AddDocument(document);
                this.indexWriter.Flush(false, false);
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Skipping Document {document.GetField("Title")}");
            }
        }

        /// <inheritdoc/>
        public void SetIndexBuilt()
        {
            this.indexWriter.Commit();
            this.indexBuilt = true;
        }
    }
}
