// <copyright file="Index.cshtml.cs" company="Jack Kelly">
// Copyright � 2024, 2025 Jack Kelly.
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
    using Lucene.Net.Index;
    using Lucene.Net.Search;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using SoftTouchSearch.Data;
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Index.Classes;
    using SoftTouchSearch.Index.Services;

    /// <summary>
    /// Main search page.
    /// </summary>
    /// <param name="indexService">Lucene indexing service.</param>
    /// <param name="context">Database context.</param>
    public class IndexModel(IIndexService indexService, SearchDbContext context) : PageModel
    {
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
        /// <returns>A <see cref="Task"/> that represents the page action.</returns>
        public async Task OnGetAsync(string? q, bool loadMore = false)
        {
            // Display for initial page load.
            this.LatestEpisode = await context.Episodes
                .Where(episode => !episode.IsNonStory)
                .OrderByDescending(episode => episode.EpisodeNumber)
                .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(q))
            {
                return;
            }

            // Search results.
            this.SearchText = q;
            Query? query = BuildQuery(this.SearchText);
            if (query != null)
            {
                this.Results = indexService.Search(query, loadMore);
            }

            // Schedule a GC collection.
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, false);
        }

        private static Query? BuildQuery(string queryString)
        {
            List<string> tokens = queryString
                .ToLower()
                .Split(null)
                .Where(token => !string.IsNullOrWhiteSpace(token))
                .ToList();
            if (tokens.Count != 0)
            {
                if (tokens.Count == 1)
                {
                    return BuildQuerySingle(tokens[0]);
                }
                else
                {
                    BooleanQuery query = [];
                    foreach (string token in tokens)
                    {
                        query.Add(BuildQueryPart(token));
                    }

                    return query;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Build a <see cref="Query"/> from a single string token.
        /// </summary>
        /// <param name="token">Search term to query with.</param>
        /// <returns>A <see cref="TermQuery"/> containing the single token.</returns>
        private static TermQuery BuildQuerySingle(string token)
        {
            token = token.Trim();
            return new TermQuery(new Term("content", token));
        }

        /// <summary>
        /// Build part of a <see cref="BooleanQuery"/> containing multiple string tokens.
        /// </summary>
        /// <remarks>
        /// Tokens can be prefixed with '+' or '-' to affect their boolean occur rule.
        /// </remarks>
        /// <param name="token">The indiviudal token to be added to the query.</param>
        /// <returns>The <see cref="BooleanClause"/> to be added to a <see cref="BooleanQuery"/>.</returns>
        private static BooleanClause BuildQueryPart(string token)
        {
            Occur occur = Occur.SHOULD;
            if (token.StartsWith('+'))
            {
                token = token.TrimStart('+');
                occur = Occur.MUST;
            }
            else if (token.StartsWith('-'))
            {
                token = token.TrimStart('-');
                occur = Occur.MUST_NOT;
            }

            return new BooleanClause(new TermQuery(new Term("content", token)), occur);
        }
    }
}