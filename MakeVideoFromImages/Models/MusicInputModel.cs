namespace MakeVideoFromImages.Models;

public sealed class MusicInputModel
{
    public string FilePath { get; set; } = string.Empty;

    public string FileName => Path.GetFileName(FilePath);

    public double DurationSeconds { get; set; }

    public double StartSeconds { get; set; }

    public double EndSeconds { get; set; }

    public double FadeSeconds { get; set; } = 2;

    public double EffectiveDurationSeconds
    {
        get
        {
            var remaining = Math.Max(0, DurationSeconds - StartSeconds);
            if (EndSeconds > StartSeconds)
            {
                return Math.Min(EndSeconds - StartSeconds, remaining);
            }

            return remaining;
        }
    }
}
