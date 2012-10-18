namespace DumbSearch
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uxRootFolderLabel = new System.Windows.Forms.Label();
            this.uxRootFolderTextBox = new System.Windows.Forms.TextBox();
            this.uxSearchTermLabel = new System.Windows.Forms.Label();
            this.uxSearchTermTextBox = new System.Windows.Forms.TextBox();
            this.uxFileCountDisplayLabel = new System.Windows.Forms.Label();
            this.uxFileCounLabel = new System.Windows.Forms.Label();
            this.uxSearchButton = new System.Windows.Forms.Button();
            this.uxPauseButton = new System.Windows.Forms.Button();
            this.uxStopButton = new System.Windows.Forms.Button();
            this.uxSearchBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.uxFilesFoundDisplayLabel = new System.Windows.Forms.Label();
            this.uxFileMatchLabel = new System.Windows.Forms.Label();
            this.uxFilesFoundListBox = new System.Windows.Forms.ListBox();
            this.uxSelectedFileTextBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uxWhatsHappenningLabel = new System.Windows.Forms.Label();
            this.uxExceptionsTextbox = new System.Windows.Forms.TextBox();
            this.uxClearLinkLabel = new System.Windows.Forms.LinkLabel();
            this.uxFilePatternLabel = new System.Windows.Forms.Label();
            this.uxFilePatternTextbox = new System.Windows.Forms.TextBox();
            this.uxClipboardLinkLabel = new System.Windows.Forms.LinkLabel();
            this.uxOveralProgressLabel = new System.Windows.Forms.Label();
            this.uxOveralProgressBar = new System.Windows.Forms.ProgressBar();
            this.uxFolderDiscoveryLabel = new System.Windows.Forms.Label();
            this.uxFolderDiscoveryProgressBar = new System.Windows.Forms.ProgressBar();
            this.uxLabelDiscoveryLabel = new System.Windows.Forms.Label();
            this.uxFileDiscoveryProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // uxRootFolderLabel
            // 
            this.uxRootFolderLabel.AutoSize = true;
            this.uxRootFolderLabel.Location = new System.Drawing.Point(7, 6);
            this.uxRootFolderLabel.Name = "uxRootFolderLabel";
            this.uxRootFolderLabel.Size = new System.Drawing.Size(62, 13);
            this.uxRootFolderLabel.TabIndex = 0;
            this.uxRootFolderLabel.Text = "Root Folder";
            // 
            // uxRootFolderTextBox
            // 
            this.uxRootFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxRootFolderTextBox.Location = new System.Drawing.Point(77, 3);
            this.uxRootFolderTextBox.Name = "uxRootFolderTextBox";
            this.uxRootFolderTextBox.Size = new System.Drawing.Size(294, 20);
            this.uxRootFolderTextBox.TabIndex = 1;
            this.uxRootFolderTextBox.TextChanged += new System.EventHandler(this.uxRootFolderTextBox_TextChanged);
            // 
            // uxSearchTermLabel
            // 
            this.uxSearchTermLabel.Location = new System.Drawing.Point(7, 58);
            this.uxSearchTermLabel.Name = "uxSearchTermLabel";
            this.uxSearchTermLabel.Size = new System.Drawing.Size(68, 31);
            this.uxSearchTermLabel.TabIndex = 4;
            this.uxSearchTermLabel.Text = "Search Term (Regex)";
            // 
            // uxSearchTermTextBox
            // 
            this.uxSearchTermTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxSearchTermTextBox.Location = new System.Drawing.Point(77, 55);
            this.uxSearchTermTextBox.Name = "uxSearchTermTextBox";
            this.uxSearchTermTextBox.Size = new System.Drawing.Size(294, 20);
            this.uxSearchTermTextBox.TabIndex = 5;
            this.uxSearchTermTextBox.TextChanged += new System.EventHandler(this.uxSearchTermTextBox_TextChanged);
            // 
            // uxFileCountDisplayLabel
            // 
            this.uxFileCountDisplayLabel.AutoSize = true;
            this.uxFileCountDisplayLabel.Location = new System.Drawing.Point(12, 201);
            this.uxFileCountDisplayLabel.Name = "uxFileCountDisplayLabel";
            this.uxFileCountDisplayLabel.Size = new System.Drawing.Size(95, 13);
            this.uxFileCountDisplayLabel.TabIndex = 11;
            this.uxFileCountDisplayLabel.Text = "Filename matches:";
            // 
            // uxFileCounLabel
            // 
            this.uxFileCounLabel.AutoSize = true;
            this.uxFileCounLabel.Location = new System.Drawing.Point(106, 201);
            this.uxFileCounLabel.Name = "uxFileCounLabel";
            this.uxFileCounLabel.Size = new System.Drawing.Size(13, 13);
            this.uxFileCounLabel.TabIndex = 12;
            this.uxFileCounLabel.Text = "0";
            // 
            // uxSearchButton
            // 
            this.uxSearchButton.Location = new System.Drawing.Point(79, 81);
            this.uxSearchButton.Name = "uxSearchButton";
            this.uxSearchButton.Size = new System.Drawing.Size(75, 23);
            this.uxSearchButton.TabIndex = 6;
            this.uxSearchButton.Text = "Search";
            this.uxSearchButton.UseVisualStyleBackColor = true;
            this.uxSearchButton.Click += new System.EventHandler(this.uxSearchButton_Click);
            // 
            // uxPauseButton
            // 
            this.uxPauseButton.Location = new System.Drawing.Point(160, 81);
            this.uxPauseButton.Name = "uxPauseButton";
            this.uxPauseButton.Size = new System.Drawing.Size(75, 23);
            this.uxPauseButton.TabIndex = 7;
            this.uxPauseButton.Text = "Pause";
            this.uxPauseButton.UseVisualStyleBackColor = true;
            this.uxPauseButton.Click += new System.EventHandler(this.uxPauseButton_Click);
            // 
            // uxStopButton
            // 
            this.uxStopButton.Location = new System.Drawing.Point(241, 81);
            this.uxStopButton.Name = "uxStopButton";
            this.uxStopButton.Size = new System.Drawing.Size(75, 23);
            this.uxStopButton.TabIndex = 8;
            this.uxStopButton.Text = "Stop";
            this.uxStopButton.UseVisualStyleBackColor = true;
            this.uxStopButton.Click += new System.EventHandler(this.uxStopButton_Click);
            // 
            // uxSearchBackgroundWorker
            // 
            this.uxSearchBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uxSearchBackgroundWorker_DoWork);
            this.uxSearchBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.uxSearchBackgroundWorker_RunWorkerCompleted);
            // 
            // uxFilesFoundDisplayLabel
            // 
            this.uxFilesFoundDisplayLabel.AutoSize = true;
            this.uxFilesFoundDisplayLabel.Location = new System.Drawing.Point(138, 201);
            this.uxFilesFoundDisplayLabel.Name = "uxFilesFoundDisplayLabel";
            this.uxFilesFoundDisplayLabel.Size = new System.Drawing.Size(90, 13);
            this.uxFilesFoundDisplayLabel.TabIndex = 13;
            this.uxFilesFoundDisplayLabel.Text = "Content matches:";
            // 
            // uxFileMatchLabel
            // 
            this.uxFileMatchLabel.AutoSize = true;
            this.uxFileMatchLabel.Location = new System.Drawing.Point(234, 201);
            this.uxFileMatchLabel.Name = "uxFileMatchLabel";
            this.uxFileMatchLabel.Size = new System.Drawing.Size(13, 13);
            this.uxFileMatchLabel.TabIndex = 14;
            this.uxFileMatchLabel.Text = "0";
            // 
            // uxFilesFoundListBox
            // 
            this.uxFilesFoundListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxFilesFoundListBox.DisplayMember = "Fullname";
            this.uxFilesFoundListBox.FormattingEnabled = true;
            this.uxFilesFoundListBox.Location = new System.Drawing.Point(10, 219);
            this.uxFilesFoundListBox.Name = "uxFilesFoundListBox";
            this.uxFilesFoundListBox.Size = new System.Drawing.Size(357, 69);
            this.uxFilesFoundListBox.TabIndex = 16;
            this.uxFilesFoundListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.uxFilesFoundListBox_MouseDoubleClick);
            this.uxFilesFoundListBox.SelectedIndexChanged += new System.EventHandler(this.uxFilesFoundListBox_SelectedIndexChanged);
            // 
            // uxSelectedFileTextBox
            // 
            this.uxSelectedFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxSelectedFileTextBox.Location = new System.Drawing.Point(10, 322);
            this.uxSelectedFileTextBox.Name = "uxSelectedFileTextBox";
            this.uxSelectedFileTextBox.Size = new System.Drawing.Size(357, 20);
            this.uxSelectedFileTextBox.TabIndex = 17;
            // 
            // timer1
            // 
            this.timer1.Interval = 123;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // uxWhatsHappenningLabel
            // 
            this.uxWhatsHappenningLabel.AutoSize = true;
            this.uxWhatsHappenningLabel.Location = new System.Drawing.Point(12, 182);
            this.uxWhatsHappenningLabel.Name = "uxWhatsHappenningLabel";
            this.uxWhatsHappenningLabel.Size = new System.Drawing.Size(107, 13);
            this.uxWhatsHappenningLabel.TabIndex = 9;
            this.uxWhatsHappenningLabel.Text = "What\'s Happenning?";
            // 
            // uxExceptionsTextbox
            // 
            this.uxExceptionsTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxExceptionsTextbox.Location = new System.Drawing.Point(10, 348);
            this.uxExceptionsTextbox.Multiline = true;
            this.uxExceptionsTextbox.Name = "uxExceptionsTextbox";
            this.uxExceptionsTextbox.Size = new System.Drawing.Size(357, 51);
            this.uxExceptionsTextbox.TabIndex = 18;
            // 
            // uxClearLinkLabel
            // 
            this.uxClearLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxClearLinkLabel.AutoSize = true;
            this.uxClearLinkLabel.Location = new System.Drawing.Point(312, 297);
            this.uxClearLinkLabel.Name = "uxClearLinkLabel";
            this.uxClearLinkLabel.Size = new System.Drawing.Size(31, 13);
            this.uxClearLinkLabel.TabIndex = 15;
            this.uxClearLinkLabel.TabStop = true;
            this.uxClearLinkLabel.Text = "Clear";
            this.uxClearLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.uxClearLinkLabel_LinkClicked);
            // 
            // uxFilePatternLabel
            // 
            this.uxFilePatternLabel.AutoSize = true;
            this.uxFilePatternLabel.Location = new System.Drawing.Point(7, 32);
            this.uxFilePatternLabel.Name = "uxFilePatternLabel";
            this.uxFilePatternLabel.Size = new System.Drawing.Size(60, 13);
            this.uxFilePatternLabel.TabIndex = 2;
            this.uxFilePatternLabel.Text = "File Pattern";
            // 
            // uxFilePatternTextbox
            // 
            this.uxFilePatternTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxFilePatternTextbox.Location = new System.Drawing.Point(77, 29);
            this.uxFilePatternTextbox.Name = "uxFilePatternTextbox";
            this.uxFilePatternTextbox.Size = new System.Drawing.Size(294, 20);
            this.uxFilePatternTextbox.TabIndex = 3;
            this.uxFilePatternTextbox.Text = "*.*";
            this.uxFilePatternTextbox.TextChanged += new System.EventHandler(this.uxFilePatternTextbox_TextChanged);
            // 
            // uxClipboardLinkLabel
            // 
            this.uxClipboardLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxClipboardLinkLabel.AutoSize = true;
            this.uxClipboardLinkLabel.Location = new System.Drawing.Point(241, 297);
            this.uxClipboardLinkLabel.Name = "uxClipboardLinkLabel";
            this.uxClipboardLinkLabel.Size = new System.Drawing.Size(51, 13);
            this.uxClipboardLinkLabel.TabIndex = 19;
            this.uxClipboardLinkLabel.TabStop = true;
            this.uxClipboardLinkLabel.Text = "Clipboard";
            this.uxClipboardLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.uxClipboardLinkLabel_LinkClicked);
            // 
            // uxOveralProgressLabel
            // 
            this.uxOveralProgressLabel.AutoSize = true;
            this.uxOveralProgressLabel.Location = new System.Drawing.Point(8, 116);
            this.uxOveralProgressLabel.Name = "uxOveralProgressLabel";
            this.uxOveralProgressLabel.Size = new System.Drawing.Size(82, 13);
            this.uxOveralProgressLabel.TabIndex = 20;
            this.uxOveralProgressLabel.Text = "Overal Progress";
            // 
            // uxOveralProgressBar
            // 
            this.uxOveralProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxOveralProgressBar.Location = new System.Drawing.Point(99, 115);
            this.uxOveralProgressBar.MarqueeAnimationSpeed = 0;
            this.uxOveralProgressBar.Maximum = 4;
            this.uxOveralProgressBar.Name = "uxOveralProgressBar";
            this.uxOveralProgressBar.Size = new System.Drawing.Size(268, 14);
            this.uxOveralProgressBar.TabIndex = 21;
            // 
            // uxFolderDiscoveryLabel
            // 
            this.uxFolderDiscoveryLabel.AutoSize = true;
            this.uxFolderDiscoveryLabel.Location = new System.Drawing.Point(8, 136);
            this.uxFolderDiscoveryLabel.Name = "uxFolderDiscoveryLabel";
            this.uxFolderDiscoveryLabel.Size = new System.Drawing.Size(86, 13);
            this.uxFolderDiscoveryLabel.TabIndex = 20;
            this.uxFolderDiscoveryLabel.Text = "Folder Discovery";
            // 
            // uxFolderDiscoveryProgressBar
            // 
            this.uxFolderDiscoveryProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxFolderDiscoveryProgressBar.Location = new System.Drawing.Point(99, 135);
            this.uxFolderDiscoveryProgressBar.MarqueeAnimationSpeed = 0;
            this.uxFolderDiscoveryProgressBar.Name = "uxFolderDiscoveryProgressBar";
            this.uxFolderDiscoveryProgressBar.Size = new System.Drawing.Size(268, 14);
            this.uxFolderDiscoveryProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.uxFolderDiscoveryProgressBar.TabIndex = 21;
            // 
            // uxLabelDiscoveryLabel
            // 
            this.uxLabelDiscoveryLabel.AutoSize = true;
            this.uxLabelDiscoveryLabel.Location = new System.Drawing.Point(8, 156);
            this.uxLabelDiscoveryLabel.Name = "uxLabelDiscoveryLabel";
            this.uxLabelDiscoveryLabel.Size = new System.Drawing.Size(73, 13);
            this.uxLabelDiscoveryLabel.TabIndex = 20;
            this.uxLabelDiscoveryLabel.Text = "File Discovery";
            // 
            // uxFileDiscoveryProgressBar
            // 
            this.uxFileDiscoveryProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxFileDiscoveryProgressBar.Location = new System.Drawing.Point(99, 155);
            this.uxFileDiscoveryProgressBar.MarqueeAnimationSpeed = 0;
            this.uxFileDiscoveryProgressBar.Name = "uxFileDiscoveryProgressBar";
            this.uxFileDiscoveryProgressBar.Size = new System.Drawing.Size(268, 14);
            this.uxFileDiscoveryProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.uxFileDiscoveryProgressBar.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 400);
            this.Controls.Add(this.uxFileDiscoveryProgressBar);
            this.Controls.Add(this.uxLabelDiscoveryLabel);
            this.Controls.Add(this.uxFolderDiscoveryProgressBar);
            this.Controls.Add(this.uxFolderDiscoveryLabel);
            this.Controls.Add(this.uxOveralProgressBar);
            this.Controls.Add(this.uxOveralProgressLabel);
            this.Controls.Add(this.uxClipboardLinkLabel);
            this.Controls.Add(this.uxClearLinkLabel);
            this.Controls.Add(this.uxExceptionsTextbox);
            this.Controls.Add(this.uxWhatsHappenningLabel);
            this.Controls.Add(this.uxSelectedFileTextBox);
            this.Controls.Add(this.uxFilesFoundListBox);
            this.Controls.Add(this.uxStopButton);
            this.Controls.Add(this.uxPauseButton);
            this.Controls.Add(this.uxSearchButton);
            this.Controls.Add(this.uxFileMatchLabel);
            this.Controls.Add(this.uxFileCounLabel);
            this.Controls.Add(this.uxFilesFoundDisplayLabel);
            this.Controls.Add(this.uxFileCountDisplayLabel);
            this.Controls.Add(this.uxFilePatternTextbox);
            this.Controls.Add(this.uxFilePatternLabel);
            this.Controls.Add(this.uxSearchTermTextBox);
            this.Controls.Add(this.uxSearchTermLabel);
            this.Controls.Add(this.uxRootFolderTextBox);
            this.Controls.Add(this.uxRootFolderLabel);
            this.MinimumSize = new System.Drawing.Size(344, 403);
            this.Name = "MainForm";
            this.Text = "DumbSearch -  by Patware";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label uxRootFolderLabel;
        private System.Windows.Forms.TextBox uxRootFolderTextBox;
        private System.Windows.Forms.Label uxSearchTermLabel;
        private System.Windows.Forms.TextBox uxSearchTermTextBox;
        private System.Windows.Forms.Label uxFileCountDisplayLabel;
        private System.Windows.Forms.Label uxFileCounLabel;
        private System.Windows.Forms.Button uxSearchButton;
        private System.Windows.Forms.Button uxPauseButton;
        private System.Windows.Forms.Button uxStopButton;
        private System.ComponentModel.BackgroundWorker uxSearchBackgroundWorker;
        private System.Windows.Forms.Label uxFilesFoundDisplayLabel;
        private System.Windows.Forms.Label uxFileMatchLabel;
        private System.Windows.Forms.ListBox uxFilesFoundListBox;
        private System.Windows.Forms.TextBox uxSelectedFileTextBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label uxWhatsHappenningLabel;
        private System.Windows.Forms.TextBox uxExceptionsTextbox;
        private System.Windows.Forms.LinkLabel uxClearLinkLabel;
        private System.Windows.Forms.Label uxFilePatternLabel;
        private System.Windows.Forms.TextBox uxFilePatternTextbox;
        private System.Windows.Forms.LinkLabel uxClipboardLinkLabel;
        private System.Windows.Forms.Label uxOveralProgressLabel;
        private System.Windows.Forms.ProgressBar uxOveralProgressBar;
        private System.Windows.Forms.Label uxFolderDiscoveryLabel;
        private System.Windows.Forms.ProgressBar uxFolderDiscoveryProgressBar;
        private System.Windows.Forms.Label uxLabelDiscoveryLabel;
        private System.Windows.Forms.ProgressBar uxFileDiscoveryProgressBar;
    }
}

