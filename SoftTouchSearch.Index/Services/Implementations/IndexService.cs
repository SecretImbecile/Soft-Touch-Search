// <copyright file="IndexService.cs" company="Jack Kelly">
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

namespace SoftTouchSearch.Index.Services.Implementations
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
    using SoftTouchSearch.Index.Classes;
    using SoftTouchSearch.Index.Services;
    using Constants = SoftTouchSearch.Index.Constants;

    /// <summary>
    /// Provides the search index for the Soft Touch Search.
    /// </summary>
    /// <param name="indexFilePath">Path to store the Lucene.NET Index file in.</param>
    /// <remarks>
    /// Initializes a new instance of the <see cref="IndexService"/> class.
    /// </remarks>
    public class IndexService(string indexFilePath) : IIndexService
    {
        // Fields

        /// <summary>
        /// Gets or sets active index writer.
        /// </summary>
        private readonly Directory indexDirectory = FSDirectory.Open(indexFilePath);

        // Methods

        /// <inheritdoc/>
        public SearchResults Search(Query query, bool loadMore = false)
        {
            using IndexReader reader = DirectoryReader.Open(this.indexDirectory);
            IndexSearcher searcher = new(reader);

            TopDocs hits;
            if (loadMore)
            {
                hits = searcher.Search(query, 1000);
            }
            else
            {
                hits = searcher.Search(query, Constants.SearchPageSize);
            }

            IEnumerable<SearchResult> results = hits.ScoreDocs
                .Select(hit => ConvertHit(hit, searcher, reader, query));
            return new SearchResults(results)
            {
                TotalHits = Math.Min(hits.TotalHits, 1000),
                LastResult = hits.ScoreDocs.LastOrDefault(),
            };
        }

        /// <summary>
        /// Convert a Lucene result into <see cref="SearchResult"/> format.
        /// </summary>
        /// <param name="scoreDoc">Lucene document to convert.</param>
        /// <param name="searcher">Lucene search instance.</param>
        /// <param name="reader">Lucene index reader instance.</param>
        /// <param name="query">Query which produced this result.</param>
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

        /// <summary>
        /// Create the results snippets for a specified query and document.
        /// </summary>
        /// <param name="docId">ID of the document to summarize.</param>
        /// <param name="content">Content of the document to summarize.</param>
        /// <param name="reader"><see cref="IndexReader"/> reference.</param>
        /// <param name="query">Query which created the result.</param>
        /// <returns>HTML formatted snippets (one or more span elements).</returns>
        private static string CreateSnippet(int docId, string content, IndexReader reader, Query query)
        {
            SimpleHTMLFormatter formatter = new("<mark>", "</mark>");
            QueryScorer scorer = new(query);
            Highlighter highlighter = new(formatter, scorer);

            TokenStream stream = TokenSources.GetAnyTokenStream(reader, docId, "content", new StandardAnalyzer(LuceneVersion.LUCENE_48));
            IEnumerable<string> fragments = [.. highlighter.GetBestFragments(stream, content, 10)];

            StringBuilder stringBuilder = new();
            foreach (string fragment in fragments)
            {
                stringBuilder.AppendLine($"<span>{fragment}</span>");
            }

            return stringBuilder.ToString();
        }
    }
}