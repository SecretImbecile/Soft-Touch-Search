// <copyright file="ChapterDTO.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Ingest.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// DTO for Chapter Ingest.
    /// </summary>
    [JsonObject]
    public class ChapterDTO
    {
        /// <summary>
        /// Gets or sets chapter number.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public required int Number { get; set; }

        /// <summary>
        /// Gets or sets chapter title.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public required string Title { get; set; }
    }
}
