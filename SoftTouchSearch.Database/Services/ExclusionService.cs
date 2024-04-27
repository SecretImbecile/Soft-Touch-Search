using Microsoft.EntityFrameworkCore;
using SoftTouchSearch.Data.Models;

namespace SoftTouchSearch.Data.Services
{
    public class ExclusionService(StoryDbContext context) : IExclusionService
    {
        // Constructors

        private readonly StoryDbContext _context = context;

        // Interfaces

        /// <inheritdoc/>
        public IList<Episode> GetEpisodes()
        {
            return GetEpisodesAsync().Result;
        }

        /// <inheritdoc/>
        public async Task<IList<Episode>> GetEpisodesAsync()
        {
            ICollection<Episode> episodes = await _context.Episodes
                .Include(episode => episode.Chapter)
                .ToListAsync();
            ICollection<ExclusionRule> rules = await _context.ExclusionRules
                .ToListAsync();

            // TODO: remove excluded episodes

            return [.. episodes.OrderBy(episode => episode.EpisodeNumber)];
        }
    }
}