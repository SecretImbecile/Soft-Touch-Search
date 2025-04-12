// <copyright file="IndexHelper.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
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