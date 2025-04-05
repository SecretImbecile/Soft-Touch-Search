// <copyright file="ChapterImport.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    /// <summary>
    /// An episode record in the import database.
    /// </summary>
    public sealed class ChapterImport : ChapterBase<EpisodeImport>
    {
        /// <summary>
        /// Gets or sets the <see cref="EpisodeImport"/> records belonging to this chapter.
        /// </summary>
        /// <inheritdoc/>
        public override required ICollection<EpisodeImport> Episodes { get; set; }
    }
}