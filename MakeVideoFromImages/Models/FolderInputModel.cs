namespace MakeVideoFromImages.Models;

public sealed class FolderInputModel
{
    public string FolderPath { get; set; } = string.Empty;

    public int ImagesPerCycle { get; set; } = 1;

    public override string ToString() => $"{FolderPath} ({ImagesPerCycle})";
}
