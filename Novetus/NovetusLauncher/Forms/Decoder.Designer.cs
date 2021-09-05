
namespace NovetusLauncher
{
    partial class Decoder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Decoder));
            this.TextEntry = new System.Windows.Forms.TextBox();
            this.Shift = new System.Windows.Forms.NumericUpDown();
            this.ResultBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Shift)).BeginInit();
            this.SuspendLayout();
            // 
            // TextEntry
            // 
            this.TextEntry.Location = new System.Drawing.Point(13, 13);
            this.TextEntry.Name = "TextEntry";
            this.TextEntry.Size = new System.Drawing.Size(174, 20);
            this.TextEntry.TabIndex = 0;
            this.TextEntry.TextChanged += new System.EventHandler(this.TextEntry_TextChanged);
            // 
            // Shift
            // 
            this.Shift.Location = new System.Drawing.Point(193, 13);
            this.Shift.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.Shift.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Shift.Name = "Shift";
            this.Shift.Size = new System.Drawing.Size(32, 20);
            this.Shift.TabIndex = 1;
            this.Shift.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Shift.ValueChanged += new System.EventHandler(this.Shift_ValueChanged);
            // 
            // ResultBox
            // 
            this.ResultBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResultBox.Location = new System.Drawing.Point(13, 40);
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.Size = new System.Drawing.Size(212, 13);
            this.ResultBox.TabIndex = 2;
            this.ResultBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Decoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(241, 72);
            this.Controls.Add(this.ResultBox);
            this.Controls.Add(this.Shift);
            this.Controls.Add(this.TextEntry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Decoder";
            this.Load += new System.EventHandler(this.Decoder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Shift)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextEntry;
        private System.Windows.Forms.NumericUpDown Shift;
        private System.Windows.Forms.TextBox ResultBox;
    }
}