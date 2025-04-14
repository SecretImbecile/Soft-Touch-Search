// <copyright file="IndexHelper.cs" company="Jack Kelly">
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

namespace SoftTouchSearchIndexBuilder.Classes
{
    using HtmlAgilityPack;
    using Lucene.Net.Documents;
    using SoftTouchSearch.Data.Models;

    /// <summary>
    /// Helper Methods for SoftTouchSearchIndexBuilder.
    /// </summary>
    internal static class IndexHelper
    {
        /// <summary>
        /// Convert an episode for indexing.
        /// </summary>
        /// <param name="episode">Episode to convert.</param>
        /// <returns>A Lucene.NET <see cref="Document"/> representing the episode.</returns>
        internal static Document ConvertEpisode(EpisodeImport episode)
        {
            Document document =
            [
                new StoredField(
                    "id",
                    episode.Id.ToString()),
                new StoredField(
                    "url",
                    episode.UrlExternal ?? episode.UrlTapas),
                new StoredField(
                    "episodenumber",
                    episode.EpisodeNumber),
                new StringField(
                    "title",
                    episode.Title,
                    Field.Store.YES),
                new TextField(
                    "description",
                    HtmlToPlainText(episode.DescriptionHtml),
                    Field.Store.YES),
                new TextField(
                    "content",
                    HtmlToPlainText(episode.ContentHtml),
                    Field.Store.YES),
            ];

            if (episode.Chapter != null)
            {
                StoredField chapter = new(
                    "chapter",
                    episode.Chapter.ToString());
                StoredField chapternumber = new(
                    "chapternumber",
                    episode.Chapter.Number);

                document.Add(chapter);
                document.Add(chapternumber);
            }

            return document;
        }

        /// <summary>
        /// Extract the plaintext from a HTML snippet.
        /// </summary>
        /// <param name="html">HTML to extract from.</param>
        /// <returns>A string of the plaintext.</returns>
        private static string HtmlToPlainText(string html)
        {
            try
            {
                HtmlDocument document = new();
                document.LoadHtml(html);

                var debug = document.DocumentNode.InnerText;

                return document.DocumentNode.InnerText;
            }
            catch (Exception)
            {
                return html;
            }
        }
    }
}