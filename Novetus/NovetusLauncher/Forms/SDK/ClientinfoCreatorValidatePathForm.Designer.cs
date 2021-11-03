
partial class ClientinfoCreatorValidatePathForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientinfoCreatorValidatePathForm));
            this.TextEntry = new System.Windows.Forms.TextBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextEntry
            // 
            this.TextEntry.Location = new System.Drawing.Point(12, 25);
            this.TextEntry.Name = "TextEntry";
            this.TextEntry.Size = new System.Drawing.Size(295, 20);
            this.TextEntry.TabIndex = 0;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(75, 9);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(179, 13);
            this.infoLabel.TabIndex = 1;
            this.infoLabel.Text = "Enter path relative to client directory.";
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(122, 51);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "OK";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // ClientinfoCreatorValidatePathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(320, 80);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.TextEntry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClientinfoCreatorValidatePathForm";
            this.Text = "Add Validate Tags for Relative Path";
            this.Load += new System.EventHandler(this.ClientinfoCreatorValidatePathForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.TextBox TextEntry;
    private System.Windows.Forms.Label infoLabel;
    private System.Windows.Forms.Button applyButton;
}