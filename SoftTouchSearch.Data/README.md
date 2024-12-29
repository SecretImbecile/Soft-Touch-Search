# SoftTouchSearch.Data

`SoftTouchSearch.Data` provides the
[Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) database
context used by *Soft Touch Search*.

## Database

SoftTouchSearch targets [SQLite](https://www.sqlite.org/index.html) as its
database engine.

The database contents are not distributed with the source code of
SoftTouchSearch, however, a `schema.sql` file containing the CREATE statements
for each table is included in the GitHub release.

## Usage

Add the required services in Program.cs in the main web app using the
`ServiceCollectionExtensions.AddDataServices` extension method.

```c#
builder.Services.AddDataServices("C:\Path\To\softtouchsearch.db");
```

### Database Context

`StoryDbContext` can then be accessed using dependency injection, e.g.

```c#
private readonly StoryDbContext context = context;

public async Task<IEnumerable<Chapter>> GetChapters()
{
    // All chapters in database
    return await this.context.Chapters
        .OrderBy(chapter => chapter.Number)
        .ToListAsync();
}
```

### Services

In addition to the database context, The `IExclusionService` service provides
methods for simultaneously fetching episodes from the database and filtering
them based on exclusion rule records.

```c#
private readonly IExclusionService exclusionService = exclusionService;

public async Task<IEnumerable<Episode>> GetEpisodes()
{
    // All episodes in database minus those which should be filtered
    return await this.exclusionService.GetEpisodesAsync();
}
```