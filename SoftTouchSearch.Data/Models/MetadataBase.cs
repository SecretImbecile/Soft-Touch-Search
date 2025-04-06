// <copyright file="MetadataBase.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    /// <summary>
    /// Metadata record for a chapter or episode.
    /// </summary>
    public abstract class MetadataBase
    {
        /// <summary>
        /// Gets or sets the number of views the chapter or episode has received.
        /// </summary>
        public required int Views { get; set; }

        /// <summary>
        /// Gets or sets the number of likes the chapter or episode has received.
        /// </summary>
        public required int Likes { get; set; }

        /// <summary>
        /// Gets or sets the number of comments the chapter or episode has received.
        /// </summary>
        public required int Comments { get; set; }
    }
}
