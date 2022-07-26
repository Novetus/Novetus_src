partial class MeshConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeshConverter));
            this.MeshConverter_MeshVersionSelector = new System.Windows.Forms.ComboBox();
            this.MeshConverter_StatusText = new System.Windows.Forms.Label();
            this.MeshConverter_MeshVersionText = new System.Windows.Forms.Label();
            this.MeshConverter_CreditText = new System.Windows.Forms.Label();
            this.MeshConverter_ConvertButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MeshConverter_MeshVersionSelector
            // 
            this.MeshConverter_MeshVersionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MeshConverter_MeshVersionSelector.FormattingEnabled = true;
            this.MeshConverter_MeshVersionSelector.Items.AddRange(new object[] {
            "1.00",
            "1.01",
            "2.00"});
            this.MeshConverter_MeshVersionSelector.Location = new System.Drawing.Point(147, 6);
            this.MeshConverter_MeshVersionSelector.Name = "MeshConverter_MeshVersionSelector";
            this.MeshConverter_MeshVersionSelector.Size = new System.Drawing.Size(105, 21);
            this.MeshConverter_MeshVersionSelector.TabIndex = 11;
            // 
            // MeshConverter_StatusText
            // 
            this.MeshConverter_StatusText.Location = new System.Drawing.Point(11, 59);
            this.MeshConverter_StatusText.Name = "MeshConverter_StatusText";
            this.MeshConverter_StatusText.Size = new System.Drawing.Size(261, 14);
            this.MeshConverter_StatusText.TabIndex = 10;
            this.MeshConverter_StatusText.Text = "Ready";
            this.MeshConverter_StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MeshConverter_MeshVersionText
            // 
            this.MeshConverter_MeshVersionText.AutoSize = true;
            this.MeshConverter_MeshVersionText.Location = new System.Drawing.Point(67, 9);
            this.MeshConverter_MeshVersionText.Name = "MeshConverter_MeshVersionText";
            this.MeshConverter_MeshVersionText.Size = new System.Drawing.Size(74, 13);
            this.MeshConverter_MeshVersionText.TabIndex = 8;
            this.MeshConverter_MeshVersionText.Text = "Mesh Version:";
            // 
            // MeshConverter_CreditText
            // 
            this.MeshConverter_CreditText.AutoSize = true;
            this.MeshConverter_CreditText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MeshConverter_CreditText.Location = new System.Drawing.Point(15, 82);
            this.MeshConverter_CreditText.Name = "MeshConverter_CreditText";
            this.MeshConverter_CreditText.Size = new System.Drawing.Size(261, 12);
            this.MeshConverter_CreditText.TabIndex = 7;
            this.MeshConverter_CreditText.Text = "ObjToRBXMesh built by coke. Modified to support old meshes.";
            // 
            // MeshConverter_ConvertButton
            // 
            this.MeshConverter_ConvertButton.Location = new System.Drawing.Point(15, 33);
            this.MeshConverter_ConvertButton.Name = "MeshConverter_ConvertButton";
            this.MeshConverter_ConvertButton.Size = new System.Drawing.Size(258, 23);
            this.MeshConverter_ConvertButton.TabIndex = 6;
            this.MeshConverter_ConvertButton.Text = "Browse for mesh and convert...";
            this.MeshConverter_ConvertButton.UseVisualStyleBackColor = true;
            this.MeshConverter_ConvertButton.Click += new System.EventHandler(this.MeshConverter_ConvertButton_Click);
            // 
            // MeshConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(287, 106);
            this.Controls.Add(this.MeshConverter_MeshVersionSelector);
            this.Controls.Add(this.MeshConverter_StatusText);
            this.Controls.Add(this.MeshConverter_MeshVersionText);
            this.Controls.Add(this.MeshConverter_ConvertButton);
            this.Controls.Add(this.MeshConverter_CreditText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MeshConverter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mesh Converter";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AssetSDK_Close);
            this.Load += new System.EventHandler(this.AssetSDK_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label MeshConverter_StatusText;
    private System.Windows.Forms.Label MeshConverter_MeshVersionText;
    private System.Windows.Forms.Label MeshConverter_CreditText;
    private System.Windows.Forms.Button MeshConverter_ConvertButton;
    private System.Windows.Forms.ComboBox MeshConverter_MeshVersionSelector;
}