using MakeVideoFromImages.Models;

namespace MakeVideoFromImages;

public partial class Main
{
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
            Description = "Escolha uma pasta de imagens para adicionar.",
            UseDescriptionForTitle = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        if (_folders.Any(folder => string.Equals(folder.FolderPath, dialog.SelectedPath, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "Essa pasta ja esta na lista.", "Pasta duplicada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _folders.Add(new FolderInputModel
        {
            FolderPath = dialog.SelectedPath,
            ImagesPerCycle = 1
        });

        AppendLog($"Pasta adicionada: {dialog.SelectedPath}");
    }

    private void RemoveFolderButton_Click(object? sender, EventArgs e)
    {
        if (folderGrid.CurrentRow?.DataBoundItem is not FolderInputModel selected)
        {
            return;
        }

        _folders.Remove(selected);
        AppendLog($"Pasta removida: {selected.FolderPath}");
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
            MessageBox.Show(this, "Fotos por ciclo deve ser um numero inteiro positivo.", "Valor invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private List<FolderInputModel> GetFolderInputs()
    {
        if (_folders.Count == 0)
        {
            throw new InvalidOperationException("Adicione pelo menos uma pasta antes de gerar o video.");
        }

        return _folders
            .Select(folder => new FolderInputModel
            {
                FolderPath = folder.FolderPath,
                ImagesPerCycle = folder.ImagesPerCycle
            })
            .ToList();
    }
}
