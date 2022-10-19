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
            this.Style2007 = new System.Windows.Forms.TextBox();
            this.Style2007FolderFinder = new System.Windows.Forms.Button();
            this.Styles2007Info = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.GraphicsWindowResolution = new System.Windows.Forms.TextBox();
            this.GraphicsFullscreenResolution = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.GraphicsModernResolution = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SettingTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsMeshQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsShadingQuality)).BeginInit();
            this.SettingTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quality Level (2010+) (0 means automatic)";
            // 
            // GraphicsLevel
            // 
            this.GraphicsLevel.Location = new System.Drawing.Point(166, 13);
            this.GraphicsLevel.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.GraphicsLevel.Name = "GraphicsLevel";
            this.GraphicsLevel.Size = new System.Drawing.Size(114, 20);
            this.GraphicsLevel.TabIndex = 1;
            this.GraphicsLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GraphicsLevel.ValueChanged += new System.EventHandler(this.GraphicsLevel_ValueChanged);
            // 
            // GraphicsMeshQuality
            // 
            this.GraphicsMeshQuality.Location = new System.Drawing.Point(166, 38);
            this.GraphicsMeshQuality.Name = "GraphicsMeshQuality";
            this.GraphicsMeshQuality.Size = new System.Drawing.Size(114, 20);
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
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mesh Quality (2007-2009)";
            // 
            // GraphicsShadingQuality
            // 
            this.GraphicsShadingQuality.Location = new System.Drawing.Point(166, 64);
            this.GraphicsShadingQuality.Name = "GraphicsShadingQuality";
            this.GraphicsShadingQuality.Size = new System.Drawing.Size(114, 20);
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
            this.label3.Location = new System.Drawing.Point(6, 68);
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
            this.GraphicsMaterialQuality.Location = new System.Drawing.Point(166, 90);
            this.GraphicsMaterialQuality.Name = "GraphicsMaterialQuality";
            this.GraphicsMaterialQuality.Size = new System.Drawing.Size(114, 21);
            this.GraphicsMaterialQuality.TabIndex = 6;
            this.GraphicsMaterialQuality.SelectedIndexChanged += new System.EventHandler(this.GraphicsMaterialQuality_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 28);
            this.label4.TabIndex = 7;
            this.label4.Text = "Material Quality/Truss Detail (2009+)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 25);
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
            this.GraphicsAntiAliasing.Location = new System.Drawing.Point(166, 21);
            this.GraphicsAntiAliasing.Name = "GraphicsAntiAliasing";
            this.GraphicsAntiAliasing.Size = new System.Drawing.Size(114, 21);
            this.GraphicsAntiAliasing.TabIndex = 9;
            this.GraphicsAntiAliasing.SelectedIndexChanged += new System.EventHandler(this.GraphicsAntiAliasing_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 51);
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
            this.GraphicsAASamples.Location = new System.Drawing.Point(166, 48);
            this.GraphicsAASamples.Name = "GraphicsAASamples";
            this.GraphicsAASamples.Size = new System.Drawing.Size(114, 21);
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
            this.GraphicsBevels.Location = new System.Drawing.Point(166, 117);
            this.GraphicsBevels.Name = "GraphicsBevels";
            this.GraphicsBevels.Size = new System.Drawing.Size(114, 21);
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
            this.GraphicsShadows2008.Location = new System.Drawing.Point(166, 75);
            this.GraphicsShadows2008.Name = "GraphicsShadows2008";
            this.GraphicsShadows2008.Size = new System.Drawing.Size(114, 21);
            this.GraphicsShadows2008.TabIndex = 13;
            this.GraphicsShadows2008.SelectedIndexChanged += new System.EventHandler(this.GraphicsShadows2008_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Bevels (2008+)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 75);
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
            this.label9.Location = new System.Drawing.Point(32, 176);
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
            this.GraphicsShadows2007.Location = new System.Drawing.Point(166, 102);
            this.GraphicsShadows2007.Name = "GraphicsShadows2007";
            this.GraphicsShadows2007.Size = new System.Drawing.Size(114, 21);
            this.GraphicsShadows2007.TabIndex = 18;
            this.GraphicsShadows2007.SelectedIndexChanged += new System.EventHandler(this.GraphicsShadows2007_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Shadows (2007)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Window Style (Early 2007)";
            // 
            // Style2007
            // 
            this.Style2007.Location = new System.Drawing.Point(141, 93);
            this.Style2007.Name = "Style2007";
            this.Style2007.Size = new System.Drawing.Size(144, 20);
            this.Style2007.TabIndex = 22;
            this.Style2007.TextChanged += new System.EventHandler(this.Style2007_TextChanged);
            // 
            // Style2007FolderFinder
            // 
            this.Style2007FolderFinder.Location = new System.Drawing.Point(141, 119);
            this.Style2007FolderFinder.Name = "Style2007FolderFinder";
            this.Style2007FolderFinder.Size = new System.Drawing.Size(99, 23);
            this.Style2007FolderFinder.TabIndex = 23;
            this.Style2007FolderFinder.Text = "Styles Folder";
            this.Style2007FolderFinder.UseVisualStyleBackColor = true;
            this.Style2007FolderFinder.Click += new System.EventHandler(this.Style2007FolderFinder_Click);
            // 
            // Styles2007Info
            // 
            this.Styles2007Info.Location = new System.Drawing.Point(246, 119);
            this.Styles2007Info.Name = "Styles2007Info";
            this.Styles2007Info.Size = new System.Drawing.Size(39, 23);
            this.Styles2007Info.TabIndex = 24;
            this.Styles2007Info.Text = "Info";
            this.Styles2007Info.UseVisualStyleBackColor = true;
            this.Styles2007Info.Click += new System.EventHandler(this.Styles2007Info_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 26);
            this.label12.TabIndex = 25;
            this.label12.Text = "Window Resolution\r\n(2007-2009)";
            // 
            // GraphicsWindowResolution
            // 
            this.GraphicsWindowResolution.Location = new System.Drawing.Point(141, 7);
            this.GraphicsWindowResolution.Name = "GraphicsWindowResolution";
            this.GraphicsWindowResolution.Size = new System.Drawing.Size(144, 20);
            this.GraphicsWindowResolution.TabIndex = 26;
            this.GraphicsWindowResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GraphicsWindowResolution.TextChanged += new System.EventHandler(this.GraphicsWindowResolution_TextChanged);
            // 
            // GraphicsFullscreenResolution
            // 
            this.GraphicsFullscreenResolution.Location = new System.Drawing.Point(141, 36);
            this.GraphicsFullscreenResolution.Name = "GraphicsFullscreenResolution";
            this.GraphicsFullscreenResolution.Size = new System.Drawing.Size(144, 20);
            this.GraphicsFullscreenResolution.TabIndex = 28;
            this.GraphicsFullscreenResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GraphicsFullscreenResolution.TextChanged += new System.EventHandler(this.GraphicsFullscreenResolution_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 26);
            this.label13.TabIndex = 27;
            this.label13.Text = "Fullscreen Resolution\r\n(2007-2009)";
            // 
            // GraphicsModernResolution
            // 
            this.GraphicsModernResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GraphicsModernResolution.FormattingEnabled = true;
            this.GraphicsModernResolution.Items.AddRange(new object[] {
            "0: Automatic",
            "1: 720x526",
            "2: 800x600",
            "3: 1024x600 (wide)",
            "4: 1024x768",
            "5: 1280x720 (wide)",
            "6: 1280x768 (wide)",
            "7: 1152x864",
            "8: 1280x800 (wide)",
            "9: 1360x768 (wide)",
            "10: 1280x960",
            "11: 1280x1024",
            "12: 1440x900 (wide)",
            "13: 1600x900 (wide)",
            "14: 1600x1024 (wide)",
            "15: 1600x1200",
            "16: 1680x1050 (wide)",
            "17: 1920x1080 (wide)",
            "18: 1920x1200 (wide)"});
            this.GraphicsModernResolution.Location = new System.Drawing.Point(141, 65);
            this.GraphicsModernResolution.Name = "GraphicsModernResolution";
            this.GraphicsModernResolution.Size = new System.Drawing.Size(144, 21);
            this.GraphicsModernResolution.TabIndex = 29;
            this.GraphicsModernResolution.SelectedIndexChanged += new System.EventHandler(this.GraphicsModernResolution_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Overall Resolution (2010+)";
            // 
            // SettingTabs
            // 
            this.SettingTabs.Controls.Add(this.tabPage1);
            this.SettingTabs.Controls.Add(this.tabPage2);
            this.SettingTabs.Controls.Add(this.tabPage3);
            this.SettingTabs.Location = new System.Drawing.Point(0, 0);
            this.SettingTabs.Name = "SettingTabs";
            this.SettingTabs.SelectedIndex = 0;
            this.SettingTabs.Size = new System.Drawing.Size(302, 173);
            this.SettingTabs.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.GraphicsBevels);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.GraphicsLevel);
            this.tabPage1.Controls.Add(this.GraphicsMaterialQuality);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.GraphicsMeshQuality);
            this.tabPage1.Controls.Add(this.GraphicsShadingQuality);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(294, 147);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Model/Texture";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.GraphicsShadows2008);
            this.tabPage2.Controls.Add(this.GraphicsAntiAliasing);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.GraphicsAASamples);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.GraphicsShadows2007);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(294, 147);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Shadows/Anti-Aliasing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.Styles2007Info);
            this.tabPage3.Controls.Add(this.GraphicsFullscreenResolution);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.Style2007FolderFinder);
            this.tabPage3.Controls.Add(this.GraphicsWindowResolution);
            this.tabPage3.Controls.Add(this.Style2007);
            this.tabPage3.Controls.Add(this.GraphicsModernResolution);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(294, 147);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Resolution/Styling";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // CustomGraphicsOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(300, 194);
            this.Controls.Add(this.SettingTabs);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomGraphicsOptions";
            this.Text = "Custom Graphics Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomGraphicsOptions_Close);
            this.Load += new System.EventHandler(this.CustomGraphicsOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsMeshQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicsShadingQuality)).EndInit();
            this.SettingTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.TextBox Style2007;
        private System.Windows.Forms.Button Style2007FolderFinder;
        private System.Windows.Forms.Button Styles2007Info;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox GraphicsWindowResolution;
        private System.Windows.Forms.TextBox GraphicsFullscreenResolution;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox GraphicsModernResolution;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabControl SettingTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
    }
}