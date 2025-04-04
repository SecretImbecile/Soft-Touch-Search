// <copyright file="IExclusionService.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Services
{
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Data.Models.Dto;

    /// <summary>
    /// Interface for services providing exclusion rules to episodes.
    /// </summary>
    public interface IExclusionService
    {
        /// <summary>
        /// Get a list of all episodes with exclusions filtered.
        /// </summary>
        /// <returns><see cref="IList{T}"/> of <see cref="Episode"/>.</returns>
        [Obsolete("Not in use")]
        IList<Episode> GetEpisodes();

        /// <summary>
        /// Get a list of all episodes asynchronously with exclusions filtered.
        /// </summary>
        /// <returns><see cref="IList{T}"/> of <see cref="Episode"/>.</returns>
        [Obsolete("Not in use")]
        Task<IList<Episode>> GetEpisodesAsync();

        /// <summary>
        /// Get the latest episode in the database.
        /// </summary>
        /// <returns>A <see cref="LatestEpisodeDto"/> detailing the latest episode by date, or <see langword="null"/>.</returns>
        LatestEpisodeDto? GetLatestEpisode();

        /// <summary>
        /// Get the latest episode in the database asynchronously.
        /// </summary>
        /// <returns>A <see cref="LatestEpisodeDto"/> detailing the latest episode by date, or <see langword="null"/>.</returns>
        Task<LatestEpisodeDto?> GetLatestEpisodeAsync();
    }
}