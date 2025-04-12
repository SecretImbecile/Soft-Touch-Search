// <copyright file="MediaController.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using SoftTouchSearch.Data;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// Controller for media served from the database.
    /// </summary>
    /// <param name="context">Database context instance.</param>
    /// <param name="cache">Memory cache service instance.</param>
    [Route("media")]
    public class MediaController(SearchDbContext context, IMemoryCache cache) : Controller
    {
        /// <summary>
        /// Get the requested <see cref="Thumbnail"/> image.
        /// </summary>
        /// <param name="id">The identifier of the thumbnail to fetch.</param>
        /// <returns>A Task representing the action.</returns>
        [HttpGet]
        [ResponseCache(Duration = 2592000, Location = ResponseCacheLocation.Any)]
        [Route("thumbnail/{id}")]
        public async Task<IActionResult> GetThumbnailAsync(Guid id)
        {
            string cachekey = $"{nameof(MediaController)}|{nameof(this.GetThumbnailAsync)}|{id}";
            if (cache.TryGetValue(cachekey, out Thumbnail? thumbnail))
            {
                return this.File(thumbnail!.Content, thumbnail!.ContentType);
            }
            else
            {
                thumbnail = await context.Thumbnails
                    .Where(thumbnail => thumbnail.Id == id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

                if (thumbnail != null)
                {
                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                        .SetPriority(CacheItemPriority.Low)
                        .SetSlidingExpiration(TimeSpan.FromHours(1));

                    cache.Set(cachekey, thumbnail, cacheOptions);

                    return this.File(thumbnail!.Content, thumbnail!.ContentType);
                }
                else
                {
                    return this.NotFound();
                }
            }
        }
    }
}
