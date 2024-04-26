// <copyright file="IIndexService.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Index.Services
{
    /// <summary>
    /// Provides the search index for the Soft Touch Search.
    /// </summary>
    public interface IIndexService
    {
        /// <summary>
        /// Test: Return the file path of the index file.
        /// </summary>
        /// <returns>index file path.</returns>
        string GetIndexFilePath();
    }
}
