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
        /// <summary>
        /// Gets or sets the <see cref="Episode"/> records belonging to this chapter.
        /// </summary>
        /// <inheritdoc/>
        public override required ICollection<Episode> Episodes { get; set; }
    }
}