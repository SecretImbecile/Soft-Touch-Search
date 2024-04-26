# Soft Touch Search

Soft Touch Search is a web application which allows users to search the text of the web novel [Soft Touch](https://tapas.io/series/Soft-Touch/info).

## Projects

### `SoftTouchSearch`

The main web application. Contains the pages which make up the front end of the application.

### `SoftTouchSearch.Data`

The database context used to store the Soft Touch text contents.

#### Migrations

The connection string must be specified manually when updating the database schema, e.g.
```
dotnet ef database update --connection "Data Source=C:\Path\To\softtouchsearch.db"
```

### `SoftTouchSearch.Data.Ingest`

Ingest tools for importing the Soft Touch text contents.

### `SoftTouchSearch.Index`

Search index powered by [Lucene.NET](https://lucenenet.apache.org/index.html).