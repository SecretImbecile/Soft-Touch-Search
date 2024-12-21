using Microsoft.EntityFrameworkCore;
using SoftTouchSearch.Data.Models;

namespace SoftTouchSearch.Data
{
    public class StoryDbContext : DbContext
    {
        // Properties

        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<ExclusionRule> ExclusionRules { get; set; }

        // Constructors

        public StoryDbContext(DbContextOptions<StoryDbContext> options) : base (options)
        {
        }

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