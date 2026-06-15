using System.Diagnostics;
using System.Text;

namespace MakeVideoFromImages.Services;

public sealed class FFmpegService
{
    public async Task EnsureAvailableAsync(
        string ffmpegPath,
        Action<string>? outputHandler,
        CancellationToken cancellationToken)
    {
        var result = await RunAsync(ffmpegPath, "-version", outputHandler, cancellationToken).ConfigureAwait(false);
        if (result.ExitCode != 0)
        {
            throw new InvalidOperationException(
                $"FFmpeg was not found or could not be started. Configure ffmpeg.exe or add it to PATH.{Environment.NewLine}{result.Error}");
        }
    }

    public async Task<ProcessResult> RunAsync(
        string executablePath,
        string arguments,
        Action<string>? outputHandler,
        CancellationToken cancellationToken)
    {
        outputHandler?.Invoke($"FFmpeg command: {Quote(executablePath)} {arguments}");
        outputHandler?.Invoke($"ProcessStartInfo.Arguments: {arguments}");

        var startInfo = new ProcessStartInfo
        {
            FileName = executablePath,
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            StandardErrorEncoding = Encoding.UTF8,
            StandardOutputEncoding = Encoding.UTF8
        };

        using var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true };
        var output = new StringBuilder();
        var error = new StringBuilder();

        process.OutputDataReceived += (_, args) =>
        {
            if (args.Data is null)
            {
                return;
            }

            output.AppendLine(args.Data);
            outputHandler?.Invoke($"stdout: {args.Data}");
        };

        process.ErrorDataReceived += (_, args) =>
        {
            if (args.Data is null)
            {
                return;
            }

            error.AppendLine(args.Data);
            outputHandler?.Invoke($"stderr: {args.Data}");
        };

        try
        {
            if (!process.Start())
            {
                throw new InvalidOperationException("FFmpeg process could not be started.");
            }
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or FileNotFoundException)
        {
            throw new InvalidOperationException(
                $"FFmpeg was not found or could not be started. Path: {executablePath}",
                ex);
        }

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);

        return new ProcessResult(process.ExitCode, output.ToString(), error.ToString());
    }

    public static string Quote(string value) => $"\"{value.Replace("\"", "\\\"")}\"";
}

public sealed record ProcessResult(int ExitCode, string Output, string Error);
