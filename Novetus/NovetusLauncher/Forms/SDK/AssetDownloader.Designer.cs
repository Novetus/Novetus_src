partial class AssetDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetDownloader));
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
            ((System.ComponentModel.ISupportInitialize)(this.AssetDownloader_AssetVersionSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // AssetDownloaderBatch_Note
            // 
            this.AssetDownloaderBatch_Note.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssetDownloaderBatch_Note.ForeColor = System.Drawing.Color.Red;
            this.AssetDownloaderBatch_Note.Location = new System.Drawing.Point(13, 150);
            this.AssetDownloaderBatch_Note.Name = "AssetDownloaderBatch_Note";
            this.AssetDownloaderBatch_Note.Size = new System.Drawing.Size(241, 42);
            this.AssetDownloaderBatch_Note.TabIndex = 23;
            this.AssetDownloaderBatch_Note.Text = "You must enter in each item as <Name>|<ID>|<Version>. \r\nExample: RedTopHat|297230" +
    "2|1";
            this.AssetDownloaderBatch_Note.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AssetDownloaderBatch_Note.Visible = false;
            // 
            // AssetDownloaderBatch_Status
            // 
            this.AssetDownloaderBatch_Status.Cursor = System.Windows.Forms.Cursors.Default;
            this.AssetDownloaderBatch_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssetDownloaderBatch_Status.ForeColor = System.Drawing.Color.Red;
            this.AssetDownloaderBatch_Status.Location = new System.Drawing.Point(12, 393);
            this.AssetDownloaderBatch_Status.Name = "AssetDownloaderBatch_Status";
            this.AssetDownloaderBatch_Status.Size = new System.Drawing.Size(241, 13);
            this.AssetDownloaderBatch_Status.TabIndex = 1;
            this.AssetDownloaderBatch_Status.Text = "Please wait...";
            this.AssetDownloaderBatch_Status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AssetDownloaderBatch_Status.Visible = false;
            // 
            // AssetDownloader_BatchMode
            // 
            this.AssetDownloader_BatchMode.Location = new System.Drawing.Point(165, 102);
            this.AssetDownloader_BatchMode.Name = "AssetDownloader_BatchMode";
            this.AssetDownloader_BatchMode.Size = new System.Drawing.Size(88, 23);
            this.AssetDownloader_BatchMode.TabIndex = 22;
            this.AssetDownloader_BatchMode.Text = "Batch Mode";
            this.AssetDownloader_BatchMode.UseVisualStyleBackColor = true;
            this.AssetDownloader_BatchMode.CheckedChanged += new System.EventHandler(this.AssetDownloader_BatchMode_CheckedChanged);
            // 
            // AssetDownloaderBatch_BatchIDBox
            // 
            this.AssetDownloaderBatch_BatchIDBox.Enabled = false;
            this.AssetDownloaderBatch_BatchIDBox.Location = new System.Drawing.Point(12, 199);
            this.AssetDownloaderBatch_BatchIDBox.Multiline = true;
            this.AssetDownloaderBatch_BatchIDBox.Name = "AssetDownloaderBatch_BatchIDBox";
            this.AssetDownloaderBatch_BatchIDBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.AssetDownloaderBatch_BatchIDBox.Size = new System.Drawing.Size(242, 185);
            this.AssetDownloaderBatch_BatchIDBox.TabIndex = 0;
            // 
            // AssetDownloader_AssetNameBox
            // 
            this.AssetDownloader_AssetNameBox.Location = new System.Drawing.Point(23, 22);
            this.AssetDownloader_AssetNameBox.Name = "AssetDownloader_AssetNameBox";
            this.AssetDownloader_AssetNameBox.Size = new System.Drawing.Size(76, 20);
            this.AssetDownloader_AssetNameBox.TabIndex = 21;
            // 
            // AssetDownloader_AssetNameText
            // 
            this.AssetDownloader_AssetNameText.Location = new System.Drawing.Point(44, 6);
            this.AssetDownloader_AssetNameText.Name = "AssetDownloader_AssetNameText";
            this.AssetDownloader_AssetNameText.Size = new System.Drawing.Size(35, 14);
            this.AssetDownloader_AssetNameText.TabIndex = 20;
            this.AssetDownloader_AssetNameText.Text = "Name";
            // 
            // AssetDownloader_AssetVersionText
            // 
            this.AssetDownloader_AssetVersionText.Location = new System.Drawing.Point(201, 6);
            this.AssetDownloader_AssetVersionText.Name = "AssetDownloader_AssetVersionText";
            this.AssetDownloader_AssetVersionText.Size = new System.Drawing.Size(55, 14);
            this.AssetDownloader_AssetVersionText.TabIndex = 17;
            this.AssetDownloader_AssetVersionText.Text = "Version";
            this.AssetDownloader_AssetVersionText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AssetDownloader_AssetIDText
            // 
            this.AssetDownloader_AssetIDText.Location = new System.Drawing.Point(127, 6);
            this.AssetDownloader_AssetIDText.Name = "AssetDownloader_AssetIDText";
            this.AssetDownloader_AssetIDText.Size = new System.Drawing.Size(41, 14);
            this.AssetDownloader_AssetIDText.TabIndex = 16;
            this.AssetDownloader_AssetIDText.Text = "Item ID";
            // 
            // AssetDownloader_AssetVersionSelector
            // 
            this.AssetDownloader_AssetVersionSelector.Location = new System.Drawing.Point(204, 22);
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
            this.AssetDownloader_AssetIDBox.Location = new System.Drawing.Point(109, 22);
            this.AssetDownloader_AssetIDBox.Name = "AssetDownloader_AssetIDBox";
            this.AssetDownloader_AssetIDBox.Size = new System.Drawing.Size(76, 20);
            this.AssetDownloader_AssetIDBox.TabIndex = 14;
            // 
            // AssetDownloader_AssetDownloaderButton
            // 
            this.AssetDownloader_AssetDownloaderButton.Location = new System.Drawing.Point(12, 124);
            this.AssetDownloader_AssetDownloaderButton.Name = "AssetDownloader_AssetDownloaderButton";
            this.AssetDownloader_AssetDownloaderButton.Size = new System.Drawing.Size(241, 23);
            this.AssetDownloader_AssetDownloaderButton.TabIndex = 13;
            this.AssetDownloader_AssetDownloaderButton.Text = "Download!";
            this.AssetDownloader_AssetDownloaderButton.UseVisualStyleBackColor = true;
            this.AssetDownloader_AssetDownloaderButton.Click += new System.EventHandler(this.AssetDownloader_AssetDownloaderButton_Click);
            // 
            // AssetDownloader_LoadHelpMessage
            // 
            this.AssetDownloader_LoadHelpMessage.Location = new System.Drawing.Point(18, 102);
            this.AssetDownloader_LoadHelpMessage.Name = "AssetDownloader_LoadHelpMessage";
            this.AssetDownloader_LoadHelpMessage.Size = new System.Drawing.Size(147, 23);
            this.AssetDownloader_LoadHelpMessage.TabIndex = 19;
            this.AssetDownloader_LoadHelpMessage.Text = "Disable Help Messages";
            this.AssetDownloader_LoadHelpMessage.UseVisualStyleBackColor = true;
            this.AssetDownloader_LoadHelpMessage.CheckedChanged += new System.EventHandler(this.AssetDownloader_LoadHelpMessage_CheckedChanged);
            // 
            // CustomDLURLLabel
            // 
            this.CustomDLURLLabel.Location = new System.Drawing.Point(5, 72);
            this.CustomDLURLLabel.Name = "CustomDLURLLabel";
            this.CustomDLURLLabel.Size = new System.Drawing.Size(67, 27);
            this.CustomDLURLLabel.TabIndex = 26;
            this.CustomDLURLLabel.Text = "Custom URL\r\n(Optional):";
            this.CustomDLURLLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // URLOverrideBox
            // 
            this.URLOverrideBox.Location = new System.Drawing.Point(72, 75);
            this.URLOverrideBox.Name = "URLOverrideBox";
            this.URLOverrideBox.Size = new System.Drawing.Size(185, 20);
            this.URLOverrideBox.TabIndex = 25;
            this.URLOverrideBox.Click += new System.EventHandler(this.URLOverrideBox_Click);
            this.URLOverrideBox.TextChanged += new System.EventHandler(this.URLOverrideBox_TextChanged);
            // 
            // URLListLabel
            // 
            this.URLListLabel.AutoSize = true;
            this.URLListLabel.Location = new System.Drawing.Point(18, 51);
            this.URLListLabel.Name = "URLListLabel";
            this.URLListLabel.Size = new System.Drawing.Size(32, 13);
            this.URLListLabel.TabIndex = 24;
            this.URLListLabel.Text = "URL:";
            // 
            // URLSelection
            // 
            this.URLSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.URLSelection.DropDownWidth = 300;
            this.URLSelection.FormattingEnabled = true;
            this.URLSelection.Location = new System.Drawing.Point(72, 48);
            this.URLSelection.Name = "URLSelection";
            this.URLSelection.Size = new System.Drawing.Size(185, 21);
            this.URLSelection.TabIndex = 18;
            this.URLSelection.SelectedIndexChanged += new System.EventHandler(this.URLSelection_SelectedIndexChanged);
            // 
            // AssetDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(265, 415);
            this.Controls.Add(this.AssetDownloaderBatch_Note);
            this.Controls.Add(this.URLOverrideBox);
            this.Controls.Add(this.AssetDownloaderBatch_Status);
            this.Controls.Add(this.CustomDLURLLabel);
            this.Controls.Add(this.AssetDownloader_BatchMode);
            this.Controls.Add(this.URLListLabel);
            this.Controls.Add(this.AssetDownloaderBatch_BatchIDBox);
            this.Controls.Add(this.AssetDownloader_AssetNameBox);
            this.Controls.Add(this.AssetDownloader_AssetNameText);
            this.Controls.Add(this.AssetDownloader_LoadHelpMessage);
            this.Controls.Add(this.AssetDownloader_AssetVersionText);
            this.Controls.Add(this.URLSelection);
            this.Controls.Add(this.AssetDownloader_AssetIDText);
            this.Controls.Add(this.AssetDownloader_AssetDownloaderButton);
            this.Controls.Add(this.AssetDownloader_AssetVersionSelector);
            this.Controls.Add(this.AssetDownloader_AssetIDBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AssetDownloader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asset Downloader";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AssetSDK_Close);
            this.Load += new System.EventHandler(this.AssetSDK_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AssetDownloader_AssetVersionSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox AssetDownloader_AssetNameBox;
    private System.Windows.Forms.Label AssetDownloader_AssetNameText;
    private System.Windows.Forms.CheckBox AssetDownloader_LoadHelpMessage;
    private System.Windows.Forms.ComboBox URLSelection;
    private System.Windows.Forms.Label AssetDownloader_AssetVersionText;
    private System.Windows.Forms.Label AssetDownloader_AssetIDText;
    private System.Windows.Forms.NumericUpDown AssetDownloader_AssetVersionSelector;
    private System.Windows.Forms.TextBox AssetDownloader_AssetIDBox;
    private System.Windows.Forms.Button AssetDownloader_AssetDownloaderButton;
    private System.Windows.Forms.CheckBox AssetDownloader_BatchMode;
    private System.Windows.Forms.TextBox AssetDownloaderBatch_BatchIDBox;
    private System.Windows.Forms.Label AssetDownloaderBatch_Status;
    private System.Windows.Forms.Label AssetDownloaderBatch_Note;
    private System.Windows.Forms.Label CustomDLURLLabel;
    private System.Windows.Forms.TextBox URLOverrideBox;
    private System.Windows.Forms.Label URLListLabel;
}