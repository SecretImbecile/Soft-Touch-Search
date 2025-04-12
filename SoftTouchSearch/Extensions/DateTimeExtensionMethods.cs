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
