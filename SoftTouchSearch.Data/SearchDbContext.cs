// <copyright file="SearchDbContext.cs" company="Jack Kelly">
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
    public class SearchDbContext(DbContextOptions<SearchDbContext> options) : DbContext(options), IDbContext<Chapter, Episode, Thumbnail>
    {
        // Properties

        /// <inheritdoc/>
        public DbSet<Chapter> Chapters { get; set; }

        /// <inheritdoc/>
        public DbSet<Episode> Episodes { get; set; }

        /// <inheritdoc/>
        public DbSet<Thumbnail> Thumbnails { get; set; }

        // Methods

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define keys
            modelBuilder.Entity<Chapter>()
                .HasKey(chapter => chapter.Id);

            modelBuilder.Entity<Episode>()
                .HasKey(episode => episode.Id);

            modelBuilder.Entity<ExclusionRule>()
                .HasKey(exclusionRule => exclusionRule.Id);

            modelBuilder.Entity<Thumbnail>()
                .HasKey(thumbnail => thumbnail.Id);

            // Define owned properties
            modelBuilder.Entity<Chapter>()
                .OwnsOne(chapter => chapter.Metadata);

            modelBuilder.Entity<Episode>()
                .OwnsOne(episode => episode.Metadata);

            // Define relationships
            modelBuilder.Entity<Chapter>()
                .HasMany(chapter => chapter.Episodes)
                .WithOne(episode => episode.Chapter)
                .HasForeignKey(episode => episode.ChapterId)
                .IsRequired(true);

            modelBuilder.Entity<Episode>()
                .HasOne(episode => episode.Thumbnail)
                .WithMany()
                .HasForeignKey(episode => episode.ThumbnailGuid)
                .IsRequired();
        }
    }
}