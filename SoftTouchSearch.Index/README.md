# SoftTouchSearch.Index

Provides the search index for *Soft Touch Search*, powered by [Lucene.NET](https://lucenenet.apache.org/index.html).

## Usage

Add the required services in Program.cs in the main web app using the `ServiceCollectionExtensions.AddIndexServices` extension method.

```c#
builder.Services.AddIndexServices("C:\Path\To\softtouchsearch_index");
```

### Perform search

Perform a search using `IIndexService.Search`.

```c#
// Lucene Query structure if the user entered "Town Hall"
BooleanQuery exampleQuery =
[
    new BooleanClause(new TermQuery(new Term("content", "Town")), Occur.SHOULD),
    new BooleanClause(new TermQuery(new Term("content", "Hall")), Occur.SHOULD),
];

SearchResults results = this.indexService.Search(exampleQuery);
```