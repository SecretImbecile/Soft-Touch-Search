// <copyright file="SearchDbContextFactory.cs" company="Jack Kelly">
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