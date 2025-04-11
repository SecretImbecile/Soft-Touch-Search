// <copyright file="EpisodeImport.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    /// <summary>
    /// An episode record in the import database.
    /// </summary>
    /// <remarks>
    /// Contains the text of episode, which is stripped from the production episode.
    /// </remarks>
    public sealed class EpisodeImport : EpisodeBase
    {
        /// <summary>
        /// Gets or sets the episode's Tapas ID.
        /// </summary>
        public required int TapasId { get; set; }

        /// <summary>
        /// Gets or sets the episode content in HTML.
        /// </summary>
        public required string ContentHtml { get; set; }

        /// <summary>
        /// Gets or sets the episode description in HTML.
        /// </summary>
        public required string DescriptionHtml { get; set; }

        /// <summary>
        /// Gets the episode's Tapas URL.
        /// </summary>
        public string UrlTapas => $"https://tapas.io/episode/{this.TapasId}";

        // Navigations

        /// <summary>
        /// Gets or sets episode's <see cref="ThumbnailImport"/>.
        /// </summary>
        public ThumbnailImport Thumbnail { get; set; } = null!;

        /// <summary>
        /// Gets or sets the <see cref="ChapterImport"/> this episode belongs to.
        /// </summary>
        public required ChapterImport Chapter { get; set; }

        /// <inheritdoc/>
        public override required MetadataEpisode Metadata { get; set; }
    }
}