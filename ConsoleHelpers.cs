namespace FolderScanToTxt;

/// <summary>
/// Provides console-based helper methods for interactive configuration.
/// </summary>
public static class ConsoleHelpers
{
    /// <summary>
    /// Prompts the user to decide whether to include hidden files.
    /// </summary>
    public static bool PromptHiddenFiles(string message = "Include hidden files and folders? (y/n): ")
    {
        Console.Write(message);
        return Console.ReadLine()?.Trim().ToLower() == "y";
    }

    /// <summary>
    /// Prompts the user to enter a file type filter.
    /// </summary>
    public static string? PromptFileType(string message = "Filter by specific file type (e.g., txt, mp3)? Leave empty for all: ")
    {
        Console.Write(message);
        string? input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? null : "." + input.Trim().ToLower();
    }
}
