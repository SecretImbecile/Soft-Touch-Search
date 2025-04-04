// <copyright file="SoftTouchSearchSettings.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Models.Configuration
{
    /// <summary>
    /// Options pattern configuration for Soft Touch Search.
    /// </summary>
    public sealed class SoftTouchSearchSettings
    {
        /// <summary>
        /// Gets or sets the full file path of the SQLite database.
        /// </summary>
        public string PathToDatabase { get; set; } = null!;

        /// <summary>
        /// Gets or sets the path of the directory containing the Lucene.NET search index.
        /// </summary>
        public string PathToIndex { get; set; } = null!;
    }
}
