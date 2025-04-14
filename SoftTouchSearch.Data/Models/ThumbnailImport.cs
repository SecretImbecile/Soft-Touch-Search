﻿// <copyright file="ThumbnailImport.cs" company="Jack Kelly">
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

namespace SoftTouchSearch.Data.Models
{
    using System;

    /// <summary>
    /// An thumbnail record in the import database.
    /// </summary>
    public class ThumbnailImport : ThumbnailBase
    {
        /// <summary>
        /// Gets or sets the identifier of the <see cref="EpisodeImport"/> this thumbnail belongs to.
        /// </summary>
        public required Guid EpisodeId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EpisodeImport"/> this thumbnail belongs to.
        /// </summary>
        public EpisodeImport Episode { get; set; } = null!;
    }
}
