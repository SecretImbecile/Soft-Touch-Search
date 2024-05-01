// <copyright file="Index.cshtml.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Pages
{
    using Lucene.Net.Index;
    using Lucene.Net.Search;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Data.Services;
    using SoftTouchSearch.Index.Classes;
    using SoftTouchSearch.Index.Services;

    /// <summary>
    /// Main search page.
    /// </summary>
    public class IndexModel(IIndexService indexService, IExclusionService exclusionService) : PageModel
    {
        private readonly IIndexService indexService = indexService;
        private readonly IExclusionService exclusionService = exclusionService;

        /// <summary>
        /// Gets or sets a value indicating whether whether the index has finished building.
        /// </summary>
        public bool IndexBuilt { get; set; }

        /// <summary>
        /// Gets or sets the latest episode to be indexed.
        /// </summary>
        public Episode? LatestEpisode { get; set; }

        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        public SearchResults Results { get; set; } = new SearchResults() { TotalHits = 0 };

        /// <summary>
        /// Gets or sets the entered search text.
        /// </summary>
        public string? SearchText { get; set; }

        /// <summary>
        /// Page handler.
        /// </summary>
        /// <param name="q">Text input to search.</param>
        /// <param name="loadMore">If true, show more than one page of results.</param>
        public void OnGet(string? q, bool loadMore = false)
        {
            // Display 503 message until the index has been built.
            this.IndexBuilt = this.indexService.IsIndexBuilt;
            if (!this.IndexBuilt)
            {
                this.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                return;
            }

            // Display for initial page load.
            this.LatestEpisode = this.exclusionService.GetLatestEpisode();
            if (string.IsNullOrWhiteSpace(q))
            {
                return;
            }

            // Search results.
            this.SearchText = q;

            this.SearchText = "Ghost Office"; // Temp
            BooleanQuery query = new()
            {
                { new TermQuery(new Term("content", "ghost")), Occur.SHOULD },
                { new TermQuery(new Term("content", "office")), Occur.SHOULD },
            };

            this.Results = this.indexService.Search(query, loadMore);
        }
    }
}
