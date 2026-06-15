namespace MakeVideoFromImages.Services;

public sealed class RenderOptions
{
    public double ImageDurationSeconds { get; set; } = 4;

    public double TransitionDurationSeconds { get; set; } = 0.5;

    public int Width { get; set; } = 1920;

    public int Height { get; set; } = 1080;

    public string OutputPath { get; set; } = string.Empty;

    public string FFmpegPath { get; set; } = "ffmpeg";
}
