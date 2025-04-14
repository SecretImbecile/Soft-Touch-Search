// <copyright file="Chapter.cs" company="Jack Kelly">
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
    public sealed class Chapter : ChapterBase<Episode>
    {
        // Properties

        /// <summary>
        /// Gets or sets identifier of the chapters's <see cref="Models.Thumbnail"/>.
        /// </summary>
        public required Guid ThumbnailGuid { get; set; }

        // Navigations

        /// <summary>
        /// Gets or sets the <see cref="Episode"/> records belonging to this chapter.
        /// </summary>
        /// <inheritdoc/>
        public override required ICollection<Episode> Episodes { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MetadataEpisode"/> for this episode.
        /// </summary>
        public required MetadataChapter Metadata { get; set; }

        /// <summary>
        /// Gets or sets chapters's <see cref="Models.Thumbnail"/>.
        /// </summary>
        public Thumbnail Thumbnail { get; set; } = null!;
    }
}