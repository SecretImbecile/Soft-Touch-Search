// <copyright file="ImportDbContext.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
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