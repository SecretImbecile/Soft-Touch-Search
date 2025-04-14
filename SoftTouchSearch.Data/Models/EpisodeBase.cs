// <copyright file="EpisodeBase.cs" company="Jack Kelly">
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
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Episode table model.
    /// </summary>
    public abstract class EpisodeBase
    {
        // Properties

        /// <summary>
        /// Gets or sets the identifier for this record.
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the <see cref="Models.Chapter"/> this record belongs to.
        /// </summary>
        public required Guid ChapterId { get; set; }

        /// <summary>
        /// Gets or sets the episode number in Tapas.
        /// </summary>
        public required int EpisodeNumber { get; set; }

        /// <summary>
        /// Gets or sets the episode's title.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the episode's publish date.
        /// </summary>
        public required DateTime PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the episode's external URL, i.e. the link to the episode on River's site.
        /// </summary>
        public string? UrlExternal { get; set; }

        // Navigations

        /// <summary>
        /// Gets or sets the <see cref="MetadataEpisode"/> for this episode.
        /// </summary>
        public abstract required MetadataEpisode Metadata { get; set; }

        // Methods

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Title;
        }
    }
}