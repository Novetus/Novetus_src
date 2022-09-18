partial class AssetFixer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetFixer));
            this.CustomDLURLLabel = new System.Windows.Forms.Label();
            this.URLOverrideBox = new System.Windows.Forms.TextBox();
            this.URLListLabel = new System.Windows.Forms.Label();
            this.URLSelection = new System.Windows.Forms.ComboBox();
            this.AssetFixer_ProgressLabel = new System.Windows.Forms.Label();
            this.AssetFixer_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.AssetLocalization_AssetLinks = new System.Windows.Forms.CheckBox();
            this.AssetLocalization_LocalizePermanentlyBox = new System.Windows.Forms.CheckBox();
            this.AssetLocalization_SaveBackups = new System.Windows.Forms.CheckBox();
            this.AssetLocalization_AssetTypeText = new System.Windows.Forms.Label();
            this.AssetLocalization_FolderNameText = new System.Windows.Forms.Label();
            this.AssetLocalization_CustomFolderNameBox = new System.Windows.Forms.TextBox();
            this.AssetLocalization_AssetTypeBox = new System.Windows.Forms.ComboBox();
            this.AssetLocalization_LocalizeButton = new System.Windows.Forms.Button();
            this.AssetLocalization_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.AssetFixerOrganizer = new System.Windows.Forms.TabControl();
            this.LocalTab = new System.Windows.Forms.TabPage();
            this.URLTab = new System.Windows.Forms.TabPage();
            this.AssetFixerOrganizer.SuspendLayout();
            this.LocalTab.SuspendLayout();
            this.URLTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // CustomDLURLLabel
            // 
            this.CustomDLURLLabel.Location = new System.Drawing.Point(6, 48);
            this.CustomDLURLLabel.Name = "CustomDLURLLabel";
            this.CustomDLURLLabel.Size = new System.Drawing.Size(67, 27);
            this.CustomDLURLLabel.TabIndex = 26;
            this.CustomDLURLLabel.Text = "Custom URL\r\n(Optional)\r\n:";
            this.CustomDLURLLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // URLOverrideBox
            // 
            this.URLOverrideBox.Enabled = false;
            this.URLOverrideBox.Location = new System.Drawing.Point(75, 51);
            this.URLOverrideBox.Name = "URLOverrideBox";
            this.URLOverrideBox.Size = new System.Drawing.Size(197, 20);
            this.URLOverrideBox.TabIndex = 25;
            this.URLOverrideBox.Click += new System.EventHandler(this.URLOverrideBox_Click);
            this.URLOverrideBox.TextChanged += new System.EventHandler(this.URLOverrideBox_TextChanged);
            // 
            // URLListLabel
            // 
            this.URLListLabel.AutoSize = true;
            this.URLListLabel.Location = new System.Drawing.Point(21, 27);
            this.URLListLabel.Name = "URLListLabel";
            this.URLListLabel.Size = new System.Drawing.Size(32, 13);
            this.URLListLabel.TabIndex = 24;
            this.URLListLabel.Text = "URL:";
            // 
            // URLSelection
            // 
            this.URLSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.URLSelection.DropDownWidth = 242;
            this.URLSelection.Enabled = false;
            this.URLSelection.FormattingEnabled = true;
            this.URLSelection.Location = new System.Drawing.Point(75, 24);
            this.URLSelection.Name = "URLSelection";
            this.URLSelection.Size = new System.Drawing.Size(197, 21);
            this.URLSelection.TabIndex = 18;
            this.URLSelection.SelectedIndexChanged += new System.EventHandler(this.URLSelection_SelectedIndexChanged);
            // 
            // AssetFixer_ProgressLabel
            // 
            this.AssetFixer_ProgressLabel.Location = new System.Drawing.Point(8, 214);
            this.AssetFixer_ProgressLabel.Name = "AssetFixer_ProgressLabel";
            this.AssetFixer_ProgressLabel.Size = new System.Drawing.Size(278, 18);
            this.AssetFixer_ProgressLabel.TabIndex = 24;
            this.AssetFixer_ProgressLabel.Text = "Idle";
            this.AssetFixer_ProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AssetFixer_ProgressBar
            // 
            this.AssetFixer_ProgressBar.Location = new System.Drawing.Point(8, 188);
            this.AssetFixer_ProgressBar.Name = "AssetFixer_ProgressBar";
            this.AssetFixer_ProgressBar.Size = new System.Drawing.Size(278, 23);
            this.AssetFixer_ProgressBar.TabIndex = 23;
            // 
            // AssetLocalization_AssetLinks
            // 
            this.AssetLocalization_AssetLinks.AutoSize = true;
            this.AssetLocalization_AssetLinks.Location = new System.Drawing.Point(64, 3);
            this.AssetLocalization_AssetLinks.Name = "AssetLocalization_AssetLinks";
            this.AssetLocalization_AssetLinks.Size = new System.Drawing.Size(163, 17);
            this.AssetLocalization_AssetLinks.TabIndex = 22;
            this.AssetLocalization_AssetLinks.Text = "Replace URL Only (Optional)";
            this.AssetLocalization_AssetLinks.UseVisualStyleBackColor = true;
            this.AssetLocalization_AssetLinks.CheckedChanged += new System.EventHandler(this.AssetLocalization_AssetLinks_CheckedChanged);
            // 
            // AssetLocalization_LocalizePermanentlyBox
            // 
            this.AssetLocalization_LocalizePermanentlyBox.AutoSize = true;
            this.AssetLocalization_LocalizePermanentlyBox.Location = new System.Drawing.Point(45, 3);
            this.AssetLocalization_LocalizePermanentlyBox.Name = "AssetLocalization_LocalizePermanentlyBox";
            this.AssetLocalization_LocalizePermanentlyBox.Size = new System.Drawing.Size(196, 17);
            this.AssetLocalization_LocalizePermanentlyBox.TabIndex = 21;
            this.AssetLocalization_LocalizePermanentlyBox.Text = "Download To Data Folder (Optional)";
            this.AssetLocalization_LocalizePermanentlyBox.UseVisualStyleBackColor = true;
            this.AssetLocalization_LocalizePermanentlyBox.CheckedChanged += new System.EventHandler(this.AssetLocalization_LocalizePermanentlyBox_CheckedChanged);
            this.AssetLocalization_LocalizePermanentlyBox.Click += new System.EventHandler(this.AssetLocalization_LocalizePermanentlyBox_Click);
            // 
            // AssetLocalization_SaveBackups
            // 
            this.AssetLocalization_SaveBackups.AutoSize = true;
            this.AssetLocalization_SaveBackups.Location = new System.Drawing.Point(104, 111);
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
            this.AssetLocalization_AssetTypeText.Location = new System.Drawing.Point(3, 137);
            this.AssetLocalization_AssetTypeText.Name = "AssetLocalization_AssetTypeText";
            this.AssetLocalization_AssetTypeText.Size = new System.Drawing.Size(63, 13);
            this.AssetLocalization_AssetTypeText.TabIndex = 18;
            this.AssetLocalization_AssetTypeText.Text = "Asset Type:";
            // 
            // AssetLocalization_FolderNameText
            // 
            this.AssetLocalization_FolderNameText.AutoSize = true;
            this.AssetLocalization_FolderNameText.Location = new System.Drawing.Point(6, 33);
            this.AssetLocalization_FolderNameText.Name = "AssetLocalization_FolderNameText";
            this.AssetLocalization_FolderNameText.Size = new System.Drawing.Size(108, 26);
            this.AssetLocalization_FolderNameText.TabIndex = 15;
            this.AssetLocalization_FolderNameText.Text = "Custom Folder Name \r\n(Optional):";
            // 
            // AssetLocalization_CustomFolderNameBox
            // 
            this.AssetLocalization_CustomFolderNameBox.Enabled = false;
            this.AssetLocalization_CustomFolderNameBox.Location = new System.Drawing.Point(120, 32);
            this.AssetLocalization_CustomFolderNameBox.Name = "AssetLocalization_CustomFolderNameBox";
            this.AssetLocalization_CustomFolderNameBox.Size = new System.Drawing.Size(152, 20);
            this.AssetLocalization_CustomFolderNameBox.TabIndex = 14;
            this.AssetLocalization_CustomFolderNameBox.TextChanged += new System.EventHandler(this.AssetLocalization_ItemNameBox_TextChanged);
            // 
            // AssetLocalization_AssetTypeBox
            // 
            this.AssetLocalization_AssetTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AssetLocalization_AssetTypeBox.FormattingEnabled = true;
            this.AssetLocalization_AssetTypeBox.Items.AddRange(new object[] {
            "RBXL",
            "RBXM",
            "Lua Script"});
            this.AssetLocalization_AssetTypeBox.Location = new System.Drawing.Point(72, 134);
            this.AssetLocalization_AssetTypeBox.Name = "AssetLocalization_AssetTypeBox";
            this.AssetLocalization_AssetTypeBox.Size = new System.Drawing.Size(214, 21);
            this.AssetLocalization_AssetTypeBox.TabIndex = 12;
            this.AssetLocalization_AssetTypeBox.SelectedIndexChanged += new System.EventHandler(this.AssetLocalization_AssetTypeBox_SelectedIndexChanged);
            // 
            // AssetLocalization_LocalizeButton
            // 
            this.AssetLocalization_LocalizeButton.Location = new System.Drawing.Point(8, 161);
            this.AssetLocalization_LocalizeButton.Name = "AssetLocalization_LocalizeButton";
            this.AssetLocalization_LocalizeButton.Size = new System.Drawing.Size(278, 21);
            this.AssetLocalization_LocalizeButton.TabIndex = 11;
            this.AssetLocalization_LocalizeButton.Text = "Browse and Localize Model/Place";
            this.AssetLocalization_LocalizeButton.UseVisualStyleBackColor = true;
            this.AssetLocalization_LocalizeButton.Click += new System.EventHandler(this.AssetLocalization_LocalizeButton_Click);
            // 
            // AssetLocalization_BackgroundWorker
            // 
            this.AssetLocalization_BackgroundWorker.WorkerReportsProgress = true;
            this.AssetLocalization_BackgroundWorker.WorkerSupportsCancellation = true;
            this.AssetLocalization_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AssetLocalization_BackgroundWorker_DoWork);
            this.AssetLocalization_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AssetLocalization_BackgroundWorker_ProgressChanged);
            this.AssetLocalization_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AssetLocalization_BackgroundWorker_RunWorkerCompleted);
            // 
            // AssetFixerOrganizer
            // 
            this.AssetFixerOrganizer.Controls.Add(this.LocalTab);
            this.AssetFixerOrganizer.Controls.Add(this.URLTab);
            this.AssetFixerOrganizer.Location = new System.Drawing.Point(4, 2);
            this.AssetFixerOrganizer.Name = "AssetFixerOrganizer";
            this.AssetFixerOrganizer.SelectedIndex = 0;
            this.AssetFixerOrganizer.Size = new System.Drawing.Size(286, 106);
            this.AssetFixerOrganizer.TabIndex = 27;
            // 
            // LocalTab
            // 
            this.LocalTab.Controls.Add(this.AssetLocalization_LocalizePermanentlyBox);
            this.LocalTab.Controls.Add(this.AssetLocalization_FolderNameText);
            this.LocalTab.Controls.Add(this.AssetLocalization_CustomFolderNameBox);
            this.LocalTab.Location = new System.Drawing.Point(4, 22);
            this.LocalTab.Name = "LocalTab";
            this.LocalTab.Padding = new System.Windows.Forms.Padding(3);
            this.LocalTab.Size = new System.Drawing.Size(278, 80);
            this.LocalTab.TabIndex = 0;
            this.LocalTab.Text = "MAKE PLACE LOCAL";
            this.LocalTab.UseVisualStyleBackColor = true;
            // 
            // URLTab
            // 
            this.URLTab.Controls.Add(this.AssetLocalization_AssetLinks);
            this.URLTab.Controls.Add(this.URLOverrideBox);
            this.URLTab.Controls.Add(this.URLSelection);
            this.URLTab.Controls.Add(this.URLListLabel);
            this.URLTab.Controls.Add(this.CustomDLURLLabel);
            this.URLTab.Location = new System.Drawing.Point(4, 22);
            this.URLTab.Name = "URLTab";
            this.URLTab.Padding = new System.Windows.Forms.Padding(3);
            this.URLTab.Size = new System.Drawing.Size(278, 80);
            this.URLTab.TabIndex = 1;
            this.URLTab.Text = "MAKE PLACE ONLINE";
            this.URLTab.UseVisualStyleBackColor = true;
            // 
            // AssetFixer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(296, 241);
            this.Controls.Add(this.AssetFixerOrganizer);
            this.Controls.Add(this.AssetFixer_ProgressLabel);
            this.Controls.Add(this.AssetFixer_ProgressBar);
            this.Controls.Add(this.AssetLocalization_SaveBackups);
            this.Controls.Add(this.AssetLocalization_AssetTypeText);
            this.Controls.Add(this.AssetLocalization_LocalizeButton);
            this.Controls.Add(this.AssetLocalization_AssetTypeBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AssetFixer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asset Fixer";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AssetSDK_Close);
            this.Load += new System.EventHandler(this.AssetSDK_Load);
            this.AssetFixerOrganizer.ResumeLayout(false);
            this.LocalTab.ResumeLayout(false);
            this.LocalTab.PerformLayout();
            this.URLTab.ResumeLayout(false);
            this.URLTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.CheckBox AssetLocalization_SaveBackups;
    private System.Windows.Forms.Label AssetLocalization_AssetTypeText;
    private System.Windows.Forms.Label AssetLocalization_FolderNameText;
    private System.Windows.Forms.TextBox AssetLocalization_CustomFolderNameBox;
    private System.Windows.Forms.ComboBox AssetLocalization_AssetTypeBox;
    private System.Windows.Forms.Button AssetLocalization_LocalizeButton;
    private System.Windows.Forms.ComboBox URLSelection;
    private System.ComponentModel.BackgroundWorker AssetLocalization_BackgroundWorker;
    private System.Windows.Forms.Label CustomDLURLLabel;
    private System.Windows.Forms.TextBox URLOverrideBox;
    private System.Windows.Forms.Label URLListLabel;
    private System.Windows.Forms.CheckBox AssetLocalization_LocalizePermanentlyBox;
    private System.Windows.Forms.CheckBox AssetLocalization_AssetLinks;
    private System.Windows.Forms.ProgressBar AssetFixer_ProgressBar;
    private System.Windows.Forms.Label AssetFixer_ProgressLabel;
    private System.Windows.Forms.TabControl AssetFixerOrganizer;
    private System.Windows.Forms.TabPage LocalTab;
    private System.Windows.Forms.TabPage URLTab;
}