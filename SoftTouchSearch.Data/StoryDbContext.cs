// <copyright file="StoryDbContext.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data
{
    using Microsoft.EntityFrameworkCore;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// The database context for SoftTouchSearch.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="StoryDbContext"/> class.
    /// </remarks>
    /// <param name="options">Configuration options for the database context.</param>
    public class StoryDbContext(DbContextOptions<StoryDbContext> options) : DbContext(options)
    {
        // Properties

        /// <summary>
        /// Gets or sets the <see cref="Chapter"/> table.
        /// </summary>
        public DbSet<Chapter> Chapters { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Episode"/> table.
        /// </summary>
        public DbSet<Episode> Episodes { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ExclusionRules"/> table.
        /// </summary>
        public DbSet<ExclusionRule> ExclusionRules { get; set; }

        // Methods

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chapter>()
                .HasMany(e => e.Episodes)
                .WithOne(e => e.Chapter)
                .HasForeignKey("ChapterId")
                .IsRequired(false);
        }
    }
}