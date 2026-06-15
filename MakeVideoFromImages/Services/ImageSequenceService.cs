using MakeVideoFromImages.Models;

namespace MakeVideoFromImages.Services;

public sealed class ImageSequenceService
{
    private static readonly HashSet<string> SupportedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp",
        ".heic"
    };

    public IReadOnlyList<string> BuildSequence(
        IEnumerable<FolderInputModel> folderInputs,
        bool repeatImages = false,
        bool shuffleImages = true)
    {
        ArgumentNullException.ThrowIfNull(folderInputs);

        var folders = folderInputs
            .Select(input => new FolderState(input, GetImages(input.FolderPath, shuffleImages), shuffleImages))
            .ToList();

        if (folders.Count == 0)
        {
            throw new InvalidOperationException("Add at least one image folder.");
        }

        var emptyFolders = folders.Where(folder => folder.Images.Count == 0).ToList();
        if (emptyFolders.Count > 0)
        {
            var paths = string.Join(Environment.NewLine, emptyFolders.Select(folder => folder.Input.FolderPath));
            throw new InvalidOperationException($"No supported images were found in:{Environment.NewLine}{paths}");
        }

        var sequence = new List<string>();

        var cycleCount = repeatImages
            ? folders.Max(folder => (int)Math.Ceiling((double)folder.Images.Count / folder.Input.ImagesPerCycle))
            : int.MaxValue;

        var currentCycle = 0;
        while (folders.Any(folder => folder.HasImagesRemaining) && currentCycle < cycleCount)
        {
            foreach (var folder in folders)
            {
                if (!folder.HasImagesRemaining && repeatImages)
                {
                    folder.Reset();
                }

                if (!folder.HasImagesRemaining)
                {
                    continue;
                }

                var takeCount = Math.Min(folder.Input.ImagesPerCycle, folder.Images.Count - folder.NextIndex);
                for (var i = 0; i < takeCount; i++)
                {
                    sequence.Add(folder.Images[folder.NextIndex]);
                    folder.NextIndex++;
                }
            }

            currentCycle++;
        }

        return sequence;
    }

    private static List<string> GetImages(string folderPath, bool shuffleImages)
    {
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            throw new DirectoryNotFoundException($"Folder does not exist: {folderPath}");
        }

        var images = Directory.EnumerateFiles(folderPath)
            .Where(path => SupportedExtensions.Contains(Path.GetExtension(path)))
            .ToList();

        if (shuffleImages)
        {
            Shuffle(images);
            return images;
        }

        return images
            .OrderBy(path => Path.GetFileName(path), StringComparer.CurrentCultureIgnoreCase)
            .ToList();
    }

    private static void Shuffle(List<string> images)
    {
        for (var i = images.Count - 1; i > 0; i--)
        {
            var j = Random.Shared.Next(i + 1);
            (images[i], images[j]) = (images[j], images[i]);
        }
    }

    private sealed class FolderState
    {
        private readonly bool _shuffleImages;

        public FolderState(FolderInputModel input, List<string> images, bool shuffleImages)
        {
            Input = input;
            Images = images;
            _shuffleImages = shuffleImages;
        }

        public FolderInputModel Input { get; }

        public List<string> Images { get; }

        public int NextIndex { get; set; }

        public bool HasImagesRemaining => NextIndex < Images.Count;

        public void Reset()
        {
            if (_shuffleImages)
            {
                Shuffle(Images);
            }

            NextIndex = 0;
        }
    }
}
