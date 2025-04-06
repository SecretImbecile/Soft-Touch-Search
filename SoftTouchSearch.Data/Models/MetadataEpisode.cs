// <copyright file="MetadataEpisode.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    /// <summary>
    /// Metadata record for a <see cref="Episode"/>.
    /// </summary>
    public sealed class MetadataEpisode : MetadataBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether the episode is mature.
        /// </summary>
        public required bool Mature { get; set; }
    }
}