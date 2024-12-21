# SoftTouchSearch.Data.Ingest

`SoftTouchSearch.Data.Ingest` provides an API to ingest data into the *Soft Touch Search* database.

## Endpoints

- `POST` `/ingest/Chapter`
    - Insert a story chapter into the database.
    - Request body should be a `ChapterDTO` JSON string.
- `POST` `/ingest/Episode`
    - Insert a story episode into the database.
    - Request body should be a `EpisodeDTO` JSON string.
- `POST` `/ingest/Exclusion`
    - Insert an exclusion rule into the database.
    - Request body should be a `ExclusionDTO` JSON string.

## Usage

Ensure you have added the required services for `SoftTouchSearch.Data` to the main web app.

```c#
builder.Services.AddDataServices("C:\Path\To\softtouchsearch.db");
```

Map the ingest controllers in Program.cs in the main web app using the `EndpointRouteBuilderExtensions.MapIngestControllerRoute` extension method.

```c#
#if DEBUG
app.MapIngestControllerRoute();
#endif
```

Once the web app is running, you can submit requests from an external program.

## Security

The controller actions provided by the library do not require any form of authentication. You should:

1. Only map the controller when running locally, i.e. wrap the IEndpointRouteBuilder extension call with an `#if DEBUG` compiler directive.
2. Optionally, remove the project reference entirely when not in use.