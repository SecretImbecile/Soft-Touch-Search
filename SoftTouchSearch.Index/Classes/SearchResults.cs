// <copyright file="SearchResults.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Index.Classes
{
    using Lucene.Net.Search;

    /// <summary>
    /// Paged search results from the index service.
    /// </summary>
    public class SearchResults : List<SearchResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResults"/> class.
        /// </summary>
        public SearchResults()
        {
            this.TotalHits = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResults"/> class.
        /// </summary>
        /// <param name="results">Results to initialise with.</param>
        public SearchResults(IEnumerable<SearchResult> results)
            : base(results)
        {
        }

        /// <summary>
        /// Gets or sets the total number of results.
        /// </summary>
        public required int TotalHits { get; set; }

        /// <summary>
        /// Gets or sets the last result for this page.
        /// </summary>
        public ScoreDoc? LastResult { get; set; }
    }
}