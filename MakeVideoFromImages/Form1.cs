using System.ComponentModel;
using MakeVideoFromImages.Models;
using MakeVideoFromImages.Services;

namespace MakeVideoFromImages
{
    public partial class Form1 : Form
    {
        private readonly BindingList<FolderInputModel> _folders = new();
        private readonly ImageSequenceService _imageSequenceService = new();
        private readonly VideoRenderService _videoRenderService = new(new FFmpegService());

        public Form1()
        {
            InitializeComponent();
            WireEvents();
            ConfigureFolderGrid();
            outputPathTextBox.Text = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                "output.mp4");
            renderProgressBar.Minimum = 0;
            renderProgressBar.Maximum = 100;
            renderProgressBar.Value = 0;
        }

        private void WireEvents()
        {
            addFolderButton.Click += AddFolderButton_Click;
            removeFolderButton.Click += RemoveFolderButton_Click;
            previewSequenceButton.Click += PreviewSequenceButton_Click;
            renderButton.Click += RenderButton_Click;
            browseOutputButton.Click += BrowseOutputButton_Click;
            monitorResolutionRadioButton.CheckedChanged += ResolutionPresetRadioButton_CheckedChanged;
            phoneResolutionRadioButton.CheckedChanged += ResolutionPresetRadioButton_CheckedChanged;
        }

        private void ResolutionPresetRadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            if (monitorResolutionRadioButton.Checked)
            {
                widthInput.Value = 1920;
                heightInput.Value = 1080;
                return;
            }

            if (phoneResolutionRadioButton.Checked)
            {
                widthInput.Value = 1080;
                heightInput.Value = 1920;
            }
        }

        private void ConfigureFolderGrid()
        {
            folderGrid.AutoGenerateColumns = false;
            folderGrid.Columns.Clear();

            folderGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(FolderInputModel.FolderPath),
                HeaderText = "Pasta",
                ReadOnly = true,
                FillWeight = 82
            });

            folderGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(FolderInputModel.ImagesPerCycle),
                HeaderText = "Fotos por ciclo",
                FillWeight = 18
            });

            folderGrid.DataSource = _folders;
            folderGrid.CellValidating += FolderGrid_CellValidating;
            folderGrid.CellEndEdit += (_, _) => folderGrid.Refresh();
        }

        private void AddFolderButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Choose an image folder to add to the slideshow.",
                UseDescriptionForTitle = true
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            if (_folders.Any(folder => string.Equals(folder.FolderPath, dialog.SelectedPath, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(this, "That folder is already in the list.", "Duplicate folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _folders.Add(new FolderInputModel
            {
                FolderPath = dialog.SelectedPath,
                ImagesPerCycle = 1
            });

            AppendLog($"Added folder: {dialog.SelectedPath}");
        }

        private void RemoveFolderButton_Click(object? sender, EventArgs e)
        {
            if (folderGrid.CurrentRow?.DataBoundItem is not FolderInputModel selected)
            {
                return;
            }

            _folders.Remove(selected);
            AppendLog($"Removed folder: {selected.FolderPath}");
        }

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
                AppendLog($"Sequência criada com {sequence.Count} imagens.");
                AppendLog(repeatImagesCheckBox.Checked
                    ? "Repetição ligada: pastas menores podem repetir fotos até acompanhar a maior sequência."
                    : "Repetição desligada: quando uma pasta acaba, ela é pulada.");
                AppendLog(shuffleImagesCheckBox.Checked
                    ? "Ordem das fotos: embaralhada dentro de cada pasta."
                    : "Ordem das fotos: alfabetica dentro de cada pasta.");

                foreach (var item in sequence.Take(200))
                {
                    AppendLog(item);
                }

                if (sequence.Count > 200)
                {
                    AppendLog($"... {sequence.Count - 200} imagens não exibidas.");
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
                statusLabel.Text = "Done";
                MessageBox.Show(this, "Video render completed.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Failed";
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
                Filter = "MP4 video (*.mp4)|*.mp4|All files (*.*)|*.*",
                FileName = string.IsNullOrWhiteSpace(outputPathTextBox.Text) ? "output.mp4" : Path.GetFileName(outputPathTextBox.Text),
                InitialDirectory = GetExistingDirectoryOrDesktop(outputPathTextBox.Text)
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                outputPathTextBox.Text = dialog.FileName;
            }
        }

        private void FolderGrid_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (folderGrid.Columns[e.ColumnIndex].DataPropertyName != nameof(FolderInputModel.ImagesPerCycle))
            {
                return;
            }

            if (!int.TryParse(Convert.ToString(e.FormattedValue), out var batchSize) || batchSize <= 0)
            {
                e.Cancel = true;
                MessageBox.Show(this, "Images per cycle must be a positive whole number.", "Invalid batch size", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private List<FolderInputModel> GetFolderInputs()
        {
            if (_folders.Count == 0)
            {
                throw new InvalidOperationException("Add at least one folder before rendering.");
            }

            return _folders
                .Select(folder => new FolderInputModel
                {
                    FolderPath = folder.FolderPath,
                    ImagesPerCycle = folder.ImagesPerCycle
                })
                .ToList();
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
                FFmpegPath = FFmpegPathResolver.Resolve()
            };
        }

        private void SetBusy(bool isBusy)
        {
            addFolderButton.Enabled = !isBusy;
            removeFolderButton.Enabled = !isBusy;
            previewSequenceButton.Enabled = !isBusy;
            renderButton.Enabled = !isBusy;
            folderGrid.Enabled = !isBusy;
            repeatImagesCheckBox.Enabled = !isBusy;
            shuffleImagesCheckBox.Enabled = !isBusy;
            showTechnicalLogCheckBox.Enabled = !isBusy;
            statusLabel.Text = isBusy ? "Rendering..." : statusLabel.Text;
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        }

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
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string GetExistingDirectoryOrDesktop(string path)
        {
            var directory = string.IsNullOrWhiteSpace(path) ? null : Path.GetDirectoryName(path);
            return !string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory)
                ? directory
                : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }
    }
}
