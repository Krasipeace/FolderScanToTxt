# FolderScanToTxt

[FolderScanToTxt](https://www.nuget.org/packages/FolderScanToTxt/) is a lightweight .NET library for scanning directories, filtering files/folders, and exporting the structure to a text file. It is designed to be UI-agnostic, so you can use it in console apps, desktop apps, or web applications.

## Quick Start

```csharp
using FolderScanToTxt;

class Program
{
    static void Main()
    {
        var options = new FolderScanOptions
        {
            IncludeHidden = false,
            FileTypeFilter = ".txt",
            ExcludeFileName1 = "app.exe",
            ExcludeFileName2 = "folder-contents.txt"
        };

        var result = FolderScannerService.ScanAndSave(
            baseDirectory: @"C:\MyFolder",
            outputFileName: "folder-contents.txt",
            options: options
        );

        Console.WriteLine($"Done! Files: {result.FileCount}, Folders: {result.FolderCount}");
    }
}
```

## Features

- Scan directories and list all files/folders.
- Optionally include or exclude hidden files/folders.
- Filter by specific file type (e.g., .txt, .mp3).
- Exclude specific files (e.g., the scanner’s own executable).
- Save results to a .txt file.
- Optional console helpers for interactive prompts.

## Installation

nuget.org: [FolderScanToTxt](https://www.nuget.org/packages/FolderScanToTxt/)

From NuGet.org

```bash
dotnet add package FolderScanToTxt --version 1.0.0
```

## Usage

Using Console Helpers (Optional)

```csharp
bool includeHidden = ConsoleHelpers.PromptHiddenFiles();
string? filter = ConsoleHelpers.PromptFileType();

var options = new FolderScanOptions
{
    IncludeHidden = includeHidden,
    FileTypeFilter = filter,
    ExcludeFileName1 = "app.exe",
    ExcludeFileName2 = "folder-contents.txt"
};

FolderScannerService.ScanAndSave(@"C:\MyFolder", "folder-contents.txt", options);
```

You can also check a console project that uses this library at: [FolderToTxt](https://github.com/Krasipeace/FolderToTxt)

## Output Example

```
MyFolder/
 ├── doc.txt
 ├── image.png
 └── SubFolder/
     ├── notes.txt
     └── song.mp3
```

The exported folder-contents.txt might look like:

```bash
SubFolder/
../notes.txt
../song.mp3
doc.txt
image.png
```

## API Overview

- FolderScanToTxt
    - ScanAndSave(string baseDirectory, string outputFileName, FolderScanOptions options)

- FolderScanOptions
    - bool IncludeHidden → include hidden files/folders.
    - string? FileTypeFilter → e.g. .txt or .mp3.
    - string? ExcludeFileName1, ExcludeFileName2 → filenames to skip.

- FolderScanResult
    - int FileCount
    - int FolderCount
    - string OutputPath

- ConsoleHelpers (Optional)
    - PromptHiddenFiles(string message = "...")
    - PromptFileType(string message = "...")

## License

MIT License. See [LICENSE](https://github.com/Krasipeace/FolderScanToTxt/blob/main/LICENSE)