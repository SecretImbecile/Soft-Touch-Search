using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftTouchSearch.Data.Services;

namespace SoftTouchSearch.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServices(this IServiceCollection services, string databaseFilePath)
        {
            services.AddDbContext<StoryDbContext>(
                options => options.UseSqlite($"Data Source={databaseFilePath}"));

            services.AddScoped<IExclusionService, ExclusionService>();
        }
    }
}