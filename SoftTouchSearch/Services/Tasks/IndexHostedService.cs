// <copyright file="IndexHostedService.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Services.Tasks
{
    using HtmlAgilityPack;
    using Lucene.Net.Documents;
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Data.Services;
    using SoftTouchSearch.Index.Services;

    /// <summary>
    /// Scheduled task to build the search index on program start.
    /// </summary>
    /// <param name="exclusionService">Episode filtering service.</param>
    /// <param name="indexService">Search index service.</param>
    public class IndexHostedService(IServiceScopeFactory scopeFactory) : IHostedService, IDisposable
    {
        // Fields

        /// <summary>Service scope factory.</summary>
        private readonly IServiceScopeFactory scopeFactory = scopeFactory;

        /// <summary>Scheduled task timer.</summary>
        private Timer? timer = null;

        // Methods

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(
                this.DoWork,
                null,
                TimeSpan.Zero,
                Timeout.InfiniteTimeSpan);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.timer?.Dispose();
            GC.SuppressFinalize(this);
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

        /// <summary>
        /// Rebuilds the search index.
        /// </summary>
        /// <param name="state">(Unused) timer state.</param>
        private void DoWork(object? state)
        {
            using var scope = this.scopeFactory.CreateScope();
            IExclusionService exclusionService = scope.ServiceProvider.GetRequiredService<IExclusionService>();
            IIndexService indexService = scope.ServiceProvider.GetRequiredService<IIndexService>();

            IList<Episode> episodes = exclusionService.GetEpisodes();
            IList<Document> documents = episodes
                .Select(episode => ConvertEpisode(episode))
                .ToList();

            indexService.BuildIndex(documents);
        }
    }
}
