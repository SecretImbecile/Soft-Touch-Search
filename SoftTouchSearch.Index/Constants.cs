// <copyright file="Constants.cs" company="Jack Kelly">
// Copyright (c) Jack Kelly. All rights reserved.
// </copyright>

namespace SoftTouchSearch.Index
{
    using Lucene.Net.Util;

    /// <summary>
    /// Public constants for SoftTouchSearch.Index.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Lucene compatability version.
        /// </summary>
        public const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

        /// <summary>
        /// Page size of search results.
        /// </summary>
        public const int SearchPageSize = 10;
    }
}
