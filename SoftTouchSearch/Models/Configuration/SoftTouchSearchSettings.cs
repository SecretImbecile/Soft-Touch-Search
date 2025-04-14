// <copyright file="SoftTouchSearchSettings.cs" company="Jack Kelly">
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
