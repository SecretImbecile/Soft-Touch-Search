// <copyright file="Episode.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Episode table model.
    /// </summary>
    public class Episode
    {
        /// <summary>
        /// Gets or sets the identifier for this episode.
        /// </summary>
        [Key]
        public required Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the <see cref="Models.Chapter"/> this episode belongs to.
        /// </summary>
        public required Guid ChapterId { get; set; }

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
        /// Gets or sets the Tapas episode ID.
        /// </summary>
        public required int UrlId { get; set; }

        /// <summary>
        /// Gets or sets the alternate episode URL, i.e. link to the episode on River's site.
        /// </summary>
        public string? UrlExternal { get; set; }

        /// <summary>
        /// Gets or sets the episode content in HTML.
        /// </summary>
        public required string ContentHtml { get; set; }

        /// <summary>
        /// Gets or sets the episode description in HTML.
        /// </summary>
        public required string DescriptionHtml { get; set; }

        // Navigations

        /// <summary>
        /// Gets or sets the <see cref="Models.Chapter"/> this episode belongs to.
        /// </summary>
        public Chapter? Chapter { get; set; }

        // Accessors

        /// <summary>
        /// Gets the Tapas or External episode URL.
        /// </summary>
        public string Url
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.UrlExternal))
                {
                    return this.UrlExternal;
                }
                else
                {
                    return $"https://tapas.io/episode/{this.UrlId}";
                }
            }
        }

        // Methods

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Title;
        }
    }
}