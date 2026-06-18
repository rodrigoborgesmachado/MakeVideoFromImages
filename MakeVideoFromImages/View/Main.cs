using System.ComponentModel;
using System.Media;
using MakeVideoFromImages.Metadata;
using MakeVideoFromImages.Models;
using MakeVideoFromImages.Services;

namespace MakeVideoFromImages;

public partial class Main : Form
{
    private readonly BindingList<FolderInputModel> _folders = new();
    private readonly BindingList<MusicInputModel> _musicTracks = new();
    private readonly ImageSequenceService _imageSequenceService = new();
    private readonly FFmpegService _ffmpegService = new();
    private readonly VideoRenderService _videoRenderService;
    private readonly FFprobeService _ffprobeService;
    private bool _updatingMusicSelection;
    private SoundPlayer? _musicPreviewPlayer;
    private string? _musicPreviewPath;
    private bool _isMusicPreviewPlaying;
    private int _musicPreviewGeneration;

    public Main()
    {
        _videoRenderService = new VideoRenderService(_ffmpegService);
        _ffprobeService = new FFprobeService(_ffmpegService);

        InitializeComponent();
        WireEvents();
        ConfigureFolderGrid();
        ConfigureMusicGrid();
        InitializeDefaults();
    }

    private void InitializeDefaults()
    {
        Text = $"{ApplicationOwnership.ProductName} - {ApplicationOwnership.CompanyName}";

        outputPathTextBox.Text = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "output.mp4");

        renderProgressBar.Minimum = 0;
        renderProgressBar.Maximum = 100;
        renderProgressBar.Value = 0;
        UpdateVideoEstimate();
    }

    private void WireEvents()
    {
        addFolderButton.Click += AddFolderButton_Click;
        removeFolderButton.Click += RemoveFolderButton_Click;
        previewSequenceButton.Click += PreviewSequenceButton_Click;
        renderButton.Click += RenderButton_Click;
        browseOutputButton.Click += BrowseOutputButton_Click;
        aboutButton.Click += AboutButton_Click;
        addMusicButton.Click += AddMusicButton_Click;
        removeMusicButton.Click += RemoveMusicButton_Click;
        moveMusicUpButton.Click += MoveMusicUpButton_Click;
        moveMusicDownButton.Click += MoveMusicDownButton_Click;
        musicGrid.SelectionChanged += MusicGrid_SelectionChanged;
        musicRangeTrackBar.RangeChanged += MusicRangeTrackBar_RangeChanged;
        musicRangeTrackBar.RangeChangeCommitted += MusicRangeTrackBar_RangeChangeCommitted;
        playMusicPreviewButton.Click += PlayMusicPreviewButton_Click;
        stopMusicPreviewButton.Click += StopMusicPreviewButton_Click;
        monitorResolutionRadioButton.CheckedChanged += ResolutionPresetRadioButton_CheckedChanged;
        phoneResolutionRadioButton.CheckedChanged += ResolutionPresetRadioButton_CheckedChanged;
        imageDurationInput.ValueChanged += (_, _) => UpdateVideoEstimate();
        transitionDurationInput.ValueChanged += (_, _) => UpdateVideoEstimate();
        repeatImagesCheckBox.CheckedChanged += (_, _) => UpdateVideoEstimate();
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

    private void UpdateVideoEstimate()
    {
        if (_folders.Count == 0)
        {
            videoEstimateLabel.Text = "Nenhuma pasta adicionada.";
            return;
        }

        var imageCount = _imageSequenceService.EstimateSequenceImageCount(_folders, repeatImagesCheckBox.Checked);
        var durationSeconds = imageCount * (double)imageDurationInput.Value;
        var duration = TimeSpan.FromSeconds(durationSeconds);
        var fadeSeconds = (double)transitionDurationInput.Value;
        var musicDuration = TimeSpan.FromSeconds(_musicTracks.Sum(track => track.EffectiveDurationSeconds));

        videoEstimateLabel.Text =
            $"Estimativa: {imageCount} fotos | video {FormatDuration(duration)} | " +
            $"musicas {FormatDuration(musicDuration)} | foto {(double)imageDurationInput.Value:0.##}s | fade {fadeSeconds:0.##}s";
    }

    private static string FormatDuration(TimeSpan duration)
    {
        return duration.TotalHours >= 1
            ? duration.ToString(@"h\:mm\:ss")
            : duration.ToString(@"m\:ss");
    }
}
