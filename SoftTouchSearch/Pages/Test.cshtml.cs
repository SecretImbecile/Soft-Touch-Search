// <copyright file="Test.cshtml.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Pages
{
    using Lucene.Net.Index;
    using Lucene.Net.Search;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SoftTouchSearch.Data;
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Index.Classes;
    using SoftTouchSearch.Index.Services;

    /// <summary>
    /// Test page.
    /// </summary>
    /// <param name="indexService">index service.</param>
    /// <param name="context">database context.</param>
    public class TestModel(IIndexService indexService, StoryDbContext context) : PageModel
    {
        private readonly IIndexService indexService = indexService;

        private readonly StoryDbContext context = context;

        /// <summary>
        /// Gets or sets search text.
        /// </summary>
        [BindProperty]
        public string? QueryText { get; set; } = "Ghost Office";

        /// <summary>
        /// Gets or sets search results.
        /// </summary>
        public SearchResults Results { get; set; } = new SearchResults() { TotalHits = 0 };

        public IActionResult OnGet()
        {
            if (!this.indexService.IsIndexBuilt)
            {
                return this.StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            BooleanQuery query = new()
            {
                { new TermQuery(new Term("content", "ghost")), Occur.SHOULD },
                { new TermQuery(new Term("content", "office")), Occur.SHOULD },
            };

            this.Results = this.indexService.Search(query);

            return this.Page();
        }
    }
}
