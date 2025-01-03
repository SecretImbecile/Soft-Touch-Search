﻿// <copyright file="ChapterListing.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Models.Listings
{
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using Humanizer;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// DTO for listing an individual chapter and its episodes.
    /// </summary>
    public sealed class ChapterListing : IEnumerable<EpisodeListing>
    {
        // Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChapterListing"/> class.
        /// </summary>
        public ChapterListing()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChapterListing"/> class.
        /// </summary>
        /// <param name="chapter">Complete chapter record.</param>
        /// <param name="episodes">Complete episode records for this chapter.</param>
        /// <param name="exclusionRules">Exclusion rules to apply to the episodes.</param>
        [SetsRequiredMembers]
        public ChapterListing(Chapter chapter, IEnumerable<Episode> episodes, IEnumerable<ExclusionRule> exclusionRules)
        {
            episodes = episodes
                .OrderBy(episode => episode.EpisodeNumber);

            this.Number = chapter.Number;
            this.Title = chapter.Title;

            this.Episodes = [];
            foreach (Episode episode in episodes)
            {
                bool excluded = DetermineExcluded(episode, exclusionRules);

                if (this.Episodes.Count > 0)
                {
                    this.Episodes.Add(new(episode, false, excluded));
                }
                else
                {
                    this.Episodes.Add(new(episode, true, excluded));
                }
            }
        }

        // Properties

        /// <summary>
        /// Gets or sets the chapter Number.
        /// </summary>
        public required int Number { get; set; }

        /// <summary>
        /// Gets or sets the chapter title.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets list of episodes contained in this chapter.
        /// </summary>
        public required List<EpisodeListing> Episodes { get; set; }

        // Methods

        /// <inheritdoc/>
        public IEnumerator<EpisodeListing> GetEnumerator()
        {
            return this.Episodes.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Episodes.GetEnumerator();
        }

        /// <inheritdoc/>
        /// <remarks>
        /// e.g. 'Chapter Twenty-Seven: Hold Fast'.
        /// </remarks>
        public override string ToString()
        {
            return $"Chapter {this.Number.ToWords().Humanize(LetterCasing.Title)}: {this.Title}";
        }

        /// <summary>
        /// Determine whether the episode is non-story content.
        /// </summary>
        /// <param name="episode">Full episode record.</param>
        /// <param name="exclusionRules">Exclusion rules to check against.</param>
        /// <returns>A bool indicating whether the episode is non-story content.</returns>
        private static bool DetermineExcluded(Episode episode, IEnumerable<ExclusionRule> exclusionRules)
        {
            foreach (ExclusionRule rule in exclusionRules)
            {
                if (rule.CheckEpisodeExcluded(episode))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
