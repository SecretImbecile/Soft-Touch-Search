// <copyright file="Chapter.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Humanizer;

    /// <summary>
    /// Chapter marker table model.
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// Gets or sets the identifier for this chapter.
        /// </summary>
        [Key]
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
        /// Gets or sets the list of <see cref="Episode"/> belonging to this chapter.
        /// </summary>
        public ICollection<Episode> Episodes { get; set; } = [];

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