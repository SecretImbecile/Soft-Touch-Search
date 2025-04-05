// <copyright file="ChapterBase.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Humanizer;

    /// <summary>
    /// Chapter marker table model.
    /// </summary>
    /// <typeparam name="TEpisode">The type of episode entity this record references.</typeparam>
    public abstract class ChapterBase<TEpisode>
        where TEpisode : EpisodeBase
    {
        // Properties

        /// <summary>
        /// Gets or sets the identifier for this chapter.
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the chapter Number.
        /// </summary>
        public required int Number { get; set; }

        /// <summary>
        /// Gets or sets the chapter title.
        /// </summary>
        public required string Title { get; set; }

        // Navigations

        /// <summary>
        /// Gets or sets the episode records belonging to this chapter.
        /// </summary>
        public abstract required ICollection<TEpisode> Episodes { get; set; }

        // Methods

        /// <inheritdoc/>
        /// <remarks>
        /// e.g. 'Chapter Twenty-Seven: Hold Fast'.
        /// </remarks>
        public override string ToString()
        {
            return $"Chapter {this.Number.ToWords().Humanize(LetterCasing.Title)}: {this.Title}";
        }
    }
}