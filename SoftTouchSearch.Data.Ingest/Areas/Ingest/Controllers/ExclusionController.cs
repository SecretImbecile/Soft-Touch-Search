// <copyright file="ExclusionController.cs" company="Jack Kelly">
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
    /// Ingest controller for <see cref="ExclusionRule"/> records.
    /// </summary>
    /// <param name="context">Database Context.</param>
    [Area("Ingest")]
    public class ExclusionController(StoryDbContext context) : Controller
    {
        /// <summary>
        /// Database context dependancy.
        /// </summary>
        private readonly StoryDbContext context = context;

        /// <summary>
        /// Insert a new <see cref="ExclusionRule"/> into the database.
        /// </summary>
        /// <param name="data">DTO containing the exclusion data.</param>
        /// <returns>A <see cref="StatusCodeResult"/> indicating whether the chapter was added.</returns>
        [HttpPost]
        public async Task<IActionResult> IndexAsync([FromBody] ExclusionDTO data)
        {
            // Convert the different enum definitions
            int typeInt = (int)data.Type;
            ExclusionRule.ExclusionType type = (ExclusionRule.ExclusionType)typeInt;

            ExclusionRule? existingRule = await this.context.ExclusionRules
                .Where(rule => rule.Type == type)
                .SingleOrDefaultAsync();
            if (existingRule != null)
            {
                // Rule already exists, no action necessary.
                return this.StatusCode(StatusCodes.Status202Accepted);
            }

            ExclusionRule newRule = new()
            {
                Id = Guid.NewGuid(),
                Type = type,
                Value = data.Value,
            };

            this.context.ExclusionRules.Add(newRule);
            await this.context.SaveChangesAsync();
            return this.StatusCode(StatusCodes.Status201Created);
        }
    }
}