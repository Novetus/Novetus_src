/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/13/2017
 * Time: 4:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace NovetusURI
{
	partial class QuickConfigure
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickConfigure));
            this.NameBox = new System.Windows.Forms.TextBox();
            this.IDBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomizeButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.RegenIDButton = new System.Windows.Forms.Button();
            this.TripcodeLabel = new System.Windows.Forms.Label();
            this.DontShowBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(55, 12);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(155, 20);
            this.NameBox.TabIndex = 0;
            this.NameBox.Text = "Player";
            this.NameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NameBox.TextChanged += new System.EventHandler(this.TextBox1TextChanged);
            // 
            // IDBox
            // 
            this.IDBox.Location = new System.Drawing.Point(55, 38);
            this.IDBox.Name = "IDBox";
            this.IDBox.Size = new System.Drawing.Size(155, 20);
            this.IDBox.TabIndex = 1;
            this.IDBox.Text = "0";
            this.IDBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IDBox.TextChanged += new System.EventHandler(this.TextBox2TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID";
            // 
            // CustomizeButton
            // 
            this.CustomizeButton.Location = new System.Drawing.Point(214, 10);
            this.CustomizeButton.Name = "CustomizeButton";
            this.CustomizeButton.Size = new System.Drawing.Size(198, 23);
            this.CustomizeButton.TabIndex = 4;
            this.CustomizeButton.Text = "Customize Character";
            this.CustomizeButton.UseVisualStyleBackColor = true;
            this.CustomizeButton.Click += new System.EventHandler(this.Button1Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayButton.Location = new System.Drawing.Point(15, 111);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(397, 43);
            this.PlayButton.TabIndex = 5;
            this.PlayButton.Text = "Play!";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.Button2Click);
            // 
            // RegenIDButton
            // 
            this.RegenIDButton.Location = new System.Drawing.Point(214, 36);
            this.RegenIDButton.Name = "RegenIDButton";
            this.RegenIDButton.Size = new System.Drawing.Size(198, 23);
            this.RegenIDButton.TabIndex = 6;
            this.RegenIDButton.Text = "Regenerate Player ID";
            this.RegenIDButton.UseVisualStyleBackColor = true;
            this.RegenIDButton.Click += new System.EventHandler(this.Button3Click);
            // 
            // TripcodeLabel
            // 
            this.TripcodeLabel.Location = new System.Drawing.Point(4, 61);
            this.TripcodeLabel.Name = "TripcodeLabel";
            this.TripcodeLabel.Size = new System.Drawing.Size(419, 18);
            this.TripcodeLabel.TabIndex = 7;
            this.TripcodeLabel.Text = "qwertyuiopasdfghjklz";
            this.TripcodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DontShowBox
            // 
            this.DontShowBox.AutoSize = true;
            this.DontShowBox.Location = new System.Drawing.Point(162, 88);
            this.DontShowBox.Name = "DontShowBox";
            this.DontShowBox.Size = new System.Drawing.Size(108, 17);
            this.DontShowBox.TabIndex = 8;
            this.DontShowBox.Text = "Don\'t show again";
            this.DontShowBox.UseVisualStyleBackColor = true;
            this.DontShowBox.CheckedChanged += new System.EventHandler(this.DontShowBox_CheckedChanged);
            // 
            // QuickConfigure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(424, 166);
            this.Controls.Add(this.DontShowBox);
            this.Controls.Add(this.TripcodeLabel);
            this.Controls.Add(this.RegenIDButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.CustomizeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IDBox);
            this.Controls.Add(this.NameBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickConfigure";
            this.Text = "Player Configuration";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.QuickConfigureClose);
            this.Load += new System.EventHandler(this.QuickConfigureLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button RegenIDButton;
		private System.Windows.Forms.Button PlayButton;
		private System.Windows.Forms.Button CustomizeButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox IDBox;
		private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label TripcodeLabel;
        private System.Windows.Forms.CheckBox DontShowBox;
    }
}
