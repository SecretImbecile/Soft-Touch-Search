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
    /// <param name="dbPath">Full file path to the SQLite database file.</param>
    public class ImportDbContext(string dbPath) : DbContext, IDbContext<ChapterImport, EpisodeImport>
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

        // Methods

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(dbPath);
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChapterImport>()
                .HasKey(chapter => chapter.Id);

            modelBuilder.Entity<EpisodeImport>()
                .HasKey(episode => episode.Id);

            modelBuilder.Entity<ExclusionRule>()
                .HasKey(exclusionRule => exclusionRule.Id);

            modelBuilder.Entity<ChapterImport>()
                .HasMany(chapter => chapter.Episodes)
                .WithOne(episode => episode.Chapter)
                .HasForeignKey(episode => episode.ChapterId)
                .IsRequired(true);
        }
    }
}