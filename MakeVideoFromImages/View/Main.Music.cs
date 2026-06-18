using System.Media;
using MakeVideoFromImages.Models;

namespace MakeVideoFromImages;

public partial class Main
{
    private const int MusicTrackBarScale = 10;

    private static readonly string[] SupportedAudioExtensions =
    [
        ".mp3",
        ".wav",
        ".m4a",
        ".aac",
        ".flac",
        ".ogg",
        ".wma"
    ];

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        StopMusicPreview();
        base.OnFormClosing(e);
    }

    private void ConfigureMusicGrid()
    {
        musicGrid.AutoGenerateColumns = false;
        musicGrid.Columns.Clear();

        musicGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(MusicInputModel.FileName),
            HeaderText = "Musica",
            ReadOnly = true,
            FillWeight = 45
        });

        musicGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(MusicInputModel.DurationSeconds),
            HeaderText = "Duracao (s)",
            ReadOnly = true,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.##" },
            FillWeight = 15
        });

        musicGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(MusicInputModel.StartSeconds),
            HeaderText = "Inicio (s)",
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.##" },
            FillWeight = 15
        });

        musicGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(MusicInputModel.EndSeconds),
            HeaderText = "Fim (s)",
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.##" },
            FillWeight = 15
        });

        musicGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(MusicInputModel.FadeSeconds),
            HeaderText = "Fade audio (s)",
            DefaultCellStyle = new DataGridViewCellStyle { Format = "0.##" },
            FillWeight = 15
        });

        musicGrid.DataSource = _musicTracks;
        musicGrid.CellValidating += MusicGrid_CellValidating;
        musicGrid.CellEndEdit += (_, _) =>
        {
            ClampSelectedMusicValues();
            musicGrid.Refresh();
            UpdateMusicSelectionUi();
            UpdateMusicSummary();
            UpdateVideoEstimate();
        };
    }

    private async void AddMusicButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            AddExtension = true,
            CheckFileExists = true,
            Multiselect = true,
            Filter = "Arquivos de audio|*.mp3;*.wav;*.m4a;*.aac;*.flac;*.ogg;*.wma|Todos os arquivos (*.*)|*.*",
            Title = "Escolha uma ou mais musicas"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            SetBusy(true);
            foreach (var fileName in dialog.FileNames)
            {
                if (!SupportedAudioExtensions.Contains(Path.GetExtension(fileName), StringComparer.OrdinalIgnoreCase))
                {
                    AppendLog($"Audio ignorado por extensao nao suportada: {fileName}");
                    continue;
                }

                var duration = await _ffprobeService.GetDurationSecondsAsync(fileName, CancellationToken.None);
                _musicTracks.Add(new MusicInputModel
                {
                    FilePath = fileName,
                    DurationSeconds = duration,
                    StartSeconds = 0,
                    EndSeconds = duration,
                    FadeSeconds = 2
                });

                AppendLog($"Musica adicionada: {Path.GetFileName(fileName)} ({FormatDuration(TimeSpan.FromSeconds(duration))})");
            }
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
        finally
        {
            SetBusy(false);
            UpdateMusicSelectionUi();
            UpdateMusicSummary();
            UpdateVideoEstimate();
        }
    }

    private void RemoveMusicButton_Click(object? sender, EventArgs e)
    {
        StopMusicPreview();
        if (musicGrid.CurrentRow?.DataBoundItem is not MusicInputModel selected)
        {
            return;
        }

        _musicTracks.Remove(selected);
        UpdateMusicSelectionUi();
        UpdateMusicSummary();
        UpdateVideoEstimate();
    }

    private void MoveMusicUpButton_Click(object? sender, EventArgs e) => MoveSelectedMusic(-1);

    private void MoveMusicDownButton_Click(object? sender, EventArgs e) => MoveSelectedMusic(1);

    private void MoveSelectedMusic(int offset)
    {
        StopMusicPreview();
        if (musicGrid.CurrentRow?.DataBoundItem is not MusicInputModel selected)
        {
            return;
        }

        var oldIndex = _musicTracks.IndexOf(selected);
        var newIndex = oldIndex + offset;
        if (newIndex < 0 || newIndex >= _musicTracks.Count)
        {
            return;
        }

        _musicTracks.RemoveAt(oldIndex);
        _musicTracks.Insert(newIndex, selected);
        musicGrid.ClearSelection();
        musicGrid.Rows[newIndex].Selected = true;
        musicGrid.CurrentCell = musicGrid.Rows[newIndex].Cells[0];
        UpdateMusicSummary();
    }

    private void MusicGrid_SelectionChanged(object? sender, EventArgs e) => UpdateMusicSelectionUi();

    private void MusicRangeTrackBar_RangeChanged(object? sender, EventArgs e)
    {
        if (_updatingMusicSelection || musicGrid.CurrentRow?.DataBoundItem is not MusicInputModel selected)
        {
            return;
        }

        selected.StartSeconds = TrackBarValueToSeconds(musicRangeTrackBar.StartValue);
        selected.EndSeconds = TrackBarValueToSeconds(musicRangeTrackBar.EndValue);
        ClampMusicValues(selected);
        musicGrid.Refresh();
        UpdateMusicSelectionUi();
        UpdateMusicSummary();
        UpdateVideoEstimate();
    }

    private async void MusicRangeTrackBar_RangeChangeCommitted(object? sender, EventArgs e)
    {
        if (!_isMusicPreviewPlaying)
        {
            return;
        }

        await PlaySelectedMusicPreviewAsync();
    }

    private async void PlayMusicPreviewButton_Click(object? sender, EventArgs e)
    {
        await PlaySelectedMusicPreviewAsync();
    }

    private async Task PlaySelectedMusicPreviewAsync()
    {
        if (musicGrid.CurrentRow?.DataBoundItem is not MusicInputModel selected)
        {
            return;
        }

        try
        {
            var previewGeneration = ++_musicPreviewGeneration;
            StopMusicPreview(invalidatePendingPreview: false, clearPlayingState: false);

            var previewDuration = selected.EffectiveDurationSeconds;
            if (previewDuration <= 0)
            {
                MessageBox.Show(this, "O fim da musica precisa ser maior que o inicio.", "Trecho invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _isMusicPreviewPlaying = false;
                return;
            }

            var previewPath = Path.Combine(Path.GetTempPath(), "MakeVideoFromImages", $"preview_{Guid.NewGuid():N}.wav");
            Directory.CreateDirectory(Path.GetDirectoryName(previewPath)!);

            var arguments =
                $"-y -ss {FormatSecondsForFFmpeg(selected.StartSeconds)} -i {MakeVideoFromImages.Services.FFmpegService.Quote(selected.FilePath)} " +
                $"-t {FormatSecondsForFFmpeg(previewDuration)} -vn {BuildPreviewFadeFilter(selected.FadeSeconds, previewDuration)}" +
                $"-ac 2 -ar 44100 -sample_fmt s16 {MakeVideoFromImages.Services.FFmpegService.Quote(previewPath)}";

            var result = await _ffmpegService.RunAsync(
                MakeVideoFromImages.Services.FFmpegPathResolver.Resolve(),
                arguments,
                showTechnicalLogCheckBox.Checked ? AppendLog : null,
                CancellationToken.None);

            if (result.ExitCode != 0)
            {
                throw new InvalidOperationException($"Nao foi possivel gerar a previa da musica:{Environment.NewLine}{result.Error}");
            }

            if (previewGeneration != _musicPreviewGeneration)
            {
                TryDeleteFile(previewPath);
                return;
            }

            _musicPreviewPath = previewPath;
            _musicPreviewPlayer = new SoundPlayer(_musicPreviewPath);
            _musicPreviewPlayer.Play();
            _isMusicPreviewPlaying = true;
            AppendLog($"Tocando previa: {selected.FileName} ({FormatDuration(TimeSpan.FromSeconds(selected.StartSeconds))} - {FormatDuration(TimeSpan.FromSeconds(selected.EndSeconds))})");
        }
        catch (Exception ex)
        {
            _isMusicPreviewPlaying = false;
            ShowError(ex);
        }
    }

    private void StopMusicPreviewButton_Click(object? sender, EventArgs e) => StopMusicPreview();

    private void MusicGrid_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
    {
        var propertyName = musicGrid.Columns[e.ColumnIndex].DataPropertyName;
        if (propertyName is not nameof(MusicInputModel.StartSeconds) and not nameof(MusicInputModel.EndSeconds) and not nameof(MusicInputModel.FadeSeconds))
        {
            return;
        }

        if (!double.TryParse(Convert.ToString(e.FormattedValue), out var value) || value < 0)
        {
            e.Cancel = true;
            MessageBox.Show(this, "Use um numero positivo. O fim precisa ser maior que o inicio.", "Valor invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void UpdateMusicSelectionUi()
    {
        _updatingMusicSelection = true;
        try
        {
            if (musicGrid.CurrentRow?.DataBoundItem is not MusicInputModel selected || selected.DurationSeconds <= 0)
            {
                musicRangeTrackBar.Enabled = false;
                musicRangeTrackBar.SetRange(0, 0, 0);
                musicStartLabel.Text = "Trecho da musica";
                musicEndLabel.Text = "Trecho selecionado: -";
                return;
            }

            musicRangeTrackBar.Enabled = true;
            var maxValue = SecondsToTrackBarValue(selected.DurationSeconds);
            var startValue = Math.Clamp(SecondsToTrackBarValue(selected.StartSeconds), 0, maxValue);
            var endValue = Math.Clamp(SecondsToTrackBarValue(selected.EndSeconds), startValue, maxValue);
            musicRangeTrackBar.SetRange(maxValue, startValue, endValue);
            musicStartLabel.Text =
                $"Inicio {FormatDuration(TimeSpan.FromSeconds(selected.StartSeconds))} | " +
                $"fim {FormatDuration(TimeSpan.FromSeconds(selected.EndSeconds))}";
            musicEndLabel.Text =
                $"Trecho selecionado: {FormatDuration(TimeSpan.FromSeconds(selected.StartSeconds))} - " +
                $"{FormatDuration(TimeSpan.FromSeconds(selected.EndSeconds))} | fade {selected.FadeSeconds:0.##}s";
        }
        finally
        {
            _updatingMusicSelection = false;
        }
    }

    private void UpdateMusicSummary()
    {
        if (_musicTracks.Count == 0)
        {
            musicSummaryLabel.Text = "Nenhuma musica.";
            return;
        }

        var total = TimeSpan.FromSeconds(_musicTracks.Sum(track => track.EffectiveDurationSeconds));
        musicSummaryLabel.Text = $"{_musicTracks.Count} musica(s) | audio selecionado {FormatDuration(total)}";
    }

    private IReadOnlyList<MusicInputModel> GetMusicInputs()
    {
        return _musicTracks
            .Select(track => new MusicInputModel
            {
                FilePath = track.FilePath,
                DurationSeconds = track.DurationSeconds,
                StartSeconds = track.StartSeconds,
                EndSeconds = track.EndSeconds,
                FadeSeconds = track.FadeSeconds
            })
            .ToList();
    }

    private void ClampSelectedMusicValues()
    {
        if (musicGrid.CurrentRow?.DataBoundItem is MusicInputModel selected)
        {
            ClampMusicValues(selected);
        }
    }

    private static void ClampMusicValues(MusicInputModel music)
    {
        music.StartSeconds = Math.Clamp(music.StartSeconds, 0, Math.Max(0, music.DurationSeconds));
        music.EndSeconds = Math.Clamp(music.EndSeconds, 0, Math.Max(0, music.DurationSeconds));
        if (music.EndSeconds <= music.StartSeconds)
        {
            music.EndSeconds = Math.Min(music.DurationSeconds, music.StartSeconds + 1);
        }

        music.FadeSeconds = Math.Clamp(music.FadeSeconds, 0, Math.Max(0, music.EffectiveDurationSeconds / 2));
    }

    private void StopMusicPreview(bool invalidatePendingPreview = true, bool clearPlayingState = true)
    {
        if (invalidatePendingPreview)
        {
            _musicPreviewGeneration++;
        }

        if (clearPlayingState)
        {
            _isMusicPreviewPlaying = false;
        }

        _musicPreviewPlayer?.Stop();
        _musicPreviewPlayer?.Dispose();
        _musicPreviewPlayer = null;

        if (!string.IsNullOrWhiteSpace(_musicPreviewPath) && File.Exists(_musicPreviewPath))
        {
            TryDeleteFile(_musicPreviewPath);
        }

        _musicPreviewPath = null;
    }

    private static void TryDeleteFile(string path)
    {
        try
        {
            File.Delete(path);
        }
        catch
        {
            // Preview cleanup is best effort.
        }
    }

    private static string FormatSecondsForFFmpeg(double value)
    {
        return value.ToString("0.###", System.Globalization.CultureInfo.InvariantCulture);
    }

    private static int SecondsToTrackBarValue(double seconds)
    {
        return Math.Max(0, (int)Math.Round(seconds * MusicTrackBarScale));
    }

    private static double TrackBarValueToSeconds(int value)
    {
        return value / (double)MusicTrackBarScale;
    }

    private static string BuildPreviewFadeFilter(double requestedFadeSeconds, double clipDurationSeconds)
    {
        var fadeSeconds = Math.Min(requestedFadeSeconds, clipDurationSeconds / 2);
        if (fadeSeconds <= 0)
        {
            return string.Empty;
        }

        var fadeOutStart = Math.Max(0, clipDurationSeconds - fadeSeconds);
        var filter =
            $"afade=t=in:st=0:d={FormatSecondsForFFmpeg(fadeSeconds)}," +
            $"afade=t=out:st={FormatSecondsForFFmpeg(fadeOutStart)}:d={FormatSecondsForFFmpeg(fadeSeconds)}";
        return $"-af {MakeVideoFromImages.Services.FFmpegService.Quote(filter)} ";
    }
}
