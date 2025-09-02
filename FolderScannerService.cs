namespace FolderScanToTxt;

public static class FolderScannerService
{
    public static FolderScanResult ScanAndSave(string baseDirectory, string outputFileName, FolderScanOptions options)
    {
        string outputPath = Path.Combine(baseDirectory, outputFileName);

        int totalItems = CountItems(baseDirectory, options);

        int fileCount = 0, folderCount = 0, processedItems = 0;
        using (StreamWriter writer = new(outputPath))
        {
            WriteFiles(baseDirectory, writer, "", options, ref fileCount, ref folderCount, ref processedItems, totalItems);
        }

        return new FolderScanResult
        {
            FileCount = fileCount,
            FolderCount = folderCount,
            OutputPath = outputPath
        };
    }

    private static int CountItems(string dir, FolderScanOptions options)
    {
        int count = 0;

        foreach (var filePath in Directory.GetFiles(dir))
        {
            string fileName = Path.GetFileName(filePath);
            var attrs = File.GetAttributes(filePath);

            if ((!options.IncludeHidden && attrs.HasFlag(FileAttributes.Hidden)) ||
                fileName.Equals(options.ExcludeFileName1, StringComparison.OrdinalIgnoreCase) ||
                fileName.Equals(options.ExcludeFileName2, StringComparison.OrdinalIgnoreCase) ||
                (options.FileTypeFilter != null && !fileName.ToLower().EndsWith(options.FileTypeFilter)))
                continue;

            count++;
        }

        foreach (var subDir in Directory.GetDirectories(dir))
        {
            DirectoryInfo dirInfo = new(subDir);
            bool flowControl = CheckOptions(options, dirInfo);
            if (!flowControl) continue;     

            count++;
            count += CountItems(subDir, options);
        }

        return count;
    }

    private static void WriteFiles(
        string dir,
        StreamWriter writer,
        string relativePath,
        FolderScanOptions options,
        ref int fileCount,
        ref int folderCount,
        ref int processedItems,
        int totalItems)
    {
        if (!string.IsNullOrEmpty(relativePath))
        {
            writer.WriteLine($"{relativePath.Replace("\\", "/")}/");
            folderCount++;
            processedItems++;
        }

        foreach (string filePath in Directory.GetFiles(dir))
        {
            string fileName = Path.GetFileName(filePath);
            var attrs = File.GetAttributes(filePath);

            if ((!options.IncludeHidden && attrs.HasFlag(FileAttributes.Hidden)) ||
                fileName.Equals(options.ExcludeFileName1, StringComparison.OrdinalIgnoreCase) ||
                fileName.Equals(options.ExcludeFileName2, StringComparison.OrdinalIgnoreCase) ||
                (options.FileTypeFilter != null && !fileName.ToLower().EndsWith(options.FileTypeFilter)))
                continue;

            string fileLine = string.IsNullOrEmpty(relativePath) ? fileName : $"../{fileName}";
            writer.WriteLine(fileLine);
            fileCount++;
            processedItems++;
        }

        foreach (string subDir in Directory.GetDirectories(dir))
        {
            DirectoryInfo dirInfo = new(subDir);
            bool flowControl = CheckOptions(options, dirInfo);
            if (!flowControl) continue;

            string folderName = Path.GetFileName(subDir);
            string newRelativePath = string.IsNullOrEmpty(relativePath)
                ? folderName
                : Path.Combine(relativePath, folderName);

            WriteFiles(subDir, writer, newRelativePath, options, ref fileCount, ref folderCount, ref processedItems, totalItems);
        }
    }

    private static bool CheckOptions(FolderScanOptions options, DirectoryInfo dirInfo)
    {
        if (!options.IncludeHidden && dirInfo.Attributes.HasFlag(FileAttributes.Hidden)) return false;
        return true;
    }
}
