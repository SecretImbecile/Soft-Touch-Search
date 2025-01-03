﻿// <copyright file="IIndexService.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
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