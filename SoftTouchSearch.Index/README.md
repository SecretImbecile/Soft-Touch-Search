# SoftTouchSearch.Index

Provides the search index for *Soft Touch Search*, powered by [Lucene.NET](https://lucenenet.apache.org/index.html).

## Usage

Add the required services in Program.cs in the main web app using the `ServiceCollectionExtensions.AddIndexServices` extension method.

```c#
builder.Services.AddIndexServices("C:\Path\To\softtouchsearch_index");
```

### Build index

The index must be built prior to performing any searches by calling `IIndexService.BuildIndex`.
It is recommended to do this within a scheduled HostedService.

```c#
private readonly IServiceScopeFactory scopeFactory = scopeFactory;

private void DoWork(object? state)
{
    using var scope = this.scopeFactory.CreateScope();
    IIndexService indexService = scope.ServiceProvider.GetRequiredService<IIndexService>();

    IList<Document> documents = // Build the Lucene.NET Documents

    indexService.BuildIndex(documents);
}
```

### Perform search

Check if the index has been built using the `IIndexService.IsIndexBuilt` property.

```c#
if (this.indexService.IsIndexBuilt)
{
    // Continue with search
}
else
{
    // Return a notice to the user, i.e. 503 Service Unavailable
}
```

If the index is present, perform a search using `IIndexService.Search`.

```c#
// Lucene Query structure if the user entered "Town Hall"
BooleanQuery exampleQuery =
[
    new BooleanClause(new TermQuery(new Term("content", "Town")), Occur.SHOULD),
    new BooleanClause(new TermQuery(new Term("content", "Hall")), Occur.SHOULD),
];

SearchResults results = this.indexService.Search(exampleQuery);
```