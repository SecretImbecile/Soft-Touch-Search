using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace SoftTouchSearch.Data.Models
{
    /// <summary>
    /// Chapter marker table model
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// Identifier for this chapter.
        /// </summary>
        [Key]
        public required Guid Id { get; set; }

        /// <summary>
        /// Chapter Number
        /// </summary>
        public required int Number { get; set; }

        /// <summary>
        /// Chapter title
        /// </summary>
        public required string Title { get; set; }

        // Navigations

        /// <summary>
        /// List of <see cref="Episode"/> belonging to this chapter.
        /// </summary>
        public ICollection<Episode> Episodes { get; set; } = [];

        // Methods

        /// <inheritdoc/>
        /// <remarks>
        /// e.g. 'Chapter Twenty-Seven: Hold Fast
        /// </remarks>
        public override string ToString()
        {
            return $"Chapter {Number.ToWords().Humanize(LetterCasing.Title)}: {Title}";
        }
    }
}