// <copyright file="ServiceCollectionExtensions.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SoftTouchSearch.Data.Services;
    using SoftTouchSearch.Data.Services.Implementations;

    /// <summary>
    /// Provides service registration for SoftTouchSearch.Data.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the required services for SoftTouchSearch.Data.
        /// </summary>
        /// <param name="services">Service collection to append to.</param>
        /// <param name="databaseFilePath">Path of the SQLite database file.</param>
        public static void AddDataServices(this IServiceCollection services, string databaseFilePath)
        {
            services.AddDbContext<StoryDbContext>(
                options => options.UseSqlite($"Data Source={databaseFilePath}"));

            services.AddScoped<IExclusionService, ExclusionService>();
        }
    }
}