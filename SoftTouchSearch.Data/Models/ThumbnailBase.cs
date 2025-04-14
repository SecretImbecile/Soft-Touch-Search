// <copyright file="ThumbnailBase.cs" company="Jack Kelly">
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
    /// Thumbnail table model.
    /// </summary>
    public class ThumbnailBase
    {
        /// <summary>
        /// Gets or sets the primary identifier for this thumbnail.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the binary content of the thumbnail image.
        /// </summary>
        public required byte[] Content { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the thumbnail image.
        /// </summary>
        public required string ContentType { get; set; }
    }
}
