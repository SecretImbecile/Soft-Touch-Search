// <copyright file="Episode.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    /// <summary>
    /// An episode record in the production database.
    /// </summary>
    /// <remarks>
    /// Contains the text of episode, which is stripped from the production episode.
    /// </remarks>
    public sealed class Episode : EpisodeBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether the episode is a non-story update.
        /// </summary>
        /// <remarks>
        /// For author updates, fan art, playlists, etc.
        /// </remarks>
        public required bool IsNonStory { get; set; }

        /// <summary>
        /// Gets or sets the episode's Tapas URL.
        /// </summary>
        public required string UrlTapas { get; set; }

        // Navigations

        /// <summary>
        /// Gets or sets the <see cref="Chapter"/> this episode belongs to.
        /// </summary>
        public required Chapter Chapter { get; set; }
    }
}