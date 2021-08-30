partial class XMLContentEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMLContentEditor));
            this.XMLStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentProvidersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.partColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XMLView = new System.Windows.Forms.DataGridView();
            this.XMLContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XMLStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XMLView)).BeginInit();
            this.XMLContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // XMLStrip
            // 
            this.XMLStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.XMLStrip.BackColor = System.Drawing.Color.Transparent;
            this.XMLStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.XMLStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.XMLStrip.Location = new System.Drawing.Point(0, 0);
            this.XMLStrip.Name = "XMLStrip";
            this.XMLStrip.Size = new System.Drawing.Size(45, 24);
            this.XMLStrip.TabIndex = 29;
            this.XMLStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentProvidersToolStripMenuItem,
            this.partColorsToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // contentProvidersToolStripMenuItem
            // 
            this.contentProvidersToolStripMenuItem.Name = "contentProvidersToolStripMenuItem";
            this.contentProvidersToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.contentProvidersToolStripMenuItem.Text = "Content Providers";
            this.contentProvidersToolStripMenuItem.Click += new System.EventHandler(this.contentProvidersToolStripMenuItem_Click);
            // 
            // partColorsToolStripMenuItem
            // 
            this.partColorsToolStripMenuItem.Name = "partColorsToolStripMenuItem";
            this.partColorsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.partColorsToolStripMenuItem.Text = "Part Colors";
            this.partColorsToolStripMenuItem.Click += new System.EventHandler(this.partColorsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // XMLView
            // 
            this.XMLView.AllowUserToOrderColumns = true;
            this.XMLView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.XMLView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.XMLView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.XMLView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.XMLView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.XMLView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.XMLView.Location = new System.Drawing.Point(0, 24);
            this.XMLView.Name = "XMLView";
            this.XMLView.Size = new System.Drawing.Size(800, 426);
            this.XMLView.TabIndex = 30;
            this.XMLView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.XMLView_CellMouseUp);
            this.XMLView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.XMLView_MouseClick);
            // 
            // XMLContextMenuStrip
            // 
            this.XMLContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertRowToolStripMenuItem,
            this.deleteRowToolStripMenuItem});
            this.XMLContextMenuStrip.Name = "contextMenuStrip1";
            this.XMLContextMenuStrip.Size = new System.Drawing.Size(134, 48);
            // 
            // insertRowToolStripMenuItem
            // 
            this.insertRowToolStripMenuItem.Name = "insertRowToolStripMenuItem";
            this.insertRowToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.insertRowToolStripMenuItem.Text = "Insert Row";
            this.insertRowToolStripMenuItem.Click += new System.EventHandler(this.insertRowToolStripMenuItem_Click);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // XMLContentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.XMLView);
            this.Controls.Add(this.XMLStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(340, 210);
            this.Name = "XMLContentEditor";
            this.Text = "XML Content Editor";
            this.XMLStrip.ResumeLayout(false);
            this.XMLStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XMLView)).EndInit();
            this.XMLContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.MenuStrip XMLStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem contentProvidersToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem partColorsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.DataGridView XMLView;
    private System.Windows.Forms.ContextMenuStrip XMLContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem insertRowToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
}