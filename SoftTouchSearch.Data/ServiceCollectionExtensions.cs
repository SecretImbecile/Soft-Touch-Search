// <copyright file="ServiceCollectionExtensions.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
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