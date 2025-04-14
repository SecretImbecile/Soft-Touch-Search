// <copyright file="SearchResults.cs" company="Jack Kelly">
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