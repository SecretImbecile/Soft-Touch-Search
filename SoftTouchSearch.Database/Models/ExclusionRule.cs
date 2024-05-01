using System.ComponentModel.DataAnnotations;

namespace SoftTouchSearch.Data.Models
{
    /// <summary>
    /// Rules applied to exclude episodes from the final episode list
    /// </summary>
    public class ExclusionRule
    {
        /// <summary>
        /// Identifier for this exclusion rule.
        /// </summary>
        [Key]
        public required Guid Id { get; set; }

        /// <summary>
        /// Type of rule to apply
        /// </summary>
        public required ExclusionType Type { get; set; }

        /// <summary>
        /// Value to match against
        /// </summary>
        public required string Value { get; set; }

        /// <summary>
        /// Supported exclusion types.
        /// </summary>
        public enum ExclusionType
        {
            TitleContains = 1,
            ContentContains = 2,
            DescriptionContains = 3,
        }

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