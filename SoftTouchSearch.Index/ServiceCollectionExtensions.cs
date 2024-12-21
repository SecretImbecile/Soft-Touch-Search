// <copyright file="ServiceCollectionExtensions.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Index
{
    using Microsoft.Extensions.DependencyInjection;
    using SoftTouchSearch.Index.Services;

    /// <summary>
    /// Provides service registration for SoftTouchSearch.Index.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the required services for SoftTouchSearch.Index.
        /// </summary>
        /// <param name="services">Service collection to add to.</param>
        /// <param name="indexFilePath">File path to use for the index file.</param>
        public static void AddIndexServices(this IServiceCollection services, string indexFilePath)
        {
            services.AddSingleton<IIndexService>(provider => new IndexService(indexFilePath));
        }
    }
}