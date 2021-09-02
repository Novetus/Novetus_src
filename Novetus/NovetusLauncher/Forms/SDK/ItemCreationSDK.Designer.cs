
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
            this.UsesHatMeshLabel = new System.Windows.Forms.Label();
            this.UsesHatMeshBox = new System.Windows.Forms.ComboBox();
            this.MeshOptionsGroup = new System.Windows.Forms.GroupBox();
            this.SpecialMeshTypeBox = new System.Windows.Forms.ComboBox();
            this.LODYLabel = new System.Windows.Forms.Label();
            this.LODYBox = new System.Windows.Forms.NumericUpDown();
            this.SpecialMeshTypeLabel = new System.Windows.Forms.Label();
            this.LODXLabel = new System.Windows.Forms.Label();
            this.LODXBox = new System.Windows.Forms.NumericUpDown();
            this.MeshTypeLabel = new System.Windows.Forms.Label();
            this.MeshTypeBox = new System.Windows.Forms.ComboBox();
            this.BulgeBox = new System.Windows.Forms.NumericUpDown();
            this.RoundnessBox = new System.Windows.Forms.NumericUpDown();
            this.BevelBox = new System.Windows.Forms.NumericUpDown();
            this.BulgeLabel = new System.Windows.Forms.Label();
            this.RoundnessLabel = new System.Windows.Forms.Label();
            this.BevelLabel = new System.Windows.Forms.Label();
            this.CoordGroup = new System.Windows.Forms.GroupBox();
            this.ZBox = new System.Windows.Forms.NumericUpDown();
            this.YBox = new System.Windows.Forms.NumericUpDown();
            this.XBox = new System.Windows.Forms.NumericUpDown();
            this.ZLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.Option2BrowseButton = new System.Windows.Forms.Button();
            this.Option2TextBox = new System.Windows.Forms.TextBox();
            this.Option2Label = new System.Windows.Forms.Label();
            this.Option1BrowseButton = new System.Windows.Forms.Button();
            this.Option1TextBox = new System.Windows.Forms.TextBox();
            this.Option1Label = new System.Windows.Forms.Label();
            this.CreateItemButton = new System.Windows.Forms.Button();
            this.ItemIcon = new System.Windows.Forms.PictureBox();
            this.ItemDescLabel = new System.Windows.Forms.Label();
            this.DescBox = new System.Windows.Forms.TextBox();
            this.ItemNameLabel = new System.Windows.Forms.Label();
            this.ItemNameBox = new System.Windows.Forms.TextBox();
            this.CoordGroup2 = new System.Windows.Forms.GroupBox();
            this.ZBox2 = new System.Windows.Forms.NumericUpDown();
            this.YBox2 = new System.Windows.Forms.NumericUpDown();
            this.XBox360 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ItemSettingsGroup.SuspendLayout();
            this.MeshOptionsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LODYBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LODXBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BulgeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoundnessBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BevelBox)).BeginInit();
            this.CoordGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemIcon)).BeginInit();
            this.CoordGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XBox360)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemTypeListBox
            // 
            this.ItemTypeListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ItemTypeListBox.FormattingEnabled = true;
            this.ItemTypeListBox.Items.AddRange(new object[] {
            "Hat",
            "Head (Custom Mesh)",
            "Head (Mesh Shape)",
            "Face",
            "T-Shirt",
            "Shirt",
            "Pants"});
            this.ItemTypeListBox.Location = new System.Drawing.Point(12, 64);
            this.ItemTypeListBox.Name = "ItemTypeListBox";
            this.ItemTypeListBox.Size = new System.Drawing.Size(132, 21);
            this.ItemTypeListBox.TabIndex = 0;
            this.ItemTypeListBox.SelectedIndexChanged += new System.EventHandler(this.ItemTypeListBox_SelectedIndexChanged);
            // 
            // ItemTypeLabel
            // 
            this.ItemTypeLabel.AutoSize = true;
            this.ItemTypeLabel.Location = new System.Drawing.Point(9, 49);
            this.ItemTypeLabel.Name = "ItemTypeLabel";
            this.ItemTypeLabel.Size = new System.Drawing.Size(54, 13);
            this.ItemTypeLabel.TabIndex = 1;
            this.ItemTypeLabel.Text = "Item Type";
            // 
            // ItemIconLabel
            // 
            this.ItemIconLabel.AutoSize = true;
            this.ItemIconLabel.Location = new System.Drawing.Point(164, 23);
            this.ItemIconLabel.Name = "ItemIconLabel";
            this.ItemIconLabel.Size = new System.Drawing.Size(51, 13);
            this.ItemIconLabel.TabIndex = 3;
            this.ItemIconLabel.Text = "Item Icon";
            // 
            // BrowseImageButton
            // 
            this.BrowseImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseImageButton.Location = new System.Drawing.Point(163, 42);
            this.BrowseImageButton.Name = "BrowseImageButton";
            this.BrowseImageButton.Size = new System.Drawing.Size(55, 20);
            this.BrowseImageButton.TabIndex = 4;
            this.BrowseImageButton.Text = "Browse...";
            this.BrowseImageButton.UseVisualStyleBackColor = true;
            this.BrowseImageButton.Click += new System.EventHandler(this.BrowseImageButton_Click);
            // 
            // ItemSettingsGroup
            // 
            this.ItemSettingsGroup.Controls.Add(this.CoordGroup2);
            this.ItemSettingsGroup.Controls.Add(this.UsesHatMeshLabel);
            this.ItemSettingsGroup.Controls.Add(this.UsesHatMeshBox);
            this.ItemSettingsGroup.Controls.Add(this.MeshOptionsGroup);
            this.ItemSettingsGroup.Controls.Add(this.CoordGroup);
            this.ItemSettingsGroup.Controls.Add(this.Option2BrowseButton);
            this.ItemSettingsGroup.Controls.Add(this.Option2TextBox);
            this.ItemSettingsGroup.Controls.Add(this.Option2Label);
            this.ItemSettingsGroup.Controls.Add(this.Option1BrowseButton);
            this.ItemSettingsGroup.Controls.Add(this.Option1TextBox);
            this.ItemSettingsGroup.Controls.Add(this.Option1Label);
            this.ItemSettingsGroup.Location = new System.Drawing.Point(305, 12);
            this.ItemSettingsGroup.Name = "ItemSettingsGroup";
            this.ItemSettingsGroup.Size = new System.Drawing.Size(276, 522);
            this.ItemSettingsGroup.TabIndex = 5;
            this.ItemSettingsGroup.TabStop = false;
            this.ItemSettingsGroup.Text = "Item Settings";
            // 
            // UsesHatMeshLabel
            // 
            this.UsesHatMeshLabel.AutoSize = true;
            this.UsesHatMeshLabel.Location = new System.Drawing.Point(4, 60);
            this.UsesHatMeshLabel.Name = "UsesHatMeshLabel";
            this.UsesHatMeshLabel.Size = new System.Drawing.Size(114, 13);
            this.UsesHatMeshLabel.TabIndex = 18;
            this.UsesHatMeshLabel.Text = "This option is disabled.";
            // 
            // UsesHatMeshBox
            // 
            this.UsesHatMeshBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UsesHatMeshBox.Enabled = false;
            this.UsesHatMeshBox.FormattingEnabled = true;
            this.UsesHatMeshBox.Location = new System.Drawing.Point(6, 76);
            this.UsesHatMeshBox.Name = "UsesHatMeshBox";
            this.UsesHatMeshBox.Size = new System.Drawing.Size(264, 21);
            this.UsesHatMeshBox.TabIndex = 17;
            this.UsesHatMeshBox.SelectedIndexChanged += new System.EventHandler(this.UsesHatMeshBox_SelectedIndexChanged);
            // 
            // MeshOptionsGroup
            // 
            this.MeshOptionsGroup.Controls.Add(this.SpecialMeshTypeBox);
            this.MeshOptionsGroup.Controls.Add(this.LODYLabel);
            this.MeshOptionsGroup.Controls.Add(this.LODYBox);
            this.MeshOptionsGroup.Controls.Add(this.SpecialMeshTypeLabel);
            this.MeshOptionsGroup.Controls.Add(this.LODXLabel);
            this.MeshOptionsGroup.Controls.Add(this.LODXBox);
            this.MeshOptionsGroup.Controls.Add(this.MeshTypeLabel);
            this.MeshOptionsGroup.Controls.Add(this.MeshTypeBox);
            this.MeshOptionsGroup.Controls.Add(this.BulgeBox);
            this.MeshOptionsGroup.Controls.Add(this.RoundnessBox);
            this.MeshOptionsGroup.Controls.Add(this.BevelBox);
            this.MeshOptionsGroup.Controls.Add(this.BulgeLabel);
            this.MeshOptionsGroup.Controls.Add(this.RoundnessLabel);
            this.MeshOptionsGroup.Controls.Add(this.BevelLabel);
            this.MeshOptionsGroup.Enabled = false;
            this.MeshOptionsGroup.Location = new System.Drawing.Point(7, 334);
            this.MeshOptionsGroup.Name = "MeshOptionsGroup";
            this.MeshOptionsGroup.Size = new System.Drawing.Size(263, 179);
            this.MeshOptionsGroup.TabIndex = 7;
            this.MeshOptionsGroup.TabStop = false;
            this.MeshOptionsGroup.Text = "This option is disabled.";
            // 
            // SpecialMeshTypeBox
            // 
            this.SpecialMeshTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpecialMeshTypeBox.FormattingEnabled = true;
            this.SpecialMeshTypeBox.Items.AddRange(new object[] {
            "Head",
            "Torso",
            "Wedge",
            "Sphere",
            "Cylinder",
            "FileMesh",
            "Brick",
            "Prism",
            "Pyramid",
            "ParallelRamp",
            "RightAngleRamp",
            "CornerWedge"});
            this.SpecialMeshTypeBox.Location = new System.Drawing.Point(104, 121);
            this.SpecialMeshTypeBox.Name = "SpecialMeshTypeBox";
            this.SpecialMeshTypeBox.Size = new System.Drawing.Size(153, 21);
            this.SpecialMeshTypeBox.TabIndex = 14;
            // 
            // LODYLabel
            // 
            this.LODYLabel.AutoSize = true;
            this.LODYLabel.Location = new System.Drawing.Point(133, 155);
            this.LODYLabel.Name = "LODYLabel";
            this.LODYLabel.Size = new System.Drawing.Size(39, 13);
            this.LODYLabel.TabIndex = 13;
            this.LODYLabel.Text = "LOD Y";
            // 
            // LODYBox
            // 
            this.LODYBox.Location = new System.Drawing.Point(178, 153);
            this.LODYBox.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.LODYBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LODYBox.Name = "LODYBox";
            this.LODYBox.Size = new System.Drawing.Size(59, 20);
            this.LODYBox.TabIndex = 12;
            this.LODYBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // SpecialMeshTypeLabel
            // 
            this.SpecialMeshTypeLabel.AutoSize = true;
            this.SpecialMeshTypeLabel.Location = new System.Drawing.Point(7, 124);
            this.SpecialMeshTypeLabel.Name = "SpecialMeshTypeLabel";
            this.SpecialMeshTypeLabel.Size = new System.Drawing.Size(95, 13);
            this.SpecialMeshTypeLabel.TabIndex = 11;
            this.SpecialMeshTypeLabel.Text = "SpecialMesh Type";
            // 
            // LODXLabel
            // 
            this.LODXLabel.AutoSize = true;
            this.LODXLabel.Location = new System.Drawing.Point(23, 155);
            this.LODXLabel.Name = "LODXLabel";
            this.LODXLabel.Size = new System.Drawing.Size(39, 13);
            this.LODXLabel.TabIndex = 9;
            this.LODXLabel.Text = "LOD X";
            // 
            // LODXBox
            // 
            this.LODXBox.Location = new System.Drawing.Point(68, 153);
            this.LODXBox.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.LODXBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LODXBox.Name = "LODXBox";
            this.LODXBox.Size = new System.Drawing.Size(59, 20);
            this.LODXBox.TabIndex = 8;
            this.LODXBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MeshTypeLabel
            // 
            this.MeshTypeLabel.AutoSize = true;
            this.MeshTypeLabel.Location = new System.Drawing.Point(7, 26);
            this.MeshTypeLabel.Name = "MeshTypeLabel";
            this.MeshTypeLabel.Size = new System.Drawing.Size(60, 13);
            this.MeshTypeLabel.TabIndex = 7;
            this.MeshTypeLabel.Text = "Mesh Type";
            // 
            // MeshTypeBox
            // 
            this.MeshTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MeshTypeBox.FormattingEnabled = true;
            this.MeshTypeBox.Items.AddRange(new object[] {
            "BlockMesh",
            "CylinderMesh",
            "SpecialMesh"});
            this.MeshTypeBox.Location = new System.Drawing.Point(104, 23);
            this.MeshTypeBox.Name = "MeshTypeBox";
            this.MeshTypeBox.Size = new System.Drawing.Size(153, 21);
            this.MeshTypeBox.TabIndex = 6;
            // 
            // BulgeBox
            // 
            this.BulgeBox.DecimalPlaces = 6;
            this.BulgeBox.Location = new System.Drawing.Point(104, 98);
            this.BulgeBox.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.BulgeBox.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.BulgeBox.Name = "BulgeBox";
            this.BulgeBox.Size = new System.Drawing.Size(153, 20);
            this.BulgeBox.TabIndex = 5;
            // 
            // RoundnessBox
            // 
            this.RoundnessBox.DecimalPlaces = 6;
            this.RoundnessBox.Location = new System.Drawing.Point(104, 73);
            this.RoundnessBox.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.RoundnessBox.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.RoundnessBox.Name = "RoundnessBox";
            this.RoundnessBox.Size = new System.Drawing.Size(153, 20);
            this.RoundnessBox.TabIndex = 4;
            // 
            // BevelBox
            // 
            this.BevelBox.DecimalPlaces = 6;
            this.BevelBox.Location = new System.Drawing.Point(104, 49);
            this.BevelBox.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.BevelBox.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.BevelBox.Name = "BevelBox";
            this.BevelBox.Size = new System.Drawing.Size(153, 20);
            this.BevelBox.TabIndex = 3;
            // 
            // BulgeLabel
            // 
            this.BulgeLabel.AutoSize = true;
            this.BulgeLabel.Location = new System.Drawing.Point(7, 100);
            this.BulgeLabel.Name = "BulgeLabel";
            this.BulgeLabel.Size = new System.Drawing.Size(34, 13);
            this.BulgeLabel.TabIndex = 2;
            this.BulgeLabel.Text = "Bulge";
            // 
            // RoundnessLabel
            // 
            this.RoundnessLabel.AutoSize = true;
            this.RoundnessLabel.Location = new System.Drawing.Point(7, 76);
            this.RoundnessLabel.Name = "RoundnessLabel";
            this.RoundnessLabel.Size = new System.Drawing.Size(91, 13);
            this.RoundnessLabel.TabIndex = 1;
            this.RoundnessLabel.Text = "Bevel Roundness";
            // 
            // BevelLabel
            // 
            this.BevelLabel.AutoSize = true;
            this.BevelLabel.Location = new System.Drawing.Point(7, 51);
            this.BevelLabel.Name = "BevelLabel";
            this.BevelLabel.Size = new System.Drawing.Size(34, 13);
            this.BevelLabel.TabIndex = 0;
            this.BevelLabel.Text = "Bevel";
            // 
            // CoordGroup
            // 
            this.CoordGroup.Controls.Add(this.ZBox);
            this.CoordGroup.Controls.Add(this.YBox);
            this.CoordGroup.Controls.Add(this.XBox);
            this.CoordGroup.Controls.Add(this.ZLabel);
            this.CoordGroup.Controls.Add(this.YLabel);
            this.CoordGroup.Controls.Add(this.XLabel);
            this.CoordGroup.Enabled = false;
            this.CoordGroup.Location = new System.Drawing.Point(7, 142);
            this.CoordGroup.Name = "CoordGroup";
            this.CoordGroup.Size = new System.Drawing.Size(263, 90);
            this.CoordGroup.TabIndex = 6;
            this.CoordGroup.TabStop = false;
            this.CoordGroup.Text = "This option is disabled.";
            // 
            // ZBox
            // 
            this.ZBox.DecimalPlaces = 6;
            this.ZBox.Location = new System.Drawing.Point(27, 65);
            this.ZBox.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.ZBox.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.ZBox.Name = "ZBox";
            this.ZBox.Size = new System.Drawing.Size(230, 20);
            this.ZBox.TabIndex = 5;
            this.ZBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // YBox
            // 
            this.YBox.DecimalPlaces = 6;
            this.YBox.Location = new System.Drawing.Point(27, 39);
            this.YBox.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.YBox.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.YBox.Name = "YBox";
            this.YBox.Size = new System.Drawing.Size(230, 20);
            this.YBox.TabIndex = 4;
            this.YBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // XBox
            // 
            this.XBox.DecimalPlaces = 6;
            this.XBox.Location = new System.Drawing.Point(27, 14);
            this.XBox.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.XBox.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.XBox.Name = "XBox";
            this.XBox.Size = new System.Drawing.Size(230, 20);
            this.XBox.TabIndex = 3;
            this.XBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ZLabel
            // 
            this.ZLabel.AutoSize = true;
            this.ZLabel.Location = new System.Drawing.Point(6, 67);
            this.ZLabel.Name = "ZLabel";
            this.ZLabel.Size = new System.Drawing.Size(14, 13);
            this.ZLabel.TabIndex = 2;
            this.ZLabel.Text = "Z";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(7, 41);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(14, 13);
            this.YLabel.TabIndex = 1;
            this.YLabel.Text = "Y";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(7, 16);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(14, 13);
            this.XLabel.TabIndex = 0;
            this.XLabel.Text = "X";
            // 
            // Option2BrowseButton
            // 
            this.Option2BrowseButton.Enabled = false;
            this.Option2BrowseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Option2BrowseButton.Location = new System.Drawing.Point(214, 116);
            this.Option2BrowseButton.Name = "Option2BrowseButton";
            this.Option2BrowseButton.Size = new System.Drawing.Size(56, 20);
            this.Option2BrowseButton.TabIndex = 5;
            this.Option2BrowseButton.Text = "Browse...";
            this.Option2BrowseButton.UseVisualStyleBackColor = true;
            this.Option2BrowseButton.Click += new System.EventHandler(this.Option2BrowseButton_Click);
            // 
            // Option2TextBox
            // 
            this.Option2TextBox.Location = new System.Drawing.Point(6, 116);
            this.Option2TextBox.Name = "Option2TextBox";
            this.Option2TextBox.ReadOnly = true;
            this.Option2TextBox.Size = new System.Drawing.Size(202, 20);
            this.Option2TextBox.TabIndex = 4;
            // 
            // Option2Label
            // 
            this.Option2Label.AutoSize = true;
            this.Option2Label.Location = new System.Drawing.Point(4, 100);
            this.Option2Label.Name = "Option2Label";
            this.Option2Label.Size = new System.Drawing.Size(114, 13);
            this.Option2Label.TabIndex = 3;
            this.Option2Label.Text = "This option is disabled.";
            // 
            // Option1BrowseButton
            // 
            this.Option1BrowseButton.Enabled = false;
            this.Option1BrowseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Option1BrowseButton.Location = new System.Drawing.Point(214, 37);
            this.Option1BrowseButton.Name = "Option1BrowseButton";
            this.Option1BrowseButton.Size = new System.Drawing.Size(56, 20);
            this.Option1BrowseButton.TabIndex = 2;
            this.Option1BrowseButton.Text = "Browse...";
            this.Option1BrowseButton.UseVisualStyleBackColor = true;
            this.Option1BrowseButton.Click += new System.EventHandler(this.Option1BrowseButton_Click);
            // 
            // Option1TextBox
            // 
            this.Option1TextBox.Location = new System.Drawing.Point(7, 37);
            this.Option1TextBox.Name = "Option1TextBox";
            this.Option1TextBox.ReadOnly = true;
            this.Option1TextBox.Size = new System.Drawing.Size(201, 20);
            this.Option1TextBox.TabIndex = 1;
            // 
            // Option1Label
            // 
            this.Option1Label.AutoSize = true;
            this.Option1Label.Location = new System.Drawing.Point(4, 21);
            this.Option1Label.Name = "Option1Label";
            this.Option1Label.Size = new System.Drawing.Size(114, 13);
            this.Option1Label.TabIndex = 0;
            this.Option1Label.Text = "This option is disabled.";
            // 
            // CreateItemButton
            // 
            this.CreateItemButton.Location = new System.Drawing.Point(12, 511);
            this.CreateItemButton.Name = "CreateItemButton";
            this.CreateItemButton.Size = new System.Drawing.Size(288, 23);
            this.CreateItemButton.TabIndex = 6;
            this.CreateItemButton.Text = "Create Item";
            this.CreateItemButton.UseVisualStyleBackColor = true;
            this.CreateItemButton.Click += new System.EventHandler(this.CreateItemButton_Click);
            // 
            // ItemIcon
            // 
            this.ItemIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItemIcon.Location = new System.Drawing.Point(224, 9);
            this.ItemIcon.Name = "ItemIcon";
            this.ItemIcon.Size = new System.Drawing.Size(76, 76);
            this.ItemIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ItemIcon.TabIndex = 2;
            this.ItemIcon.TabStop = false;
            // 
            // ItemDescLabel
            // 
            this.ItemDescLabel.AutoSize = true;
            this.ItemDescLabel.Location = new System.Drawing.Point(87, 96);
            this.ItemDescLabel.Name = "ItemDescLabel";
            this.ItemDescLabel.Size = new System.Drawing.Size(131, 13);
            this.ItemDescLabel.TabIndex = 7;
            this.ItemDescLabel.Text = "Item Description (Optional)";
            // 
            // DescBox
            // 
            this.DescBox.Location = new System.Drawing.Point(12, 112);
            this.DescBox.Multiline = true;
            this.DescBox.Name = "DescBox";
            this.DescBox.Size = new System.Drawing.Size(288, 393);
            this.DescBox.TabIndex = 8;
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.AutoSize = true;
            this.ItemNameLabel.Location = new System.Drawing.Point(8, 9);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(58, 13);
            this.ItemNameLabel.TabIndex = 9;
            this.ItemNameLabel.Text = "Item Name";
            // 
            // ItemNameBox
            // 
            this.ItemNameBox.Location = new System.Drawing.Point(11, 23);
            this.ItemNameBox.Name = "ItemNameBox";
            this.ItemNameBox.Size = new System.Drawing.Size(134, 20);
            this.ItemNameBox.TabIndex = 10;
            // 
            // CoordGroup2
            // 
            this.CoordGroup2.Controls.Add(this.ZBox2);
            this.CoordGroup2.Controls.Add(this.YBox2);
            this.CoordGroup2.Controls.Add(this.XBox360);
            this.CoordGroup2.Controls.Add(this.label1);
            this.CoordGroup2.Controls.Add(this.label2);
            this.CoordGroup2.Controls.Add(this.label3);
            this.CoordGroup2.Enabled = false;
            this.CoordGroup2.Location = new System.Drawing.Point(7, 238);
            this.CoordGroup2.Name = "CoordGroup2";
            this.CoordGroup2.Size = new System.Drawing.Size(263, 90);
            this.CoordGroup2.TabIndex = 19;
            this.CoordGroup2.TabStop = false;
            this.CoordGroup2.Text = "This option is disabled.";
            // 
            // ZBox2
            // 
            this.ZBox2.DecimalPlaces = 6;
            this.ZBox2.Location = new System.Drawing.Point(27, 65);
            this.ZBox2.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.ZBox2.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.ZBox2.Name = "ZBox2";
            this.ZBox2.Size = new System.Drawing.Size(230, 20);
            this.ZBox2.TabIndex = 5;
            this.ZBox2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // YBox2
            // 
            this.YBox2.DecimalPlaces = 6;
            this.YBox2.Location = new System.Drawing.Point(27, 39);
            this.YBox2.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.YBox2.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.YBox2.Name = "YBox2";
            this.YBox2.Size = new System.Drawing.Size(230, 20);
            this.YBox2.TabIndex = 4;
            this.YBox2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // XBox360
            // 
            this.XBox360.DecimalPlaces = 6;
            this.XBox360.Location = new System.Drawing.Point(27, 14);
            this.XBox360.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.XBox360.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.XBox360.Name = "XBox360";
            this.XBox360.Size = new System.Drawing.Size(230, 20);
            this.XBox360.TabIndex = 3;
            this.XBox360.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Z";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "X";
            // 
            // ItemCreationSDK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(585, 539);
            this.Controls.Add(this.ItemNameBox);
            this.Controls.Add(this.ItemNameLabel);
            this.Controls.Add(this.DescBox);
            this.Controls.Add(this.ItemDescLabel);
            this.Controls.Add(this.CreateItemButton);
            this.Controls.Add(this.ItemSettingsGroup);
            this.Controls.Add(this.BrowseImageButton);
            this.Controls.Add(this.ItemIconLabel);
            this.Controls.Add(this.ItemIcon);
            this.Controls.Add(this.ItemTypeLabel);
            this.Controls.Add(this.ItemTypeListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ItemCreationSDK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Novetus Item Creation SDK";
            this.Load += new System.EventHandler(this.ItemCreationSDK_Load);
            this.ItemSettingsGroup.ResumeLayout(false);
            this.ItemSettingsGroup.PerformLayout();
            this.MeshOptionsGroup.ResumeLayout(false);
            this.MeshOptionsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LODYBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LODXBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BulgeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoundnessBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BevelBox)).EndInit();
            this.CoordGroup.ResumeLayout(false);
            this.CoordGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemIcon)).EndInit();
            this.CoordGroup2.ResumeLayout(false);
            this.CoordGroup2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XBox360)).EndInit();
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
    private System.Windows.Forms.TextBox DescBox;
    private System.Windows.Forms.Label ItemNameLabel;
    private System.Windows.Forms.TextBox ItemNameBox;
    private System.Windows.Forms.Button Option2BrowseButton;
    private System.Windows.Forms.TextBox Option2TextBox;
    private System.Windows.Forms.Label Option2Label;
    private System.Windows.Forms.Button Option1BrowseButton;
    private System.Windows.Forms.TextBox Option1TextBox;
    private System.Windows.Forms.Label Option1Label;
    private System.Windows.Forms.GroupBox MeshOptionsGroup;
    private System.Windows.Forms.NumericUpDown BulgeBox;
    private System.Windows.Forms.NumericUpDown RoundnessBox;
    private System.Windows.Forms.NumericUpDown BevelBox;
    private System.Windows.Forms.Label BulgeLabel;
    private System.Windows.Forms.Label RoundnessLabel;
    private System.Windows.Forms.Label BevelLabel;
    private System.Windows.Forms.GroupBox CoordGroup;
    private System.Windows.Forms.NumericUpDown ZBox;
    private System.Windows.Forms.NumericUpDown YBox;
    private System.Windows.Forms.NumericUpDown XBox;
    private System.Windows.Forms.Label ZLabel;
    private System.Windows.Forms.Label YLabel;
    private System.Windows.Forms.Label XLabel;
    private System.Windows.Forms.Label UsesHatMeshLabel;
    private System.Windows.Forms.ComboBox UsesHatMeshBox;
    private System.Windows.Forms.Label MeshTypeLabel;
    private System.Windows.Forms.ComboBox MeshTypeBox;
    private System.Windows.Forms.Label LODYLabel;
    private System.Windows.Forms.NumericUpDown LODYBox;
    private System.Windows.Forms.Label SpecialMeshTypeLabel;
    private System.Windows.Forms.Label LODXLabel;
    private System.Windows.Forms.NumericUpDown LODXBox;
    private System.Windows.Forms.ComboBox SpecialMeshTypeBox;
    private System.Windows.Forms.GroupBox CoordGroup2;
    private System.Windows.Forms.NumericUpDown ZBox2;
    private System.Windows.Forms.NumericUpDown YBox2;
    private System.Windows.Forms.NumericUpDown XBox360;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
}