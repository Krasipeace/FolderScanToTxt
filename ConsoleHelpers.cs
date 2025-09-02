namespace FolderScanToTxt;

public static class ConsoleHelpers
{
    public static bool PromptHiddenFiles(string message = "Include hidden files and folders? (y/n): ")
    {
        Console.Write(message);
        return Console.ReadLine()?.Trim().ToLower() == "y";
    }

    public static string? PromptFileType(string message = "Filter by specific file type (e.g., txt, mp3)? Leave empty for all: ")
    {
        Console.Write(message);
        string? input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? null : "." + input.Trim().ToLower();
    }
}
