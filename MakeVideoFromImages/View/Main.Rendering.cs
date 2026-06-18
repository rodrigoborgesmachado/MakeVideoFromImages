using MakeVideoFromImages.Services;

namespace MakeVideoFromImages;

public partial class Main
{
    private void PreviewSequenceButton_Click(object? sender, EventArgs e)
    {
        try
        {
            folderGrid.EndEdit();
            var sequence = _imageSequenceService.BuildSequence(
                GetFolderInputs(),
                repeatImagesCheckBox.Checked,
                shuffleImagesCheckBox.Checked);

            logTextBox.Clear();
            AppendLog($"Sequencia criada com {sequence.Count} imagens.");
            AppendLog(repeatImagesCheckBox.Checked
                ? "Repeticao ligada: pastas menores podem repetir fotos ate acompanhar a maior sequencia."
                : "Repeticao desligada: quando uma pasta acaba, ela e pulada.");
            AppendLog(shuffleImagesCheckBox.Checked
                ? "Ordem das fotos: embaralhada dentro de cada pasta."
                : "Ordem das fotos: alfabetica dentro de cada pasta.");

            foreach (var item in sequence.Take(200))
            {
                AppendLog(item);
            }

            if (sequence.Count > 200)
            {
                AppendLog($"... {sequence.Count - 200} imagens nao exibidas.");
            }
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private async void RenderButton_Click(object? sender, EventArgs e)
    {
        try
        {
            folderGrid.EndEdit();
            logTextBox.Clear();
            renderProgressBar.Value = 0;
            SetBusy(true);

            var sequence = _imageSequenceService.BuildSequence(
                GetFolderInputs(),
                repeatImagesCheckBox.Checked,
                shuffleImagesCheckBox.Checked);
            var options = GetRenderOptions();
            var progress = new Progress<RenderProgress>(HandleRenderProgress);

            AppendLog($"Renderizando {sequence.Count} imagens.");
            await _videoRenderService.RenderAsync(sequence, options, progress, CancellationToken.None);
            statusLabel.Text = "Concluido";
            MessageBox.Show(this, "Video gerado com sucesso.", "Concluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Falhou";
            ShowError(ex);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void BrowseOutputButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog
        {
            AddExtension = true,
            DefaultExt = "mp4",
            Filter = "Video MP4 (*.mp4)|*.mp4|Todos os arquivos (*.*)|*.*",
            FileName = string.IsNullOrWhiteSpace(outputPathTextBox.Text) ? "output.mp4" : Path.GetFileName(outputPathTextBox.Text),
            InitialDirectory = GetExistingDirectoryOrDesktop(outputPathTextBox.Text)
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            outputPathTextBox.Text = dialog.FileName;
        }
    }

    private RenderOptions GetRenderOptions()
    {
        return new RenderOptions
        {
            ImageDurationSeconds = (double)imageDurationInput.Value,
            TransitionDurationSeconds = (double)transitionDurationInput.Value,
            Width = (int)widthInput.Value,
            Height = (int)heightInput.Value,
            OutputPath = outputPathTextBox.Text.Trim(),
            FFmpegPath = FFmpegPathResolver.Resolve(),
            MusicTracks = GetMusicInputs()
        };
    }

    private void SetBusy(bool isBusy)
    {
        addFolderButton.Enabled = !isBusy;
        removeFolderButton.Enabled = !isBusy;
        previewSequenceButton.Enabled = !isBusy;
        renderButton.Enabled = !isBusy;
        aboutButton.Enabled = !isBusy;
        addMusicButton.Enabled = !isBusy;
        removeMusicButton.Enabled = !isBusy;
        moveMusicUpButton.Enabled = !isBusy;
        moveMusicDownButton.Enabled = !isBusy;
        musicGrid.Enabled = !isBusy;
        musicRangeTrackBar.Enabled = !isBusy && musicGrid.CurrentRow?.DataBoundItem is not null;
        playMusicPreviewButton.Enabled = !isBusy && musicGrid.CurrentRow?.DataBoundItem is not null;
        stopMusicPreviewButton.Enabled = !isBusy;
        folderGrid.Enabled = !isBusy;
        repeatImagesCheckBox.Enabled = !isBusy;
        shuffleImagesCheckBox.Enabled = !isBusy;
        showTechnicalLogCheckBox.Enabled = !isBusy;
        monitorResolutionRadioButton.Enabled = !isBusy;
        phoneResolutionRadioButton.Enabled = !isBusy;
        statusLabel.Text = isBusy ? "Renderizando..." : statusLabel.Text;
        Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
    }
}
