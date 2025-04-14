// <copyright file="Episode.cs" company="Jack Kelly">
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
    /// An episode record in the production database.
    /// </summary>
    /// <remarks>
    /// Contains the text of episode, which is stripped from the production episode.
    /// </remarks>
    public sealed class Episode : EpisodeBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether the episode is a non-story update.
        /// </summary>
        /// <remarks>
        /// For author updates, fan art, playlists, etc.
        /// </remarks>
        public required bool IsNonStory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this episode is the first in its chapter.
        /// </summary>
        public required bool IsFirstEpisodeInChapter { get; set; }

        /// <summary>
        /// Gets a value indicating whether the episode is mature.
        /// </summary>
        public bool IsMature => this.Metadata.Mature;

        /// <summary>
        /// Gets or sets identifier of the episode's <see cref="Models.Thumbnail"/>.
        /// </summary>
        public required Guid ThumbnailGuid { get; set; }

        /// <summary>
        /// Gets or sets the episode's Tapas URL.
        /// </summary>
        public required string UrlTapas { get; set; }

        // Navigations

        /// <summary>
        /// Gets or sets episode's <see cref="Models.Thumbnail"/>.
        /// </summary>
        public Thumbnail Thumbnail { get; set; } = null!;

        /// <summary>
        /// Gets or sets the <see cref="Chapter"/> this episode belongs to.
        /// </summary>
        public required Chapter Chapter { get; set; }

        /// <inheritdoc/>
        public override required MetadataEpisode Metadata { get; set; }
    }
}