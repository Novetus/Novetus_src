
partial class ItemCreationSDKColorMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemCreationSDKColorMenu));
            this.colorMenu = new System.Windows.Forms.ListView();
            this.Note = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // colorMenu
            // 
            this.colorMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorMenu.HideSelection = false;
            this.colorMenu.Location = new System.Drawing.Point(12, 12);
            this.colorMenu.MultiSelect = false;
            this.colorMenu.Name = "colorMenu";
            this.colorMenu.Size = new System.Drawing.Size(666, 385);
            this.colorMenu.TabIndex = 1;
            this.colorMenu.UseCompatibleStateImageBehavior = false;
            this.colorMenu.SelectedIndexChanged += new System.EventHandler(this.colorMenu_SelectedIndexChanged);
            // 
            // Note
            // 
            this.Note.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Note.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Note.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Note.ForeColor = System.Drawing.Color.Red;
            this.Note.Location = new System.Drawing.Point(21, 410);
            this.Note.Name = "Note";
            this.Note.Size = new System.Drawing.Size(643, 13);
            this.Note.TabIndex = 85;
            this.Note.Text = "Select a color to use for your item! The window will close and automatically appl" +
    "y it.";
            this.Note.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ItemCreationSDKColorMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(690, 432);
            this.Controls.Add(this.Note);
            this.Controls.Add(this.colorMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(578, 213);
            this.Name = "ItemCreationSDKColorMenu";
            this.Text = "Color Menu";
            this.Load += new System.EventHandler(this.ItemCreationSDKColorMenu_Load);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView colorMenu;
    private System.Windows.Forms.Label Note;
}