// <copyright file="ExclusionRule.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Rules applied to exclude episodes from the final episode list.
    /// </summary>
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
        [Key]
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
        public bool CheckEpisodeExcluded(Episode episode)
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