using Microsoft.EntityFrameworkCore;
using SoftTouchSearch.Data.Models;

namespace SoftTouchSearch.Data.Services
{
    internal class ExclusionService : IExclusionService
    {
        // Constructors

        private readonly StoryDbContext _context;

        public ExclusionService(StoryDbContext context)
        {
            _context = context;
        }

        // Interfaces

        /// <inheritdoc/>
        public async Task<IList<Episode>> GetEpisodesAsync()
        {
            ICollection<Episode> episodes = await _context.Episodes
                .ToListAsync();
            ICollection<ExclusionRule> rules = await _context.ExclusionRules
                .ToListAsync();

            // TODO: remove excluded episodes

            return episodes
                .OrderBy(episode => episode.EpisodeNumber)
                .ToList();
        }
    }
}