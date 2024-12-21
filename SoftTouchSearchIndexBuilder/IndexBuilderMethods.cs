// <copyright file="IndexBuilderMethods.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearchIndexBuilder
{
    using HtmlAgilityPack;
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Store;
    using SoftTouchSearch.Data;
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Data.Services;
    using SoftTouchSearch.Index.Services;

    /// <summary>
    /// Helper Methods for SoftTouchSearchIndexBuilder.
    /// </summary>
    /// <param name="context">Database context.</param>
    /// <param name="indexDirectory">where the index will be saved.</param>
    internal class IndexBuilderMethods(StoryDbContext context, string indexDirectory)
    {
        // Fields

        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        private readonly StoryDbContext context = context;

        /// <summary>
        /// Directory where the index will be saved.
        /// </summary>
        private readonly Directory indexDirectory = FSDirectory.Open(indexDirectory);

        // Methods

        /// <summary>
        /// Convert all <see cref="Episode"/> models in the database into indexable <see cref="Document"/> objects.
        /// </summary>
        /// <returns>An IEnumerable of <see cref="Document"/> objects.</returns>
        public IEnumerable<Document> ConvertEpisodes()
        {
            ExclusionService exclusionService = new(this.context, null!);
            IList<Episode> episodes = exclusionService.GetEpisodes();

            IList<Document> documents = episodes
                .Select(episode => ConvertEpisode(episode))
                .ToList();

            return documents;
        }

        /// <summary>
        /// Build the final search index.
        /// </summary>
        /// <param name="documents">Lucene documents to build index using.</param>
        public void BuildIndex(IEnumerable<Document> documents)
        {
            Analyzer analyzer = new StandardAnalyzer(IndexService.AppLuceneVersion);
            IndexWriterConfig indexConfig = new(IndexService.AppLuceneVersion, analyzer);
            using IndexWriter indexWriter = new(this.indexDirectory, indexConfig);

            if (indexWriter.NumDocs > 0)
            {
                // Remove any existing index contents
                indexWriter.DeleteAll();
                indexWriter.Commit();
            }

            foreach (Document document in documents)
            {
                indexWriter.AddDocument(document);
            }

            indexWriter.Flush(false, false);
        }

        /// <summary>
        /// Convert an episode for indexing.
        /// </summary>
        /// <param name="episode">Episode to convert.</param>
        /// <returns>A Lucene.NET <see cref="Document"/> representing the episode.</returns>
        private static Document ConvertEpisode(Episode episode)
        {
            Document document =
            [
                new StoredField(
                    "id",
                    episode.Id.ToString()),
                new StoredField(
                    "url",
                    episode.Url),
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