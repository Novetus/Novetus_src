partial class AssetSDK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetSDK));
            this.AssetDownloader = new System.Windows.Forms.GroupBox();
            this.AssetDownloader_AssetNameBox = new System.Windows.Forms.TextBox();
            this.AssetDownloader_AssetNameText = new System.Windows.Forms.Label();
            this.AssetDownloader_LoadHelpMessage = new System.Windows.Forms.CheckBox();
            this.AssetDownloader_URLSelection = new System.Windows.Forms.ComboBox();
            this.AssetDownloader_AssetVersionText = new System.Windows.Forms.Label();
            this.AssetDownloader_AssetIDText = new System.Windows.Forms.Label();
            this.AssetDownloader_AssetVersionSelector = new System.Windows.Forms.NumericUpDown();
            this.AssetDownloader_AssetIDBox = new System.Windows.Forms.TextBox();
            this.AssetDownloader_AssetDownloaderButton = new System.Windows.Forms.Button();
            this.AssetLocalization = new System.Windows.Forms.GroupBox();
            this.AssetLocalization_SaveBackups = new System.Windows.Forms.CheckBox();
            this.AssetLocalization_StatusBar = new System.Windows.Forms.ProgressBar();
            this.AssetLocalization_AssetTypeText = new System.Windows.Forms.Label();
            this.AssetLocalization_UsesHatMeshText = new System.Windows.Forms.Label();
            this.AssetLocalization_UsesHatMeshBox = new System.Windows.Forms.ComboBox();
            this.AssetLocalization_ItemNameText = new System.Windows.Forms.Label();
            this.AssetLocalization_ItemNameBox = new System.Windows.Forms.TextBox();
            this.AssetLocalization_StatusText = new System.Windows.Forms.Label();
            this.AssetLocalization_AssetTypeBox = new System.Windows.Forms.ComboBox();
            this.AssetLocalization_LocalizeButton = new System.Windows.Forms.Button();
            this.MeshConverter = new System.Windows.Forms.GroupBox();
            this.MeshConverter_StatusText = new System.Windows.Forms.Label();
            this.MeshConverter_MeshVersionSelector = new System.Windows.Forms.NumericUpDown();
            this.MeshConverter_MeshVersionText = new System.Windows.Forms.Label();
            this.MeshConverter_CreditText = new System.Windows.Forms.Label();
            this.MeshConverter_ConvertButton = new System.Windows.Forms.Button();
            this.AssetLocalization_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.AssetDownloaderBatch = new System.Windows.Forms.GroupBox();
            this.AssetDownloaderBatch_BatchIDBox = new System.Windows.Forms.TextBox();
            this.AssetDownloader_BatchMode = new System.Windows.Forms.CheckBox();
            this.AssetDownloaderBatch_Status = new System.Windows.Forms.Label();
            this.AssetDownloader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AssetDownloader_AssetVersionSelector)).BeginInit();
            this.AssetLocalization.SuspendLayout();
            this.MeshConverter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MeshConverter_MeshVersionSelector)).BeginInit();
            this.AssetDownloaderBatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // AssetDownloader
            // 
            this.AssetDownloader.Controls.Add(this.AssetDownloader_BatchMode);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetNameBox);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetNameText);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_LoadHelpMessage);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_URLSelection);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetVersionText);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetIDText);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetVersionSelector);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetIDBox);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetDownloaderButton);
            this.AssetDownloader.Location = new System.Drawing.Point(12, 12);
            this.AssetDownloader.Name = "AssetDownloader";
            this.AssetDownloader.Size = new System.Drawing.Size(260, 142);
            this.AssetDownloader.TabIndex = 0;
            this.AssetDownloader.TabStop = false;
            this.AssetDownloader.Text = "Asset Downloader";
            // 
            // AssetDownloader_AssetNameBox
            // 
            this.AssetDownloader_AssetNameBox.Location = new System.Drawing.Point(16, 30);
            this.AssetDownloader_AssetNameBox.Name = "AssetDownloader_AssetNameBox";
            this.AssetDownloader_AssetNameBox.Size = new System.Drawing.Size(76, 20);
            this.AssetDownloader_AssetNameBox.TabIndex = 21;
            // 
            // AssetDownloader_AssetNameText
            // 
            this.AssetDownloader_AssetNameText.Location = new System.Drawing.Point(37, 14);
            this.AssetDownloader_AssetNameText.Name = "AssetDownloader_AssetNameText";
            this.AssetDownloader_AssetNameText.Size = new System.Drawing.Size(35, 14);
            this.AssetDownloader_AssetNameText.TabIndex = 20;
            this.AssetDownloader_AssetNameText.Text = "Name";
            // 
            // AssetDownloader_LoadHelpMessage
            // 
            this.AssetDownloader_LoadHelpMessage.Location = new System.Drawing.Point(6, 114);
            this.AssetDownloader_LoadHelpMessage.Name = "AssetDownloader_LoadHelpMessage";
            this.AssetDownloader_LoadHelpMessage.Size = new System.Drawing.Size(145, 24);
            this.AssetDownloader_LoadHelpMessage.TabIndex = 19;
            this.AssetDownloader_LoadHelpMessage.Text = "Disable Help Messages";
            this.AssetDownloader_LoadHelpMessage.UseVisualStyleBackColor = true;
            this.AssetDownloader_LoadHelpMessage.CheckedChanged += new System.EventHandler(this.AssetDownloader_LoadHelpMessage_CheckedChanged);
            // 
            // AssetDownloader_URLSelection
            // 
            this.AssetDownloader_URLSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AssetDownloader_URLSelection.FormattingEnabled = true;
            this.AssetDownloader_URLSelection.Items.AddRange(new object[] {
            "https://assetdelivery.roblox.com/",
            "https://www.roblox.com/catalog/",
            "https://www.roblox.com/library/"});
            this.AssetDownloader_URLSelection.Location = new System.Drawing.Point(7, 58);
            this.AssetDownloader_URLSelection.Name = "AssetDownloader_URLSelection";
            this.AssetDownloader_URLSelection.Size = new System.Drawing.Size(242, 21);
            this.AssetDownloader_URLSelection.TabIndex = 18;
            this.AssetDownloader_URLSelection.SelectedIndexChanged += new System.EventHandler(this.AssetDownloader_URLSelection_SelectedIndexChanged);
            // 
            // AssetDownloader_AssetVersionText
            // 
            this.AssetDownloader_AssetVersionText.Location = new System.Drawing.Point(194, 14);
            this.AssetDownloader_AssetVersionText.Name = "AssetDownloader_AssetVersionText";
            this.AssetDownloader_AssetVersionText.Size = new System.Drawing.Size(55, 14);
            this.AssetDownloader_AssetVersionText.TabIndex = 17;
            this.AssetDownloader_AssetVersionText.Text = "Version";
            this.AssetDownloader_AssetVersionText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AssetDownloader_AssetIDText
            // 
            this.AssetDownloader_AssetIDText.Location = new System.Drawing.Point(120, 14);
            this.AssetDownloader_AssetIDText.Name = "AssetDownloader_AssetIDText";
            this.AssetDownloader_AssetIDText.Size = new System.Drawing.Size(41, 14);
            this.AssetDownloader_AssetIDText.TabIndex = 16;
            this.AssetDownloader_AssetIDText.Text = "Item ID";
            // 
            // AssetDownloader_AssetVersionSelector
            // 
            this.AssetDownloader_AssetVersionSelector.Location = new System.Drawing.Point(197, 30);
            this.AssetDownloader_AssetVersionSelector.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.AssetDownloader_AssetVersionSelector.Name = "AssetDownloader_AssetVersionSelector";
            this.AssetDownloader_AssetVersionSelector.Size = new System.Drawing.Size(52, 20);
            this.AssetDownloader_AssetVersionSelector.TabIndex = 15;
            // 
            // AssetDownloader_AssetIDBox
            // 
            this.AssetDownloader_AssetIDBox.Location = new System.Drawing.Point(102, 30);
            this.AssetDownloader_AssetIDBox.Name = "AssetDownloader_AssetIDBox";
            this.AssetDownloader_AssetIDBox.Size = new System.Drawing.Size(76, 20);
            this.AssetDownloader_AssetIDBox.TabIndex = 14;
            // 
            // AssetDownloader_AssetDownloaderButton
            // 
            this.AssetDownloader_AssetDownloaderButton.Location = new System.Drawing.Point(7, 85);
            this.AssetDownloader_AssetDownloaderButton.Name = "AssetDownloader_AssetDownloaderButton";
            this.AssetDownloader_AssetDownloaderButton.Size = new System.Drawing.Size(242, 23);
            this.AssetDownloader_AssetDownloaderButton.TabIndex = 13;
            this.AssetDownloader_AssetDownloaderButton.Text = "Download!";
            this.AssetDownloader_AssetDownloaderButton.UseVisualStyleBackColor = true;
            this.AssetDownloader_AssetDownloaderButton.Click += new System.EventHandler(this.AssetDownloader_AssetDownloaderButton_Click);
            // 
            // AssetLocalization
            // 
            this.AssetLocalization.Controls.Add(this.AssetLocalization_SaveBackups);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_StatusBar);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_AssetTypeText);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_UsesHatMeshText);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_UsesHatMeshBox);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_ItemNameText);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_ItemNameBox);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_StatusText);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_AssetTypeBox);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_LocalizeButton);
            this.AssetLocalization.Location = new System.Drawing.Point(278, 126);
            this.AssetLocalization.Name = "AssetLocalization";
            this.AssetLocalization.Size = new System.Drawing.Size(267, 182);
            this.AssetLocalization.TabIndex = 1;
            this.AssetLocalization.TabStop = false;
            this.AssetLocalization.Text = "Asset Localization";
            // 
            // AssetLocalization_SaveBackups
            // 
            this.AssetLocalization_SaveBackups.AutoSize = true;
            this.AssetLocalization_SaveBackups.Location = new System.Drawing.Point(87, 98);
            this.AssetLocalization_SaveBackups.Name = "AssetLocalization_SaveBackups";
            this.AssetLocalization_SaveBackups.Size = new System.Drawing.Size(96, 17);
            this.AssetLocalization_SaveBackups.TabIndex = 20;
            this.AssetLocalization_SaveBackups.Text = "Save Backups";
            this.AssetLocalization_SaveBackups.UseVisualStyleBackColor = true;
            this.AssetLocalization_SaveBackups.CheckedChanged += new System.EventHandler(this.AssetLocalization_SaveBackups_CheckedChanged);
            // 
            // AssetLocalization_StatusBar
            // 
            this.AssetLocalization_StatusBar.Location = new System.Drawing.Point(6, 161);
            this.AssetLocalization_StatusBar.Name = "AssetLocalization_StatusBar";
            this.AssetLocalization_StatusBar.Size = new System.Drawing.Size(254, 16);
            this.AssetLocalization_StatusBar.TabIndex = 19;
            // 
            // AssetLocalization_AssetTypeText
            // 
            this.AssetLocalization_AssetTypeText.AutoSize = true;
            this.AssetLocalization_AssetTypeText.Location = new System.Drawing.Point(6, 16);
            this.AssetLocalization_AssetTypeText.Name = "AssetLocalization_AssetTypeText";
            this.AssetLocalization_AssetTypeText.Size = new System.Drawing.Size(63, 13);
            this.AssetLocalization_AssetTypeText.TabIndex = 18;
            this.AssetLocalization_AssetTypeText.Text = "Asset Type:";
            // 
            // AssetLocalization_UsesHatMeshText
            // 
            this.AssetLocalization_UsesHatMeshText.AutoSize = true;
            this.AssetLocalization_UsesHatMeshText.Location = new System.Drawing.Point(6, 69);
            this.AssetLocalization_UsesHatMeshText.Name = "AssetLocalization_UsesHatMeshText";
            this.AssetLocalization_UsesHatMeshText.Size = new System.Drawing.Size(118, 13);
            this.AssetLocalization_UsesHatMeshText.TabIndex = 17;
            this.AssetLocalization_UsesHatMeshText.Text = "Uses Mesh (Hats Only):";
            // 
            // AssetLocalization_UsesHatMeshBox
            // 
            this.AssetLocalization_UsesHatMeshBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AssetLocalization_UsesHatMeshBox.FormattingEnabled = true;
            this.AssetLocalization_UsesHatMeshBox.Items.AddRange(new object[] {
            "None"});
            this.AssetLocalization_UsesHatMeshBox.Location = new System.Drawing.Point(134, 66);
            this.AssetLocalization_UsesHatMeshBox.Name = "AssetLocalization_UsesHatMeshBox";
            this.AssetLocalization_UsesHatMeshBox.Size = new System.Drawing.Size(126, 21);
            this.AssetLocalization_UsesHatMeshBox.TabIndex = 16;
            this.AssetLocalization_UsesHatMeshBox.SelectedIndexChanged += new System.EventHandler(this.AssetLocalization_UsesHatMeshBox_SelectedIndexChanged);
            // 
            // AssetLocalization_ItemNameText
            // 
            this.AssetLocalization_ItemNameText.AutoSize = true;
            this.AssetLocalization_ItemNameText.Location = new System.Drawing.Point(6, 43);
            this.AssetLocalization_ItemNameText.Name = "AssetLocalization_ItemNameText";
            this.AssetLocalization_ItemNameText.Size = new System.Drawing.Size(125, 13);
            this.AssetLocalization_ItemNameText.TabIndex = 15;
            this.AssetLocalization_ItemNameText.Text = "Asset Name (Items Only):";
            // 
            // AssetLocalization_ItemNameBox
            // 
            this.AssetLocalization_ItemNameBox.Location = new System.Drawing.Point(134, 40);
            this.AssetLocalization_ItemNameBox.Name = "AssetLocalization_ItemNameBox";
            this.AssetLocalization_ItemNameBox.Size = new System.Drawing.Size(126, 20);
            this.AssetLocalization_ItemNameBox.TabIndex = 14;
            this.AssetLocalization_ItemNameBox.TextChanged += new System.EventHandler(this.AssetLocalization_ItemNameBox_TextChanged);
            // 
            // AssetLocalization_StatusText
            // 
            this.AssetLocalization_StatusText.Location = new System.Drawing.Point(6, 145);
            this.AssetLocalization_StatusText.Name = "AssetLocalization_StatusText";
            this.AssetLocalization_StatusText.Size = new System.Drawing.Size(254, 13);
            this.AssetLocalization_StatusText.TabIndex = 13;
            this.AssetLocalization_StatusText.Text = "Idle";
            this.AssetLocalization_StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AssetLocalization_AssetTypeBox
            // 
            this.AssetLocalization_AssetTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AssetLocalization_AssetTypeBox.FormattingEnabled = true;
            this.AssetLocalization_AssetTypeBox.Items.AddRange(new object[] {
            "RBXL",
            "RBXM",
            "Hat",
            "Head",
            "Face",
            "Shirt",
            "T-Shirt",
            "Pants",
            "Lua Script"});
            this.AssetLocalization_AssetTypeBox.Location = new System.Drawing.Point(75, 13);
            this.AssetLocalization_AssetTypeBox.Name = "AssetLocalization_AssetTypeBox";
            this.AssetLocalization_AssetTypeBox.Size = new System.Drawing.Size(185, 21);
            this.AssetLocalization_AssetTypeBox.TabIndex = 12;
            this.AssetLocalization_AssetTypeBox.SelectedIndexChanged += new System.EventHandler(this.AssetLocalization_AssetTypeBox_SelectedIndexChanged);
            // 
            // AssetLocalization_LocalizeButton
            // 
            this.AssetLocalization_LocalizeButton.Location = new System.Drawing.Point(6, 121);
            this.AssetLocalization_LocalizeButton.Name = "AssetLocalization_LocalizeButton";
            this.AssetLocalization_LocalizeButton.Size = new System.Drawing.Size(254, 21);
            this.AssetLocalization_LocalizeButton.TabIndex = 11;
            this.AssetLocalization_LocalizeButton.Text = "Browse and Localize Model/Place";
            this.AssetLocalization_LocalizeButton.UseVisualStyleBackColor = true;
            this.AssetLocalization_LocalizeButton.Click += new System.EventHandler(this.AssetLocalization_LocalizeButton_Click);
            // 
            // MeshConverter
            // 
            this.MeshConverter.Controls.Add(this.MeshConverter_StatusText);
            this.MeshConverter.Controls.Add(this.MeshConverter_MeshVersionSelector);
            this.MeshConverter.Controls.Add(this.MeshConverter_MeshVersionText);
            this.MeshConverter.Controls.Add(this.MeshConverter_CreditText);
            this.MeshConverter.Controls.Add(this.MeshConverter_ConvertButton);
            this.MeshConverter.Location = new System.Drawing.Point(278, 14);
            this.MeshConverter.Name = "MeshConverter";
            this.MeshConverter.Size = new System.Drawing.Size(262, 106);
            this.MeshConverter.TabIndex = 2;
            this.MeshConverter.TabStop = false;
            this.MeshConverter.Text = "Mesh Converter";
            // 
            // MeshConverter_StatusText
            // 
            this.MeshConverter_StatusText.Location = new System.Drawing.Point(12, 66);
            this.MeshConverter_StatusText.Name = "MeshConverter_StatusText";
            this.MeshConverter_StatusText.Size = new System.Drawing.Size(239, 14);
            this.MeshConverter_StatusText.TabIndex = 10;
            this.MeshConverter_StatusText.Text = "Ready";
            this.MeshConverter_StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MeshConverter_MeshVersionSelector
            // 
            this.MeshConverter_MeshVersionSelector.Location = new System.Drawing.Point(144, 14);
            this.MeshConverter_MeshVersionSelector.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MeshConverter_MeshVersionSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MeshConverter_MeshVersionSelector.Name = "MeshConverter_MeshVersionSelector";
            this.MeshConverter_MeshVersionSelector.Size = new System.Drawing.Size(56, 20);
            this.MeshConverter_MeshVersionSelector.TabIndex = 9;
            this.MeshConverter_MeshVersionSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MeshConverter_MeshVersionText
            // 
            this.MeshConverter_MeshVersionText.AutoSize = true;
            this.MeshConverter_MeshVersionText.Location = new System.Drawing.Point(64, 16);
            this.MeshConverter_MeshVersionText.Name = "MeshConverter_MeshVersionText";
            this.MeshConverter_MeshVersionText.Size = new System.Drawing.Size(74, 13);
            this.MeshConverter_MeshVersionText.TabIndex = 8;
            this.MeshConverter_MeshVersionText.Text = "Mesh Version:";
            // 
            // MeshConverter_CreditText
            // 
            this.MeshConverter_CreditText.AutoSize = true;
            this.MeshConverter_CreditText.Location = new System.Drawing.Point(51, 90);
            this.MeshConverter_CreditText.Name = "MeshConverter_CreditText";
            this.MeshConverter_CreditText.Size = new System.Drawing.Size(167, 13);
            this.MeshConverter_CreditText.TabIndex = 7;
            this.MeshConverter_CreditText.Text = "RBXMeshConverter built by coke.";
            // 
            // MeshConverter_ConvertButton
            // 
            this.MeshConverter_ConvertButton.Location = new System.Drawing.Point(12, 40);
            this.MeshConverter_ConvertButton.Name = "MeshConverter_ConvertButton";
            this.MeshConverter_ConvertButton.Size = new System.Drawing.Size(239, 23);
            this.MeshConverter_ConvertButton.TabIndex = 6;
            this.MeshConverter_ConvertButton.Text = "Browse for mesh and convert...";
            this.MeshConverter_ConvertButton.UseVisualStyleBackColor = true;
            this.MeshConverter_ConvertButton.Click += new System.EventHandler(this.MeshConverter_ConvertButton_Click);
            // 
            // AssetLocalization_BackgroundWorker
            // 
            this.AssetLocalization_BackgroundWorker.WorkerReportsProgress = true;
            this.AssetLocalization_BackgroundWorker.WorkerSupportsCancellation = true;
            this.AssetLocalization_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AssetLocalization_BackgroundWorker_DoWork);
            this.AssetLocalization_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AssetLocalization_BackgroundWorker_ProgressChanged);
            this.AssetLocalization_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AssetLocalization_BackgroundWorker_RunWorkerCompleted);
            // 
            // AssetDownloaderBatch
            // 
            this.AssetDownloaderBatch.Controls.Add(this.AssetDownloaderBatch_Status);
            this.AssetDownloaderBatch.Controls.Add(this.AssetDownloaderBatch_BatchIDBox);
            this.AssetDownloaderBatch.Location = new System.Drawing.Point(12, 160);
            this.AssetDownloaderBatch.Name = "AssetDownloaderBatch";
            this.AssetDownloaderBatch.Size = new System.Drawing.Size(260, 148);
            this.AssetDownloaderBatch.TabIndex = 3;
            this.AssetDownloaderBatch.TabStop = false;
            this.AssetDownloaderBatch.Text = "Asset Downloader Batch Queue (IDs here!)";
            // 
            // AssetDownloaderBatch_BatchIDBox
            // 
            this.AssetDownloaderBatch_BatchIDBox.Enabled = false;
            this.AssetDownloaderBatch_BatchIDBox.Location = new System.Drawing.Point(6, 19);
            this.AssetDownloaderBatch_BatchIDBox.Multiline = true;
            this.AssetDownloaderBatch_BatchIDBox.Name = "AssetDownloaderBatch_BatchIDBox";
            this.AssetDownloaderBatch_BatchIDBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AssetDownloaderBatch_BatchIDBox.Size = new System.Drawing.Size(242, 105);
            this.AssetDownloaderBatch_BatchIDBox.TabIndex = 0;
            // 
            // AssetDownloader_BatchMode
            // 
            this.AssetDownloader_BatchMode.AutoSize = true;
            this.AssetDownloader_BatchMode.Location = new System.Drawing.Point(157, 118);
            this.AssetDownloader_BatchMode.Name = "AssetDownloader_BatchMode";
            this.AssetDownloader_BatchMode.Size = new System.Drawing.Size(84, 17);
            this.AssetDownloader_BatchMode.TabIndex = 22;
            this.AssetDownloader_BatchMode.Text = "Batch Mode";
            this.AssetDownloader_BatchMode.UseVisualStyleBackColor = true;
            this.AssetDownloader_BatchMode.CheckedChanged += new System.EventHandler(this.AssetDownloader_BatchMode_CheckedChanged);
            // 
            // AssetDownloaderBatch_Status
            // 
            this.AssetDownloaderBatch_Status.AutoSize = true;
            this.AssetDownloaderBatch_Status.Cursor = System.Windows.Forms.Cursors.Default;
            this.AssetDownloaderBatch_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssetDownloaderBatch_Status.ForeColor = System.Drawing.Color.Red;
            this.AssetDownloaderBatch_Status.Location = new System.Drawing.Point(94, 127);
            this.AssetDownloaderBatch_Status.Name = "AssetDownloaderBatch_Status";
            this.AssetDownloaderBatch_Status.Size = new System.Drawing.Size(84, 13);
            this.AssetDownloaderBatch_Status.TabIndex = 1;
            this.AssetDownloaderBatch_Status.Text = "Please wait...";
            this.AssetDownloaderBatch_Status.Visible = false;
            // 
            // AssetSDK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(557, 320);
            this.Controls.Add(this.AssetDownloaderBatch);
            this.Controls.Add(this.MeshConverter);
            this.Controls.Add(this.AssetLocalization);
            this.Controls.Add(this.AssetDownloader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AssetSDK";
            this.Text = "Novetus Asset SDK";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AssetSDK_Close);
            this.Load += new System.EventHandler(this.AssetSDK_Load);
            this.AssetDownloader.ResumeLayout(false);
            this.AssetDownloader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AssetDownloader_AssetVersionSelector)).EndInit();
            this.AssetLocalization.ResumeLayout(false);
            this.AssetLocalization.PerformLayout();
            this.MeshConverter.ResumeLayout(false);
            this.MeshConverter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MeshConverter_MeshVersionSelector)).EndInit();
            this.AssetDownloaderBatch.ResumeLayout(false);
            this.AssetDownloaderBatch.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox AssetDownloader;
    private System.Windows.Forms.GroupBox AssetLocalization;
    private System.Windows.Forms.GroupBox MeshConverter;
    private System.Windows.Forms.Label MeshConverter_StatusText;
    private System.Windows.Forms.NumericUpDown MeshConverter_MeshVersionSelector;
    private System.Windows.Forms.Label MeshConverter_MeshVersionText;
    private System.Windows.Forms.Label MeshConverter_CreditText;
    private System.Windows.Forms.Button MeshConverter_ConvertButton;
    private System.Windows.Forms.CheckBox AssetLocalization_SaveBackups;
    private System.Windows.Forms.ProgressBar AssetLocalization_StatusBar;
    private System.Windows.Forms.Label AssetLocalization_AssetTypeText;
    private System.Windows.Forms.Label AssetLocalization_UsesHatMeshText;
    private System.Windows.Forms.ComboBox AssetLocalization_UsesHatMeshBox;
    private System.Windows.Forms.Label AssetLocalization_ItemNameText;
    private System.Windows.Forms.TextBox AssetLocalization_ItemNameBox;
    private System.Windows.Forms.Label AssetLocalization_StatusText;
    private System.Windows.Forms.ComboBox AssetLocalization_AssetTypeBox;
    private System.Windows.Forms.Button AssetLocalization_LocalizeButton;
    private System.Windows.Forms.TextBox AssetDownloader_AssetNameBox;
    private System.Windows.Forms.Label AssetDownloader_AssetNameText;
    private System.Windows.Forms.CheckBox AssetDownloader_LoadHelpMessage;
    private System.Windows.Forms.ComboBox AssetDownloader_URLSelection;
    private System.Windows.Forms.Label AssetDownloader_AssetVersionText;
    private System.Windows.Forms.Label AssetDownloader_AssetIDText;
    private System.Windows.Forms.NumericUpDown AssetDownloader_AssetVersionSelector;
    private System.Windows.Forms.TextBox AssetDownloader_AssetIDBox;
    private System.Windows.Forms.Button AssetDownloader_AssetDownloaderButton;
    private System.ComponentModel.BackgroundWorker AssetLocalization_BackgroundWorker;
    private System.Windows.Forms.CheckBox AssetDownloader_BatchMode;
    private System.Windows.Forms.GroupBox AssetDownloaderBatch;
    private System.Windows.Forms.TextBox AssetDownloaderBatch_BatchIDBox;
    private System.Windows.Forms.Label AssetDownloaderBatch_Status;
}