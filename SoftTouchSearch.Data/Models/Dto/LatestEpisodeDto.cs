// <copyright file="LatestEpisodeDto.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models.Dto
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// DTO containing information about the latest indexed episode.
    /// </summary>
    public sealed class LatestEpisodeDto
    {
        // Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LatestEpisodeDto"/> class.
        /// </summary>
        public LatestEpisodeDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatestEpisodeDto"/> class.
        /// </summary>
        /// <param name="episodeNumber">The episode number.</param>
        /// <param name="urlId">The Tapas episode ID.</param>
        /// <param name="urlExternal">The alternate episode URL, if any.</param>
        [SetsRequiredMembers]
        public LatestEpisodeDto(int episodeNumber, int urlId, string? urlExternal)
        {
            this.EpisodeNumber = episodeNumber;

            if (!string.IsNullOrWhiteSpace(urlExternal))
            {
                this.Url = urlExternal;
            }
            else
            {
                this.Url = $"https://tapas.io/episode/{urlId}";
            }
        }

        // Properties

        /// <summary>
        /// Gets or sets the episode number.
        /// </summary>
        public required int EpisodeNumber { get; set; }

        /// <summary>
        /// Gets or sets the URL to the episode.
        /// </summary>
        public required string Url { get; set; }
    }
}