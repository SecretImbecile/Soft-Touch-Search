namespace SoftTouchSearch.Index.Services
{
    /// <summary>
    /// Provides the search index for the Soft Touch Search.
    /// </summary>
    /// <param name="indexFilePath">Path to store the Lucene.NET Index file in.</param>
    public class IndexService(string indexFilePath) : IIndexService
    {
        // Properties

        /// <summary>
        /// File path of the Lucene.NET Index file.
        /// </summary>
        private readonly string indexFilePath = indexFilePath;

        // Methods

        /// <inheritdoc/>>
        public string GetIndexFilePath()
        {
            return this.indexFilePath;
        }
    }
}
