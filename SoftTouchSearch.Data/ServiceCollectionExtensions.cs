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

namespace SoftTouchSearch.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Provides service registration for SoftTouchSearch.Data.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the <see cref="ImportDbContext"/> used by the index builder.
        /// </summary>
        /// <param name="services">Service collection to append to.</param>
        /// <param name="databaseFilePath">Full file path to the import database.</param>
        public static void AddImportDatabase(this IServiceCollection services, string databaseFilePath)
        {
            services.AddDbContext<ImportDbContext>(
                options => options.UseSqlite($"Data Source={databaseFilePath}"));
        }

        /// <summary>
        /// Registers the <see cref="SearchDbContext"/> used by the web application.
        /// </summary>
        /// <param name="services">Service collection to append to.</param>
        /// <param name="databaseFilePath">Full file path to the export (production) database.</param>
        public static void AddSearchDatabase(this IServiceCollection services, string databaseFilePath)
        {
            services.AddDbContext<SearchDbContext>(
                options => options.UseSqlite($"Data Source={databaseFilePath}"));
        }
    }
}