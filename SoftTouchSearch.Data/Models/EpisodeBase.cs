// <copyright file="EpisodeBase.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Episode table model.
    /// </summary>
    public abstract class EpisodeBase
    {
        // Properties

        /// <summary>
        /// Gets or sets the identifier for this record.
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the <see cref="Models.Chapter"/> this record belongs to.
        /// </summary>
        public required Guid ChapterId { get; set; }

        /// <summary>
        /// Gets or sets the episode number in Tapas.
        /// </summary>
        public required int EpisodeNumber { get; set; }

        /// <summary>
        /// Gets or sets the episode's title.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the episode's publish date.
        /// </summary>
        public required DateTime PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the episode's external URL, i.e. the link to the episode on River's site.
        /// </summary>
        public string? UrlExternal { get; set; }

        // Methods

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Title;
        }
    }
}