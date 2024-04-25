﻿using Microsoft.Extensions.DependencyInjection;
using SoftTouchSearch.Data.Services;

namespace SoftTouchSearch.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<StoryDbContext>();
            services.AddScoped<IExclusionService, ExclusionService>();
        }
    }
}