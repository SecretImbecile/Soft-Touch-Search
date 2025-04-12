// <copyright file="ThumbnailBase.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
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
