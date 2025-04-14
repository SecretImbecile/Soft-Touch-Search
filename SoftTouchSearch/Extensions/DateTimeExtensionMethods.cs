// <copyright file="DateTimeExtensionMethods.cs" company="Jack Kelly">
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

namespace SoftTouchSearch.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    internal static class DateTimeExtensionMethods
    {
        /// <summary>
        /// Returns the <see langword="string"/> representation of the <see cref="DateTime"/> in the format expected by an HTML datetime attribute.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to format.</param>
        /// <returns>The <see langword="string"/> representation of the <see cref="DateTime"/>.</returns>
        public static string ToHtmlString(this DateTime dateTime)
        {
            return dateTime
                .ToUniversalTime()
                .ToString("yyyy-MM-dd'T'hh:mm:ssK");
        }
    }
}
