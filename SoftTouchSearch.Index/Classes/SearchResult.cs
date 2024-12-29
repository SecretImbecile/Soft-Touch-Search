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
        // Properties

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

        // Accessors

        /// <summary>
        /// Gets chapter name of the search result.
        /// </summary>
        public string Chapter => this.Document.Get("chapter");

        /// <summary>
        /// Gets episode title of the search result.
        /// </summary>
        public int ChapterNumber => int.Parse(this.Document.Get("chapternumber"));

        /// <summary>
        /// Gets episode title of the search result.
        /// </summary>
        public int EpisodeNumber => int.Parse(this.Document.Get("episodenumber"));

        /// <summary>
        /// Gets episode title of the search result.
        /// </summary>
        public string Title => this.Document.Get("title");

        /// <summary>
        /// Gets episode URL.
        /// </summary>
        public string Url => this.Document.Get("url");
    }
}