
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
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.logoImageBox = new System.Windows.Forms.PictureBox();
            this.splashLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.elementHost1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.elementHost1.Location = new System.Drawing.Point(0, 116);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(706, 303);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // logoImageBox
            // 
            this.logoImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logoImageBox.BackColor = System.Drawing.Color.Transparent;
            this.logoImageBox.Image = ((System.Drawing.Image)(resources.GetObject("logoImageBox.Image")));
            this.logoImageBox.Location = new System.Drawing.Point(12, 3);
            this.logoImageBox.Name = "logoImageBox";
            this.logoImageBox.Size = new System.Drawing.Size(683, 75);
            this.logoImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoImageBox.TabIndex = 3;
            this.logoImageBox.TabStop = false;
            // 
            // splashLabel
            // 
            this.splashLabel.AutoEllipsis = true;
            this.splashLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(135)))), ((int)(((byte)(13)))));
            this.splashLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splashLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
            this.splashLabel.ForeColor = System.Drawing.Color.White;
            this.splashLabel.Location = new System.Drawing.Point(9, 81);
            this.splashLabel.Name = "splashLabel";
            this.splashLabel.Size = new System.Drawing.Size(689, 32);
            this.splashLabel.TabIndex = 5;
            this.splashLabel.Text = "Novetus!";
            this.splashLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.splashLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.splashLabel_Paint);
            // 
            // LauncherFormStylish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ClientSize = new System.Drawing.Size(707, 419);
            this.Controls.Add(this.splashLabel);
            this.Controls.Add(this.logoImageBox);
            this.Controls.Add(this.elementHost1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LauncherFormStylish";
            this.Text = "Novetus";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LauncherFormStylish_Close);
            this.Load += new System.EventHandler(this.LauncherFormStylish_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.PictureBox logoImageBox;
        private System.Windows.Forms.Label splashLabel;
    }
}