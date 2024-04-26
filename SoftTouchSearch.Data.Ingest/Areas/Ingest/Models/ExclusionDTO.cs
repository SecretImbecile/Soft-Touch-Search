// <copyright file="ExclusionDTO.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Ingest.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// DTO for Exclusion rule ingest.
    /// </summary>
    [JsonObject]
    public class ExclusionDTO
    {
        /// <summary>
        /// Supported exclusion types.
        /// </summary>
        public enum ExclusionType
        {
            /// <summary>
            /// Filter by title text contents.
            /// </summary>
            TitleContains = 1,

            /// <summary>
            /// Filter by episode text contents.
            /// </summary>
            ContentContains = 2,

            /// <summary>
            /// Filter by description text contents.
            /// </summary>
            DescriptionContains = 3,
        }

        /// <summary>
        /// Gets or sets type of rule to apply.
        /// </summary>
        public required ExclusionType Type { get; set; }

        /// <summary>
        /// Gets or sets value to match against.
        /// </summary>
        public required string Value { get; set; }
    }
}