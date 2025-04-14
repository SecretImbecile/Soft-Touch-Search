// <copyright file="ServiceCollectionExtensions.cs" company="Jack Kelly">
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

namespace SoftTouchSearch.Index
{
    using Microsoft.Extensions.DependencyInjection;
    using SoftTouchSearch.Index.Services;
    using SoftTouchSearch.Index.Services.Implementations;

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