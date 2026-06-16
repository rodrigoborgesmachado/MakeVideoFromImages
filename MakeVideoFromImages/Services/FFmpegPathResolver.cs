namespace MakeVideoFromImages.Services;

public static class FFmpegPathResolver
{
    private const string RelativeFFmpegPath = "ffmpeg\\bin\\ffmpeg.exe";

    public static string Resolve()
    {
        var outputPath = Path.Combine(AppContext.BaseDirectory, RelativeFFmpegPath);
        if (File.Exists(outputPath))
        {
            return outputPath;
        }

        var currentDirectoryPath = Path.Combine(Environment.CurrentDirectory, RelativeFFmpegPath);
        if (File.Exists(currentDirectoryPath))
        {
            return currentDirectoryPath;
        }

        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, RelativeFFmpegPath);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        throw new FileNotFoundException(
            $"FFmpeg was not found. Expected it at '{RelativeFFmpegPath}' relative to the app or project folder.");
    }
}
