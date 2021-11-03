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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashTester));
            this.splashLabelNormal = new System.Windows.Forms.Label();
            this.entryBox = new System.Windows.Forms.TextBox();
            this.Preview = new System.Windows.Forms.Label();
            this.splashLabelStylish = new System.Windows.Forms.Label();
            this.changeStylishColor = new System.Windows.Forms.Button();
            this.splashScriptMenu = new System.Windows.Forms.MenuStrip();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomtextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextyearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.branchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextbranchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.fromDate = new System.Windows.Forms.Label();
            this.toDate = new System.Windows.Forms.Label();
            this.persistantToDate = new System.Windows.Forms.Label();
            this.persistantFromDate = new System.Windows.Forms.Label();
            this.persistantTo = new System.Windows.Forms.Label();
            this.persistantFrom = new System.Windows.Forms.Label();
            this.persistant = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.specialSplashTesting = new System.Windows.Forms.CheckBox();
            this.contextToolTipNormal = new System.Windows.Forms.ToolTip(this.components);
            this.contextToolTipStylish = new System.Windows.Forms.ToolTip(this.components);
            this.splashScriptMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splashLabelNormal
            // 
            this.splashLabelNormal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.splashLabelNormal.AutoEllipsis = true;
            this.splashLabelNormal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splashLabelNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splashLabelNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splashLabelNormal.ForeColor = System.Drawing.Color.Black;
            this.splashLabelNormal.Location = new System.Drawing.Point(301, 23);
            this.splashLabelNormal.Name = "splashLabelNormal";
            this.splashLabelNormal.Size = new System.Drawing.Size(214, 17);
            this.splashLabelNormal.TabIndex = 0;
            this.splashLabelNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // entryBox
            // 
            this.entryBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entryBox.Location = new System.Drawing.Point(12, 123);
            this.entryBox.Multiline = true;
            this.entryBox.Name = "entryBox";
            this.entryBox.Size = new System.Drawing.Size(795, 117);
            this.entryBox.TabIndex = 52;
            this.entryBox.Text = "Novetus!";
            this.entryBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.entryBox.TextChanged += new System.EventHandler(this.entryBox_TextChanged);
            // 
            // Preview
            // 
            this.Preview.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Preview.AutoSize = true;
            this.Preview.Location = new System.Drawing.Point(385, 7);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(45, 13);
            this.Preview.TabIndex = 53;
            this.Preview.Text = "Preview";
            // 
            // splashLabelStylish
            // 
            this.splashLabelStylish.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.splashLabelStylish.AutoEllipsis = true;
            this.splashLabelStylish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(135)))), ((int)(((byte)(13)))));
            this.splashLabelStylish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splashLabelStylish.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold);
            this.splashLabelStylish.ForeColor = System.Drawing.Color.White;
            this.splashLabelStylish.Location = new System.Drawing.Point(64, 60);
            this.splashLabelStylish.Name = "splashLabelStylish";
            this.splashLabelStylish.Size = new System.Drawing.Size(689, 32);
            this.splashLabelStylish.TabIndex = 54;
            this.splashLabelStylish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.splashLabelStylish.Paint += new System.Windows.Forms.PaintEventHandler(this.splashLabelStylish_Paint);
            // 
            // changeStylishColor
            // 
            this.changeStylishColor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.changeStylishColor.Location = new System.Drawing.Point(357, 95);
            this.changeStylishColor.Name = "changeStylishColor";
            this.changeStylishColor.Size = new System.Drawing.Size(103, 22);
            this.changeStylishColor.TabIndex = 55;
            this.changeStylishColor.Text = "Change Color";
            this.changeStylishColor.UseVisualStyleBackColor = true;
            this.changeStylishColor.Click += new System.EventHandler(this.changeStylishColor_Click);
            // 
            // splashScriptMenu
            // 
            this.splashScriptMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splashScriptMenu.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splashScriptMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.splashScriptMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem});
            this.splashScriptMenu.Location = new System.Drawing.Point(3, 3);
            this.splashScriptMenu.Name = "splashScriptMenu";
            this.splashScriptMenu.Size = new System.Drawing.Size(123, 24);
            this.splashScriptMenu.TabIndex = 56;
            this.splashScriptMenu.Text = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagsToolStripMenuItem,
            this.variablesToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.addToolStripMenuItem.Text = "Splash Formatting";
            // 
            // tagsToolStripMenuItem
            // 
            this.tagsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientToolStripMenuItem,
            this.serverToolStripMenuItem});
            this.tagsToolStripMenuItem.Name = "tagsToolStripMenuItem";
            this.tagsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.tagsToolStripMenuItem.Text = "Add Tags";
            // 
            // clientToolStripMenuItem
            // 
            this.clientToolStripMenuItem.Name = "clientToolStripMenuItem";
            this.clientToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.clientToolStripMenuItem.Text = "[stylish]";
            this.clientToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.serverToolStripMenuItem.Text = "[normal]";
            this.serverToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // variablesToolStripMenuItem
            // 
            this.variablesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameToolStripMenuItem,
            this.randomtextToolStripMenuItem,
            this.versionToolStripMenuItem,
            this.yearToolStripMenuItem,
            this.dayToolStripMenuItem,
            this.monthToolStripMenuItem,
            this.nextyearToolStripMenuItem,
            this.newlineToolStripMenuItem,
            this.branchToolStripMenuItem,
            this.nextbranchToolStripMenuItem});
            this.variablesToolStripMenuItem.Name = "variablesToolStripMenuItem";
            this.variablesToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.variablesToolStripMenuItem.Text = "Add Variables";
            // 
            // nameToolStripMenuItem
            // 
            this.nameToolStripMenuItem.Name = "nameToolStripMenuItem";
            this.nameToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.nameToolStripMenuItem.Text = "%name%";
            this.nameToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // randomtextToolStripMenuItem
            // 
            this.randomtextToolStripMenuItem.Name = "randomtextToolStripMenuItem";
            this.randomtextToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.randomtextToolStripMenuItem.Text = "%randomtext%";
            this.randomtextToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.versionToolStripMenuItem.Text = "%version%";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // yearToolStripMenuItem
            // 
            this.yearToolStripMenuItem.Name = "yearToolStripMenuItem";
            this.yearToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.yearToolStripMenuItem.Text = "%year%";
            this.yearToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // dayToolStripMenuItem
            // 
            this.dayToolStripMenuItem.Name = "dayToolStripMenuItem";
            this.dayToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.dayToolStripMenuItem.Text = "%day%";
            this.dayToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // monthToolStripMenuItem
            // 
            this.monthToolStripMenuItem.Name = "monthToolStripMenuItem";
            this.monthToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.monthToolStripMenuItem.Text = "%month%";
            this.monthToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // nextyearToolStripMenuItem
            // 
            this.nextyearToolStripMenuItem.Name = "nextyearToolStripMenuItem";
            this.nextyearToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.nextyearToolStripMenuItem.Text = "%nextyear%";
            this.nextyearToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // newlineToolStripMenuItem
            // 
            this.newlineToolStripMenuItem.Name = "newlineToolStripMenuItem";
            this.newlineToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.newlineToolStripMenuItem.Text = "%newline%";
            this.newlineToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // branchToolStripMenuItem
            // 
            this.branchToolStripMenuItem.Name = "branchToolStripMenuItem";
            this.branchToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.branchToolStripMenuItem.Text = "%branch%";
            this.branchToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // nextbranchToolStripMenuItem
            // 
            this.nextbranchToolStripMenuItem.Name = "nextbranchToolStripMenuItem";
            this.nextbranchToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.nextbranchToolStripMenuItem.Text = "%nextbranch%";
            this.nextbranchToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Location = new System.Drawing.Point(554, 23);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(30, 13);
            this.fromLabel.TabIndex = 57;
            this.fromLabel.Text = "From";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(622, 23);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(20, 13);
            this.toLabel.TabIndex = 58;
            this.toLabel.Text = "To";
            // 
            // fromDate
            // 
            this.fromDate.AutoSize = true;
            this.fromDate.Location = new System.Drawing.Point(554, 38);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(0, 13);
            this.fromDate.TabIndex = 59;
            // 
            // toDate
            // 
            this.toDate.AutoSize = true;
            this.toDate.Location = new System.Drawing.Point(619, 38);
            this.toDate.Name = "toDate";
            this.toDate.Size = new System.Drawing.Size(0, 13);
            this.toDate.TabIndex = 61;
            // 
            // persistantToDate
            // 
            this.persistantToDate.AutoSize = true;
            this.persistantToDate.Location = new System.Drawing.Point(754, 38);
            this.persistantToDate.Name = "persistantToDate";
            this.persistantToDate.Size = new System.Drawing.Size(0, 13);
            this.persistantToDate.TabIndex = 65;
            // 
            // persistantFromDate
            // 
            this.persistantFromDate.AutoSize = true;
            this.persistantFromDate.Location = new System.Drawing.Point(689, 38);
            this.persistantFromDate.Name = "persistantFromDate";
            this.persistantFromDate.Size = new System.Drawing.Size(0, 13);
            this.persistantFromDate.TabIndex = 64;
            // 
            // persistantTo
            // 
            this.persistantTo.AutoSize = true;
            this.persistantTo.Location = new System.Drawing.Point(734, 23);
            this.persistantTo.Name = "persistantTo";
            this.persistantTo.Size = new System.Drawing.Size(63, 13);
            this.persistantTo.TabIndex = 63;
            this.persistantTo.Text = "Increase on";
            // 
            // persistantFrom
            // 
            this.persistantFrom.AutoSize = true;
            this.persistantFrom.Location = new System.Drawing.Point(689, 23);
            this.persistantFrom.Name = "persistantFrom";
            this.persistantFrom.Size = new System.Drawing.Size(29, 13);
            this.persistantFrom.TabIndex = 62;
            this.persistantFrom.Text = "Start";
            // 
            // persistant
            // 
            this.persistant.AutoSize = true;
            this.persistant.Location = new System.Drawing.Point(694, 7);
            this.persistant.Name = "persistant";
            this.persistant.Size = new System.Drawing.Size(85, 13);
            this.persistant.TabIndex = 66;
            this.persistant.Text = "Low Persistance";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(579, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "Lifetime";
            // 
            // specialSplashTesting
            // 
            this.specialSplashTesting.AutoSize = true;
            this.specialSplashTesting.Location = new System.Drawing.Point(18, 31);
            this.specialSplashTesting.Name = "specialSplashTesting";
            this.specialSplashTesting.Size = new System.Drawing.Size(134, 17);
            this.specialSplashTesting.TabIndex = 68;
            this.specialSplashTesting.Text = "Special Splash Testing";
            this.specialSplashTesting.UseVisualStyleBackColor = true;
            this.specialSplashTesting.CheckedChanged += new System.EventHandler(this.entryBox_TextChanged);
            // 
            // SplashTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(819, 247);
            this.Controls.Add(this.specialSplashTesting);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.persistant);
            this.Controls.Add(this.persistantToDate);
            this.Controls.Add(this.persistantFromDate);
            this.Controls.Add(this.persistantTo);
            this.Controls.Add(this.persistantFrom);
            this.Controls.Add(this.toDate);
            this.Controls.Add(this.fromDate);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.fromLabel);
            this.Controls.Add(this.splashScriptMenu);
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
            this.splashScriptMenu.ResumeLayout(false);
            this.splashScriptMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    private System.Windows.Forms.Label splashLabelNormal;
    private System.Windows.Forms.TextBox entryBox;
    private System.Windows.Forms.Label Preview;
    private System.Windows.Forms.Label splashLabelStylish;
    private System.Windows.Forms.Button changeStylishColor;
    private System.Windows.Forms.MenuStrip splashScriptMenu;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tagsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clientToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem variablesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem nameToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem randomtextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem yearToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem dayToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem monthToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem nextyearToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newlineToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem branchToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem nextbranchToolStripMenuItem;
    private System.Windows.Forms.Label fromLabel;
    private System.Windows.Forms.Label toLabel;
    private System.Windows.Forms.Label fromDate;
    private System.Windows.Forms.Label toDate;
    private System.Windows.Forms.Label persistantToDate;
    private System.Windows.Forms.Label persistantFromDate;
    private System.Windows.Forms.Label persistantTo;
    private System.Windows.Forms.Label persistantFrom;
    private System.Windows.Forms.Label persistant;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox specialSplashTesting;
    private System.Windows.Forms.ToolTip contextToolTipNormal;
    private System.Windows.Forms.ToolTip contextToolTipStylish;
}
