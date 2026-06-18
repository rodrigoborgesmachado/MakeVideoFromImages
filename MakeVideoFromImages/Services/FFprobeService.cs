using System.Globalization;

namespace MakeVideoFromImages.Services;

public sealed class FFprobeService
{
    private readonly FFmpegService _processService;

    public FFprobeService(FFmpegService processService)
    {
        _processService = processService;
    }

    public async Task<double> GetDurationSecondsAsync(string mediaPath, CancellationToken cancellationToken)
    {
        var arguments =
            "-v error -show_entries format=duration " +
            "-of default=noprint_wrappers=1:nokey=1 " +
            $"{FFmpegService.Quote(mediaPath)}";

        var result = await _processService
            .RunAsync(FFmpegPathResolver.ResolveFFprobe(), arguments, null, cancellationToken)
            .ConfigureAwait(false);

        if (result.ExitCode != 0)
        {
            throw new InvalidOperationException($"Could not read media duration:{Environment.NewLine}{result.Error}");
        }

        var raw = result.Output.Trim();
        return double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out var duration)
            ? duration
            : throw new InvalidOperationException($"Could not parse media duration returned by ffprobe: {raw}");
    }
}
