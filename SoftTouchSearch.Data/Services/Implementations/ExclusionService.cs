// <copyright file="ExclusionService.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Services.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// Service providing exclusion rules to episodes.
    /// </summary>
    internal class ExclusionService(StoryDbContext context, IMemoryCache memoryCache) : IExclusionService
    {
        private readonly StoryDbContext context = context;
        private readonly IMemoryCache memoryCache = memoryCache;

        // Methods

        /// <inheritdoc/>
        public IList<Episode> GetEpisodes()
        {
            return this.GetEpisodesAsync().Result;
        }

        /// <inheritdoc/>
        public async Task<IList<Episode>> GetEpisodesAsync()
        {
            ICollection<Episode> episodes = await this.context.Episodes
                .Include(episode => episode.Chapter)
                .ToListAsync();
            ICollection<ExclusionRule> rules = await this.context.ExclusionRules
                .ToListAsync();

            // Build a list of episodes which are not excluded
            List<Episode> filteredEpisodes = [];
            foreach (Episode episode in episodes)
            {
                bool includeEpisode = true;
                foreach (ExclusionRule rule in rules)
                {
                    if (includeEpisode == true && rule.CheckEpisodeExcluded(episode))
                    {
                        includeEpisode = false;
                    }
                }

                if (includeEpisode == true)
                {
                    filteredEpisodes.Add(episode);
                }
            }

            return [.. filteredEpisodes.OrderBy(episode => episode.EpisodeNumber)];
        }

        /// <inheritdoc/>
        public Episode? GetLatestEpisode()
        {
            return this.GetLatestEpisodeAsync().Result;
        }

        /// <inheritdoc/>
        public async Task<Episode?> GetLatestEpisodeAsync()
        {
            string cacheKey = $"{nameof(IExclusionService)}|{nameof(this.GetLatestEpisode)}";
            if (this.memoryCache.TryGetValue(cacheKey, out Episode? episode))
            {
                return episode;
            }
            else
            {
                episode = await this.context.Episodes
                    .OrderByDescending(episode => episode.PublishDate)
                    .FirstOrDefaultAsync();

                if (episode != null)
                {
                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                        .SetPriority(CacheItemPriority.High)
                        .SetSlidingExpiration(TimeSpan.FromHours(1))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    this.memoryCache.Set(cacheKey, episode);
                }

                return episode;
            }
        }
    }
}