using System.ComponentModel.DataAnnotations;

namespace SoftTouchSearch.Data.Models
{
    /// <summary>
    /// Episode table model
    /// </summary>
    public class Episode
    {
        /// <summary>
        /// Identifier for this episode.
        /// </summary>
        [Key]
        public required Guid Id { get; set; }

        /// <summary>
        /// Episode number in Tapas
        /// </summary>
        public required int EpisodeNumber { get; set; }

        /// <summary>
        /// Title of episode
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Date of episode
        /// </summary>
        public required DateTime PublishDate { get; set; }

        /// <summary>
        /// Tapas episode ID
        /// </summary>
        public required int UrlId { get; set; }

        /// <summary>
        /// Alternate episode URL i.e. external blog URL
        /// </summary>
        public string? UrlExternal { get; set; }

        /// <summary>
        /// Episode content in HTML
        /// </summary>
        public required string ContentHtml { get; set; }

        /// <summary>
        /// Episode content in HTML
        /// </summary>
        public required string DescriptionHtml { get; set; }

        // Navigations

        /// <summary>
        /// <see cref="Models.Chapter"/> this episode belongs to.
        /// </summary>
        public Chapter? Chapter { get; set; }

        // Accessors

        /// <summary>
        /// Tapas episode URL
        /// </summary>
        public string? Url => $"https://tapas.io/episode/{UrlId}";
    }
}