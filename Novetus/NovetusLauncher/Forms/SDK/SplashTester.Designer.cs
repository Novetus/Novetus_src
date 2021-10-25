/*
 * Created by SharpDevelop.
 * User: BITL-Gaming
 * Date: 10/7/2016
 * Time: 3:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
partial class SplashTester
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
        if (disposing)
        {
            if (components != null)
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashTester));
            this.splashLabelNormal = new System.Windows.Forms.Label();
            this.entryBox = new System.Windows.Forms.TextBox();
            this.Preview = new System.Windows.Forms.Label();
            this.splashLabelStylish = new System.Windows.Forms.Label();
            this.changeStylishColor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // splashLabelNormal
            // 
            this.splashLabelNormal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.splashLabelNormal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splashLabelNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splashLabelNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splashLabelNormal.ForeColor = System.Drawing.Color.Black;
            this.splashLabelNormal.Location = new System.Drawing.Point(297, 23);
            this.splashLabelNormal.Name = "splashLabelNormal";
            this.splashLabelNormal.Size = new System.Drawing.Size(214, 17);
            this.splashLabelNormal.TabIndex = 0;
            this.splashLabelNormal.Text = "Novetus!";
            this.splashLabelNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // entryBox
            // 
            this.entryBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entryBox.Location = new System.Drawing.Point(12, 110);
            this.entryBox.Multiline = true;
            this.entryBox.Name = "entryBox";
            this.entryBox.Size = new System.Drawing.Size(795, 96);
            this.entryBox.TabIndex = 52;
            this.entryBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.entryBox.TextChanged += new System.EventHandler(this.entryBox_TextChanged);
            // 
            // Preview
            // 
            this.Preview.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Preview.AutoSize = true;
            this.Preview.Location = new System.Drawing.Point(381, 7);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(45, 13);
            this.Preview.TabIndex = 53;
            this.Preview.Text = "Preview";
            // 
            // splashLabelStylish
            // 
            this.splashLabelStylish.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.splashLabelStylish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(135)))), ((int)(((byte)(13)))));
            this.splashLabelStylish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splashLabelStylish.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
            this.splashLabelStylish.ForeColor = System.Drawing.Color.White;
            this.splashLabelStylish.Location = new System.Drawing.Point(62, 47);
            this.splashLabelStylish.Name = "splashLabelStylish";
            this.splashLabelStylish.Size = new System.Drawing.Size(689, 32);
            this.splashLabelStylish.TabIndex = 54;
            this.splashLabelStylish.Text = "Novetus!";
            this.splashLabelStylish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.splashLabelStylish.Paint += new System.Windows.Forms.PaintEventHandler(this.splashLabelStylish_Paint);
            // 
            // changeStylishColor
            // 
            this.changeStylishColor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.changeStylishColor.Location = new System.Drawing.Point(353, 82);
            this.changeStylishColor.Name = "changeStylishColor";
            this.changeStylishColor.Size = new System.Drawing.Size(103, 22);
            this.changeStylishColor.TabIndex = 55;
            this.changeStylishColor.Text = "Change Color";
            this.changeStylishColor.UseVisualStyleBackColor = true;
            this.changeStylishColor.Click += new System.EventHandler(this.changeStylishColor_Click);
            // 
            // SplashTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(819, 213);
            this.Controls.Add(this.changeStylishColor);
            this.Controls.Add(this.splashLabelStylish);
            this.Controls.Add(this.Preview);
            this.Controls.Add(this.entryBox);
            this.Controls.Add(this.splashLabelNormal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(835, 252);
            this.Name = "SplashTester";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash Tester";
            this.Load += new System.EventHandler(this.SplashTester_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    private System.Windows.Forms.Label splashLabelNormal;
    private System.Windows.Forms.TextBox entryBox;
    private System.Windows.Forms.Label Preview;
    private System.Windows.Forms.Label splashLabelStylish;
    private System.Windows.Forms.Button changeStylishColor;
}
