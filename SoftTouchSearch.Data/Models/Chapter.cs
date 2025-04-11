// <copyright file="Chapter.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    /// <summary>
    /// An episode record in the production database.
    /// </summary>
    public sealed class Chapter : ChapterBase<Episode>
    {
        // Properties

        /// <summary>
        /// Gets or sets identifier of the chapters's <see cref="Models.Thumbnail"/>.
        /// </summary>
        public required Guid ThumbnailGuid { get; set; }

        // Navigations

        /// <summary>
        /// Gets or sets the <see cref="Episode"/> records belonging to this chapter.
        /// </summary>
        /// <inheritdoc/>
        public override required ICollection<Episode> Episodes { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MetadataEpisode"/> for this episode.
        /// </summary>
        public required MetadataChapter Metadata { get; set; }

        /// <summary>
        /// Gets or sets chapters's <see cref="Models.Thumbnail"/>.
        /// </summary>
        public Thumbnail Thumbnail { get; set; } = null!;
    }
}