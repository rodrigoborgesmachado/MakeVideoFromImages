namespace MakeVideoFromImages.Services;

public sealed record RenderProgress(
    string Message,
    int? CurrentStep = null,
    int? TotalSteps = null,
    bool IsTechnical = false);
