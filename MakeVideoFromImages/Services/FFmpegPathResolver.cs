namespace MakeVideoFromImages.Services;

public static class FFmpegPathResolver
{
    private const string RelativeFFmpegPath = "ffmpeg\\bin\\ffmpeg.exe";
    private const string RelativeFFprobePath = "ffmpeg\\bin\\ffprobe.exe";

    public static string Resolve() => ResolveTool(RelativeFFmpegPath, "FFmpeg");

    public static string ResolveFFprobe() => ResolveTool(RelativeFFprobePath, "FFprobe");

    private static string ResolveTool(string relativePath, string toolName)
    {
        var outputPath = Path.Combine(AppContext.BaseDirectory, relativePath);
        if (File.Exists(outputPath))
        {
            return outputPath;
        }

        var currentDirectoryPath = Path.Combine(Environment.CurrentDirectory, relativePath);
        if (File.Exists(currentDirectoryPath))
        {
            return currentDirectoryPath;
        }

        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, relativePath);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        throw new FileNotFoundException(
            $"{toolName} was not found. Expected it at '{relativePath}' relative to the app or project folder.");
    }
}
