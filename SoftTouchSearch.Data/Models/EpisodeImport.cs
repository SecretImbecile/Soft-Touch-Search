// <copyright file="EpisodeImport.cs" company="Jack Kelly">
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