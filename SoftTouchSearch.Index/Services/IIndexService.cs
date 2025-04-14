// <copyright file="IIndexService.cs" company="Jack Kelly">
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

namespace SoftTouchSearch.Index.Services
{
    using Lucene.Net.Search;
    using SoftTouchSearch.Index.Classes;

    /// <summary>
    /// Provides the search index for the Soft Touch Search.
    /// </summary>
    public interface IIndexService
    {
        /// <summary>
        /// Perform a search with the provided query.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="loadMore">If true, load results beyond the standard page size.</param>
        /// <returns>A <see cref="SearchResults"/> containing the search results.</returns>
        SearchResults Search(Query query, bool loadMore = false);
    }
}