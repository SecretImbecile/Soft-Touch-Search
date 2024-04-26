// <copyright file="ServiceCollectionExtensions.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Index
{
    using Microsoft.Extensions.DependencyInjection;
    using SoftTouchSearch.Index.Services;

    /// <summary>
    /// Provides extension methods to register the indexing service.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register the indexing service.
        /// </summary>
        /// <param name="services">Service collection to add to.</param>
        /// <param name="indexFilePath">File path to use for the index file.</param>

        public static void AddIndexServices(this IServiceCollection services, string indexFilePath)
        {
            services.AddSingleton<IIndexService>(provider => new IndexService(indexFilePath));
        }
    }
}