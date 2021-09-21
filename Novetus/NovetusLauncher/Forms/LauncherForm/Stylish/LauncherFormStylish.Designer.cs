
namespace NovetusLauncher
{
    partial class LauncherFormStylish
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherFormStylish));
            this.splashLabel = new System.Windows.Forms.Label();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.launcherFormStylishInterface1 = new LauncherFormStylishInterface(this);
            this.logoImageBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splashLabel
            // 
            this.splashLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splashLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(135)))), ((int)(((byte)(13)))));
            this.splashLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splashLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splashLabel.ForeColor = System.Drawing.Color.White;
            this.splashLabel.Location = new System.Drawing.Point(12, 122);
            this.splashLabel.Name = "splashLabel";
            this.splashLabel.Size = new System.Drawing.Size(683, 32);
            this.splashLabel.TabIndex = 1;
            this.splashLabel.Text = "Novetus!";
            this.splashLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.splashLabel.UseWaitCursor = true;
            this.splashLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.splashLabel_Paint);
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(78)))), ((int)(((byte)(100)))));
            this.elementHost1.Location = new System.Drawing.Point(0, 157);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(707, 303);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.launcherFormStylishInterface1;
            // 
            // logoImageBox
            // 
            this.logoImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logoImageBox.BackColor = System.Drawing.Color.Transparent;
            this.logoImageBox.Image = ((System.Drawing.Image)(resources.GetObject("logoImageBox.Image")));
            this.logoImageBox.Location = new System.Drawing.Point(12, 3);
            this.logoImageBox.Name = "logoImageBox";
            this.logoImageBox.Size = new System.Drawing.Size(683, 116);
            this.logoImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoImageBox.TabIndex = 3;
            this.logoImageBox.TabStop = false;
            // 
            // LauncherFormStylish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(78)))), ((int)(((byte)(100)))));
            this.ClientSize = new System.Drawing.Size(707, 461);
            this.Controls.Add(this.logoImageBox);
            this.Controls.Add(this.splashLabel);
            this.Controls.Add(this.elementHost1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LauncherFormStylish";
            this.Text = "Novetus";
            this.Load += new System.EventHandler(this.LauncherFormStylish_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LauncherFormStylish_Close);
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private LauncherFormStylishInterface launcherFormStylishInterface1;
        private System.Windows.Forms.Label splashLabel;
        private System.Windows.Forms.PictureBox logoImageBox;
    }
}