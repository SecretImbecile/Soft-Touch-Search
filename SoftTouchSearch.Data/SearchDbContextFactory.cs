// <copyright file="SearchDbContextFactory.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    /// <summary>
    /// Design-time factory for <see cref="SearchDbContext"/>.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/en-gb/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#from-a-design-time-factory"/>
    internal class SearchDbContextFactory : IDesignTimeDbContextFactory<SearchDbContext>
    {
        /// <inheritdoc/>
        public SearchDbContext CreateDbContext(string[] args)
        {
            if (args.Length <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(args), "Expected an argument for the database path");
            }

            DbContextOptions<SearchDbContext> options = new DbContextOptionsBuilder<SearchDbContext>()
                .UseSqlite($"Data Source={args[0]}")
                .Options;

            return new SearchDbContext(options);
        }
    }
}