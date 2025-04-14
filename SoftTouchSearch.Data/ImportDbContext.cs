// <copyright file="ImportDbContext.cs" company="Jack Kelly">
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

namespace SoftTouchSearch.Data
{
    using Microsoft.EntityFrameworkCore;
    using SoftTouchSearch.Data.Interfaces;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// The production database context for SoftTouchSearch.
    /// </summary>
    /// <param name="options">Configuration options for the database context.</param>
    public class ImportDbContext(DbContextOptions<ImportDbContext> options) : DbContext(options), IDbContext<ChapterImport, EpisodeImport, ThumbnailImport>
    {
        // Properties

        /// <inheritdoc/>
        public DbSet<ChapterImport> Chapters { get; set; }

        /// <inheritdoc/>
        public DbSet<EpisodeImport> Episodes { get; set; }

        /// <summary>
        /// Gets or sets the exclusion rules table.
        /// </summary>
        public DbSet<ExclusionRule> ExclusionRules { get; set; }

        /// <inheritdoc/>
        public DbSet<ThumbnailImport> Thumbnails { get; set; }

        // Methods

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define keys
            modelBuilder.Entity<ChapterImport>()
                .HasKey(chapter => chapter.Id);

            modelBuilder.Entity<EpisodeImport>()
                .HasKey(episode => episode.Id);

            modelBuilder.Entity<ExclusionRule>()
                .HasKey(exclusionRule => exclusionRule.Id);

            modelBuilder.Entity<ThumbnailImport>()
                .HasKey(thumbnail => thumbnail.Id);

            // Define owned properties
            modelBuilder.Entity<EpisodeImport>()
                .OwnsOne(episode => episode.Metadata);

            // Define relationships
            modelBuilder.Entity<ChapterImport>()
                .HasMany(chapter => chapter.Episodes)
                .WithOne(episode => episode.Chapter)
                .HasForeignKey(episode => episode.ChapterId)
                .IsRequired(true);

            modelBuilder.Entity<EpisodeImport>()
                .HasOne(episode => episode.Thumbnail)
                .WithOne(thumbnail => thumbnail.Episode)
                .HasForeignKey<ThumbnailImport>(thumbnail => thumbnail.EpisodeId)
                .IsRequired();
        }
    }
}