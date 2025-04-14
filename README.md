# Soft Touch Search

Soft Touch search is a web application written in
[ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) 8 and utilising
[Lucene.NET](https://lucenenet.apache.org/index.html), which allows users to
search the full text of the web novel
[Soft Touch](https://tapas.io/series/Soft-Touch/info).

At the time of writing this, the novel has exceeded two million published
words, and has an enthusiastic fan following. This tool was written to allow
them to re-find favourite passages and document the story.

Additional documentation is viewable in the `README.md` of each project folder
within this repo:

- [SoftTouchSearchIndexBuilder](SoftTouchSearchIndexBuilder)
- [SoftTouchSearch.Data](SoftTouchSearch.Data)
- [SoftTouchSearch.Index](SoftTouchSearch.Index)

## Data Usage

The application is reliant on having a full copy of the story text stored in
its database/search index. This has **not** been included in the source code,
nor has any means of obtaining it. You would need to scrape the online version
yourself in order to construct a functioning version of the application.

The application does not provide to end-users any means of downloading the
text, in part or full, of Soft Touch. It only presents short snippets of text
based on the phrases you search for, with links to the officially published
episodes where that text can be found in full.

## Configuration

The `SoftTouchSearch` application requires the following .NET configuration
properties:

```jsonc
// On Windows environments:
{
  "SoftTouchSearchSettings": {
    "PathToDatabase": "C:\\Path\\To\\SoftTouchSearch\\export\\softtouchsearch.db",
    "PathToIndex": "C:\\Path\\To\\SoftTouchSearch\\export\\softtouchsearch_index"
  }
}

// On Linux/Mac environments:
{
  "SoftTouchSearchSettings": {
    "PathToDatabase": "/Path/To/SoftTouchSearch/export/softtouchsearch.db",
    "PathToIndex": "/Path/To/SoftTouchSearch/export/softtouchsearch_index"
  }
}
```

These properties specify paths to:
1. An SQLite database file, by default named `softtouchsearch.db`
2. A prebuilt Lucene.NET index folder, by default named `softtouchsearch_index`

The `SoftTouchSearchIndexBuilder` project can be used to generate this index
and folder structure.

You can define these properties in several locations, as described in
[Configuration in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration#concepts-and-abstractions).

## License

_Soft Touch Search_ is free software: you can redistribute it and/or modify it
under the terms of the GNU General Public License as published by the Free
Software Foundation, either version 3 of the License, or (at your option) any
later version.

_Soft Touch Search_ is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
details.

You should have [received a copy](LICENSE) of the GNU General Public License
along with Soft Touch Search. If not, see https://www.gnu.org/licenses/.
