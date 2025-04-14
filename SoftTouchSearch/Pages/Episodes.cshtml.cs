// <copyright file="Episodes.cshtml.cs" company="Jack Kelly">
// Copyright © 2024, 2025 Jack Kelly.
//
// Soft Touch Search is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the
// Free Software Foundation, either version 3 of the License, or
// (at your option)any later version.
//
// Soft Touch Search is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// Soft Touch Search. If not, see [https://www.gnu.org/licenses/].
// </copyright>

namespace SoftTouchSearch.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using SoftTouchSearch.Data;
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Models.Listings;

    /// <summary>
    /// The 'Episode Listing' page model.
    /// </summary>
    [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
    public class EpisodesModel(IMemoryCache memoryCache, SearchDbContext context) : PageModel
    {
        private readonly IMemoryCache memoryCache = memoryCache;
        private readonly SearchDbContext context = context;

        // Properties

        /// <summary>
        /// Gets or sets listing of chapters/episodes in the story.
        /// </summary>
        public StoryListing Listing { get; set; } = null!;

        /// <summary>
        /// Gets or sets the latest episode in the database.
        /// </summary>
        public Episode? LatestEpisode { get; set; }

        // Methods

        /// <summary>
        /// GET action for page.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous page result.</returns>
        public async Task OnGetAsync()
        {
            this.Listing = await this.GetEpisodeListingAsync();

            this.LatestEpisode = await this.context.Episodes
                .Where(episode => !episode.IsNonStory)
                .OrderByDescending(episode => episode.EpisodeNumber)
                .FirstOrDefaultAsync();
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

                listing = [];
                foreach (Chapter chapter in chapters)
                {
                    listing.Add(chapter);
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