// <copyright file="StoryListing.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
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
