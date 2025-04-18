﻿// <copyright file="MetadataBase.cs" company="Jack Kelly">
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
    /// <summary>
    /// Metadata record for a chapter or episode.
    /// </summary>
    public abstract class MetadataBase
    {
        // Properties

        /// <summary>
        /// Gets or sets the number of views the chapter or episode has received.
        /// </summary>
        public required int Views { get; set; }

        /// <summary>
        /// Gets <see cref="Views"/> in the format "54,321".
        /// </summary>
        public string ViewsFormattedFull => this.Views.ToString("N0");

        /// <summary>
        /// Gets <see cref="Views"/> in the format "54.3k".
        /// </summary>
        public string ViewsFormattedShort => FormatShort(this.Views);

        /// <summary>
        /// Gets or sets the number of likes the chapter or episode has received.
        /// </summary>
        public required int Likes { get; set; }

        /// <summary>
        /// Gets <see cref="Likes"/> in the format "54,321".
        /// </summary>
        public string LikesFormattedFull => this.Likes.ToString("N0");

        /// <summary>
        /// Gets <see cref="Likes"/> in the format "54.3k".
        /// </summary>
        public string LikesFormattedShort => FormatShort(this.Likes);

        /// <summary>
        /// Gets or sets the number of comments the chapter or episode has received.
        /// </summary>
        public required int Comments { get; set; }

        /// <summary>
        /// Gets <see cref="Comments"/> in the format "54,321".
        /// </summary>
        public string CommentsFormattedFull => this.Comments.ToString("N0");

        /// <summary>
        /// Gets <see cref="Comments"/> in the format "54.3k".
        /// </summary>
        public string CommentsFormattedShort => FormatShort(this.Comments);

        // Methods

        /// <summary>
        /// String formats the provided value.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Below 1,000</term>
        /// <description>"834"</description>
        /// </item>
        /// <item>
        /// <term>Above 1,000</term>
        /// <description>"498.9k"</description>
        /// </item>
        /// <item>
        /// <term>Above 1,000,000</term>
        /// <description>"4.5m"</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <param name="value">The number to format.</param>
        /// <returns>A <see langword="string"/> representing the number.</returns>
        private static string FormatShort(int value)
        {
            if (value > 1000000)
            {
                float millions = (float)value / 1000000;
                return $"{millions:0.0}m";
            }
            else if (value > 1000)
            {
                float thousands = (float)value / 1000;
                return $"{thousands:0.0}k";
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
