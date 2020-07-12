namespace Novetus.ClientScriptTester
{
    partial class ClientScriptTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientScriptTestForm));
            this.ResultLabel = new System.Windows.Forms.Label();
            this.Divider1 = new System.Windows.Forms.Label();
            this.OutputBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(12, 9);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(102, 13);
            this.ResultLabel.TabIndex = 0;
            this.ResultLabel.Text = "Here are the results:";
            // 
            // Divider1
            // 
            this.Divider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Divider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Divider1.Location = new System.Drawing.Point(8, 31);
            this.Divider1.Name = "Divider1";
            this.Divider1.Size = new System.Drawing.Size(780, 2);
            this.Divider1.TabIndex = 1;
            // 
            // OutputBox
            // 
            this.OutputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.OutputBox.Location = new System.Drawing.Point(8, 36);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(780, 402);
            this.OutputBox.TabIndex = 2;
            this.OutputBox.Text = "";
            // 
            // ClientScriptTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.Divider1);
            this.Controls.Add(this.ResultLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(816, 200);
            this.Name = "ClientScriptTestForm";
            this.Text = "ClientScript Tester";
            this.Load += new System.EventHandler(this.ClientScriptTestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.Label Divider1;
        private System.Windows.Forms.RichTextBox OutputBox;
    }
}

