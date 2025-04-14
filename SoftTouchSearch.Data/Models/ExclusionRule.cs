// <copyright file="ExclusionRule.cs" company="Jack Kelly">
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
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// An exclusion rule to be calculated against imported <see cref="EpisodeImport"/> records.
    /// </summary>
    /// <remarks>
    /// Exclusion rules are caclulated and stored to <see cref="Episode.IsNonStory"/>. They are not kept in the production database.
    /// </remarks>
    public class ExclusionRule
    {
        /// <summary>
        /// Supported exclusion types.
        /// </summary>
        public enum ExclusionType
        {
            /// <summary>
            /// Episode is excluded if the title contains a specified value.
            /// </summary>
            TitleContains = 1,

            /// <summary>
            /// Episode is excluded if the content contains a specified value.
            /// </summary>
            ContentContains = 2,

            /// <summary>
            /// Episode is excluded if the description contains a specified value.
            /// </summary>
            DescriptionContains = 3,
        }

        /// <summary>
        /// Gets or sets the identifier for this exclusion rule.
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of rule to apply.
        /// </summary>
        public required ExclusionType Type { get; set; }

        /// <summary>
        /// Gets or sets the value to match against.
        /// </summary>
        public required string Value { get; set; }

        /// <summary>
        /// Check whether an episode is excluded due to this rule.
        /// </summary>
        /// <param name="episode">Episode to check.</param>
        /// <returns>true if the episode should be excluded by this rule.</returns>
        public bool CheckEpisodeExcluded(EpisodeImport episode)
        {
            if (this.Type == ExclusionType.TitleContains)
            {
                return episode.Title.Contains(this.Value, StringComparison.InvariantCultureIgnoreCase);
            }
            else if (this.Type == ExclusionType.ContentContains)
            {
                return episode.ContentHtml.Contains(this.Value, StringComparison.InvariantCultureIgnoreCase);
            }
            else if (this.Type == ExclusionType.DescriptionContains)
            {
                return episode.DescriptionHtml.Contains(this.Value, StringComparison.InvariantCultureIgnoreCase);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}