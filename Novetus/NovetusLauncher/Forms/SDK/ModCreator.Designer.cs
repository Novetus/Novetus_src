partial class ModCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModCreator));
            this.ModFilesListing = new System.Windows.Forms.ListBox();
            this.SavePackageButton = new System.Windows.Forms.Button();
            this.FileListingLabel = new System.Windows.Forms.Label();
            this.ModNameBox = new System.Windows.Forms.TextBox();
            this.ModNameLabel = new System.Windows.Forms.Label();
            this.RefreshFileListButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ModFilesListing
            // 
            this.ModFilesListing.FormattingEnabled = true;
            this.ModFilesListing.Location = new System.Drawing.Point(10, 56);
            this.ModFilesListing.Name = "ModFilesListing";
            this.ModFilesListing.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ModFilesListing.Size = new System.Drawing.Size(407, 316);
            this.ModFilesListing.TabIndex = 0;
            // 
            // SavePackageButton
            // 
            this.SavePackageButton.Location = new System.Drawing.Point(10, 378);
            this.SavePackageButton.Name = "SavePackageButton";
            this.SavePackageButton.Size = new System.Drawing.Size(308, 23);
            this.SavePackageButton.TabIndex = 4;
            this.SavePackageButton.Text = "Save Package";
            this.SavePackageButton.UseVisualStyleBackColor = true;
            this.SavePackageButton.Click += new System.EventHandler(this.SavePackageButton_Click);
            // 
            // FileListingLabel
            // 
            this.FileListingLabel.AutoSize = true;
            this.FileListingLabel.Location = new System.Drawing.Point(11, 35);
            this.FileListingLabel.Name = "FileListingLabel";
            this.FileListingLabel.Size = new System.Drawing.Size(406, 13);
            this.FileListingLabel.TabIndex = 5;
            this.FileListingLabel.Text = "Select which files you wish to include in your mod, then click \"Save Package\" bel" +
    "ow";
            // 
            // ModNameBox
            // 
            this.ModNameBox.Location = new System.Drawing.Point(80, 9);
            this.ModNameBox.Name = "ModNameBox";
            this.ModNameBox.Size = new System.Drawing.Size(337, 20);
            this.ModNameBox.TabIndex = 6;
            // 
            // ModNameLabel
            // 
            this.ModNameLabel.AutoSize = true;
            this.ModNameLabel.Location = new System.Drawing.Point(12, 12);
            this.ModNameLabel.Name = "ModNameLabel";
            this.ModNameLabel.Size = new System.Drawing.Size(62, 13);
            this.ModNameLabel.TabIndex = 7;
            this.ModNameLabel.Text = "Mod Name:";
            // 
            // RefreshFileListButton
            // 
            this.RefreshFileListButton.Location = new System.Drawing.Point(324, 378);
            this.RefreshFileListButton.Name = "RefreshFileListButton";
            this.RefreshFileListButton.Size = new System.Drawing.Size(93, 23);
            this.RefreshFileListButton.TabIndex = 8;
            this.RefreshFileListButton.Text = "Refresh File List";
            this.RefreshFileListButton.UseVisualStyleBackColor = true;
            this.RefreshFileListButton.Click += new System.EventHandler(this.RefreshFileListButton_Click);
            // 
            // ModCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(429, 410);
            this.Controls.Add(this.RefreshFileListButton);
            this.Controls.Add(this.ModNameLabel);
            this.Controls.Add(this.ModNameBox);
            this.Controls.Add(this.FileListingLabel);
            this.Controls.Add(this.SavePackageButton);
            this.Controls.Add(this.ModFilesListing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModCreator";
            this.Text = "Mod Package Creator";
            this.Load += new System.EventHandler(this.ModCreator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox ModFilesListing;
    private System.Windows.Forms.Button SavePackageButton;
    private System.Windows.Forms.Label FileListingLabel;
    private System.Windows.Forms.TextBox ModNameBox;
    private System.Windows.Forms.Label ModNameLabel;
    private System.Windows.Forms.Button RefreshFileListButton;
}