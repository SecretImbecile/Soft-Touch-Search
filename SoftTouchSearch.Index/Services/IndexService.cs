// <copyright file="IndexService.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Index.Services
{
    using System.Text;
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Search;
    using Lucene.Net.Search.Highlight;
    using Lucene.Net.Store;
    using Lucene.Net.Util;
    using Microsoft.AspNetCore.Html;
    using SoftTouchSearch.Index.Classes;

    /// <summary>
    /// Provides the search index for the Soft Touch Search.
    /// </summary>
    /// <param name="indexFilePath">Path to store the Lucene.NET Index file in.</param>
    public class IndexService : IIndexService
    {
        // Fields

        /// <summary>
        /// Page size of search results.
        /// </summary>
        public const int SearchPageSize = 10;

        /// <summary>
        /// Gets or sets active analyzer.
        /// </summary>
        private readonly Analyzer analyzer;

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
        public SearchResults Search(Query query, bool loadMore = false)
        {
            using IndexReader reader = this.indexWriter.GetReader(applyAllDeletes: true);
            IndexSearcher searcher = new(reader);

            TopDocs hits;
            if (loadMore)
            {
                hits = searcher.Search(query, 1000);
            }
            else
            {
                hits = searcher.Search(query, SearchPageSize);
            }

            IEnumerable<SearchResult> results = hits.ScoreDocs
                .Select(hit => ConvertHit(hit, searcher, reader, query));
            return new SearchResults(results)
            {
                TotalHits = Math.Min(hits.TotalHits, 1000),
                LastResult = hits.ScoreDocs.LastOrDefault(),
            };
        }

        /// <inheritdoc/>
        public void SetIndexBuilt()
        {
            this.indexWriter.Commit();
            this.indexBuilt = true;
        }

        /// <summary>
        /// Convert a Lucene result into <see cref="SearchResult"/> format.
        /// </summary>
        /// <param name="scoreDoc">Lucene document to convert.</param>
        /// <param name="searcher">Lucene search instance.</param>
        /// <returns><see cref="SearchResult"/> structure containing the search result.</returns>
        private static SearchResult ConvertHit(ScoreDoc scoreDoc, IndexSearcher searcher, IndexReader reader, Query query)
        {
            Document document = searcher.Doc(scoreDoc.Doc);
            string id = document.Get("id");

            return new SearchResult()
            {
                Document = document,
                Id = Guid.Parse(id),
                Score = scoreDoc,
                Snippet = CreateSnippet(scoreDoc.Doc, document.Get("content"), reader, query),
            };
        }

        private static HtmlString CreateSnippet(int docId, string content, IndexReader reader, Query query)
        {
            SimpleHTMLFormatter formatter = new("<mark>", "</mark>");
            QueryScorer scorer = new(query);
            Highlighter highlighter = new(formatter, scorer);
            SimpleSpanFragmenter fragmenter = new(scorer, 10);

            TokenStream stream = TokenSources.GetAnyTokenStream(reader, docId, "content", new StandardAnalyzer(LuceneVersion.LUCENE_48));
            IEnumerable<string> fragments = highlighter.GetBestFragments(stream, content, 10).ToList();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string fragment in fragments)
            {
                stringBuilder.AppendLine($"<span>{fragment}</span>");
            }
            return new HtmlString(stringBuilder.ToString());
        }
    }
}
