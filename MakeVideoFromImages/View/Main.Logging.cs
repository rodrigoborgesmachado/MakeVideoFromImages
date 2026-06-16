using MakeVideoFromImages.Services;

namespace MakeVideoFromImages;

public partial class Main
{
    private void AppendLog(string message)
    {
        logTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        statusLabel.Text = message;
    }

    private void HandleRenderProgress(RenderProgress progress)
    {
        if (progress.CurrentStep.HasValue && progress.TotalSteps.HasValue && progress.TotalSteps.Value > 0)
        {
            var value = (int)Math.Round(progress.CurrentStep.Value * 100d / progress.TotalSteps.Value);
            renderProgressBar.Value = Math.Clamp(value, renderProgressBar.Minimum, renderProgressBar.Maximum);
        }

        if (!progress.IsTechnical || showTechnicalLogCheckBox.Checked)
        {
            AppendLog(progress.IsTechnical ? $"Detalhe FFmpeg: {progress.Message}" : progress.Message);
        }
        else if (!statusLabel.Text.StartsWith("Renderizando", StringComparison.OrdinalIgnoreCase))
        {
            statusLabel.Text = progress.Message;
        }
    }

    private void ShowError(Exception ex)
    {
        AppendLog(ex.Message);
        MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private static string GetExistingDirectoryOrDesktop(string path)
    {
        var directory = string.IsNullOrWhiteSpace(path) ? null : Path.GetDirectoryName(path);
        return !string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory)
            ? directory
            : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    }
}
