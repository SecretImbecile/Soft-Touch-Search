using SoftTouchSearch.Data.Models;

namespace SoftTouchSearch.Data.Services
{
    public interface IExclusionService
    {
        /// <summary>
        /// Get a list of all episodes with exclusions filtered.
        /// </summary>
        /// <returns><see cref="IList{T}"/> of <see cref="Episode"/>.</returns>
        IList<Episode> GetEpisodes();

        /// <summary>
        /// Get a list of all episodes asynchronously with exclusions filtered.
        /// </summary>
        /// <returns><see cref="IList{T}"/> of <see cref="Episode"/>.</returns>
        Task<IList<Episode>> GetEpisodesAsync();

        /// <summary>
        /// Get the latest episode in the database.
        /// </summary>
        /// <returns>The latest <see cref="Episode"/> record by date, if any.</returns>
        Episode? GetLatestEpisode();

        /// <summary>
        /// Get the latest episode in the database asynchronously.
        /// </summary>
        /// <returns>The latest <see cref="Episode"/> record by date, if any.</returns>
        Task<Episode?> GetLatestEpisodeAsync();
    }
}