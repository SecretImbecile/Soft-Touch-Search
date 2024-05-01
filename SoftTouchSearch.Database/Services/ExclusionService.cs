using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SoftTouchSearch.Data.Models;

namespace SoftTouchSearch.Data.Services
{
    public class ExclusionService(StoryDbContext context, IMemoryCache memoryCache) : IExclusionService
    {
        // Constructors

        private readonly StoryDbContext _context = context;
        private readonly IMemoryCache _memoryCache = memoryCache;

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

        /// <inheritdoc/>
        public Episode? GetLatestEpisode()
        {
            return GetLatestEpisodeAsync().Result;
        }

        /// <inheritdoc/>
        public async Task<Episode?> GetLatestEpisodeAsync()
        {
            string cacheKey = $"{nameof(IExclusionService)}|{nameof(GetLatestEpisode)}";
            if (_memoryCache.TryGetValue(cacheKey, out Episode? episode))
            {
                return episode;
            }
            else
            {
                episode = await _context.Episodes
                    .OrderByDescending(episode => episode.PublishDate)
                    .FirstOrDefaultAsync();

                if (episode != null)
                {
                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                        .SetPriority(CacheItemPriority.High)
                        .SetSlidingExpiration(TimeSpan.FromHours(1))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    _memoryCache.Set(cacheKey, episode);
                }

                return episode;
            }
        }
    }
}