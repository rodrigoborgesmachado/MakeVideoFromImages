namespace MakeVideoFromImages.Models;

public sealed class FolderInputModel
{
    public string FolderPath { get; set; } = string.Empty;

    public int ImagesPerCycle { get; set; } = 1;

    public int ImageCount { get; set; }

    public override string ToString() => $"{FolderPath} ({ImagesPerCycle})";
}
