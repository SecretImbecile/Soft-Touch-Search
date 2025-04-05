// <copyright file="IndexBuilderSettings.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
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