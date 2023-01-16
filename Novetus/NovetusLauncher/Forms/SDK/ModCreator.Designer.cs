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
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DescBox = new System.Windows.Forms.TextBox();
            this.AuthorBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ModFilesListing
            // 
            this.ModFilesListing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModFilesListing.FormattingEnabled = true;
            this.ModFilesListing.Location = new System.Drawing.Point(10, 56);
            this.ModFilesListing.Name = "ModFilesListing";
            this.ModFilesListing.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ModFilesListing.Size = new System.Drawing.Size(417, 173);
            this.ModFilesListing.TabIndex = 0;
            // 
            // SavePackageButton
            // 
            this.SavePackageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePackageButton.Location = new System.Drawing.Point(10, 429);
            this.SavePackageButton.Name = "SavePackageButton";
            this.SavePackageButton.Size = new System.Drawing.Size(318, 23);
            this.SavePackageButton.TabIndex = 4;
            this.SavePackageButton.Text = "Save Package";
            this.SavePackageButton.UseVisualStyleBackColor = true;
            this.SavePackageButton.Click += new System.EventHandler(this.SavePackageButton_Click);
            // 
            // FileListingLabel
            // 
            this.FileListingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileListingLabel.Location = new System.Drawing.Point(11, 35);
            this.FileListingLabel.Name = "FileListingLabel";
            this.FileListingLabel.Size = new System.Drawing.Size(413, 13);
            this.FileListingLabel.TabIndex = 5;
            this.FileListingLabel.Text = "Select which files you wish to include in your mod.";
            this.FileListingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ModNameBox
            // 
            this.ModNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModNameBox.Location = new System.Drawing.Point(80, 9);
            this.ModNameBox.Name = "ModNameBox";
            this.ModNameBox.Size = new System.Drawing.Size(347, 20);
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
            this.RefreshFileListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshFileListButton.Location = new System.Drawing.Point(334, 429);
            this.RefreshFileListButton.Name = "RefreshFileListButton";
            this.RefreshFileListButton.Size = new System.Drawing.Size(93, 23);
            this.RefreshFileListButton.TabIndex = 8;
            this.RefreshFileListButton.Text = "Refresh File List";
            this.RefreshFileListButton.UseVisualStyleBackColor = true;
            this.RefreshFileListButton.Click += new System.EventHandler(this.RefreshFileListButton_Click);
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(7, 243);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(38, 13);
            this.AuthorLabel.TabIndex = 10;
            this.AuthorLabel.Text = "Author";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(184, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Description";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DescBox
            // 
            this.DescBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescBox.Location = new System.Drawing.Point(10, 279);
            this.DescBox.Multiline = true;
            this.DescBox.Name = "DescBox";
            this.DescBox.Size = new System.Drawing.Size(417, 144);
            this.DescBox.TabIndex = 12;
            // 
            // AuthorBox
            // 
            this.AuthorBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AuthorBox.Location = new System.Drawing.Point(51, 240);
            this.AuthorBox.Name = "AuthorBox";
            this.AuthorBox.Size = new System.Drawing.Size(376, 20);
            this.AuthorBox.TabIndex = 14;
            // 
            // ModCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(439, 461);
            this.Controls.Add(this.AuthorBox);
            this.Controls.Add(this.DescBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AuthorLabel);
            this.Controls.Add(this.RefreshFileListButton);
            this.Controls.Add(this.ModNameLabel);
            this.Controls.Add(this.ModNameBox);
            this.Controls.Add(this.FileListingLabel);
            this.Controls.Add(this.SavePackageButton);
            this.Controls.Add(this.ModFilesListing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(455, 500);
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
    private System.Windows.Forms.Label AuthorLabel;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox DescBox;
    private System.Windows.Forms.TextBox AuthorBox;
}