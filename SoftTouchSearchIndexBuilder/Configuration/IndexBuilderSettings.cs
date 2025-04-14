// <copyright file="IndexBuilderSettings.cs" company="Jack Kelly">
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

namespace SoftTouchSearchIndexBuilder.Configuration
{
    /// <summary>
    /// Options pattern model for configuration of SoftTouchSearchIndexBuilder.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/core/extensions/options"/>
    public sealed class IndexBuilderSettings
    {
        /// <summary>
        /// Gets or sets the path to the SQLite file for the import database.
        /// </summary>
        public string ImportDbPath { get; set; } = null!;

        /// <summary>
        /// Gets or sets the path to use as the export folder.
        /// </summary>
        public string ExportFolderPath { get; set; } = null!;

        /// <summary>
        /// Gets or sets the file name (including extension) to use for the production database.
        /// </summary>
        public string ExportDbFileName { get; set; } = "softtouchsearch.db";

        /// <summary>
        /// Gets or sets the folder name to use for the search index files.
        /// </summary>
        public string ExportIndexFolderName { get; set; } = "softtouchsearch_index";
    }
}