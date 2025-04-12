// <copyright file="IDbContext.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
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