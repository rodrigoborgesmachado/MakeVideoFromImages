namespace MakeVideoFromImages
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            folderGrid = new DataGridView();
            addFolderButton = new Button();
            removeFolderButton = new Button();
            previewSequenceButton = new Button();
            renderButton = new Button();
            imageDurationInput = new NumericUpDown();
            transitionDurationInput = new NumericUpDown();
            widthInput = new NumericUpDown();
            heightInput = new NumericUpDown();
            outputPathTextBox = new TextBox();
            browseOutputButton = new Button();
            ffmpegPathTextBox = new TextBox();
            browseFFmpegButton = new Button();
            logTextBox = new TextBox();
            optionsPanel = new TableLayoutPanel();
            imageDurationLabel = new Label();
            transitionDurationLabel = new Label();
            widthLabel = new Label();
            heightLabel = new Label();
            outputPathLabel = new Label();
            ffmpegPathLabel = new Label();
            repeatImagesCheckBox = new CheckBox();
            shuffleImagesCheckBox = new CheckBox();
            showTechnicalLogCheckBox = new CheckBox();
            resolutionPresetPanel = new FlowLayoutPanel();
            monitorResolutionRadioButton = new RadioButton();
            phoneResolutionRadioButton = new RadioButton();
            buttonsPanel = new FlowLayoutPanel();
            statusLabel = new Label();
            renderProgressBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)folderGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageDurationInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)transitionDurationInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)widthInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heightInput).BeginInit();
            optionsPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();

            folderGrid.AllowUserToAddRows = false;
            folderGrid.AllowUserToDeleteRows = false;
            folderGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            folderGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            folderGrid.Location = new Point(12, 54);
            folderGrid.MultiSelect = false;
            folderGrid.Name = "folderGrid";
            folderGrid.RowHeadersWidth = 24;
            folderGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            folderGrid.Size = new Size(860, 196);
            folderGrid.TabIndex = 0;

            addFolderButton.AutoSize = true;
            addFolderButton.Location = new Point(3, 3);
            addFolderButton.Name = "addFolderButton";
            addFolderButton.Size = new Size(99, 25);
            addFolderButton.TabIndex = 1;
            addFolderButton.Text = "Adicionar pasta";
            addFolderButton.UseVisualStyleBackColor = true;

            removeFolderButton.AutoSize = true;
            removeFolderButton.Location = new Point(108, 3);
            removeFolderButton.Name = "removeFolderButton";
            removeFolderButton.Size = new Size(111, 25);
            removeFolderButton.TabIndex = 2;
            removeFolderButton.Text = "Remover pasta";
            removeFolderButton.UseVisualStyleBackColor = true;

            previewSequenceButton.AutoSize = true;
            previewSequenceButton.Location = new Point(225, 3);
            previewSequenceButton.Name = "previewSequenceButton";
            previewSequenceButton.Size = new Size(75, 25);
            previewSequenceButton.TabIndex = 3;
            previewSequenceButton.Text = "Previa";
            previewSequenceButton.UseVisualStyleBackColor = true;

            renderButton.AutoSize = true;
            renderButton.Location = new Point(306, 3);
            renderButton.Name = "renderButton";
            renderButton.Size = new Size(91, 25);
            renderButton.TabIndex = 4;
            renderButton.Text = "Gerar video";
            renderButton.UseVisualStyleBackColor = true;

            imageDurationInput.DecimalPlaces = 1;
            imageDurationInput.Dock = DockStyle.Fill;
            imageDurationInput.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            imageDurationInput.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            imageDurationInput.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            imageDurationInput.Name = "imageDurationInput";
            imageDurationInput.TabIndex = 5;
            imageDurationInput.Value = new decimal(new int[] { 4, 0, 0, 0 });

            transitionDurationInput.DecimalPlaces = 1;
            transitionDurationInput.Dock = DockStyle.Fill;
            transitionDurationInput.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            transitionDurationInput.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            transitionDurationInput.Name = "transitionDurationInput";
            transitionDurationInput.TabIndex = 6;
            transitionDurationInput.Value = new decimal(new int[] { 5, 0, 0, 65536 });

            widthInput.Dock = DockStyle.Fill;
            widthInput.Maximum = new decimal(new int[] { 7680, 0, 0, 0 });
            widthInput.Minimum = new decimal(new int[] { 320, 0, 0, 0 });
            widthInput.Name = "widthInput";
            widthInput.TabIndex = 7;
            widthInput.Value = new decimal(new int[] { 1920, 0, 0, 0 });

            heightInput.Dock = DockStyle.Fill;
            heightInput.Maximum = new decimal(new int[] { 4320, 0, 0, 0 });
            heightInput.Minimum = new decimal(new int[] { 240, 0, 0, 0 });
            heightInput.Name = "heightInput";
            heightInput.TabIndex = 8;
            heightInput.Value = new decimal(new int[] { 1080, 0, 0, 0 });

            outputPathTextBox.Dock = DockStyle.Fill;
            outputPathTextBox.Name = "outputPathTextBox";
            outputPathTextBox.TabIndex = 9;

            browseOutputButton.Dock = DockStyle.Fill;
            browseOutputButton.Name = "browseOutputButton";
            browseOutputButton.TabIndex = 10;
            browseOutputButton.Text = "Escolher...";
            browseOutputButton.UseVisualStyleBackColor = true;

            ffmpegPathTextBox.Dock = DockStyle.Fill;
            ffmpegPathTextBox.Name = "ffmpegPathTextBox";
            ffmpegPathTextBox.TabIndex = 11;
            ffmpegPathTextBox.Text = "ffmpeg";

            browseFFmpegButton.Dock = DockStyle.Fill;
            browseFFmpegButton.Name = "browseFFmpegButton";
            browseFFmpegButton.TabIndex = 12;
            browseFFmpegButton.Text = "Escolher...";
            browseFFmpegButton.UseVisualStyleBackColor = true;

            logTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logTextBox.Location = new Point(12, 484);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.ScrollBars = ScrollBars.Vertical;
            logTextBox.Size = new Size(860, 65);
            logTextBox.TabIndex = 13;

            optionsPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            optionsPanel.ColumnCount = 5;
            optionsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            optionsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            optionsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            optionsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            optionsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            optionsPanel.Controls.Add(imageDurationLabel, 0, 0);
            optionsPanel.Controls.Add(imageDurationInput, 1, 0);
            optionsPanel.Controls.Add(transitionDurationLabel, 2, 0);
            optionsPanel.Controls.Add(transitionDurationInput, 3, 0);
            optionsPanel.Controls.Add(widthLabel, 0, 1);
            optionsPanel.Controls.Add(widthInput, 1, 1);
            optionsPanel.Controls.Add(heightLabel, 2, 1);
            optionsPanel.Controls.Add(heightInput, 3, 1);
            optionsPanel.Controls.Add(repeatImagesCheckBox, 0, 2);
            optionsPanel.SetColumnSpan(repeatImagesCheckBox, 2);
            optionsPanel.Controls.Add(shuffleImagesCheckBox, 2, 2);
            optionsPanel.SetColumnSpan(shuffleImagesCheckBox, 2);
            optionsPanel.Controls.Add(outputPathLabel, 0, 3);
            optionsPanel.Controls.Add(outputPathTextBox, 1, 3);
            optionsPanel.SetColumnSpan(outputPathTextBox, 3);
            optionsPanel.Controls.Add(browseOutputButton, 4, 3);
            optionsPanel.Controls.Add(ffmpegPathLabel, 0, 4);
            optionsPanel.Controls.Add(ffmpegPathTextBox, 1, 4);
            optionsPanel.SetColumnSpan(ffmpegPathTextBox, 3);
            optionsPanel.Controls.Add(browseFFmpegButton, 4, 4);
            optionsPanel.Controls.Add(showTechnicalLogCheckBox, 0, 5);
            optionsPanel.SetColumnSpan(showTechnicalLogCheckBox, 2);
            optionsPanel.Controls.Add(resolutionPresetPanel, 2, 5);
            optionsPanel.SetColumnSpan(resolutionPresetPanel, 3);
            optionsPanel.Location = new Point(12, 256);
            optionsPanel.Name = "optionsPanel";
            optionsPanel.RowCount = 6;
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.Size = new Size(860, 174);
            optionsPanel.TabIndex = 14;

            ConfigureLabel(imageDurationLabel, "Duracao da foto (s)");
            ConfigureLabel(transitionDurationLabel, "Intervalo/Fade (s)");
            ConfigureLabel(widthLabel, "Largura");
            ConfigureLabel(heightLabel, "Altura");
            ConfigureLabel(outputPathLabel, "Arquivo final");
            ConfigureLabel(ffmpegPathLabel, "FFmpeg");

            repeatImagesCheckBox.AutoSize = true;
            repeatImagesCheckBox.Dock = DockStyle.Fill;
            repeatImagesCheckBox.Name = "repeatImagesCheckBox";
            repeatImagesCheckBox.TabIndex = 17;
            repeatImagesCheckBox.Text = "Repetir fotos se uma pasta acabar";
            repeatImagesCheckBox.UseVisualStyleBackColor = true;

            shuffleImagesCheckBox.AutoSize = true;
            shuffleImagesCheckBox.Checked = true;
            shuffleImagesCheckBox.CheckState = CheckState.Checked;
            shuffleImagesCheckBox.Dock = DockStyle.Fill;
            shuffleImagesCheckBox.Name = "shuffleImagesCheckBox";
            shuffleImagesCheckBox.TabIndex = 20;
            shuffleImagesCheckBox.Text = "Embaralhar fotos";
            shuffleImagesCheckBox.UseVisualStyleBackColor = true;

            showTechnicalLogCheckBox.AutoSize = true;
            showTechnicalLogCheckBox.Dock = DockStyle.Fill;
            showTechnicalLogCheckBox.Name = "showTechnicalLogCheckBox";
            showTechnicalLogCheckBox.TabIndex = 18;
            showTechnicalLogCheckBox.Text = "Log tecnico";
            showTechnicalLogCheckBox.UseVisualStyleBackColor = true;

            resolutionPresetPanel.AutoSize = true;
            resolutionPresetPanel.Controls.Add(monitorResolutionRadioButton);
            resolutionPresetPanel.Controls.Add(phoneResolutionRadioButton);
            resolutionPresetPanel.Dock = DockStyle.Fill;
            resolutionPresetPanel.Name = "resolutionPresetPanel";
            resolutionPresetPanel.TabIndex = 21;

            monitorResolutionRadioButton.AutoSize = true;
            monitorResolutionRadioButton.Checked = true;
            monitorResolutionRadioButton.Name = "monitorResolutionRadioButton";
            monitorResolutionRadioButton.Size = new Size(139, 19);
            monitorResolutionRadioButton.TabIndex = 22;
            monitorResolutionRadioButton.TabStop = true;
            monitorResolutionRadioButton.Text = "Monitor 1920x1080";
            monitorResolutionRadioButton.UseVisualStyleBackColor = true;

            phoneResolutionRadioButton.AutoSize = true;
            phoneResolutionRadioButton.Name = "phoneResolutionRadioButton";
            phoneResolutionRadioButton.Size = new Size(133, 19);
            phoneResolutionRadioButton.TabIndex = 23;
            phoneResolutionRadioButton.Text = "Telefone 1080x1920";
            phoneResolutionRadioButton.UseVisualStyleBackColor = true;

            buttonsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonsPanel.Controls.Add(addFolderButton);
            buttonsPanel.Controls.Add(removeFolderButton);
            buttonsPanel.Controls.Add(previewSequenceButton);
            buttonsPanel.Controls.Add(renderButton);
            buttonsPanel.Location = new Point(12, 12);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new Size(860, 36);
            buttonsPanel.TabIndex = 15;

            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            statusLabel.Location = new Point(12, 552);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(860, 23);
            statusLabel.TabIndex = 16;
            statusLabel.Text = "Ready";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;

            renderProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            renderProgressBar.Location = new Point(12, 454);
            renderProgressBar.Name = "renderProgressBar";
            renderProgressBar.Size = new Size(860, 23);
            renderProgressBar.TabIndex = 19;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 581);
            Controls.Add(statusLabel);
            Controls.Add(renderProgressBar);
            Controls.Add(buttonsPanel);
            Controls.Add(optionsPanel);
            Controls.Add(logTextBox);
            Controls.Add(folderGrid);
            MinimumSize = new Size(900, 620);
            Name = "Form1";
            Text = "Wedding Slideshow Video Maker";
            ((System.ComponentModel.ISupportInitialize)folderGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageDurationInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)transitionDurationInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)widthInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)heightInput).EndInit();
            optionsPanel.ResumeLayout(false);
            optionsPanel.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void ConfigureLabel(Label label, string text)
        {
            label.Dock = DockStyle.Fill;
            label.Text = text;
            label.TextAlign = ContentAlignment.MiddleLeft;
        }

        private DataGridView folderGrid;
        private Button addFolderButton;
        private Button removeFolderButton;
        private Button previewSequenceButton;
        private Button renderButton;
        private NumericUpDown imageDurationInput;
        private NumericUpDown transitionDurationInput;
        private NumericUpDown widthInput;
        private NumericUpDown heightInput;
        private TextBox outputPathTextBox;
        private Button browseOutputButton;
        private TextBox ffmpegPathTextBox;
        private Button browseFFmpegButton;
        private TextBox logTextBox;
        private TableLayoutPanel optionsPanel;
        private Label imageDurationLabel;
        private Label transitionDurationLabel;
        private Label widthLabel;
        private Label heightLabel;
        private Label outputPathLabel;
        private Label ffmpegPathLabel;
        private FlowLayoutPanel buttonsPanel;
        private Label statusLabel;
        private CheckBox repeatImagesCheckBox;
        private CheckBox showTechnicalLogCheckBox;
        private ProgressBar renderProgressBar;
        private CheckBox shuffleImagesCheckBox;
        private FlowLayoutPanel resolutionPresetPanel;
        private RadioButton monitorResolutionRadioButton;
        private RadioButton phoneResolutionRadioButton;
    }
}
