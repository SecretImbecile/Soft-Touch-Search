# SoftTouchSearch.Data

`SoftTouchSearch.Data` provides the
[Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) database
contexts used by *Soft Touch Search*. There are two database contexts used
by the solution:

- __ImportDbContext__ is the schema for `SoftTouchSearchIndexBuilder`
    - This is the format you need to provide to create a working version of *Soft Touch Search*.
    - Includes the full story text to be indexed.
- __SearchDbContext__ is the schema for the live application.
    - This is generated and exported by running `SoftTouchSearchIndexBuilder`.
    - Story text and other unnecessary fields are removed to save space/memory.
    - Some computed properties are added to optimise queries in the web application.

SoftTouchSearch targets [SQLite](https://www.sqlite.org/index.html) as its
database engine.

The database contents are not distributed with the source code of
SoftTouchSearch, however, a `schema.sql` file containing the CREATE statements
for each table is included in the GitHub release.

## Usage

### Registration

During application startup register the desired database context using the
extension methods provided by `ServiceCollectionExtensions`:


```c#
// Import database
builder.Services.AddImportDatabase("C:\Path\To\softtouchsearch_import.db");

// Export (production) database
builder.Services.AddSearchDatabase("C:\Path\To\export\softtouchsearch.db");
```

### Making Calls

The chosen context can then be accessed using dependency injection, e.g.

```c#
public class ExampleClass(SearchDbContext context)
{
    public async Task<IEnumerable<Chapter>> GetChapters()
    {
        // All chapters in database
        return await context.Chapters
            .OrderBy(chapter => chapter.Number)
            .ToListAsync();
    }
}
```