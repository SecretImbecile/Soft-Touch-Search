// <copyright file="IIndexService.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Index.Services
{
    using Lucene.Net.Documents;
    using Lucene.Net.Search;
    using SoftTouchSearch.Index.Classes;

    /// <summary>
    /// Provides the search index for the Soft Touch Search.
    /// </summary>
    public interface IIndexService
    {
        /// <summary>
        /// Gets a value indicating whether the index has completed building.
        /// </summary>
        bool IsIndexBuilt { get; }

        /// <summary>
        /// Add a document to the search index.
        /// </summary>
        /// <param name="document">Lucene.NET document to add.</param>
        void AddToIndex(Document document);

        /// <summary>
        /// Perform a search with the provided query.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="after">Last result, if fetching paged results.</param>
        /// <returns>A <see cref="SearchResults"/> containing the search results.</returns>
        SearchResults Search(Query query, ScoreDoc? after = null);

        /// <summary>
        /// Mark the search index as completed.
        /// </summary>
        void SetIndexBuilt();
    }
}
