// <copyright file="EpisodeDTO.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Ingest.Models
{
    using Newtonsoft.Json;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// DTO for Episode Ingest.
    /// </summary>
    [JsonObject]
    public class EpisodeDTO
    {
        /// <summary>
        /// Gets or sets chapter number.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public required int ChapterNumber { get; set; }

        /// <summary>
        /// Gets or sets episode number in Tapas.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public required int EpisodeNumber { get; set; }

        /// <summary>
        /// Gets or sets title of episode.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets date of episode.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public required DateTime PublishDate { get; set; }

        /// <summary>
        /// Gets or sets Tapas episode ID.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public required int UrlId { get; set; }

        /// <summary>
        /// Gets or sets alternate episode URL i.e. external blog URL.
        /// </summary>
        [JsonProperty(Required = Required.AllowNull)]
        public string? UrlExternal { get; set; }

        /// <summary>
        /// Gets or sets episode content in HTML.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public required string ContentHtml { get; set; }

        /// <summary>
        /// Gets or sets episode content in HTML.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public required string DescriptionHtml { get; set; }
    }
}