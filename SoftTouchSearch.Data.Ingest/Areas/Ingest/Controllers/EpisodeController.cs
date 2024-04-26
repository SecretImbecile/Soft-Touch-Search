// <copyright file="EpisodeController.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Ingest.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SoftTouchSearch.Data;
    using SoftTouchSearch.Data.Ingest.Models;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// Ingest controller for <see cref="Episode"/> records.
    /// </summary>
    /// <param name="context">Database Context.</param>
    [Area("Ingest")]
    public class EpisodeController(StoryDbContext context) : Controller
    {
        /// <summary>
        /// Database context dependancy.
        /// </summary>
        private readonly StoryDbContext context = context;

        /// <summary>
        /// Insert a new <see cref="Episode"/> into the database.
        /// </summary>
        /// <param name="data">DTO containing the episode data.</param>
        /// <returns>A <see cref="StatusCodeResult"/> indicating whether the episode was added.</returns>
        [HttpPost]
        public async Task<IActionResult> IndexAsync([FromBody] EpisodeDTO data)
        {
            Episode? existingEpisode = await this.context.Episodes
                .Where(episode => episode.EpisodeNumber == data.EpisodeNumber)
                .SingleOrDefaultAsync();
            if (existingEpisode != null)
            {
                // Episode already exists, no action necessary.
                return this.StatusCode(StatusCodes.Status202Accepted);
            }

            Chapter? chapter = await this.context.Chapters
                .Where(chapter => chapter.Number == data.ChapterNumber)
                .SingleOrDefaultAsync();
            if (chapter == null)
            {
                // Chapter was not found;
                return this.StatusCode(StatusCodes.Status404NotFound);
            }

            Episode newEpisode = new()
            {
                Id = Guid.NewGuid(),
                EpisodeNumber = data.EpisodeNumber,
                Title = data.Title,
                PublishDate = data.PublishDate,
                UrlId = data.UrlId,
                UrlExternal = data.UrlExternal,
                ContentHtml = data.ContentHtml,
                DescriptionHtml = data.DescriptionHtml,
                Chapter = chapter,
            };

            this.context.Episodes.Add(newEpisode);
            await this.context.SaveChangesAsync();
            return this.StatusCode(StatusCodes.Status201Created);
        }
    }
}