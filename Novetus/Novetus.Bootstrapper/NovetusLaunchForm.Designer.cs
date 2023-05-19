
namespace Novetus.Bootstrapper
{
    partial class NovetusLaunchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovetusLaunchForm));
            this.LaunchNovetusButton = new System.Windows.Forms.Button();
            this.LaunchSDKButton = new System.Windows.Forms.Button();
            this.CMDButton = new System.Windows.Forms.Button();
            this.CMDHelpButton = new System.Windows.Forms.Button();
            this.DependencyInstallerButton = new System.Windows.Forms.Button();
            this.CMDGroup = new System.Windows.Forms.GroupBox();
            this.ArgLabel = new System.Windows.Forms.Label();
            this.ArgBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LauncherBox = new System.Windows.Forms.CheckBox();
            this.URIButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LaunchNovetusWithConsoleButton = new System.Windows.Forms.Button();
            this.CMDGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LaunchNovetusButton
            // 
            this.LaunchNovetusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchNovetusButton.Location = new System.Drawing.Point(12, 99);
            this.LaunchNovetusButton.Name = "LaunchNovetusButton";
            this.LaunchNovetusButton.Size = new System.Drawing.Size(485, 50);
            this.LaunchNovetusButton.TabIndex = 0;
            this.LaunchNovetusButton.Text = "PLAY NOVETUS";
            this.LaunchNovetusButton.UseVisualStyleBackColor = true;
            this.LaunchNovetusButton.Click += new System.EventHandler(this.LaunchNovetusButton_Click);
            // 
            // LaunchSDKButton
            // 
            this.LaunchSDKButton.Location = new System.Drawing.Point(18, 19);
            this.LaunchSDKButton.Name = "LaunchSDKButton";
            this.LaunchSDKButton.Size = new System.Drawing.Size(93, 23);
            this.LaunchSDKButton.TabIndex = 2;
            this.LaunchSDKButton.Text = "LAUNCH SDK";
            this.LaunchSDKButton.UseVisualStyleBackColor = true;
            this.LaunchSDKButton.Click += new System.EventHandler(this.LaunchSDKButton_Click);
            // 
            // CMDButton
            // 
            this.CMDButton.Location = new System.Drawing.Point(9, 36);
            this.CMDButton.Name = "CMDButton";
            this.CMDButton.Size = new System.Drawing.Size(248, 23);
            this.CMDButton.TabIndex = 3;
            this.CMDButton.Text = "LAUNCH SERVER";
            this.CMDButton.UseVisualStyleBackColor = true;
            this.CMDButton.Click += new System.EventHandler(this.CMDButton_Click);
            // 
            // CMDHelpButton
            // 
            this.CMDHelpButton.Location = new System.Drawing.Point(9, 63);
            this.CMDHelpButton.Name = "CMDHelpButton";
            this.CMDHelpButton.Size = new System.Drawing.Size(248, 23);
            this.CMDHelpButton.TabIndex = 4;
            this.CMDHelpButton.Text = "CONSOLE HELP";
            this.CMDHelpButton.UseVisualStyleBackColor = true;
            this.CMDHelpButton.Click += new System.EventHandler(this.CMDHelpButton_Click);
            // 
            // DependencyInstallerButton
            // 
            this.DependencyInstallerButton.Location = new System.Drawing.Point(18, 42);
            this.DependencyInstallerButton.Name = "DependencyInstallerButton";
            this.DependencyInstallerButton.Size = new System.Drawing.Size(182, 23);
            this.DependencyInstallerButton.TabIndex = 5;
            this.DependencyInstallerButton.Text = "DEPENDENCY INSTALLER";
            this.DependencyInstallerButton.UseVisualStyleBackColor = true;
            this.DependencyInstallerButton.Click += new System.EventHandler(this.DependencyInstallerButton_Click);
            // 
            // CMDGroup
            // 
            this.CMDGroup.Controls.Add(this.ArgLabel);
            this.CMDGroup.Controls.Add(this.ArgBox);
            this.CMDGroup.Controls.Add(this.CMDHelpButton);
            this.CMDGroup.Controls.Add(this.CMDButton);
            this.CMDGroup.Location = new System.Drawing.Point(12, 190);
            this.CMDGroup.Name = "CMDGroup";
            this.CMDGroup.Size = new System.Drawing.Size(263, 92);
            this.CMDGroup.TabIndex = 6;
            this.CMDGroup.TabStop = false;
            this.CMDGroup.Text = "Novetus Console";
            // 
            // ArgLabel
            // 
            this.ArgLabel.AutoSize = true;
            this.ArgLabel.Location = new System.Drawing.Point(6, 16);
            this.ArgLabel.Name = "ArgLabel";
            this.ArgLabel.Size = new System.Drawing.Size(60, 13);
            this.ArgLabel.TabIndex = 6;
            this.ArgLabel.Text = "Arguments:";
            // 
            // ArgBox
            // 
            this.ArgBox.Location = new System.Drawing.Point(66, 13);
            this.ArgBox.Name = "ArgBox";
            this.ArgBox.Size = new System.Drawing.Size(191, 20);
            this.ArgBox.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LauncherBox);
            this.groupBox1.Controls.Add(this.URIButton);
            this.groupBox1.Controls.Add(this.LaunchSDKButton);
            this.groupBox1.Controls.Add(this.DependencyInstallerButton);
            this.groupBox1.Location = new System.Drawing.Point(281, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 91);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Other Options";
            // 
            // LauncherBox
            // 
            this.LauncherBox.AutoSize = true;
            this.LauncherBox.Location = new System.Drawing.Point(51, 68);
            this.LauncherBox.Name = "LauncherBox";
            this.LauncherBox.Size = new System.Drawing.Size(120, 17);
            this.LauncherBox.TabIndex = 7;
            this.LauncherBox.Text = "Skip on next launch";
            this.LauncherBox.UseVisualStyleBackColor = true;
            this.LauncherBox.CheckedChanged += new System.EventHandler(this.LauncherBox_CheckedChanged);
            // 
            // URIButton
            // 
            this.URIButton.Location = new System.Drawing.Point(117, 19);
            this.URIButton.Name = "URIButton";
            this.URIButton.Size = new System.Drawing.Size(83, 23);
            this.URIButton.TabIndex = 6;
            this.URIButton.Text = "INSTALL URI";
            this.URIButton.UseVisualStyleBackColor = true;
            this.URIButton.Click += new System.EventHandler(this.URIButton_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.ForeColor = System.Drawing.Color.IndianRed;
            this.VersionLabel.Location = new System.Drawing.Point(20, 73);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(470, 26);
            this.VersionLabel.TabIndex = 8;
            this.VersionLabel.Text = "v1.0";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Novetus.Bootstrapper.Properties.Resources.NOVETUS_new_final_smol;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(70, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(371, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // LaunchNovetusWithConsoleButton
            // 
            this.LaunchNovetusWithConsoleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchNovetusWithConsoleButton.Location = new System.Drawing.Point(12, 155);
            this.LaunchNovetusWithConsoleButton.Name = "LaunchNovetusWithConsoleButton";
            this.LaunchNovetusWithConsoleButton.Size = new System.Drawing.Size(485, 30);
            this.LaunchNovetusWithConsoleButton.TabIndex = 9;
            this.LaunchNovetusWithConsoleButton.Text = "PLAY NOVETUS WITH CONSOLE";
            this.LaunchNovetusWithConsoleButton.UseVisualStyleBackColor = true;
            this.LaunchNovetusWithConsoleButton.Click += new System.EventHandler(this.LaunchNovetusWithConsoleButton_Click);
            // 
            // NovetusLaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(508, 294);
            this.Controls.Add(this.LaunchNovetusWithConsoleButton);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CMDGroup);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LaunchNovetusButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NovetusLaunchForm";
            this.Text = "Novetus";
            this.Load += new System.EventHandler(this.NovetusLaunchForm_Load);
            this.CMDGroup.ResumeLayout(false);
            this.CMDGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LaunchNovetusButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button LaunchSDKButton;
        private System.Windows.Forms.Button CMDButton;
        private System.Windows.Forms.Button CMDHelpButton;
        private System.Windows.Forms.Button DependencyInstallerButton;
        private System.Windows.Forms.GroupBox CMDGroup;
        private System.Windows.Forms.Label ArgLabel;
        private System.Windows.Forms.TextBox ArgBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button URIButton;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.CheckBox LauncherBox;
        private System.Windows.Forms.Button LaunchNovetusWithConsoleButton;
    }
}

