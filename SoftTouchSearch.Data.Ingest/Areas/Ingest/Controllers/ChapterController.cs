// <copyright file="ChapterController.cs" company="Jack Kelly">
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
    /// Ingest controller for <see cref="Chapter"/> records.
    /// </summary>
    /// <param name="context">Database Context.</param>
    [Area("Ingest")]
    public class ChapterController(StoryDbContext context) : Controller
    {
        /// <summary>
        /// Database context dependancy.
        /// </summary>
        private readonly StoryDbContext context = context;

        /// <summary>
        /// Insert a new <see cref="Chapter"/> into the database.
        /// </summary>
        /// <param name="data">DTO containing the chapter data.</param>
        /// <returns>A <see cref="StatusCodeResult"/> indicating whether the chapter was added.</returns>
        [HttpPost]
        public async Task<IActionResult> IndexAsync([FromBody] ChapterDTO data)
        {
            Chapter? existingChapter = await this.context.Chapters
                .Where(chapter => chapter.Number == data.Number)
                .SingleOrDefaultAsync();
            if (existingChapter != null)
            {
                // Chapter already exists, no action necessary.
                return this.StatusCode(StatusCodes.Status202Accepted);
            }

            Chapter newChapter = new()
            {
                Id = Guid.NewGuid(),
                Number = data.Number,
                Title = data.Title,
            };

            this.context.Chapters.Add(newChapter);
            await this.context.SaveChangesAsync();
            return this.StatusCode(StatusCodes.Status201Created);
        }
    }
}