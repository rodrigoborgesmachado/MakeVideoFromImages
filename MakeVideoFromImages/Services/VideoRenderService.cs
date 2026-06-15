using System.Globalization;
using System.Text;

namespace MakeVideoFromImages.Services;

public sealed class VideoRenderService
{
    private static readonly Encoding Utf8WithoutBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
    private readonly FFmpegService _ffmpegService;

    public VideoRenderService(FFmpegService ffmpegService)
    {
        _ffmpegService = ffmpegService;
    }

    public async Task RenderAsync(
        IReadOnlyList<string> imageSequence,
        RenderOptions options,
        IProgress<RenderProgress>? progress,
        CancellationToken cancellationToken)
    {
        Validate(imageSequence, options);

        var tempRoot = Path.Combine(Path.GetTempPath(), "MakeVideoFromImages", DateTime.Now.ToString("yyyyMMdd_HHmmss_fff", CultureInfo.InvariantCulture));
        Directory.CreateDirectory(tempRoot);

        try
        {
            await _ffmpegService
                .EnsureAvailableAsync(options.FFmpegPath, CreateTechnicalLogger(progress), cancellationToken)
                .ConfigureAwait(false);

            var clipPaths = new List<string>(imageSequence.Count);
            var totalSteps = imageSequence.Count + 1;
            for (var i = 0; i < imageSequence.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var sourceImagePath = await PrepareImageForRenderingAsync(
                    imageSequence[i],
                    tempRoot,
                    i,
                    options,
                    progress,
                    cancellationToken).ConfigureAwait(false);

                var clipPath = Path.Combine(tempRoot, $"clip_{i:D6}.mp4");
                progress?.Report(new RenderProgress(
                    $"Rendering image {i + 1} of {imageSequence.Count}: {Path.GetFileName(imageSequence[i])}",
                    i,
                    totalSteps));

                var arguments = BuildImageClipArguments(sourceImagePath, clipPath, options);
                var result = await _ffmpegService
                    .RunAsync(options.FFmpegPath, arguments, CreateTechnicalLogger(progress), cancellationToken)
                    .ConfigureAwait(false);
                if (result.ExitCode != 0)
                {
                    throw new InvalidOperationException($"FFmpeg failed while rendering {imageSequence[i]}:{Environment.NewLine}{result.Error}");
                }

                clipPaths.Add(clipPath);
                progress?.Report(new RenderProgress(
                    $"Rendered image {i + 1} of {imageSequence.Count}",
                    i + 1,
                    totalSteps));
            }

            progress?.Report(new RenderProgress("Combining clips into final video...", imageSequence.Count, totalSteps));
            var concatFile = Path.Combine(tempRoot, "concat.txt");
            var concatFileContent = BuildConcatFile(clipPaths);
            await File.WriteAllTextAsync(concatFile, concatFileContent, Utf8WithoutBom, cancellationToken).ConfigureAwait(false);
            progress?.Report(new RenderProgress($"Concat file: {concatFile}", IsTechnical: true));
            progress?.Report(new RenderProgress($"Concat file content:{Environment.NewLine}{concatFileContent}", IsTechnical: true));

            var outputDirectory = Path.GetDirectoryName(options.OutputPath);
            if (!string.IsNullOrWhiteSpace(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            var concatArguments = $"-y -f concat -safe 0 -i {FFmpegService.Quote(concatFile)} -c copy {FFmpegService.Quote(options.OutputPath)}";
            var concatResult = await _ffmpegService
                .RunAsync(options.FFmpegPath, concatArguments, CreateTechnicalLogger(progress), cancellationToken)
                .ConfigureAwait(false);
            if (concatResult.ExitCode != 0)
            {
                throw new InvalidOperationException($"FFmpeg failed while creating the final video:{Environment.NewLine}{concatResult.Error}");
            }

            progress?.Report(new RenderProgress($"Video created: {options.OutputPath}", totalSteps, totalSteps));
        }
        finally
        {
            TryDeleteDirectory(tempRoot);
        }
    }

    private static void Validate(IReadOnlyList<string> imageSequence, RenderOptions options)
    {
        if (imageSequence.Count == 0)
        {
            throw new InvalidOperationException("No images were selected for rendering.");
        }

        if (options.ImageDurationSeconds <= 0)
        {
            throw new InvalidOperationException("Image duration must be greater than zero.");
        }

        if (options.TransitionDurationSeconds < 0)
        {
            throw new InvalidOperationException("Transition duration cannot be negative.");
        }

        if (options.TransitionDurationSeconds * 2 >= options.ImageDurationSeconds)
        {
            throw new InvalidOperationException("Transition duration must be less than half of the image duration.");
        }

        if (options.Width <= 0 || options.Height <= 0)
        {
            throw new InvalidOperationException("Output resolution must be valid.");
        }

        if (string.IsNullOrWhiteSpace(options.OutputPath))
        {
            throw new InvalidOperationException("Choose an output video path.");
        }
    }

    private static string BuildImageClipArguments(string imagePath, string clipPath, RenderOptions options)
    {
        var duration = FormatSeconds(options.ImageDurationSeconds);
        var size = $"{options.Width}:{options.Height}";
        var transitionFilter = string.Empty;
        if (options.TransitionDurationSeconds > 0)
        {
            var fadeDuration = FormatSeconds(options.TransitionDurationSeconds);
            var fadeOutStart = FormatSeconds(options.ImageDurationSeconds - options.TransitionDurationSeconds);
            transitionFilter = $"fade=t=in:st=0:d={fadeDuration},fade=t=out:st={fadeOutStart}:d={fadeDuration},";
        }

        var filter =
            $"[0:v]scale={size}:force_original_aspect_ratio=increase,crop={size},boxblur=20:1[bg];" +
            $"[0:v]scale={size}:force_original_aspect_ratio=decrease[fg];" +
            $"[bg][fg]overlay=(W-w)/2:(H-h)/2,format=yuv420p," +
            $"{transitionFilter}setsar=1[v]";

        return "-y " +
            $"-loop 1 -i {FFmpegService.Quote(imagePath)} -t {duration} " +
            $"-filter_complex {FFmpegService.Quote(filter)} -map \"[v]\" " +
            "-r 30 -an -c:v libx264 -preset veryfast -crf 20 " +
            $"-movflags +faststart {FFmpegService.Quote(clipPath)}";
    }

    private async Task<string> PrepareImageForRenderingAsync(
        string imagePath,
        string tempRoot,
        int imageIndex,
        RenderOptions options,
        IProgress<RenderProgress>? progress,
        CancellationToken cancellationToken)
    {
        if (!IsHeicImage(imagePath))
        {
            return imagePath;
        }

        var convertedPath = Path.Combine(tempRoot, $"heic_{imageIndex:D6}.jpg");
        progress?.Report(new RenderProgress($"Converting HEIC to JPG: {Path.GetFileName(imagePath)}"));

        var arguments =
            $"-y -i {FFmpegService.Quote(imagePath)} " +
            "-frames:v 1 -q:v 2 " +
            $"{FFmpegService.Quote(convertedPath)}";

        var result = await _ffmpegService
            .RunAsync(options.FFmpegPath, arguments, CreateTechnicalLogger(progress), cancellationToken)
            .ConfigureAwait(false);

        if (result.ExitCode != 0)
        {
            throw new InvalidOperationException($"FFmpeg failed while converting HEIC image {imagePath}:{Environment.NewLine}{result.Error}");
        }

        return convertedPath;
    }

    private static Action<string>? CreateTechnicalLogger(IProgress<RenderProgress>? progress)
    {
        return progress is null
            ? null
            : message => progress.Report(new RenderProgress(message, IsTechnical: true));
    }

    private static bool IsHeicImage(string imagePath)
    {
        var extension = Path.GetExtension(imagePath);
        return extension.Equals(".heic", StringComparison.OrdinalIgnoreCase) ||
            extension.Equals(".heif", StringComparison.OrdinalIgnoreCase);
    }

    private static string BuildConcatFile(IEnumerable<string> clipPaths)
    {
        var builder = new StringBuilder();
        foreach (var clipPath in clipPaths)
        {
            builder.Append("file '");
            builder.Append(clipPath.Replace("\\", "/").Replace("'", "'\\''"));
            builder.AppendLine("'");
        }

        return builder.ToString();
    }

    private static string FormatSeconds(double value) => value.ToString("0.###", CultureInfo.InvariantCulture);

    private static void TryDeleteDirectory(string path)
    {
        try
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive: true);
            }
        }
        catch
        {
            // Temporary files are best-effort cleanup; rendering errors should stay visible.
        }
    }
}
