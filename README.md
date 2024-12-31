# Soft Touch Search

Soft Touch search is a web application written in .NET Core 8 and utilising
[Lucene.NET](https://lucenenet.apache.org/index.html) , which allows
users to search the full text of the web novel
[Soft Touch](https://tapas.io/series/Soft-Touch/info).

At the time of writing this, the novel has exceeded two million published
words, and has an enthusiastic fan following. This tool was written to allow
them to re-find favourite passeages and document the story.

Additional documentation is viewable in the `README.md` of each project folder
within this repo:

- [SoftTouchSearchIndexBuilder](SoftTouchSearchIndexBuilder)
- [SoftTouchSearch.Data](SoftTouchSearch.Data)
- [SoftTouchSearch.Data.Ingest](SoftTouchSearch.Data.Ingest)
- [SoftTouchSearch.Index](SoftTouchSearch.Index)

## Data Usage

The application is reliant on having a full copy of the story text stored in
its database/search index. This has __not__ been included in the source code,
nor has any means of obtaining it. You would need to scrape the online version
yourself in order to construct a functioning version of the application.

The application does not provide to end-users any means of downloading the
text, in part or full, of Soft Touch. It only presents short snippets of text
based on the phrases you search for, with links to the officially published
episodes where that text can be found in full.

## Command Line Arguments

The `SoftTouchSearch` must be ran with a command line specifying a 'data' folder, e.g.

```bash
$ dotnet ./SoftTouchSearch.dll "/Path/To/dataFolder"
```

The data folder should contain:

1. An SQLite database file named `softtouchsearch.db`
2. A prebuilt Lucene.NET index in a folder named `softtouchsearch_index`

The `SoftTouchSearchIndexBuilder` project can be used to generate this index and folder structure.