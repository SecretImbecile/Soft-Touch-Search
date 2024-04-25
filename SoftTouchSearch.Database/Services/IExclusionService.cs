using SoftTouchSearch.Data.Models;

namespace SoftTouchSearch.Data.Services
{
    internal interface IExclusionService
    {
        /// <summary>
        /// Get a list of all episodes with exclusions filtered.
        /// </summary>
        /// <returns><see cref="IList{T}"/> of <see cref="Episode"/>.</returns>
        public Task<IList<Episode>> GetEpisodesAsync();
    }
}