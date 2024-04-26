// <copyright file="ChapterController.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Ingest.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// Ingest controller for <see cref="Chapter"/> records.
    /// </summary>
    [Area("Ingest")]
    public class ChapterController : Controller
    {
        public IActionResult Index()
        {
            return StatusCode(StatusCodes.Status302Found);
        }
    }
}
