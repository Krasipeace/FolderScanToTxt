namespace FolderScanToTxt;

public class FolderScanOptions
{
    public bool IncludeHidden { get; set; } = false;
    public string? FileTypeFilter { get; set; }
    public string? ExcludeFileName1 { get; set; }
    public string? ExcludeFileName2 { get; set; }
}
