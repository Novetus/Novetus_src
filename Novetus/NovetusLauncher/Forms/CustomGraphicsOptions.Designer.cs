namespace NovetusLauncher
{
    partial class CustomGraphicsOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomGraphicsOptions));
            this.label1 = new System.Windows.Forms.Label();
            this.GraphicsLevel = new System.Windows.Forms.NumericUpDown();
            this.GraphicsMeshQuality = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.GraphicsShadingQuality = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.GraphicsMaterialQuality = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.GraphicsAntiAliasing = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.GraphicsAASamples = new System.Windows.Forms.ComboBox();
            this.GraphicsBevels = new System.Windows.Forms.ComboBox();
            this.GraphicsShadows2008 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.GraphicsShadows2007 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.style2007 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsMeshQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsShadingQuality)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quality Level (2010+) (0 means automatic)";
            // 
            // GraphicsLevel
            // 
            this.GraphicsLevel.Location = new System.Drawing.Point(172, 12);
            this.GraphicsLevel.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.GraphicsLevel.Name = "GraphicsLevel";
            this.GraphicsLevel.Size = new System.Drawing.Size(155, 20);
            this.GraphicsLevel.TabIndex = 1;
            this.GraphicsLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GraphicsLevel.ValueChanged += new System.EventHandler(this.GraphicsLevel_ValueChanged);
            // 
            // GraphicsMeshQuality
            // 
            this.GraphicsMeshQuality.Location = new System.Drawing.Point(172, 38);
            this.GraphicsMeshQuality.Name = "GraphicsMeshQuality";
            this.GraphicsMeshQuality.Size = new System.Drawing.Size(155, 20);
            this.GraphicsMeshQuality.TabIndex = 2;
            this.GraphicsMeshQuality.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GraphicsMeshQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.GraphicsMeshQuality.ValueChanged += new System.EventHandler(this.GraphicsMeshQuality_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mesh Quality (2007-2009)";
            // 
            // GraphicsShadingQuality
            // 
            this.GraphicsShadingQuality.Location = new System.Drawing.Point(172, 64);
            this.GraphicsShadingQuality.Name = "GraphicsShadingQuality";
            this.GraphicsShadingQuality.Size = new System.Drawing.Size(155, 20);
            this.GraphicsShadingQuality.TabIndex = 4;
            this.GraphicsShadingQuality.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GraphicsShadingQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.GraphicsShadingQuality.ValueChanged += new System.EventHandler(this.GraphicsShadingQuality_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Shading Quality (2007-2009)";
            // 
            // GraphicsMaterialQuality
            // 
            this.GraphicsMaterialQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GraphicsMaterialQuality.FormattingEnabled = true;
            this.GraphicsMaterialQuality.Items.AddRange(new object[] {
            "Automatic",
            "Low",
            "Medium",
            "High"});
            this.GraphicsMaterialQuality.Location = new System.Drawing.Point(172, 90);
            this.GraphicsMaterialQuality.Name = "GraphicsMaterialQuality";
            this.GraphicsMaterialQuality.Size = new System.Drawing.Size(155, 21);
            this.GraphicsMaterialQuality.TabIndex = 6;
            this.GraphicsMaterialQuality.SelectedIndexChanged += new System.EventHandler(this.GraphicsMaterialQuality_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 28);
            this.label4.TabIndex = 7;
            this.label4.Text = "Material Quality/Truss Detail (2009+)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Anti-Aliasing (2008+)";
            // 
            // GraphicsAntiAliasing
            // 
            this.GraphicsAntiAliasing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GraphicsAntiAliasing.FormattingEnabled = true;
            this.GraphicsAntiAliasing.Items.AddRange(new object[] {
            "Automatic",
            "On",
            "Off"});
            this.GraphicsAntiAliasing.Location = new System.Drawing.Point(172, 117);
            this.GraphicsAntiAliasing.Name = "GraphicsAntiAliasing";
            this.GraphicsAntiAliasing.Size = new System.Drawing.Size(155, 21);
            this.GraphicsAntiAliasing.TabIndex = 9;
            this.GraphicsAntiAliasing.SelectedIndexChanged += new System.EventHandler(this.GraphicsAntiAliasing_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Anti-Aliasing Samples";
            // 
            // GraphicsAASamples
            // 
            this.GraphicsAASamples.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GraphicsAASamples.FormattingEnabled = true;
            this.GraphicsAASamples.Items.AddRange(new object[] {
            "None",
            "4x",
            "8x"});
            this.GraphicsAASamples.Location = new System.Drawing.Point(172, 144);
            this.GraphicsAASamples.Name = "GraphicsAASamples";
            this.GraphicsAASamples.Size = new System.Drawing.Size(155, 21);
            this.GraphicsAASamples.TabIndex = 11;
            this.GraphicsAASamples.SelectedIndexChanged += new System.EventHandler(this.GraphicsAASamples_SelectedIndexChanged);
            // 
            // GraphicsBevels
            // 
            this.GraphicsBevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GraphicsBevels.FormattingEnabled = true;
            this.GraphicsBevels.Items.AddRange(new object[] {
            "Automatic",
            "On",
            "Off"});
            this.GraphicsBevels.Location = new System.Drawing.Point(172, 171);
            this.GraphicsBevels.Name = "GraphicsBevels";
            this.GraphicsBevels.Size = new System.Drawing.Size(155, 21);
            this.GraphicsBevels.TabIndex = 12;
            this.GraphicsBevels.SelectedIndexChanged += new System.EventHandler(this.GraphicsBevels_SelectedIndexChanged);
            // 
            // GraphicsShadows2008
            // 
            this.GraphicsShadows2008.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GraphicsShadows2008.FormattingEnabled = true;
            this.GraphicsShadows2008.Items.AddRange(new object[] {
            "Automatic",
            "On",
            "Off",
            "Character-Only"});
            this.GraphicsShadows2008.Location = new System.Drawing.Point(172, 198);
            this.GraphicsShadows2008.Name = "GraphicsShadows2008";
            this.GraphicsShadows2008.Size = new System.Drawing.Size(155, 21);
            this.GraphicsShadows2008.TabIndex = 13;
            this.GraphicsShadows2008.SelectedIndexChanged += new System.EventHandler(this.GraphicsShadows2008_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Bevels (2008+)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Shadows (2008+)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(50, 281);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(234, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Close this window to save your settings.";
            // 
            // GraphicsShadows2007
            // 
            this.GraphicsShadows2007.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GraphicsShadows2007.FormattingEnabled = true;
            this.GraphicsShadows2007.Items.AddRange(new object[] {
            "On",
            "Off"});
            this.GraphicsShadows2007.Location = new System.Drawing.Point(172, 225);
            this.GraphicsShadows2007.Name = "GraphicsShadows2007";
            this.GraphicsShadows2007.Size = new System.Drawing.Size(155, 21);
            this.GraphicsShadows2007.TabIndex = 18;
            this.GraphicsShadows2007.SelectedIndexChanged += new System.EventHandler(this.GraphicsShadows2007_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Shadows (2007)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Style (Early 2007)";
            // 
            // style2007
            // 
            this.style2007.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.style2007.FormattingEnabled = true;
            this.style2007.Location = new System.Drawing.Point(172, 252);
            this.style2007.Name = "style2007";
            this.style2007.Size = new System.Drawing.Size(155, 21);
            this.style2007.TabIndex = 20;
            this.style2007.SelectedIndexChanged += new System.EventHandler(this.style2007_SelectedIndexChanged);
            // 
            // CustomGraphicsOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(339, 303);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.style2007);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.GraphicsShadows2007);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.GraphicsShadows2008);
            this.Controls.Add(this.GraphicsBevels);
            this.Controls.Add(this.GraphicsAASamples);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.GraphicsAntiAliasing);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GraphicsMaterialQuality);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GraphicsShadingQuality);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GraphicsMeshQuality);
            this.Controls.Add(this.GraphicsLevel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomGraphicsOptions";
            this.Text = "Novetus Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomGraphicsOptions_Close);
            this.Load += new System.EventHandler(this.CustomGraphicsOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsMeshQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsShadingQuality)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown GraphicsLevel;
        private System.Windows.Forms.NumericUpDown GraphicsMeshQuality;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown GraphicsShadingQuality;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox GraphicsMaterialQuality;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox GraphicsAntiAliasing;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox GraphicsAASamples;
        private System.Windows.Forms.ComboBox GraphicsBevels;
        private System.Windows.Forms.ComboBox GraphicsShadows2008;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox GraphicsShadows2007;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox style2007;
    }
}