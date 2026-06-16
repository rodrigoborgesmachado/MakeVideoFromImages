namespace MakeVideoFromImages
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            folderGrid = new DataGridView();
            addFolderButton = new Button();
            removeFolderButton = new Button();
            previewSequenceButton = new Button();
            renderButton = new Button();
            aboutButton = new Button();
            imageDurationInput = new NumericUpDown();
            transitionDurationInput = new NumericUpDown();
            widthInput = new NumericUpDown();
            heightInput = new NumericUpDown();
            outputPathTextBox = new TextBox();
            browseOutputButton = new Button();
            logTextBox = new TextBox();
            optionsPanel = new TableLayoutPanel();
            imageDurationLabel = new Label();
            transitionDurationLabel = new Label();
            widthLabel = new Label();
            heightLabel = new Label();
            repeatImagesCheckBox = new CheckBox();
            shuffleImagesCheckBox = new CheckBox();
            outputPathLabel = new Label();
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
            resolutionPresetPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // folderGrid
            // 
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
            // 
            // addFolderButton
            // 
            addFolderButton.AutoSize = true;
            addFolderButton.Location = new Point(3, 3);
            addFolderButton.Name = "addFolderButton";
            addFolderButton.Size = new Size(99, 25);
            addFolderButton.TabIndex = 1;
            addFolderButton.Text = "Adicionar pasta";
            addFolderButton.UseVisualStyleBackColor = true;
            // 
            // removeFolderButton
            // 
            removeFolderButton.AutoSize = true;
            removeFolderButton.Location = new Point(108, 3);
            removeFolderButton.Name = "removeFolderButton";
            removeFolderButton.Size = new Size(111, 25);
            removeFolderButton.TabIndex = 2;
            removeFolderButton.Text = "Remover pasta";
            removeFolderButton.UseVisualStyleBackColor = true;
            // 
            // previewSequenceButton
            // 
            previewSequenceButton.AutoSize = true;
            previewSequenceButton.Location = new Point(225, 3);
            previewSequenceButton.Name = "previewSequenceButton";
            previewSequenceButton.Size = new Size(75, 25);
            previewSequenceButton.TabIndex = 3;
            previewSequenceButton.Text = "Previa";
            previewSequenceButton.UseVisualStyleBackColor = true;
            // 
            // renderButton
            // 
            renderButton.AutoSize = true;
            renderButton.Location = new Point(306, 3);
            renderButton.Name = "renderButton";
            renderButton.Size = new Size(91, 25);
            renderButton.TabIndex = 4;
            renderButton.Text = "Gerar video";
            renderButton.UseVisualStyleBackColor = true;

            aboutButton.AutoSize = true;
            aboutButton.Location = new Point(403, 3);
            aboutButton.Name = "aboutButton";
            aboutButton.Size = new Size(60, 25);
            aboutButton.TabIndex = 5;
            aboutButton.Text = "Sobre";
            aboutButton.UseVisualStyleBackColor = true;
            // 
            // imageDurationInput
            // 
            imageDurationInput.DecimalPlaces = 1;
            imageDurationInput.Dock = DockStyle.Fill;
            imageDurationInput.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            imageDurationInput.Location = new Point(163, 3);
            imageDurationInput.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            imageDurationInput.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            imageDurationInput.Name = "imageDurationInput";
            imageDurationInput.Size = new Size(214, 23);
            imageDurationInput.TabIndex = 5;
            imageDurationInput.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // transitionDurationInput
            // 
            transitionDurationInput.DecimalPlaces = 1;
            transitionDurationInput.Dock = DockStyle.Fill;
            transitionDurationInput.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            transitionDurationInput.Location = new Point(543, 3);
            transitionDurationInput.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            transitionDurationInput.Name = "transitionDurationInput";
            transitionDurationInput.Size = new Size(214, 23);
            transitionDurationInput.TabIndex = 6;
            transitionDurationInput.Value = new decimal(new int[] { 5, 0, 0, 65536 });
            // 
            // widthInput
            // 
            widthInput.Dock = DockStyle.Fill;
            widthInput.Location = new Point(163, 32);
            widthInput.Maximum = new decimal(new int[] { 7680, 0, 0, 0 });
            widthInput.Minimum = new decimal(new int[] { 320, 0, 0, 0 });
            widthInput.Name = "widthInput";
            widthInput.Size = new Size(214, 23);
            widthInput.TabIndex = 7;
            widthInput.Value = new decimal(new int[] { 1920, 0, 0, 0 });
            // 
            // heightInput
            // 
            heightInput.Dock = DockStyle.Fill;
            heightInput.Location = new Point(543, 32);
            heightInput.Maximum = new decimal(new int[] { 4320, 0, 0, 0 });
            heightInput.Minimum = new decimal(new int[] { 240, 0, 0, 0 });
            heightInput.Name = "heightInput";
            heightInput.Size = new Size(214, 23);
            heightInput.TabIndex = 8;
            heightInput.Value = new decimal(new int[] { 1080, 0, 0, 0 });
            // 
            // outputPathTextBox
            // 
            optionsPanel.SetColumnSpan(outputPathTextBox, 3);
            outputPathTextBox.Dock = DockStyle.Fill;
            outputPathTextBox.Location = new Point(163, 90);
            outputPathTextBox.Name = "outputPathTextBox";
            outputPathTextBox.Size = new Size(594, 23);
            outputPathTextBox.TabIndex = 9;
            // 
            // browseOutputButton
            // 
            browseOutputButton.Dock = DockStyle.Fill;
            browseOutputButton.Location = new Point(763, 90);
            browseOutputButton.Name = "browseOutputButton";
            browseOutputButton.Size = new Size(94, 23);
            browseOutputButton.TabIndex = 10;
            browseOutputButton.Text = "Escolher...";
            browseOutputButton.UseVisualStyleBackColor = true;
            // 
            // logTextBox
            // 
            logTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logTextBox.Location = new Point(12, 455);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.ScrollBars = ScrollBars.Vertical;
            logTextBox.Size = new Size(860, 94);
            logTextBox.TabIndex = 13;
            // 
            // optionsPanel
            // 
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
            optionsPanel.Controls.Add(shuffleImagesCheckBox, 2, 2);
            optionsPanel.Controls.Add(outputPathLabel, 0, 3);
            optionsPanel.Controls.Add(outputPathTextBox, 1, 3);
            optionsPanel.Controls.Add(browseOutputButton, 4, 3);
            optionsPanel.Controls.Add(showTechnicalLogCheckBox, 0, 4);
            optionsPanel.Controls.Add(resolutionPresetPanel, 2, 4);
            optionsPanel.Location = new Point(12, 256);
            optionsPanel.Name = "optionsPanel";
            optionsPanel.RowCount = 5;
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            optionsPanel.Size = new Size(860, 145);
            optionsPanel.TabIndex = 14;
            // 
            // imageDurationLabel
            // 
            imageDurationLabel.Dock = DockStyle.Fill;
            imageDurationLabel.Location = new Point(3, 0);
            imageDurationLabel.Name = "imageDurationLabel";
            imageDurationLabel.Size = new Size(154, 29);
            imageDurationLabel.TabIndex = 0;
            imageDurationLabel.Text = "Duracao da foto (s)";
            imageDurationLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // transitionDurationLabel
            // 
            transitionDurationLabel.Dock = DockStyle.Fill;
            transitionDurationLabel.Location = new Point(383, 0);
            transitionDurationLabel.Name = "transitionDurationLabel";
            transitionDurationLabel.Size = new Size(154, 29);
            transitionDurationLabel.TabIndex = 6;
            transitionDurationLabel.Text = "Intervalo/Fade (s)";
            transitionDurationLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // widthLabel
            // 
            widthLabel.Dock = DockStyle.Fill;
            widthLabel.Location = new Point(3, 29);
            widthLabel.Name = "widthLabel";
            widthLabel.Size = new Size(154, 29);
            widthLabel.TabIndex = 7;
            widthLabel.Text = "Largura";
            widthLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // heightLabel
            // 
            heightLabel.Dock = DockStyle.Fill;
            heightLabel.Location = new Point(383, 29);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(154, 29);
            heightLabel.TabIndex = 8;
            heightLabel.Text = "Altura";
            heightLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // repeatImagesCheckBox
            // 
            repeatImagesCheckBox.AutoSize = true;
            optionsPanel.SetColumnSpan(repeatImagesCheckBox, 2);
            repeatImagesCheckBox.Dock = DockStyle.Fill;
            repeatImagesCheckBox.Location = new Point(3, 61);
            repeatImagesCheckBox.Name = "repeatImagesCheckBox";
            repeatImagesCheckBox.Size = new Size(374, 23);
            repeatImagesCheckBox.TabIndex = 17;
            repeatImagesCheckBox.Text = "Repetir fotos se uma pasta acabar";
            repeatImagesCheckBox.UseVisualStyleBackColor = true;
            // 
            // shuffleImagesCheckBox
            // 
            shuffleImagesCheckBox.AutoSize = true;
            shuffleImagesCheckBox.Checked = true;
            shuffleImagesCheckBox.CheckState = CheckState.Checked;
            optionsPanel.SetColumnSpan(shuffleImagesCheckBox, 2);
            shuffleImagesCheckBox.Dock = DockStyle.Fill;
            shuffleImagesCheckBox.Location = new Point(383, 61);
            shuffleImagesCheckBox.Name = "shuffleImagesCheckBox";
            shuffleImagesCheckBox.Size = new Size(374, 23);
            shuffleImagesCheckBox.TabIndex = 20;
            shuffleImagesCheckBox.Text = "Embaralhar fotos";
            shuffleImagesCheckBox.UseVisualStyleBackColor = true;
            // 
            // outputPathLabel
            // 
            outputPathLabel.Dock = DockStyle.Fill;
            outputPathLabel.Location = new Point(3, 87);
            outputPathLabel.Name = "outputPathLabel";
            outputPathLabel.Size = new Size(154, 29);
            outputPathLabel.TabIndex = 21;
            outputPathLabel.Text = "Arquivo final";
            outputPathLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // showTechnicalLogCheckBox
            // 
            showTechnicalLogCheckBox.AutoSize = true;
            optionsPanel.SetColumnSpan(showTechnicalLogCheckBox, 2);
            showTechnicalLogCheckBox.Dock = DockStyle.Fill;
            showTechnicalLogCheckBox.Location = new Point(3, 119);
            showTechnicalLogCheckBox.Name = "showTechnicalLogCheckBox";
            showTechnicalLogCheckBox.Size = new Size(374, 23);
            showTechnicalLogCheckBox.TabIndex = 18;
            showTechnicalLogCheckBox.Text = "Log tecnico";
            showTechnicalLogCheckBox.UseVisualStyleBackColor = true;
            // 
            // resolutionPresetPanel
            // 
            resolutionPresetPanel.AutoSize = true;
            optionsPanel.SetColumnSpan(resolutionPresetPanel, 3);
            resolutionPresetPanel.Controls.Add(monitorResolutionRadioButton);
            resolutionPresetPanel.Controls.Add(phoneResolutionRadioButton);
            resolutionPresetPanel.Dock = DockStyle.Fill;
            resolutionPresetPanel.Location = new Point(383, 119);
            resolutionPresetPanel.Name = "resolutionPresetPanel";
            resolutionPresetPanel.Size = new Size(474, 23);
            resolutionPresetPanel.TabIndex = 21;
            // 
            // monitorResolutionRadioButton
            // 
            monitorResolutionRadioButton.AutoSize = true;
            monitorResolutionRadioButton.Checked = true;
            monitorResolutionRadioButton.Location = new Point(3, 3);
            monitorResolutionRadioButton.Name = "monitorResolutionRadioButton";
            monitorResolutionRadioButton.Size = new Size(124, 19);
            monitorResolutionRadioButton.TabIndex = 22;
            monitorResolutionRadioButton.TabStop = true;
            monitorResolutionRadioButton.Text = "Monitor 1920x1080";
            monitorResolutionRadioButton.UseVisualStyleBackColor = true;
            // 
            // phoneResolutionRadioButton
            // 
            phoneResolutionRadioButton.AutoSize = true;
            phoneResolutionRadioButton.Location = new Point(133, 3);
            phoneResolutionRadioButton.Name = "phoneResolutionRadioButton";
            phoneResolutionRadioButton.Size = new Size(126, 19);
            phoneResolutionRadioButton.TabIndex = 23;
            phoneResolutionRadioButton.Text = "Telefone 1080x1920";
            phoneResolutionRadioButton.UseVisualStyleBackColor = true;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonsPanel.Controls.Add(addFolderButton);
            buttonsPanel.Controls.Add(removeFolderButton);
            buttonsPanel.Controls.Add(previewSequenceButton);
            buttonsPanel.Controls.Add(renderButton);
            buttonsPanel.Controls.Add(aboutButton);
            buttonsPanel.Location = new Point(12, 12);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new Size(860, 36);
            buttonsPanel.TabIndex = 15;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            statusLabel.Location = new Point(12, 552);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(860, 23);
            statusLabel.TabIndex = 16;
            statusLabel.Text = "Ready";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // renderProgressBar
            // 
            renderProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            renderProgressBar.Location = new Point(12, 426);
            renderProgressBar.Name = "renderProgressBar";
            renderProgressBar.Size = new Size(860, 23);
            renderProgressBar.TabIndex = 19;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 581);
            Controls.Add(statusLabel);
            Controls.Add(renderProgressBar);
            Controls.Add(buttonsPanel);
            Controls.Add(optionsPanel);
            Controls.Add(logTextBox);
            Controls.Add(folderGrid);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(900, 620);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Make Video From Images";
            ((System.ComponentModel.ISupportInitialize)folderGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageDurationInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)transitionDurationInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)widthInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)heightInput).EndInit();
            optionsPanel.ResumeLayout(false);
            optionsPanel.PerformLayout();
            resolutionPresetPanel.ResumeLayout(false);
            resolutionPresetPanel.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView folderGrid;
        private Button addFolderButton;
            private Button removeFolderButton;
            private Button previewSequenceButton;
            private Button renderButton;
        private Button aboutButton;
        private NumericUpDown imageDurationInput;
        private NumericUpDown transitionDurationInput;
        private NumericUpDown widthInput;
        private NumericUpDown heightInput;
        private TextBox outputPathTextBox;
        private Button browseOutputButton;
        private TextBox logTextBox;
        private TableLayoutPanel optionsPanel;
        private Label imageDurationLabel;
        private Label transitionDurationLabel;
        private Label widthLabel;
        private Label heightLabel;
        private Label outputPathLabel;
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
