// <copyright file="EpisodeListing.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Models.Listings
{
    using System.Diagnostics.CodeAnalysis;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// DTO for listing an individual episode.
    /// </summary>
    public sealed class EpisodeListing
    {
        // Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeListing"/> class.
        /// </summary>
        public EpisodeListing()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeListing"/> class.
        /// </summary>
        /// <param name="episode">Complete episode record.</param>
        [SetsRequiredMembers]
        public EpisodeListing(Episode episode)
        {
            this.EpisodeNumber = episode.EpisodeNumber;
            this.Title = episode.Title;
            this.PublishDate = episode.PublishDate;
            this.UrlTapas = $"https://tapas.io/episode/{episode.UrlId}";
            this.UrlBlog = episode.UrlExternal;
        }

        // Properties

        /// <summary>
        /// Gets or sets the episode number in Tapas.
        /// </summary>
        public required int EpisodeNumber { get; set; }

        /// <summary>
        /// Gets or sets the title of episode.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the date of episode.
        /// </summary>
        public required DateTime PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the URL to the episode on Tapas.
        /// </summary>
        public required string UrlTapas { get; set; }

        /// <summary>
        /// Gets or sets the URL to the River's site.
        /// </summary>
        public string? UrlBlog { get; set; }

        // Methods

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Title;
        }
    }
}
