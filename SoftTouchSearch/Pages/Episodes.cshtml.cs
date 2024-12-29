// <copyright file="Episodes.cshtml.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using SoftTouchSearch.Data;
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Models.Listings;

    /// <summary>
    /// The 'Episode Listing' page model.
    /// </summary>
    public class EpisodesModel(IMemoryCache memoryCache, StoryDbContext context) : PageModel
    {
        private readonly IMemoryCache memoryCache = memoryCache;
        private readonly StoryDbContext context = context;

        // Properties

        /// <summary>
        /// Gets or sets listing of chapters/episodes in the story.
        /// </summary>
        public StoryListing Listing { get; set; } = null!;

        /// <summary>
        /// Gets the latest episode in the database.
        /// </summary>
        public EpisodeListing? LatestEpisode => this.Listing
            .LastOrDefault()?
            .Episodes
            .LastOrDefault();

        // Methods

        /// <summary>
        /// GET action for page.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous page result.</returns>
        public async Task OnGetAsync()
        {
            this.Listing = await this.GetEpisodeListingAsync();
        }

        /// <summary>
        /// Load the episode listing from the database.
        /// </summary>
        /// <remarks>
        /// Results are cached.
        /// </remarks>
        /// <returns>A <see cref="StoryListing"/> DTO containing the episode listings.</returns>
        public async Task<StoryListing> GetEpisodeListingAsync()
        {
            string cacheKey = $"{nameof(EpisodesModel)}|{nameof(EpisodesModel.GetEpisodeListingAsync)}";
            if (this.memoryCache.TryGetValue(cacheKey, out StoryListing? listing) && listing != null)
            {
                return listing;
            }
            else
            {
                IEnumerable<Chapter> chapters = await this.context.Chapters
                .Include(chapter => chapter.Episodes)
                .OrderBy(chapter => chapter.Number)
                .ToListAsync();

                IEnumerable<ExclusionRule> exclusionRules = await this.context.ExclusionRules
                    .ToListAsync();

                listing = [];
                foreach (Chapter chapter in chapters)
                {
                    ChapterListing chapterListing = new(chapter, chapter.Episodes, exclusionRules);
                    listing.Add(chapterListing);
                }

                if (listing.Count > 0)
                {
                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                        .SetPriority(CacheItemPriority.High)
                        .SetSlidingExpiration(TimeSpan.FromHours(1))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    this.memoryCache.Set(cacheKey, listing, cacheOptions);
                }

                return listing;
            }
        }
    }
}