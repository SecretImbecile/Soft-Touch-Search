// <copyright file="EndpointRouteBuilderExtensions.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Data.Ingest
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    /// <summary>
    /// Provides extension methods to map the Ingest controller routes.
    /// </summary>
    public static class EndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Maps the Ingest controller routes.
        /// </summary>
        /// <param name="endpoints">The endpoint route builder.</param>
        public static void MapIngestControllerRoute(this IEndpointRouteBuilder endpoints) =>
            endpoints.MapAreaControllerRoute(
                name: "ingest-default",
                areaName: "ingest",
                pattern: "ingest/{controller}/{action=Index}/{id?}");
    }
}