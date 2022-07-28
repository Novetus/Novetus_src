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
            this.AddonFilesListing = new System.Windows.Forms.ListBox();
            this.SavePackageButton = new System.Windows.Forms.Button();
            this.FileListingLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddonFilesListing
            // 
            this.AddonFilesListing.FormattingEnabled = true;
            this.AddonFilesListing.Location = new System.Drawing.Point(10, 25);
            this.AddonFilesListing.Name = "AddonFilesListing";
            this.AddonFilesListing.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.AddonFilesListing.Size = new System.Drawing.Size(407, 303);
            this.AddonFilesListing.TabIndex = 0;
            // 
            // SavePackageButton
            // 
            this.SavePackageButton.Location = new System.Drawing.Point(10, 334);
            this.SavePackageButton.Name = "SavePackageButton";
            this.SavePackageButton.Size = new System.Drawing.Size(407, 23);
            this.SavePackageButton.TabIndex = 4;
            this.SavePackageButton.Text = "Save Package";
            this.SavePackageButton.UseVisualStyleBackColor = true;
            this.SavePackageButton.Click += new System.EventHandler(this.SavePackageButton_Click);
            // 
            // FileListingLabel
            // 
            this.FileListingLabel.AutoSize = true;
            this.FileListingLabel.Location = new System.Drawing.Point(6, 7);
            this.FileListingLabel.Name = "FileListingLabel";
            this.FileListingLabel.Size = new System.Drawing.Size(416, 13);
            this.FileListingLabel.TabIndex = 5;
            this.FileListingLabel.Text = "Select which files you wish to include in your addon, then click \"Save Package\" b" +
    "elow";
            // 
            // ModCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(429, 364);
            this.Controls.Add(this.FileListingLabel);
            this.Controls.Add(this.SavePackageButton);
            this.Controls.Add(this.AddonFilesListing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ModCreator";
            this.Text = "Mod Package Creator";
            this.Load += new System.EventHandler(this.ModCreator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox AddonFilesListing;
    private System.Windows.Forms.Button SavePackageButton;
    private System.Windows.Forms.Label FileListingLabel;
}