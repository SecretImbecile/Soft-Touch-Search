using Microsoft.EntityFrameworkCore;
using SoftTouchSearch.Data.Models;

namespace SoftTouchSearch.Data
{
    public class StoryDbContext : DbContext
    {
        // Properties

        public string DbPath { get; }

        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<ExclusionRule> ExclusionRules { get; set; }

        // Constructors

        public StoryDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "softtouchsearch.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");

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