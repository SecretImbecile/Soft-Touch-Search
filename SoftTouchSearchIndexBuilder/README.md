# Soft Touch Search Index Builder

The index builder is a console application that generates the
[Lucene.NET](https://lucenenet.apache.org/index.html) search index for
*Soft Touch Search*. This was previously done in the web application, but is
now done offline to minimise the memory usage of the hosted web app.

## Usage

Run the `SoftTouchSearchIndexBuilder` console application locally, then upload
the resulting files to the server where the `SoftTouchSearch` web application
is hosted.

Upload both the database and index files together to ensure they represent the
same content, as both of them are referenced at runtime.

### Command Line Arguments

`SoftTouchSearchIndexBuilder` must be ran with exactly two arguments

1. The path of the SQLite database file
2. The path to the directory you wish the resulting files to be created

```bat
.\SoftTouchSearchIndexBuilder.exe "C:\Path\To\softtouchsearch.db" "C:\Path\To\exportfolder"
```