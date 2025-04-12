// <copyright file="ThumbnailImport.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
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
