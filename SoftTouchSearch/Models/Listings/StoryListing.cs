// <copyright file="StoryListing.cs" company="Jack Kelly">
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

namespace SoftTouchSearch.Models.Listings
{
    using System.Collections;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// DTO for listing the entire story, i.e. its chapters and all episodes therein.
    /// </summary>
    public sealed class StoryListing : IEnumerable<Chapter>, ICollection<Chapter>
    {
        // Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryListing"/> class.
        /// </summary>
        public StoryListing()
        {
        }

        // Properties

        /// <summary>
        /// Gets the story's Title.
        /// </summary>
        public string Title => "Soft Touch";

        /// <summary>
        /// Gets or sets list of episodes contained in this chapter.
        /// </summary>
        public List<Chapter> Chapters { get; set; } = [];

        /// <inheritdoc/>
        public int Count => this.Chapters.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        // Methods

        /// <inheritdoc/>
        public void Add(Chapter item)
        {
            this.Chapters.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.Chapters.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(Chapter item)
        {
            return this.Chapters.Contains(item);
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void CopyTo(Chapter[] array, int arrayIndex)
        {
            this.Chapters.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<Chapter> GetEnumerator()
        {
            return this.Chapters.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Chapters.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool Remove(Chapter item)
        {
            return this.Chapters.Remove(item);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Title;
        }
    }
}
