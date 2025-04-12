// <copyright file="ImportDbContextFactory.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    /// <summary>
    /// Design-time factory for <see cref="ImportDbContext"/>.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/en-gb/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#from-a-design-time-factory"/>
    internal class ImportDbContextFactory : IDesignTimeDbContextFactory<ImportDbContext>
    {
        /// <inheritdoc/>
        public ImportDbContext CreateDbContext(string[] args)
        {
            if (args.Length <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(args), "Expected an argument for the database path");
            }

            DbContextOptions<ImportDbContext> options = new DbContextOptionsBuilder<ImportDbContext>()
                .UseSqlite($"Data Source={args[0]}")
                .Options;

            return new ImportDbContext(options);
        }
    }
}