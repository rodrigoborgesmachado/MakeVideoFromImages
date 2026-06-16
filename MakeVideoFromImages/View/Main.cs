using System.ComponentModel;
using MakeVideoFromImages.Metadata;
using MakeVideoFromImages.Models;
using MakeVideoFromImages.Services;

namespace MakeVideoFromImages;

public partial class Main : Form
{
    private readonly BindingList<FolderInputModel> _folders = new();
    private readonly ImageSequenceService _imageSequenceService = new();
    private readonly VideoRenderService _videoRenderService = new(new FFmpegService());

    public Main()
    {
        InitializeComponent();
        WireEvents();
        ConfigureFolderGrid();
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
    }

    private void WireEvents()
    {
        addFolderButton.Click += AddFolderButton_Click;
        removeFolderButton.Click += RemoveFolderButton_Click;
        previewSequenceButton.Click += PreviewSequenceButton_Click;
        renderButton.Click += RenderButton_Click;
        browseOutputButton.Click += BrowseOutputButton_Click;
        aboutButton.Click += AboutButton_Click;
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
}
