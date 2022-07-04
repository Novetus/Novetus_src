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
            this.AssetDownloaderBatch_Note = new System.Windows.Forms.Label();
            this.AssetDownloaderBatch_Status = new System.Windows.Forms.Label();
            this.AssetDownloader_BatchMode = new System.Windows.Forms.CheckBox();
            this.AssetDownloaderBatch_BatchIDBox = new System.Windows.Forms.TextBox();
            this.AssetDownloader_AssetNameBox = new System.Windows.Forms.TextBox();
            this.AssetDownloader_AssetNameText = new System.Windows.Forms.Label();
            this.AssetDownloader_AssetVersionText = new System.Windows.Forms.Label();
            this.AssetDownloader_AssetIDText = new System.Windows.Forms.Label();
            this.AssetDownloader_AssetVersionSelector = new System.Windows.Forms.NumericUpDown();
            this.AssetDownloader_AssetIDBox = new System.Windows.Forms.TextBox();
            this.AssetDownloader_AssetDownloaderButton = new System.Windows.Forms.Button();
            this.AssetDownloader_LoadHelpMessage = new System.Windows.Forms.CheckBox();
            this.CustomDLURLLabel = new System.Windows.Forms.Label();
            this.URLOverrideBox = new System.Windows.Forms.TextBox();
            this.URLListLabel = new System.Windows.Forms.Label();
            this.URLSelection = new System.Windows.Forms.ComboBox();
            this.AssetLocalization = new System.Windows.Forms.GroupBox();
            this.AssetLocalization_AssetLinks = new System.Windows.Forms.CheckBox();
            this.AssetLocalization_LocalizePermanentlyBox = new System.Windows.Forms.CheckBox();
            this.AssetLocalization_SaveBackups = new System.Windows.Forms.CheckBox();
            this.AssetLocalization_AssetTypeText = new System.Windows.Forms.Label();
            this.AssetLocalization_UsesHatMeshText = new System.Windows.Forms.Label();
            this.AssetLocalization_UsesHatMeshBox = new System.Windows.Forms.ComboBox();
            this.AssetLocalization_ItemNameText = new System.Windows.Forms.Label();
            this.AssetLocalization_ItemNameBox = new System.Windows.Forms.TextBox();
            this.AssetLocalization_StatusText = new System.Windows.Forms.Label();
            this.AssetLocalization_AssetTypeBox = new System.Windows.Forms.ComboBox();
            this.AssetLocalization_LocalizeButton = new System.Windows.Forms.Button();
            this.MeshConverter = new System.Windows.Forms.GroupBox();
            this.MeshConverter_MeshVersionSelector = new System.Windows.Forms.ComboBox();
            this.MeshConverter_StatusText = new System.Windows.Forms.Label();
            this.MeshConverter_MeshVersionText = new System.Windows.Forms.Label();
            this.MeshConverter_CreditText = new System.Windows.Forms.Label();
            this.MeshConverter_ConvertButton = new System.Windows.Forms.Button();
            this.AssetLocalization_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.AssetDownloader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AssetDownloader_AssetVersionSelector)).BeginInit();
            this.AssetLocalization.SuspendLayout();
            this.MeshConverter.SuspendLayout();
            this.SuspendLayout();
            // 
            // AssetDownloader
            // 
            this.AssetDownloader.Controls.Add(this.AssetDownloaderBatch_Note);
            this.AssetDownloader.Controls.Add(this.AssetDownloaderBatch_Status);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_BatchMode);
            this.AssetDownloader.Controls.Add(this.AssetDownloaderBatch_BatchIDBox);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetNameBox);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetNameText);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetVersionText);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetIDText);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetVersionSelector);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetIDBox);
            this.AssetDownloader.Controls.Add(this.AssetDownloader_AssetDownloaderButton);
            this.AssetDownloader.Location = new System.Drawing.Point(12, 62);
            this.AssetDownloader.Name = "AssetDownloader";
            this.AssetDownloader.Size = new System.Drawing.Size(260, 311);
            this.AssetDownloader.TabIndex = 0;
            this.AssetDownloader.TabStop = false;
            this.AssetDownloader.Text = "Asset Downloader";
            // 
            // AssetDownloaderBatch_Note
            // 
            this.AssetDownloaderBatch_Note.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssetDownloaderBatch_Note.ForeColor = System.Drawing.Color.Red;
            this.AssetDownloaderBatch_Note.Location = new System.Drawing.Point(14, 85);
            this.AssetDownloaderBatch_Note.Name = "AssetDownloaderBatch_Note";
            this.AssetDownloaderBatch_Note.Size = new System.Drawing.Size(236, 42);
            this.AssetDownloaderBatch_Note.TabIndex = 23;
            this.AssetDownloaderBatch_Note.Text = "You must enter in each item as <Name>|<ID>|<Version>. \r\nExample: RedTopHat|297230" +
    "2|1";
            this.AssetDownloaderBatch_Note.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AssetDownloaderBatch_Note.Visible = false;
            // 
            // AssetDownloaderBatch_Status
            // 
            this.AssetDownloaderBatch_Status.AutoSize = true;
            this.AssetDownloaderBatch_Status.Cursor = System.Windows.Forms.Cursors.Default;
            this.AssetDownloaderBatch_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssetDownloaderBatch_Status.ForeColor = System.Drawing.Color.Red;
            this.AssetDownloaderBatch_Status.Location = new System.Drawing.Point(89, 281);
            this.AssetDownloaderBatch_Status.Name = "AssetDownloaderBatch_Status";
            this.AssetDownloaderBatch_Status.Size = new System.Drawing.Size(84, 13);
            this.AssetDownloaderBatch_Status.TabIndex = 1;
            this.AssetDownloaderBatch_Status.Text = "Please wait...";
            this.AssetDownloaderBatch_Status.Visible = false;
            // 
            // AssetDownloader_BatchMode
            // 
            this.AssetDownloader_BatchMode.Location = new System.Drawing.Point(164, 57);
            this.AssetDownloader_BatchMode.Name = "AssetDownloader_BatchMode";
            this.AssetDownloader_BatchMode.Size = new System.Drawing.Size(90, 23);
            this.AssetDownloader_BatchMode.TabIndex = 22;
            this.AssetDownloader_BatchMode.Text = "Batch Mode";
            this.AssetDownloader_BatchMode.UseVisualStyleBackColor = true;
            this.AssetDownloader_BatchMode.CheckedChanged += new System.EventHandler(this.AssetDownloader_BatchMode_CheckedChanged);
            // 
            // AssetDownloaderBatch_BatchIDBox
            // 
            this.AssetDownloaderBatch_BatchIDBox.Enabled = false;
            this.AssetDownloaderBatch_BatchIDBox.Location = new System.Drawing.Point(8, 132);
            this.AssetDownloaderBatch_BatchIDBox.Multiline = true;
            this.AssetDownloaderBatch_BatchIDBox.Name = "AssetDownloaderBatch_BatchIDBox";
            this.AssetDownloaderBatch_BatchIDBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AssetDownloaderBatch_BatchIDBox.Size = new System.Drawing.Size(242, 146);
            this.AssetDownloaderBatch_BatchIDBox.TabIndex = 0;
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
            -727379969,
            232,
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
            this.AssetDownloader_AssetDownloaderButton.Location = new System.Drawing.Point(12, 57);
            this.AssetDownloader_AssetDownloaderButton.Name = "AssetDownloader_AssetDownloaderButton";
            this.AssetDownloader_AssetDownloaderButton.Size = new System.Drawing.Size(149, 23);
            this.AssetDownloader_AssetDownloaderButton.TabIndex = 13;
            this.AssetDownloader_AssetDownloaderButton.Text = "Download!";
            this.AssetDownloader_AssetDownloaderButton.UseVisualStyleBackColor = true;
            this.AssetDownloader_AssetDownloaderButton.Click += new System.EventHandler(this.AssetDownloader_AssetDownloaderButton_Click);
            // 
            // AssetDownloader_LoadHelpMessage
            // 
            this.AssetDownloader_LoadHelpMessage.Location = new System.Drawing.Point(204, 38);
            this.AssetDownloader_LoadHelpMessage.Name = "AssetDownloader_LoadHelpMessage";
            this.AssetDownloader_LoadHelpMessage.Size = new System.Drawing.Size(145, 24);
            this.AssetDownloader_LoadHelpMessage.TabIndex = 19;
            this.AssetDownloader_LoadHelpMessage.Text = "Disable Help Messages";
            this.AssetDownloader_LoadHelpMessage.UseVisualStyleBackColor = true;
            this.AssetDownloader_LoadHelpMessage.CheckedChanged += new System.EventHandler(this.AssetDownloader_LoadHelpMessage_CheckedChanged);
            // 
            // CustomDLURLLabel
            // 
            this.CustomDLURLLabel.AutoSize = true;
            this.CustomDLURLLabel.Location = new System.Drawing.Point(286, 14);
            this.CustomDLURLLabel.Name = "CustomDLURLLabel";
            this.CustomDLURLLabel.Size = new System.Drawing.Size(67, 13);
            this.CustomDLURLLabel.TabIndex = 26;
            this.CustomDLURLLabel.Text = "Custom URL";
            // 
            // URLOverrideBox
            // 
            this.URLOverrideBox.Location = new System.Drawing.Point(353, 11);
            this.URLOverrideBox.Name = "URLOverrideBox";
            this.URLOverrideBox.Size = new System.Drawing.Size(187, 20);
            this.URLOverrideBox.TabIndex = 25;
            this.URLOverrideBox.Click += new System.EventHandler(this.URLOverrideBox_Click);
            this.URLOverrideBox.TextChanged += new System.EventHandler(this.URLOverrideBox_TextChanged);
            // 
            // URLListLabel
            // 
            this.URLListLabel.AutoSize = true;
            this.URLListLabel.Location = new System.Drawing.Point(12, 14);
            this.URLListLabel.Name = "URLListLabel";
            this.URLListLabel.Size = new System.Drawing.Size(48, 13);
            this.URLListLabel.TabIndex = 24;
            this.URLListLabel.Text = "URL List";
            // 
            // URLSelection
            // 
            this.URLSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.URLSelection.DropDownWidth = 242;
            this.URLSelection.FormattingEnabled = true;
            this.URLSelection.Location = new System.Drawing.Point(66, 11);
            this.URLSelection.Name = "URLSelection";
            this.URLSelection.Size = new System.Drawing.Size(214, 21);
            this.URLSelection.TabIndex = 18;
            this.URLSelection.SelectedIndexChanged += new System.EventHandler(this.URLSelection_SelectedIndexChanged);
            // 
            // AssetLocalization
            // 
            this.AssetLocalization.Controls.Add(this.AssetLocalization_AssetLinks);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_LocalizePermanentlyBox);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_SaveBackups);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_AssetTypeText);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_UsesHatMeshText);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_UsesHatMeshBox);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_ItemNameText);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_ItemNameBox);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_StatusText);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_AssetTypeBox);
            this.AssetLocalization.Controls.Add(this.AssetLocalization_LocalizeButton);
            this.AssetLocalization.Location = new System.Drawing.Point(278, 176);
            this.AssetLocalization.Name = "AssetLocalization";
            this.AssetLocalization.Size = new System.Drawing.Size(267, 197);
            this.AssetLocalization.TabIndex = 1;
            this.AssetLocalization.TabStop = false;
            this.AssetLocalization.Text = "Asset Fixer";
            // 
            // AssetLocalization_AssetLinks
            // 
            this.AssetLocalization_AssetLinks.AutoSize = true;
            this.AssetLocalization_AssetLinks.Location = new System.Drawing.Point(22, 119);
            this.AssetLocalization_AssetLinks.Name = "AssetLocalization_AssetLinks";
            this.AssetLocalization_AssetLinks.Size = new System.Drawing.Size(215, 17);
            this.AssetLocalization_AssetLinks.TabIndex = 22;
            this.AssetLocalization_AssetLinks.Text = "Replace Asset Links with Selected URL";
            this.AssetLocalization_AssetLinks.UseVisualStyleBackColor = true;
            this.AssetLocalization_AssetLinks.CheckedChanged += new System.EventHandler(this.AssetLocalization_AssetLinks_CheckedChanged);
            // 
            // AssetLocalization_LocalizePermanentlyBox
            // 
            this.AssetLocalization_LocalizePermanentlyBox.AutoSize = true;
            this.AssetLocalization_LocalizePermanentlyBox.Location = new System.Drawing.Point(125, 96);
            this.AssetLocalization_LocalizePermanentlyBox.Name = "AssetLocalization_LocalizePermanentlyBox";
            this.AssetLocalization_LocalizePermanentlyBox.Size = new System.Drawing.Size(126, 17);
            this.AssetLocalization_LocalizePermanentlyBox.TabIndex = 21;
            this.AssetLocalization_LocalizePermanentlyBox.Text = "Localize Permanently";
            this.AssetLocalization_LocalizePermanentlyBox.UseVisualStyleBackColor = true;
            this.AssetLocalization_LocalizePermanentlyBox.CheckedChanged += new System.EventHandler(this.AssetLocalization_LocalizePermanentlyBox_CheckedChanged);
            this.AssetLocalization_LocalizePermanentlyBox.Click += new System.EventHandler(this.AssetLocalization_LocalizePermanentlyBox_Click);
            // 
            // AssetLocalization_SaveBackups
            // 
            this.AssetLocalization_SaveBackups.AutoSize = true;
            this.AssetLocalization_SaveBackups.Location = new System.Drawing.Point(15, 96);
            this.AssetLocalization_SaveBackups.Name = "AssetLocalization_SaveBackups";
            this.AssetLocalization_SaveBackups.Size = new System.Drawing.Size(96, 17);
            this.AssetLocalization_SaveBackups.TabIndex = 20;
            this.AssetLocalization_SaveBackups.Text = "Save Backups";
            this.AssetLocalization_SaveBackups.UseVisualStyleBackColor = true;
            this.AssetLocalization_SaveBackups.CheckedChanged += new System.EventHandler(this.AssetLocalization_SaveBackups_CheckedChanged);
            // 
            // AssetLocalization_AssetTypeText
            // 
            this.AssetLocalization_AssetTypeText.AutoSize = true;
            this.AssetLocalization_AssetTypeText.Location = new System.Drawing.Point(6, 18);
            this.AssetLocalization_AssetTypeText.Name = "AssetLocalization_AssetTypeText";
            this.AssetLocalization_AssetTypeText.Size = new System.Drawing.Size(63, 13);
            this.AssetLocalization_AssetTypeText.TabIndex = 18;
            this.AssetLocalization_AssetTypeText.Text = "Asset Type:";
            // 
            // AssetLocalization_UsesHatMeshText
            // 
            this.AssetLocalization_UsesHatMeshText.AutoSize = true;
            this.AssetLocalization_UsesHatMeshText.Location = new System.Drawing.Point(6, 71);
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
            this.AssetLocalization_UsesHatMeshBox.Location = new System.Drawing.Point(134, 68);
            this.AssetLocalization_UsesHatMeshBox.Name = "AssetLocalization_UsesHatMeshBox";
            this.AssetLocalization_UsesHatMeshBox.Size = new System.Drawing.Size(126, 21);
            this.AssetLocalization_UsesHatMeshBox.TabIndex = 16;
            this.AssetLocalization_UsesHatMeshBox.SelectedIndexChanged += new System.EventHandler(this.AssetLocalization_UsesHatMeshBox_SelectedIndexChanged);
            // 
            // AssetLocalization_ItemNameText
            // 
            this.AssetLocalization_ItemNameText.AutoSize = true;
            this.AssetLocalization_ItemNameText.Location = new System.Drawing.Point(6, 45);
            this.AssetLocalization_ItemNameText.Name = "AssetLocalization_ItemNameText";
            this.AssetLocalization_ItemNameText.Size = new System.Drawing.Size(115, 13);
            this.AssetLocalization_ItemNameText.TabIndex = 15;
            this.AssetLocalization_ItemNameText.Text = "Asset Name (Optional):";
            // 
            // AssetLocalization_ItemNameBox
            // 
            this.AssetLocalization_ItemNameBox.Location = new System.Drawing.Point(134, 42);
            this.AssetLocalization_ItemNameBox.Name = "AssetLocalization_ItemNameBox";
            this.AssetLocalization_ItemNameBox.Size = new System.Drawing.Size(126, 20);
            this.AssetLocalization_ItemNameBox.TabIndex = 14;
            this.AssetLocalization_ItemNameBox.TextChanged += new System.EventHandler(this.AssetLocalization_ItemNameBox_TextChanged);
            // 
            // AssetLocalization_StatusText
            // 
            this.AssetLocalization_StatusText.Location = new System.Drawing.Point(6, 167);
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
            "Lua Script"});
            this.AssetLocalization_AssetTypeBox.Location = new System.Drawing.Point(75, 15);
            this.AssetLocalization_AssetTypeBox.Name = "AssetLocalization_AssetTypeBox";
            this.AssetLocalization_AssetTypeBox.Size = new System.Drawing.Size(185, 21);
            this.AssetLocalization_AssetTypeBox.TabIndex = 12;
            this.AssetLocalization_AssetTypeBox.SelectedIndexChanged += new System.EventHandler(this.AssetLocalization_AssetTypeBox_SelectedIndexChanged);
            // 
            // AssetLocalization_LocalizeButton
            // 
            this.AssetLocalization_LocalizeButton.Location = new System.Drawing.Point(6, 143);
            this.AssetLocalization_LocalizeButton.Name = "AssetLocalization_LocalizeButton";
            this.AssetLocalization_LocalizeButton.Size = new System.Drawing.Size(254, 21);
            this.AssetLocalization_LocalizeButton.TabIndex = 11;
            this.AssetLocalization_LocalizeButton.Text = "Browse and Localize Model/Place";
            this.AssetLocalization_LocalizeButton.UseVisualStyleBackColor = true;
            this.AssetLocalization_LocalizeButton.Click += new System.EventHandler(this.AssetLocalization_LocalizeButton_Click);
            // 
            // MeshConverter
            // 
            this.MeshConverter.Controls.Add(this.MeshConverter_MeshVersionSelector);
            this.MeshConverter.Controls.Add(this.MeshConverter_StatusText);
            this.MeshConverter.Controls.Add(this.MeshConverter_MeshVersionText);
            this.MeshConverter.Controls.Add(this.MeshConverter_CreditText);
            this.MeshConverter.Controls.Add(this.MeshConverter_ConvertButton);
            this.MeshConverter.Location = new System.Drawing.Point(278, 62);
            this.MeshConverter.Name = "MeshConverter";
            this.MeshConverter.Size = new System.Drawing.Size(262, 106);
            this.MeshConverter.TabIndex = 2;
            this.MeshConverter.TabStop = false;
            this.MeshConverter.Text = "Mesh Converter";
            // 
            // MeshConverter_MeshVersionSelector
            // 
            this.MeshConverter_MeshVersionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MeshConverter_MeshVersionSelector.FormattingEnabled = true;
            this.MeshConverter_MeshVersionSelector.Items.AddRange(new object[] {
            "1.00",
            "1.01",
            "2.00"});
            this.MeshConverter_MeshVersionSelector.Location = new System.Drawing.Point(125, 14);
            this.MeshConverter_MeshVersionSelector.Name = "MeshConverter_MeshVersionSelector";
            this.MeshConverter_MeshVersionSelector.Size = new System.Drawing.Size(90, 21);
            this.MeshConverter_MeshVersionSelector.TabIndex = 11;
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
            // MeshConverter_MeshVersionText
            // 
            this.MeshConverter_MeshVersionText.AutoSize = true;
            this.MeshConverter_MeshVersionText.Location = new System.Drawing.Point(45, 17);
            this.MeshConverter_MeshVersionText.Name = "MeshConverter_MeshVersionText";
            this.MeshConverter_MeshVersionText.Size = new System.Drawing.Size(74, 13);
            this.MeshConverter_MeshVersionText.TabIndex = 8;
            this.MeshConverter_MeshVersionText.Text = "Mesh Version:";
            // 
            // MeshConverter_CreditText
            // 
            this.MeshConverter_CreditText.AutoSize = true;
            this.MeshConverter_CreditText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MeshConverter_CreditText.Location = new System.Drawing.Point(1, 85);
            this.MeshConverter_CreditText.Name = "MeshConverter_CreditText";
            this.MeshConverter_CreditText.Size = new System.Drawing.Size(261, 12);
            this.MeshConverter_CreditText.TabIndex = 7;
            this.MeshConverter_CreditText.Text = "ObjToRBXMesh built by coke. Modified to support old meshes.";
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
            // AssetSDK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(557, 377);
            this.Controls.Add(this.URLOverrideBox);
            this.Controls.Add(this.CustomDLURLLabel);
            this.Controls.Add(this.MeshConverter);
            this.Controls.Add(this.AssetLocalization);
            this.Controls.Add(this.URLListLabel);
            this.Controls.Add(this.AssetDownloader);
            this.Controls.Add(this.AssetDownloader_LoadHelpMessage);
            this.Controls.Add(this.URLSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AssetSDK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox AssetDownloader;
    private System.Windows.Forms.GroupBox AssetLocalization;
    private System.Windows.Forms.GroupBox MeshConverter;
    private System.Windows.Forms.Label MeshConverter_StatusText;
    private System.Windows.Forms.Label MeshConverter_MeshVersionText;
    private System.Windows.Forms.Label MeshConverter_CreditText;
    private System.Windows.Forms.Button MeshConverter_ConvertButton;
    private System.Windows.Forms.CheckBox AssetLocalization_SaveBackups;
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
    private System.Windows.Forms.ComboBox URLSelection;
    private System.Windows.Forms.Label AssetDownloader_AssetVersionText;
    private System.Windows.Forms.Label AssetDownloader_AssetIDText;
    private System.Windows.Forms.NumericUpDown AssetDownloader_AssetVersionSelector;
    private System.Windows.Forms.TextBox AssetDownloader_AssetIDBox;
    private System.Windows.Forms.Button AssetDownloader_AssetDownloaderButton;
    private System.ComponentModel.BackgroundWorker AssetLocalization_BackgroundWorker;
    private System.Windows.Forms.CheckBox AssetDownloader_BatchMode;
    private System.Windows.Forms.TextBox AssetDownloaderBatch_BatchIDBox;
    private System.Windows.Forms.Label AssetDownloaderBatch_Status;
    private System.Windows.Forms.Label AssetDownloaderBatch_Note;
    private System.Windows.Forms.Label CustomDLURLLabel;
    private System.Windows.Forms.TextBox URLOverrideBox;
    private System.Windows.Forms.Label URLListLabel;
    private System.Windows.Forms.CheckBox AssetLocalization_LocalizePermanentlyBox;
    private System.Windows.Forms.ComboBox MeshConverter_MeshVersionSelector;
    private System.Windows.Forms.CheckBox AssetLocalization_AssetLinks;
}