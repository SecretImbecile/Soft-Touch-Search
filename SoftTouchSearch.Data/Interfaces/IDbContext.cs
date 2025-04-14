// <copyright file="IDbContext.cs" company="Jack Kelly">
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

namespace SoftTouchSearch.Data.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// Interface for database contexts provided by SoftTouchSearch.Data.
    /// </summary>
    /// <typeparam name="TChapter">The type of chapter entity the context uses.</typeparam>
    /// <typeparam name="TEpisode">The type of episode entity the context uses.</typeparam>
    /// <typeparam name="TThumbnail">The type of thumbnail entity the context uses.</typeparam>
    internal interface IDbContext<TChapter, TEpisode, TThumbnail>
        where TChapter : ChapterBase<TEpisode>
        where TEpisode : EpisodeBase
        where TThumbnail : ThumbnailBase
    {
        /// <summary>
        /// Gets or sets the chapters table.
        /// </summary>
        public DbSet<TChapter> Chapters { get; set; }

        /// <summary>
        /// Gets or sets the episodes table.
        /// </summary>
        public DbSet<TEpisode> Episodes { get; set; }

        /// <summary>
        /// Gets or sets the thumbnails table.
        /// </summary>
        public DbSet<TThumbnail> Thumbnails { get; set; }
    }
}