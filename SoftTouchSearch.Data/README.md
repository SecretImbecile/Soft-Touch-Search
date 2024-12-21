# SoftTouchSearch.Data

`SoftTouchSearch.Data` provides the [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) database context used by *Soft Touch Search*.

## Database

SoftTouchSearch targets [SQLite](https://www.sqlite.org/index.html) as its database engine.

The database contents are not distributed with the source code of SoftTouchSearch, however,
a `schema.sql` file containing the CREATE statements for each table is included in the GitHub release.

## Usage

Add the required services in Program.cs in the main web app using the `ServiceCollectionExtensions.AddDataServices` extension method.

```c#
builder.Services.AddDataServices("C:\Path\To\softtouchsearch.db");
```

The `StoryDbContext` can then be accessed using dependency injection, e.g.

```c#
private readonly StoryDbContext context = context;

public async Task<IEnumerable<Chapters>> GetChapters()
{
    return await this.context.Chapters
        .OrderBy(chapter => chapter.Number)
        .ToListAsync();
}
```