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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMLContentEditor));
            this.XMLStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentProvidersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.partColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadCurrentFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insetRowAtSelectedRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XMLView = new System.Windows.Forms.DataGridView();
            this.SearchBar = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.XMLStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XMLView)).BeginInit();
            this.SuspendLayout();
            // 
            // XMLStrip
            // 
            this.XMLStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.XMLStrip.AutoSize = false;
            this.XMLStrip.BackColor = System.Drawing.Color.Transparent;
            this.XMLStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.XMLStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.XMLStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.XMLStrip.Location = new System.Drawing.Point(0, -1);
            this.XMLStrip.Name = "XMLStrip";
            this.XMLStrip.Size = new System.Drawing.Size(800, 22);
            this.XMLStrip.TabIndex = 29;
            this.XMLStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.reloadCurrentFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 18);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentProvidersToolStripMenuItem,
            this.partColorsToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
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
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // reloadCurrentFileToolStripMenuItem
            // 
            this.reloadCurrentFileToolStripMenuItem.Name = "reloadCurrentFileToolStripMenuItem";
            this.reloadCurrentFileToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.reloadCurrentFileToolStripMenuItem.Text = "Reload Current File";
            this.reloadCurrentFileToolStripMenuItem.Click += new System.EventHandler(this.reloadCurrentFileToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insetRowAtSelectedRowToolStripMenuItem,
            this.deleteSelectedRowToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 18);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // insetRowAtSelectedRowToolStripMenuItem
            // 
            this.insetRowAtSelectedRowToolStripMenuItem.Name = "insetRowAtSelectedRowToolStripMenuItem";
            this.insetRowAtSelectedRowToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.insetRowAtSelectedRowToolStripMenuItem.Text = "Insert Row at Selected Row";
            this.insetRowAtSelectedRowToolStripMenuItem.Click += new System.EventHandler(this.insertRowToolStripMenuItem_Click);
            // 
            // deleteSelectedRowToolStripMenuItem
            // 
            this.deleteSelectedRowToolStripMenuItem.Name = "deleteSelectedRowToolStripMenuItem";
            this.deleteSelectedRowToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.deleteSelectedRowToolStripMenuItem.Text = "Delete Selected Row";
            this.deleteSelectedRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
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
            this.XMLView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.XMLView_MouseClick);
            // 
            // SearchBar
            // 
            this.SearchBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBar.Location = new System.Drawing.Point(567, 2);
            this.SearchBar.Name = "SearchBar";
            this.SearchBar.Size = new System.Drawing.Size(196, 20);
            this.SearchBar.TabIndex = 31;
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchButton.Image")));
            this.SearchButton.Location = new System.Drawing.Point(767, 2);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(31, 20);
            this.SearchButton.TabIndex = 32;
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // XMLContentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchBar);
            this.Controls.Add(this.XMLView);
            this.Controls.Add(this.XMLStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(340, 210);
            this.Name = "XMLContentEditor";
            this.Text = "JSON Content Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XMLContentEditor_OnClosing);
            this.XMLStrip.ResumeLayout(false);
            this.XMLStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XMLView)).EndInit();
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
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem insetRowAtSelectedRowToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deleteSelectedRowToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem reloadCurrentFileToolStripMenuItem;
    private System.Windows.Forms.TextBox SearchBar;
    private System.Windows.Forms.Button SearchButton;
}