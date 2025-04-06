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
        /// Gets or sets the <see cref="Episode"/> records belonging to this chapter.
        /// </summary>
        /// <inheritdoc/>
        public override required ICollection<Episode> Episodes { get; set; }

        // Navigations

        /// <summary>
        /// Gets or sets the <see cref="MetadataEpisode"/> for this episode.
        /// </summary>
        public required MetadataChapter Metadata { get; set; }
    }
}