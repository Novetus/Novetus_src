
namespace NovetusLauncher
{
    partial class LauncherFormStylishServerInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherFormStylishServerInfo));
            this.ServerInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ServerInfo
            // 
            this.ServerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerInfo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ServerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ServerInfo.Location = new System.Drawing.Point(12, 12);
            this.ServerInfo.Multiline = true;
            this.ServerInfo.Name = "ServerInfo";
            this.ServerInfo.ReadOnly = true;
            this.ServerInfo.Size = new System.Drawing.Size(776, 426);
            this.ServerInfo.TabIndex = 0;
            // 
            // LauncherFormStylishServerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(152)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ServerInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LauncherFormStylishServerInfo";
            this.Text = "Server Information";
            this.Load += new System.EventHandler(this.LauncherFormStylishServerInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ServerInfo;
    }
}