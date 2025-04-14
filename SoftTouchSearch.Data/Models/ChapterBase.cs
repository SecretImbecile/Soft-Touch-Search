// <copyright file="ChapterBase.cs" company="Jack Kelly">
// Copyright © 2024, 2025 Jack Kelly.
//
// Soft Touch Search is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the
// Free Software Foundation, either version 3 of the License, or
// (at your option)any later version.
//
// Soft Touch Search is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// Soft Touch Search. If not, see [https://www.gnu.org/licenses/].
// </copyright>

namespace SoftTouchSearch.Data.Models
{
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
        /// Gets the formatted chapter number.
        /// </summary>
        /// <remarks>
        /// e.g. 'Twenty-Seven'.
        /// </remarks>
        public string NumberFormatted => this.Number
            .ToWords()
            .Humanize(LetterCasing.Title)
            .Replace(' ', '-');

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

        /// <summary>
        /// Returns a string that represents this object in a shorter form.
        /// </summary>
        /// <remarks>
        /// e.g. 'Chapter 27: Hold Fast'.
        /// </remarks>
        /// <returns>A string that represents the current object.</returns>
        [Obsolete("Not in use")]
        public string ToShortString()
        {
            return $"Chapter {this.Number}: {this.Title}";
        }

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