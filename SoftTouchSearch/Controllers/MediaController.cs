// <copyright file="MediaController.cs" company="Jack Kelly">
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
