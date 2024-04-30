// <copyright file="SearchResult.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Index.Classes
{
    using Lucene.Net.Documents;
    using Lucene.Net.Search;

    /// <summary>
    /// A single index search result.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Gets or sets the Lucene document for this result.
        /// </summary>
        public required Document Document { get; set; }

        /// <summary>
        /// Gets or sets the episode database Id of this matched episode.
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Lucene score information for this result.
        /// </summary>
        public required ScoreDoc Score { get; set; }

        /// <summary>
        /// Gets or sets the highlighted text snippet.
        /// </summary>
        public required string Snippet { get; set; }
    }
}