
partial class ItemCreationSDK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemCreationSDK));
            this.ItemTypeListBox = new System.Windows.Forms.ComboBox();
            this.ItemTypeLabel = new System.Windows.Forms.Label();
            this.ItemIconLabel = new System.Windows.Forms.Label();
            this.BrowseImageButton = new System.Windows.Forms.Button();
            this.ItemSettingsGroup = new System.Windows.Forms.GroupBox();
            this.CreateItemButton = new System.Windows.Forms.Button();
            this.ItemIcon = new System.Windows.Forms.PictureBox();
            this.ItemDescLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ItemNameLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ItemIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemTypeListBox
            // 
            this.ItemTypeListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ItemTypeListBox.FormattingEnabled = true;
            this.ItemTypeListBox.Items.AddRange(new object[] {
            "Hat",
            "Head",
            "Face",
            "T-Shirt",
            "Shirt",
            "Pants"});
            this.ItemTypeListBox.Location = new System.Drawing.Point(97, 167);
            this.ItemTypeListBox.Name = "ItemTypeListBox";
            this.ItemTypeListBox.Size = new System.Drawing.Size(132, 21);
            this.ItemTypeListBox.TabIndex = 0;
            // 
            // ItemTypeLabel
            // 
            this.ItemTypeLabel.AutoSize = true;
            this.ItemTypeLabel.Location = new System.Drawing.Point(37, 170);
            this.ItemTypeLabel.Name = "ItemTypeLabel";
            this.ItemTypeLabel.Size = new System.Drawing.Size(54, 13);
            this.ItemTypeLabel.TabIndex = 1;
            this.ItemTypeLabel.Text = "Item Type";
            // 
            // ItemIconLabel
            // 
            this.ItemIconLabel.AutoSize = true;
            this.ItemIconLabel.Location = new System.Drawing.Point(142, 26);
            this.ItemIconLabel.Name = "ItemIconLabel";
            this.ItemIconLabel.Size = new System.Drawing.Size(51, 13);
            this.ItemIconLabel.TabIndex = 3;
            this.ItemIconLabel.Text = "Item Icon";
            // 
            // BrowseImageButton
            // 
            this.BrowseImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseImageButton.Location = new System.Drawing.Point(142, 42);
            this.BrowseImageButton.Name = "BrowseImageButton";
            this.BrowseImageButton.Size = new System.Drawing.Size(55, 20);
            this.BrowseImageButton.TabIndex = 4;
            this.BrowseImageButton.Text = "Browse...";
            this.BrowseImageButton.UseVisualStyleBackColor = true;
            // 
            // ItemSettingsGroup
            // 
            this.ItemSettingsGroup.Location = new System.Drawing.Point(12, 194);
            this.ItemSettingsGroup.Name = "ItemSettingsGroup";
            this.ItemSettingsGroup.Size = new System.Drawing.Size(255, 275);
            this.ItemSettingsGroup.TabIndex = 5;
            this.ItemSettingsGroup.TabStop = false;
            this.ItemSettingsGroup.Text = "Item Settings";
            // 
            // CreateItemButton
            // 
            this.CreateItemButton.Location = new System.Drawing.Point(12, 475);
            this.CreateItemButton.Name = "CreateItemButton";
            this.CreateItemButton.Size = new System.Drawing.Size(255, 23);
            this.CreateItemButton.TabIndex = 6;
            this.CreateItemButton.Text = "Create Item";
            this.CreateItemButton.UseVisualStyleBackColor = true;
            // 
            // ItemIcon
            // 
            this.ItemIcon.Location = new System.Drawing.Point(203, 12);
            this.ItemIcon.Name = "ItemIcon";
            this.ItemIcon.Size = new System.Drawing.Size(64, 64);
            this.ItemIcon.TabIndex = 2;
            this.ItemIcon.TabStop = false;
            // 
            // ItemDescLabel
            // 
            this.ItemDescLabel.AutoSize = true;
            this.ItemDescLabel.Location = new System.Drawing.Point(68, 85);
            this.ItemDescLabel.Name = "ItemDescLabel";
            this.ItemDescLabel.Size = new System.Drawing.Size(131, 13);
            this.ItemDescLabel.TabIndex = 7;
            this.ItemDescLabel.Text = "Item Description (Optional)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 101);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(255, 60);
            this.textBox1.TabIndex = 8;
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.AutoSize = true;
            this.ItemNameLabel.Location = new System.Drawing.Point(40, 26);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(58, 13);
            this.ItemNameLabel.TabIndex = 9;
            this.ItemNameLabel.Text = "Item Name";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(124, 20);
            this.textBox2.TabIndex = 10;
            // 
            // ItemCreationSDK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(279, 510);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.ItemNameLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ItemDescLabel);
            this.Controls.Add(this.CreateItemButton);
            this.Controls.Add(this.ItemSettingsGroup);
            this.Controls.Add(this.BrowseImageButton);
            this.Controls.Add(this.ItemIconLabel);
            this.Controls.Add(this.ItemIcon);
            this.Controls.Add(this.ItemTypeLabel);
            this.Controls.Add(this.ItemTypeListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ItemCreationSDK";
            this.Text = "Novetus Item Creation SDK";
            ((System.ComponentModel.ISupportInitialize)(this.ItemIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox ItemTypeListBox;
    private System.Windows.Forms.Label ItemTypeLabel;
    private System.Windows.Forms.PictureBox ItemIcon;
    private System.Windows.Forms.Label ItemIconLabel;
    private System.Windows.Forms.Button BrowseImageButton;
    private System.Windows.Forms.GroupBox ItemSettingsGroup;
    private System.Windows.Forms.Button CreateItemButton;
    private System.Windows.Forms.Label ItemDescLabel;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label ItemNameLabel;
    private System.Windows.Forms.TextBox textBox2;
}