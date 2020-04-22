/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 2/5/2017
 * Time: 1:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
//#define EDITORLAYOUT1 //comment this out to edit the 1.1 layout.
#define EDITORLAYOUT2 //comment this out to edit the 1.2 layout.
//#define RETAIL //for release and testing.
namespace NovetusLauncher
{
	partial class CharacterCustomization
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
#if RETAIL
            if (GlobalVars.OldLayout == false)
            {
#endif
#if EDITORLAYOUT1
                this.components = new System.ComponentModel.Container();
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterCustomization));
                this.imageList1 = new System.Windows.Forms.ImageList(this.components);
                this.panel1 = new System.Windows.Forms.Panel();
                this.button80 = new System.Windows.Forms.Button();
                this.button79 = new System.Windows.Forms.Button();
                this.button78 = new System.Windows.Forms.Button();
                this.button77 = new System.Windows.Forms.Button();
                this.button76 = new System.Windows.Forms.Button();
                this.button75 = new System.Windows.Forms.Button();
                this.button74 = new System.Windows.Forms.Button();
                this.button73 = new System.Windows.Forms.Button();
                this.button72 = new System.Windows.Forms.Button();
                this.panel2 = new System.Windows.Forms.Panel();
                this.tabControl1 = new TabControlWithoutHeader(1);
                this.tabPage1 = new System.Windows.Forms.TabPage();
                this.groupBox3 = new System.Windows.Forms.GroupBox();
                this.button61 = new System.Windows.Forms.Button();
                this.button65 = new System.Windows.Forms.Button();
                this.button62 = new System.Windows.Forms.Button();
                this.button66 = new System.Windows.Forms.Button();
                this.button63 = new System.Windows.Forms.Button();
                this.button67 = new System.Windows.Forms.Button();
                this.button64 = new System.Windows.Forms.Button();
                this.button68 = new System.Windows.Forms.Button();
                this.groupBox2 = new System.Windows.Forms.GroupBox();
                this.button2 = new System.Windows.Forms.Button();
                this.button1 = new System.Windows.Forms.Button();
                this.button3 = new System.Windows.Forms.Button();
                this.button4 = new System.Windows.Forms.Button();
                this.button5 = new System.Windows.Forms.Button();
                this.button6 = new System.Windows.Forms.Button();
                this.groupBox1 = new System.Windows.Forms.GroupBox();
                this.button70 = new System.Windows.Forms.Button();
                this.button7 = new System.Windows.Forms.Button();
                this.button69 = new System.Windows.Forms.Button();
                this.button8 = new System.Windows.Forms.Button();
                this.button9 = new System.Windows.Forms.Button();
                this.button10 = new System.Windows.Forms.Button();
                this.button14 = new System.Windows.Forms.Button();
                this.button35 = new System.Windows.Forms.Button();
                this.button13 = new System.Windows.Forms.Button();
                this.button36 = new System.Windows.Forms.Button();
                this.button12 = new System.Windows.Forms.Button();
                this.button37 = new System.Windows.Forms.Button();
                this.button11 = new System.Windows.Forms.Button();
                this.button38 = new System.Windows.Forms.Button();
                this.button18 = new System.Windows.Forms.Button();
                this.button31 = new System.Windows.Forms.Button();
                this.button17 = new System.Windows.Forms.Button();
                this.button32 = new System.Windows.Forms.Button();
                this.button16 = new System.Windows.Forms.Button();
                this.button33 = new System.Windows.Forms.Button();
                this.button15 = new System.Windows.Forms.Button();
                this.button34 = new System.Windows.Forms.Button();
                this.button22 = new System.Windows.Forms.Button();
                this.button27 = new System.Windows.Forms.Button();
                this.button21 = new System.Windows.Forms.Button();
                this.button28 = new System.Windows.Forms.Button();
                this.button20 = new System.Windows.Forms.Button();
                this.button29 = new System.Windows.Forms.Button();
                this.button19 = new System.Windows.Forms.Button();
                this.button30 = new System.Windows.Forms.Button();
                this.button26 = new System.Windows.Forms.Button();
                this.button23 = new System.Windows.Forms.Button();
                this.button25 = new System.Windows.Forms.Button();
                this.button24 = new System.Windows.Forms.Button();
                this.button39 = new System.Windows.Forms.Button();
                this.button40 = new System.Windows.Forms.Button();
                this.label2 = new System.Windows.Forms.Label();
                this.label1 = new System.Windows.Forms.Label();
                this.tabPage2 = new System.Windows.Forms.TabPage();
                this.tabControl2 = new TabControlWithoutHeader(1);
                this.tabPage10 = new System.Windows.Forms.TabPage();
                this.label10 = new System.Windows.Forms.Label();
                this.textBox2 = new System.Windows.Forms.TextBox();
                this.listBox1 = new System.Windows.Forms.ListBox();
                this.pictureBox1 = new System.Windows.Forms.PictureBox();
                this.tabPage11 = new System.Windows.Forms.TabPage();
                this.label11 = new System.Windows.Forms.Label();
                this.textBox3 = new System.Windows.Forms.TextBox();
                this.listBox2 = new System.Windows.Forms.ListBox();
                this.pictureBox2 = new System.Windows.Forms.PictureBox();
                this.tabPage12 = new System.Windows.Forms.TabPage();
                this.label12 = new System.Windows.Forms.Label();
                this.textBox4 = new System.Windows.Forms.TextBox();
                this.listBox3 = new System.Windows.Forms.ListBox();
                this.pictureBox3 = new System.Windows.Forms.PictureBox();
                this.tabPage8 = new System.Windows.Forms.TabPage();
                this.textBox5 = new System.Windows.Forms.TextBox();
                this.button56 = new System.Windows.Forms.Button();
                this.button57 = new System.Windows.Forms.Button();
                this.pictureBox8 = new System.Windows.Forms.PictureBox();
                this.listBox8 = new System.Windows.Forms.ListBox();
                this.tabPage3 = new System.Windows.Forms.TabPage();
                this.textBox6 = new System.Windows.Forms.TextBox();
                this.button44 = new System.Windows.Forms.Button();
                this.button45 = new System.Windows.Forms.Button();
                this.pictureBox4 = new System.Windows.Forms.PictureBox();
                this.listBox4 = new System.Windows.Forms.ListBox();
                this.tabPage4 = new System.Windows.Forms.TabPage();
                this.textBox7 = new System.Windows.Forms.TextBox();
                this.button46 = new System.Windows.Forms.Button();
                this.button47 = new System.Windows.Forms.Button();
                this.pictureBox5 = new System.Windows.Forms.PictureBox();
                this.listBox5 = new System.Windows.Forms.ListBox();
                this.tabPage5 = new System.Windows.Forms.TabPage();
                this.textBox8 = new System.Windows.Forms.TextBox();
                this.button48 = new System.Windows.Forms.Button();
                this.button49 = new System.Windows.Forms.Button();
                this.pictureBox6 = new System.Windows.Forms.PictureBox();
                this.listBox6 = new System.Windows.Forms.ListBox();
                this.tabPage6 = new System.Windows.Forms.TabPage();
                this.textBox9 = new System.Windows.Forms.TextBox();
                this.button50 = new System.Windows.Forms.Button();
                this.button51 = new System.Windows.Forms.Button();
                this.pictureBox7 = new System.Windows.Forms.PictureBox();
                this.listBox7 = new System.Windows.Forms.ListBox();
                this.tabPage9 = new System.Windows.Forms.TabPage();
                this.textBox10 = new System.Windows.Forms.TextBox();
                this.checkBox1 = new System.Windows.Forms.CheckBox();
                this.button58 = new System.Windows.Forms.Button();
                this.button59 = new System.Windows.Forms.Button();
                this.pictureBox9 = new System.Windows.Forms.PictureBox();
                this.listBox9 = new System.Windows.Forms.ListBox();
                this.tabPage7 = new System.Windows.Forms.TabPage();
                this.button71 = new System.Windows.Forms.Button();
                this.label8 = new System.Windows.Forms.Label();
                this.pictureBox10 = new System.Windows.Forms.PictureBox();
                this.button60 = new System.Windows.Forms.Button();
                this.button43 = new System.Windows.Forms.Button();
                this.textBox1 = new System.Windows.Forms.TextBox();
                this.label7 = new System.Windows.Forms.Label();
                this.label6 = new System.Windows.Forms.Label();
                this.button55 = new System.Windows.Forms.Button();
                this.label5 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                this.label3 = new System.Windows.Forms.Label();
                this.button54 = new System.Windows.Forms.Button();
                this.button53 = new System.Windows.Forms.Button();
                this.button52 = new System.Windows.Forms.Button();
                this.panel3 = new System.Windows.Forms.Panel();
                this.button41 = new System.Windows.Forms.Button();
                this.button83 = new System.Windows.Forms.Button();
                this.button82 = new System.Windows.Forms.Button();
                this.button42 = new System.Windows.Forms.Button();
                this.button81 = new System.Windows.Forms.Button();
                this.label9 = new System.Windows.Forms.Label();
                this.panel1.SuspendLayout();
                this.panel2.SuspendLayout();
                this.tabControl1.SuspendLayout();
                this.tabPage1.SuspendLayout();
                this.groupBox3.SuspendLayout();
                this.groupBox2.SuspendLayout();
                this.groupBox1.SuspendLayout();
                this.tabPage2.SuspendLayout();
                this.tabControl2.SuspendLayout();
                this.tabPage10.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                this.tabPage11.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
                this.tabPage12.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
                this.tabPage8.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
                this.tabPage3.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
                this.tabPage4.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
                this.tabPage5.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
                this.tabPage6.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
                this.tabPage9.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
                this.tabPage7.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
                this.panel3.SuspendLayout();
                this.SuspendLayout();
                // 
                // imageList1
                // 
                this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
                this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
                this.imageList1.Images.SetKeyName(0, "BC.png");
                this.imageList1.Images.SetKeyName(1, "TBC.png");
                this.imageList1.Images.SetKeyName(2, "OBC.png");
                this.imageList1.ImageSize = new System.Drawing.Size(64,64);
                // 
                // panel1
                // 
                this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                this.panel1.Controls.Add(this.label9);
                this.panel1.Controls.Add(this.button71);
                this.panel1.Controls.Add(this.button80);
                this.panel1.Controls.Add(this.button79);
                this.panel1.Controls.Add(this.button78);
                this.panel1.Controls.Add(this.button77);
                this.panel1.Controls.Add(this.button43);
                this.panel1.Controls.Add(this.button76);
                this.panel1.Controls.Add(this.button75);
                this.panel1.Controls.Add(this.button74);
                this.panel1.Controls.Add(this.button73);
                this.panel1.Controls.Add(this.button72);
                this.panel1.Location = new System.Drawing.Point(2, 3);
                this.panel1.Name = "panel1";
                this.panel1.Size = new System.Drawing.Size(85, 302);
                this.panel1.TabIndex = 1;
                // 
                // button80
                // 
                this.button80.Location = new System.Drawing.Point(3, 211);
                this.button80.Name = "button80";
                this.button80.Size = new System.Drawing.Size(75, 23);
                this.button80.TabIndex = 8;
                this.button80.Text = "OTHER";
                this.button80.UseVisualStyleBackColor = true;
                this.button80.Click += new System.EventHandler(this.button80_Click);
                // 
                // button79
                // 
                this.button79.Location = new System.Drawing.Point(3, 185);
                this.button79.Name = "button79";
                this.button79.Size = new System.Drawing.Size(75, 23);
                this.button79.TabIndex = 7;
                this.button79.Text = "EXTRA";
                this.button79.UseVisualStyleBackColor = true;
                this.button79.Click += new System.EventHandler(this.button79_Click);
                // 
                // button78
                // 
                this.button78.Location = new System.Drawing.Point(3, 159);
                this.button78.Name = "button78";
                this.button78.Size = new System.Drawing.Size(75, 23);
                this.button78.TabIndex = 6;
                this.button78.Text = "PANTS";
                this.button78.UseVisualStyleBackColor = true;
                this.button78.Click += new System.EventHandler(this.button78_Click);
                // 
                // button77
                // 
                this.button77.Location = new System.Drawing.Point(3, 133);
                this.button77.Name = "button77";
                this.button77.Size = new System.Drawing.Size(75, 23);
                this.button77.TabIndex = 5;
                this.button77.Text = "SHIRTS";
                this.button77.UseVisualStyleBackColor = true;
                this.button77.Click += new System.EventHandler(this.button77_Click);
                // 
                // button76
                // 
                this.button76.Location = new System.Drawing.Point(3, 107);
                this.button76.Name = "button76";
                this.button76.Size = new System.Drawing.Size(75, 23);
                this.button76.TabIndex = 4;
                this.button76.Text = "T-SHIRTS";
                this.button76.UseVisualStyleBackColor = true;
                this.button76.Click += new System.EventHandler(this.button76_Click);
                // 
                // button75
                // 
                this.button75.Location = new System.Drawing.Point(3, 81);
                this.button75.Name = "button75";
                this.button75.Size = new System.Drawing.Size(75, 23);
                this.button75.TabIndex = 3;
                this.button75.Text = "FACES";
                this.button75.UseVisualStyleBackColor = true;
                this.button75.Click += new System.EventHandler(this.button75_Click);
                // 
                // button74
                // 
                this.button74.Location = new System.Drawing.Point(3, 55);
                this.button74.Name = "button74";
                this.button74.Size = new System.Drawing.Size(75, 23);
                this.button74.TabIndex = 2;
                this.button74.Text = "HEADS";
                this.button74.UseVisualStyleBackColor = true;
                this.button74.Click += new System.EventHandler(this.button74_Click);
                // 
                // button73
                // 
                this.button73.Location = new System.Drawing.Point(3, 29);
                this.button73.Name = "button73";
                this.button73.Size = new System.Drawing.Size(75, 23);
                this.button73.TabIndex = 1;
                this.button73.Text = "HATS";
                this.button73.UseVisualStyleBackColor = true;
                this.button73.Click += new System.EventHandler(this.button73_Click);
                // 
                // button72
                // 
                this.button72.Location = new System.Drawing.Point(3, 3);
                this.button72.Name = "button72";
                this.button72.Size = new System.Drawing.Size(75, 23);
                this.button72.TabIndex = 0;
                this.button72.Text = "BODY";
                this.button72.UseVisualStyleBackColor = true;
                this.button72.Click += new System.EventHandler(this.button72_Click);
                // 
                // panel2
                // 
                this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                this.panel2.Controls.Add(this.tabControl1);
                this.panel2.Location = new System.Drawing.Point(93, 3);
                this.panel2.Name = "panel2";
                this.panel2.Size = new System.Drawing.Size(568, 329);
                this.panel2.TabIndex = 2;
                // 
                // tabControl1
                // 
                this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
                this.tabControl1.Controls.Add(this.tabPage1);
                this.tabControl1.Controls.Add(this.tabPage2);
                this.tabControl1.Controls.Add(this.tabPage8);
                this.tabControl1.Controls.Add(this.tabPage3);
                this.tabControl1.Controls.Add(this.tabPage4);
                this.tabControl1.Controls.Add(this.tabPage5);
                this.tabControl1.Controls.Add(this.tabPage6);
                this.tabControl1.Controls.Add(this.tabPage9);
                this.tabControl1.Controls.Add(this.tabPage7);
                this.tabControl1.Location = new System.Drawing.Point(3, 3);
                this.tabControl1.Multiline = true;
                this.tabControl1.Name = "tabControl1";
                this.tabControl1.SelectedIndex = 0;
                this.tabControl1.Size = new System.Drawing.Size(557, 319);
                this.tabControl1.TabIndex = 0;
                this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
                // 
                // tabPage1
                // 
                this.tabPage1.Controls.Add(this.groupBox3);
                this.tabPage1.Controls.Add(this.groupBox2);
                this.tabPage1.Controls.Add(this.groupBox1);
                this.tabPage1.Controls.Add(this.button39);
                this.tabPage1.Controls.Add(this.button40);
                this.tabPage1.Controls.Add(this.label2);
                this.tabPage1.Controls.Add(this.label1);
                this.tabPage1.Location = new System.Drawing.Point(4, 4);
                this.tabPage1.Name = "tabPage1";
                this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage1.Size = new System.Drawing.Size(549, 293);
                this.tabPage1.TabIndex = 0;
                this.tabPage1.Text = "BODY";
                this.tabPage1.UseVisualStyleBackColor = true;
                // 
                // groupBox3
                // 
                this.groupBox3.Controls.Add(this.button61);
                this.groupBox3.Controls.Add(this.button65);
                this.groupBox3.Controls.Add(this.button62);
                this.groupBox3.Controls.Add(this.button66);
                this.groupBox3.Controls.Add(this.button63);
                this.groupBox3.Controls.Add(this.button67);
                this.groupBox3.Controls.Add(this.button64);
                this.groupBox3.Controls.Add(this.button68);
                this.groupBox3.Location = new System.Drawing.Point(411, 210);
                this.groupBox3.Name = "groupBox3";
                this.groupBox3.Size = new System.Drawing.Size(109, 68);
                this.groupBox3.TabIndex = 60;
                this.groupBox3.TabStop = false;
                this.groupBox3.Text = "\'06 Color Presets";
                // 
                // button61
                // 
                this.button61.Location = new System.Drawing.Point(6, 16);
                this.button61.Name = "button61";
                this.button61.Size = new System.Drawing.Size(24, 23);
                this.button61.TabIndex = 51;
                this.button61.Text = "1";
                this.button61.UseVisualStyleBackColor = true;
                this.button61.Click += new System.EventHandler(this.button61_Click);
                // 
                // button65
                // 
                this.button65.Location = new System.Drawing.Point(81, 42);
                this.button65.Name = "button65";
                this.button65.Size = new System.Drawing.Size(24, 23);
                this.button65.TabIndex = 58;
                this.button65.Text = "8";
                this.button65.UseVisualStyleBackColor = true;
                this.button65.Click += new System.EventHandler(this.button65_Click);
                // 
                // button62
                // 
                this.button62.Location = new System.Drawing.Point(31, 16);
                this.button62.Name = "button62";
                this.button62.Size = new System.Drawing.Size(24, 23);
                this.button62.TabIndex = 52;
                this.button62.Text = "2";
                this.button62.UseVisualStyleBackColor = true;
                this.button62.Click += new System.EventHandler(this.button62_Click);
                // 
                // button66
                // 
                this.button66.Location = new System.Drawing.Point(56, 42);
                this.button66.Name = "button66";
                this.button66.Size = new System.Drawing.Size(24, 23);
                this.button66.TabIndex = 57;
                this.button66.Text = "7";
                this.button66.UseVisualStyleBackColor = true;
                this.button66.Click += new System.EventHandler(this.button66_Click);
                // 
                // button63
                // 
                this.button63.Location = new System.Drawing.Point(56, 16);
                this.button63.Name = "button63";
                this.button63.Size = new System.Drawing.Size(24, 23);
                this.button63.TabIndex = 53;
                this.button63.Text = "3";
                this.button63.UseVisualStyleBackColor = true;
                this.button63.Click += new System.EventHandler(this.button63_Click);
                // 
                // button67
                // 
                this.button67.Location = new System.Drawing.Point(31, 42);
                this.button67.Name = "button67";
                this.button67.Size = new System.Drawing.Size(24, 23);
                this.button67.TabIndex = 56;
                this.button67.Text = "6";
                this.button67.UseVisualStyleBackColor = true;
                this.button67.Click += new System.EventHandler(this.button67_Click);
                // 
                // button64
                // 
                this.button64.Location = new System.Drawing.Point(81, 16);
                this.button64.Name = "button64";
                this.button64.Size = new System.Drawing.Size(24, 23);
                this.button64.TabIndex = 54;
                this.button64.Text = "4";
                this.button64.UseVisualStyleBackColor = true;
                this.button64.Click += new System.EventHandler(this.button64_Click);
                // 
                // button68
                // 
                this.button68.Location = new System.Drawing.Point(6, 42);
                this.button68.Name = "button68";
                this.button68.Size = new System.Drawing.Size(24, 23);
                this.button68.TabIndex = 55;
                this.button68.Text = "5";
                this.button68.UseVisualStyleBackColor = true;
                this.button68.Click += new System.EventHandler(this.button68_Click);
                // 
                // groupBox2
                // 
                this.groupBox2.Controls.Add(this.button2);
                this.groupBox2.Controls.Add(this.button1);
                this.groupBox2.Controls.Add(this.button3);
                this.groupBox2.Controls.Add(this.button4);
                this.groupBox2.Controls.Add(this.button5);
                this.groupBox2.Controls.Add(this.button6);
                this.groupBox2.Location = new System.Drawing.Point(6, 6);
                this.groupBox2.Name = "groupBox2";
                this.groupBox2.Size = new System.Drawing.Size(187, 281);
                this.groupBox2.TabIndex = 50;
                this.groupBox2.TabStop = false;
                this.groupBox2.Text = "Body Parts";
                // 
                // button2
                // 
                this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(105)))), ((int)(((byte)(172)))));
                this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button2.Location = new System.Drawing.Point(55, 90);
                this.button2.Name = "button2";
                this.button2.Size = new System.Drawing.Size(77, 87);
                this.button2.TabIndex = 1;
                this.button2.UseVisualStyleBackColor = false;
                this.button2.Click += new System.EventHandler(this.Button2Click);
                // 
                // button1
                // 
                this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(205)))), ((int)(((byte)(47)))));
                this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button1.Location = new System.Drawing.Point(65, 26);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(58, 59);
                this.button1.TabIndex = 0;
                this.button1.UseVisualStyleBackColor = false;
                this.button1.Click += new System.EventHandler(this.Button1Click);
                // 
                // button3
                // 
                this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(205)))), ((int)(((byte)(47)))));
                this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button3.Location = new System.Drawing.Point(12, 90);
                this.button3.Name = "button3";
                this.button3.Size = new System.Drawing.Size(37, 87);
                this.button3.TabIndex = 2;
                this.button3.UseVisualStyleBackColor = false;
                this.button3.Click += new System.EventHandler(this.Button3Click);
                // 
                // button4
                // 
                this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(205)))), ((int)(((byte)(47)))));
                this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button4.Location = new System.Drawing.Point(138, 90);
                this.button4.Name = "button4";
                this.button4.Size = new System.Drawing.Size(37, 87);
                this.button4.TabIndex = 3;
                this.button4.UseVisualStyleBackColor = false;
                this.button4.Click += new System.EventHandler(this.Button4Click);
                // 
                // button5
                // 
                this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(189)))), ((int)(((byte)(71)))));
                this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button5.Location = new System.Drawing.Point(55, 182);
                this.button5.Name = "button5";
                this.button5.Size = new System.Drawing.Size(37, 87);
                this.button5.TabIndex = 4;
                this.button5.UseVisualStyleBackColor = false;
                this.button5.Click += new System.EventHandler(this.Button5Click);
                // 
                // button6
                // 
                this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(189)))), ((int)(((byte)(71)))));
                this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button6.Location = new System.Drawing.Point(95, 182);
                this.button6.Name = "button6";
                this.button6.Size = new System.Drawing.Size(37, 87);
                this.button6.TabIndex = 5;
                this.button6.UseVisualStyleBackColor = false;
                this.button6.Click += new System.EventHandler(this.Button6Click);
                // 
                // groupBox1
                // 
                this.groupBox1.Controls.Add(this.button70);
                this.groupBox1.Controls.Add(this.button7);
                this.groupBox1.Controls.Add(this.button69);
                this.groupBox1.Controls.Add(this.button8);
                this.groupBox1.Controls.Add(this.button9);
                this.groupBox1.Controls.Add(this.button10);
                this.groupBox1.Controls.Add(this.button14);
                this.groupBox1.Controls.Add(this.button35);
                this.groupBox1.Controls.Add(this.button13);
                this.groupBox1.Controls.Add(this.button36);
                this.groupBox1.Controls.Add(this.button12);
                this.groupBox1.Controls.Add(this.button37);
                this.groupBox1.Controls.Add(this.button11);
                this.groupBox1.Controls.Add(this.button38);
                this.groupBox1.Controls.Add(this.button18);
                this.groupBox1.Controls.Add(this.button31);
                this.groupBox1.Controls.Add(this.button17);
                this.groupBox1.Controls.Add(this.button32);
                this.groupBox1.Controls.Add(this.button16);
                this.groupBox1.Controls.Add(this.button33);
                this.groupBox1.Controls.Add(this.button15);
                this.groupBox1.Controls.Add(this.button34);
                this.groupBox1.Controls.Add(this.button22);
                this.groupBox1.Controls.Add(this.button27);
                this.groupBox1.Controls.Add(this.button21);
                this.groupBox1.Controls.Add(this.button28);
                this.groupBox1.Controls.Add(this.button20);
                this.groupBox1.Controls.Add(this.button29);
                this.groupBox1.Controls.Add(this.button19);
                this.groupBox1.Controls.Add(this.button30);
                this.groupBox1.Controls.Add(this.button26);
                this.groupBox1.Controls.Add(this.button23);
                this.groupBox1.Controls.Add(this.button25);
                this.groupBox1.Controls.Add(this.button24);
                this.groupBox1.Location = new System.Drawing.Point(204, 6);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(332, 201);
                this.groupBox1.TabIndex = 49;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "Part Colors";
                // 
                // button70
                // 
                this.button70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(122)))), ((int)(((byte)(89)))));
                this.button70.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button70.Location = new System.Drawing.Point(293, 124);
                this.button70.Name = "button70";
                this.button70.Size = new System.Drawing.Size(30, 30);
                this.button70.TabIndex = 33;
                this.button70.UseVisualStyleBackColor = false;
                this.button70.Click += new System.EventHandler(this.button70_Click);
                // 
                // button7
                // 
                this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
                this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button7.Location = new System.Drawing.Point(6, 16);
                this.button7.Name = "button7";
                this.button7.Size = new System.Drawing.Size(30, 30);
                this.button7.TabIndex = 6;
                this.button7.UseVisualStyleBackColor = false;
                this.button7.Click += new System.EventHandler(this.Button7Click);
                // 
                // button69
                // 
                this.button69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(112)))), ((int)(((byte)(160)))));
                this.button69.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button69.Location = new System.Drawing.Point(47, 124);
                this.button69.Name = "button69";
                this.button69.Size = new System.Drawing.Size(30, 30);
                this.button69.TabIndex = 32;
                this.button69.UseVisualStyleBackColor = false;
                this.button69.Click += new System.EventHandler(this.button69_Click);
                // 
                // button8
                // 
                this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(229)))), ((int)(((byte)(224)))));
                this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button8.Location = new System.Drawing.Point(47, 16);
                this.button8.Name = "button8";
                this.button8.Size = new System.Drawing.Size(30, 30);
                this.button8.TabIndex = 7;
                this.button8.UseVisualStyleBackColor = false;
                this.button8.Click += new System.EventHandler(this.Button8Click);
                // 
                // button9
                // 
                this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(165)))));
                this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button9.Location = new System.Drawing.Point(88, 16);
                this.button9.Name = "button9";
                this.button9.Size = new System.Drawing.Size(30, 30);
                this.button9.TabIndex = 8;
                this.button9.UseVisualStyleBackColor = false;
                this.button9.Click += new System.EventHandler(this.Button9Click);
                // 
                // button10
                // 
                this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(95)))), ((int)(((byte)(96)))));
                this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button10.Location = new System.Drawing.Point(129, 16);
                this.button10.Name = "button10";
                this.button10.Size = new System.Drawing.Size(30, 30);
                this.button10.TabIndex = 9;
                this.button10.UseVisualStyleBackColor = false;
                this.button10.Click += new System.EventHandler(this.Button10Click);
                // 
                // button14
                // 
                this.button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(42)))), ((int)(((byte)(53)))));
                this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button14.Location = new System.Drawing.Point(170, 16);
                this.button14.Name = "button14";
                this.button14.Size = new System.Drawing.Size(30, 30);
                this.button14.TabIndex = 10;
                this.button14.UseVisualStyleBackColor = false;
                this.button14.Click += new System.EventHandler(this.Button14Click);
                // 
                // button35
                // 
                this.button35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(185)))), ((int)(((byte)(145)))));
                this.button35.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button35.Location = new System.Drawing.Point(47, 160);
                this.button35.Name = "button35";
                this.button35.Size = new System.Drawing.Size(30, 30);
                this.button35.TabIndex = 37;
                this.button35.UseVisualStyleBackColor = false;
                this.button35.Click += new System.EventHandler(this.Button35Click);
                // 
                // button13
                // 
                this.button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(40)))), ((int)(((byte)(27)))));
                this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button13.Location = new System.Drawing.Point(211, 16);
                this.button13.Name = "button13";
                this.button13.Size = new System.Drawing.Size(30, 30);
                this.button13.TabIndex = 11;
                this.button13.UseVisualStyleBackColor = false;
                this.button13.Click += new System.EventHandler(this.Button13Click);
                // 
                // button36
                // 
                this.button36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(142)))), ((int)(((byte)(105)))));
                this.button36.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button36.Location = new System.Drawing.Point(6, 160);
                this.button36.Name = "button36";
                this.button36.Size = new System.Drawing.Size(30, 30);
                this.button36.TabIndex = 36;
                this.button36.UseVisualStyleBackColor = false;
                this.button36.Click += new System.EventHandler(this.Button36Click);
                // 
                // button12
                // 
                this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(205)))), ((int)(((byte)(47)))));
                this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button12.Location = new System.Drawing.Point(252, 16);
                this.button12.Name = "button12";
                this.button12.Size = new System.Drawing.Size(30, 30);
                this.button12.TabIndex = 12;
                this.button12.UseVisualStyleBackColor = false;
                this.button12.Click += new System.EventHandler(this.Button12Click);
                // 
                // button37
                // 
                this.button37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(92)))), ((int)(((byte)(69)))));
                this.button37.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button37.Location = new System.Drawing.Point(252, 124);
                this.button37.Name = "button37";
                this.button37.Size = new System.Drawing.Size(30, 30);
                this.button37.TabIndex = 35;
                this.button37.UseVisualStyleBackColor = false;
                this.button37.Click += new System.EventHandler(this.Button37Click);
                // 
                // button11
                // 
                this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(234)))), ((int)(((byte)(142)))));
                this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button11.Location = new System.Drawing.Point(293, 16);
                this.button11.Name = "button11";
                this.button11.Size = new System.Drawing.Size(30, 30);
                this.button11.TabIndex = 13;
                this.button11.UseVisualStyleBackColor = false;
                this.button11.Click += new System.EventHandler(this.Button11Click);
                // 
                // button38
                // 
                this.button38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(122)))), ((int)(((byte)(118)))));
                this.button38.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button38.Location = new System.Drawing.Point(211, 124);
                this.button38.Name = "button38";
                this.button38.Size = new System.Drawing.Size(30, 30);
                this.button38.TabIndex = 34;
                this.button38.UseVisualStyleBackColor = false;
                this.button38.Click += new System.EventHandler(this.Button38Click);
                // 
                // button18
                // 
                this.button18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(105)))), ((int)(((byte)(172)))));
                this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button18.Location = new System.Drawing.Point(6, 52);
                this.button18.Name = "button18";
                this.button18.Size = new System.Drawing.Size(30, 30);
                this.button18.TabIndex = 14;
                this.button18.UseVisualStyleBackColor = false;
                this.button18.Click += new System.EventHandler(this.Button18Click);
                // 
                // button31
                // 
                this.button31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(196)))), ((int)(((byte)(153)))));
                this.button31.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button31.Location = new System.Drawing.Point(170, 124);
                this.button31.Name = "button31";
                this.button31.Size = new System.Drawing.Size(30, 30);
                this.button31.TabIndex = 33;
                this.button31.UseVisualStyleBackColor = false;
                this.button31.Click += new System.EventHandler(this.Button31Click);
                // 
                // button17
                // 
                this.button17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(143)))), ((int)(((byte)(155)))));
                this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button17.Location = new System.Drawing.Point(47, 52);
                this.button17.Name = "button17";
                this.button17.Size = new System.Drawing.Size(30, 30);
                this.button17.TabIndex = 15;
                this.button17.UseVisualStyleBackColor = false;
                this.button17.Click += new System.EventHandler(this.Button17Click);
                // 
                // button32
                // 
                this.button32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(135)))), ((int)(((byte)(121)))));
                this.button32.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button32.Location = new System.Drawing.Point(129, 124);
                this.button32.Name = "button32";
                this.button32.Size = new System.Drawing.Size(30, 30);
                this.button32.TabIndex = 32;
                this.button32.UseVisualStyleBackColor = false;
                this.button32.Click += new System.EventHandler(this.Button32Click);
                // 
                // button16
                // 
                this.button16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(153)))), ((int)(((byte)(201)))));
                this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button16.Location = new System.Drawing.Point(88, 52);
                this.button16.Name = "button16";
                this.button16.Size = new System.Drawing.Size(30, 30);
                this.button16.TabIndex = 16;
                this.button16.UseVisualStyleBackColor = false;
                this.button16.Click += new System.EventHandler(this.Button16Click);
                // 
                // button33
                // 
                this.button33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
                this.button33.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button33.Location = new System.Drawing.Point(88, 124);
                this.button33.Name = "button33";
                this.button33.Size = new System.Drawing.Size(30, 30);
                this.button33.TabIndex = 31;
                this.button33.UseVisualStyleBackColor = false;
                this.button33.Click += new System.EventHandler(this.Button33Click);
                // 
                // button15
                // 
                this.button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(186)))), ((int)(((byte)(219)))));
                this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button15.Location = new System.Drawing.Point(129, 52);
                this.button15.Name = "button15";
                this.button15.Size = new System.Drawing.Size(30, 30);
                this.button15.TabIndex = 17;
                this.button15.UseVisualStyleBackColor = false;
                this.button15.Click += new System.EventHandler(this.Button15Click);
                // 
                // button34
                // 
                this.button34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(50)))), ((int)(((byte)(123)))));
                this.button34.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button34.Location = new System.Drawing.Point(6, 124);
                this.button34.Name = "button34";
                this.button34.Size = new System.Drawing.Size(30, 30);
                this.button34.TabIndex = 30;
                this.button34.UseVisualStyleBackColor = false;
                this.button34.Click += new System.EventHandler(this.Button34Click);
                // 
                // button22
                // 
                this.button22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(210)))), ((int)(((byte)(228)))));
                this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button22.Location = new System.Drawing.Point(170, 52);
                this.button22.Name = "button22";
                this.button22.Size = new System.Drawing.Size(30, 30);
                this.button22.TabIndex = 18;
                this.button22.UseVisualStyleBackColor = false;
                this.button22.Click += new System.EventHandler(this.Button22Click);
                // 
                // button27
                // 
                this.button27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(63)))), ((int)(((byte)(39)))));
                this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button27.Location = new System.Drawing.Point(293, 88);
                this.button27.Name = "button27";
                this.button27.Size = new System.Drawing.Size(30, 30);
                this.button27.TabIndex = 29;
                this.button27.UseVisualStyleBackColor = false;
                this.button27.Click += new System.EventHandler(this.Button27Click);
                // 
                // button21
                // 
                this.button21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(134)))), ((int)(((byte)(156)))));
                this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button21.Location = new System.Drawing.Point(211, 52);
                this.button21.Name = "button21";
                this.button21.Size = new System.Drawing.Size(30, 30);
                this.button21.TabIndex = 19;
                this.button21.UseVisualStyleBackColor = false;
                this.button21.Click += new System.EventHandler(this.Button21Click);
                // 
                // button28
                // 
                this.button28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(95)))), ((int)(((byte)(55)))));
                this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button28.Location = new System.Drawing.Point(252, 88);
                this.button28.Name = "button28";
                this.button28.Size = new System.Drawing.Size(30, 30);
                this.button28.TabIndex = 28;
                this.button28.UseVisualStyleBackColor = false;
                this.button28.Click += new System.EventHandler(this.Button28Click);
                // 
                // button20
                // 
                this.button20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(134)))), ((int)(((byte)(64)))));
                this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button20.Location = new System.Drawing.Point(252, 52);
                this.button20.Name = "button20";
                this.button20.Size = new System.Drawing.Size(30, 30);
                this.button20.TabIndex = 20;
                this.button20.UseVisualStyleBackColor = false;
                this.button20.Click += new System.EventHandler(this.Button20Click);
                // 
                // button29
                // 
                this.button29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(130)))));
                this.button29.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button29.Location = new System.Drawing.Point(211, 88);
                this.button29.Name = "button29";
                this.button29.Size = new System.Drawing.Size(30, 30);
                this.button29.TabIndex = 27;
                this.button29.UseVisualStyleBackColor = false;
                this.button29.Click += new System.EventHandler(this.Button29Click);
                // 
                // button19
                // 
                this.button19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(155)))), ((int)(((byte)(63)))));
                this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button19.Location = new System.Drawing.Point(293, 52);
                this.button19.Name = "button19";
                this.button19.Size = new System.Drawing.Size(30, 30);
                this.button19.TabIndex = 21;
                this.button19.UseVisualStyleBackColor = false;
                this.button19.Click += new System.EventHandler(this.Button19Click);
                // 
                // button30
                // 
                this.button30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(196)))), ((int)(((byte)(140)))));
                this.button30.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button30.Location = new System.Drawing.Point(170, 88);
                this.button30.Name = "button30";
                this.button30.Size = new System.Drawing.Size(30, 30);
                this.button30.TabIndex = 26;
                this.button30.UseVisualStyleBackColor = false;
                this.button30.Click += new System.EventHandler(this.Button30Click);
                // 
                // button26
                // 
                this.button26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(70)))), ((int)(((byte)(43)))));
                this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button26.Location = new System.Drawing.Point(6, 88);
                this.button26.Name = "button26";
                this.button26.Size = new System.Drawing.Size(30, 30);
                this.button26.TabIndex = 22;
                this.button26.UseVisualStyleBackColor = false;
                this.button26.Click += new System.EventHandler(this.Button26Click);
                // 
                // button23
                // 
                this.button23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(189)))), ((int)(((byte)(71)))));
                this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button23.Location = new System.Drawing.Point(129, 88);
                this.button23.Name = "button23";
                this.button23.Size = new System.Drawing.Size(30, 30);
                this.button23.TabIndex = 25;
                this.button23.UseVisualStyleBackColor = false;
                this.button23.Click += new System.EventHandler(this.Button23Click);
                // 
                // button25
                // 
                this.button25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(126)))), ((int)(((byte)(71)))));
                this.button25.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button25.Location = new System.Drawing.Point(47, 88);
                this.button25.Name = "button25";
                this.button25.Size = new System.Drawing.Size(30, 30);
                this.button25.TabIndex = 23;
                this.button25.UseVisualStyleBackColor = false;
                this.button25.Click += new System.EventHandler(this.Button25Click);
                // 
                // button24
                // 
                this.button24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(150)))), ((int)(((byte)(73)))));
                this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button24.Location = new System.Drawing.Point(88, 88);
                this.button24.Name = "button24";
                this.button24.Size = new System.Drawing.Size(30, 30);
                this.button24.TabIndex = 24;
                this.button24.UseVisualStyleBackColor = false;
                this.button24.Click += new System.EventHandler(this.Button24Click);
                // 
                // button39
                // 
                this.button39.Location = new System.Drawing.Point(230, 229);
                this.button39.Name = "button39";
                this.button39.Size = new System.Drawing.Size(89, 49);
                this.button39.TabIndex = 48;
                this.button39.Text = "Randomize Colors";
                this.button39.UseVisualStyleBackColor = true;
                this.button39.Click += new System.EventHandler(this.Button39Click);
                // 
                // button40
                // 
                this.button40.Location = new System.Drawing.Point(320, 229);
                this.button40.Name = "button40";
                this.button40.Size = new System.Drawing.Size(90, 49);
                this.button40.TabIndex = 47;
                this.button40.Text = "Reset Colors";
                this.button40.UseVisualStyleBackColor = true;
                this.button40.Click += new System.EventHandler(this.Button40Click);
                // 
                // label2
                // 
                this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.label2.Location = new System.Drawing.Point(330, 210);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(80, 16);
                this.label2.TabIndex = 46;
                // 
                // label1
                // 
                this.label1.Location = new System.Drawing.Point(230, 211);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(105, 16);
                this.label1.TabIndex = 45;
                this.label1.Text = "SELECTED PART:";
                // 
                // tabPage2
                // 
                this.tabPage2.Controls.Add(this.tabControl2);
                this.tabPage2.Location = new System.Drawing.Point(4, 4);
                this.tabPage2.Name = "tabPage2";
                this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage2.Size = new System.Drawing.Size(549, 293);
                this.tabPage2.TabIndex = 1;
                this.tabPage2.Text = "HATS";
                this.tabPage2.UseVisualStyleBackColor = true;
                // 
                // tabControl2
                // 
                this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Bottom;
                this.tabControl2.Controls.Add(this.tabPage10);
                this.tabControl2.Controls.Add(this.tabPage11);
                this.tabControl2.Controls.Add(this.tabPage12);
                this.tabControl2.Location = new System.Drawing.Point(3, 0);
                this.tabControl2.Multiline = true;
                this.tabControl2.Name = "tabControl2";
                this.tabControl2.SelectedIndex = 0;
                this.tabControl2.Size = new System.Drawing.Size(543, 247);
                this.tabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
                this.tabControl2.TabIndex = 57;
                // 
                // tabPage10
                // 
                this.tabPage10.Controls.Add(this.label10);
                this.tabPage10.Controls.Add(this.textBox2);
                this.tabPage10.Controls.Add(this.listBox1);
                this.tabPage10.Controls.Add(this.pictureBox1);
                this.tabPage10.Location = new System.Drawing.Point(4, 4);
                this.tabPage10.Name = "tabPage10";
                this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage10.Size = new System.Drawing.Size(535, 221);
                this.tabPage10.TabIndex = 0;
                this.tabPage10.Text = "HAT 1";
                this.tabPage10.UseVisualStyleBackColor = true;
                // 
                // label10
                // 
                this.label10.AutoSize = true;
                this.label10.Location = new System.Drawing.Point(293, 126);
                this.label10.Name = "label10";
                this.label10.Size = new System.Drawing.Size(40, 13);
                this.label10.TabIndex = 52;
                this.label10.Text = "Hat #1";
                // 
                // textBox2
                // 
                this.textBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox2.ImeMode = System.Windows.Forms.ImeMode.Katakana;
                this.textBox2.Location = new System.Drawing.Point(279, 145);
                this.textBox2.Multiline = true;
                this.textBox2.Name = "textBox2";
                this.textBox2.ReadOnly = true;
                this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox2.Size = new System.Drawing.Size(250, 73);
                this.textBox2.TabIndex = 51;
                // 
                // listBox1
                // 
                this.listBox1.FormattingEnabled = true;
                this.listBox1.Location = new System.Drawing.Point(6, 6);
                this.listBox1.Name = "listBox1";
                this.listBox1.Size = new System.Drawing.Size(244, 212);
                this.listBox1.TabIndex = 46;
                this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1SelectedIndexChanged);
                // 
                // pictureBox1
                // 
                this.pictureBox1.Location = new System.Drawing.Point(339, 8);
                this.pictureBox1.Name = "pictureBox1";
                this.pictureBox1.Size = new System.Drawing.Size(131, 131);
                this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox1.TabIndex = 50;
                this.pictureBox1.TabStop = false;
                // 
                // tabPage11
                // 
                this.tabPage11.Controls.Add(this.label11);
                this.tabPage11.Controls.Add(this.textBox3);
                this.tabPage11.Controls.Add(this.listBox2);
                this.tabPage11.Controls.Add(this.pictureBox2);
                this.tabPage11.Location = new System.Drawing.Point(4, 4);
                this.tabPage11.Name = "tabPage11";
                this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage11.Size = new System.Drawing.Size(535, 221);
                this.tabPage11.TabIndex = 1;
                this.tabPage11.Text = "HAT 2";
                this.tabPage11.UseVisualStyleBackColor = true;
                // 
                // label11
                // 
                this.label11.AutoSize = true;
                this.label11.Location = new System.Drawing.Point(293, 126);
                this.label11.Name = "label11";
                this.label11.Size = new System.Drawing.Size(40, 13);
                this.label11.TabIndex = 53;
                this.label11.Text = "Hat #2";
                // 
                // textBox3
                // 
                this.textBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox3.Location = new System.Drawing.Point(279, 145);
                this.textBox3.Multiline = true;
                this.textBox3.Name = "textBox3";
                this.textBox3.ReadOnly = true;
                this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox3.Size = new System.Drawing.Size(250, 73);
                this.textBox3.TabIndex = 52;
                // 
                // listBox2
                // 
                this.listBox2.FormattingEnabled = true;
                this.listBox2.Location = new System.Drawing.Point(6, 6);
                this.listBox2.Name = "listBox2";
                this.listBox2.Size = new System.Drawing.Size(244, 212);
                this.listBox2.TabIndex = 47;
                this.listBox2.SelectedIndexChanged += new System.EventHandler(this.ListBox2SelectedIndexChanged);
                // 
                // pictureBox2
                // 
                this.pictureBox2.Location = new System.Drawing.Point(339, 8);
                this.pictureBox2.Name = "pictureBox2";
                this.pictureBox2.Size = new System.Drawing.Size(131, 131);
                this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox2.TabIndex = 51;
                this.pictureBox2.TabStop = false;
                // 
                // tabPage12
                // 
                this.tabPage12.Controls.Add(this.label12);
                this.tabPage12.Controls.Add(this.textBox4);
                this.tabPage12.Controls.Add(this.listBox3);
                this.tabPage12.Controls.Add(this.pictureBox3);
                this.tabPage12.Location = new System.Drawing.Point(4, 4);
                this.tabPage12.Name = "tabPage12";
                this.tabPage12.Size = new System.Drawing.Size(535, 221);
                this.tabPage12.TabIndex = 2;
                this.tabPage12.Text = "HAT 3";
                this.tabPage12.UseVisualStyleBackColor = true;
                // 
                // label12
                // 
                this.label12.AutoSize = true;
                this.label12.Location = new System.Drawing.Point(293, 126);
                this.label12.Name = "label12";
                this.label12.Size = new System.Drawing.Size(40, 13);
                this.label12.TabIndex = 56;
                this.label12.Text = "Hat #3";
                // 
                // textBox4
                // 
                this.textBox4.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox4.Location = new System.Drawing.Point(279, 145);
                this.textBox4.Multiline = true;
                this.textBox4.Name = "textBox4";
                this.textBox4.ReadOnly = true;
                this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox4.Size = new System.Drawing.Size(250, 73);
                this.textBox4.TabIndex = 55;
                // 
                // listBox3
                // 
                this.listBox3.FormattingEnabled = true;
                this.listBox3.Location = new System.Drawing.Point(6, 6);
                this.listBox3.Name = "listBox3";
                this.listBox3.Size = new System.Drawing.Size(244, 212);
                this.listBox3.TabIndex = 52;
                this.listBox3.SelectedIndexChanged += new System.EventHandler(this.ListBox3SelectedIndexChanged);
                // 
                // pictureBox3
                // 
                this.pictureBox3.Location = new System.Drawing.Point(339, 8);
                this.pictureBox3.Name = "pictureBox3";
                this.pictureBox3.Size = new System.Drawing.Size(131, 131);
                this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox3.TabIndex = 54;
                this.pictureBox3.TabStop = false;
                // 
                // tabPage8
                // 
                this.tabPage8.Controls.Add(this.textBox5);
                this.tabPage8.Controls.Add(this.button56);
                this.tabPage8.Controls.Add(this.button57);
                this.tabPage8.Controls.Add(this.pictureBox8);
                this.tabPage8.Controls.Add(this.listBox8);
                this.tabPage8.Location = new System.Drawing.Point(4, 4);
                this.tabPage8.Name = "tabPage8";
                this.tabPage8.Size = new System.Drawing.Size(549, 293);
                this.tabPage8.TabIndex = 7;
                this.tabPage8.Text = "HEADS";
                this.tabPage8.UseVisualStyleBackColor = true;
                // 
                // textBox5
                // 
                this.textBox5.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox5.Location = new System.Drawing.Point(283, 155);
                this.textBox5.Multiline = true;
                this.textBox5.Name = "textBox5";
                this.textBox5.ReadOnly = true;
                this.textBox5.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox5.Size = new System.Drawing.Size(263, 86);
                this.textBox5.TabIndex = 66;
                // 
                // button56
                // 
                this.button56.Location = new System.Drawing.Point(283, 248);
                this.button56.Name = "button56";
                this.button56.Size = new System.Drawing.Size(263, 42);
                this.button56.TabIndex = 65;
                this.button56.Text = "Reset";
                this.button56.UseVisualStyleBackColor = true;
                this.button56.Click += new System.EventHandler(this.Button56Click);
                // 
                // button57
                // 
                this.button57.Location = new System.Drawing.Point(3, 248);
                this.button57.Name = "button57";
                this.button57.Size = new System.Drawing.Size(253, 42);
                this.button57.TabIndex = 64;
                this.button57.Text = "Randomize";
                this.button57.UseVisualStyleBackColor = true;
                this.button57.Click += new System.EventHandler(this.Button57Click);
                // 
                // pictureBox8
                // 
                this.pictureBox8.Location = new System.Drawing.Point(343, 15);
                this.pictureBox8.Name = "pictureBox8";
                this.pictureBox8.Size = new System.Drawing.Size(134, 134);
                this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox8.TabIndex = 63;
                this.pictureBox8.TabStop = false;
                // 
                // listBox8
                // 
                this.listBox8.FormattingEnabled = true;
                this.listBox8.Location = new System.Drawing.Point(3, 3);
                this.listBox8.Name = "listBox8";
                this.listBox8.Size = new System.Drawing.Size(253, 238);
                this.listBox8.TabIndex = 62;
                this.listBox8.SelectedIndexChanged += new System.EventHandler(this.ListBox8SelectedIndexChanged);
                // 
                // tabPage3
                // 
                this.tabPage3.Controls.Add(this.textBox6);
                this.tabPage3.Controls.Add(this.button44);
                this.tabPage3.Controls.Add(this.button45);
                this.tabPage3.Controls.Add(this.pictureBox4);
                this.tabPage3.Controls.Add(this.listBox4);
                this.tabPage3.Location = new System.Drawing.Point(4, 4);
                this.tabPage3.Name = "tabPage3";
                this.tabPage3.Size = new System.Drawing.Size(549, 293);
                this.tabPage3.TabIndex = 2;
                this.tabPage3.Text = "FACES";
                this.tabPage3.UseVisualStyleBackColor = true;
                // 
                // textBox6
                // 
                this.textBox6.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox6.Location = new System.Drawing.Point(283, 155);
                this.textBox6.Multiline = true;
                this.textBox6.Name = "textBox6";
                this.textBox6.ReadOnly = true;
                this.textBox6.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox6.Size = new System.Drawing.Size(263, 86);
                this.textBox6.TabIndex = 67;
                // 
                // button44
                // 
                this.button44.Location = new System.Drawing.Point(283, 248);
                this.button44.Name = "button44";
                this.button44.Size = new System.Drawing.Size(263, 42);
                this.button44.TabIndex = 61;
                this.button44.Text = "Reset";
                this.button44.UseVisualStyleBackColor = true;
                this.button44.Click += new System.EventHandler(this.Button44Click);
                // 
                // button45
                // 
                this.button45.Location = new System.Drawing.Point(3, 248);
                this.button45.Name = "button45";
                this.button45.Size = new System.Drawing.Size(253, 42);
                this.button45.TabIndex = 60;
                this.button45.Text = "Randomize";
                this.button45.UseVisualStyleBackColor = true;
                this.button45.Click += new System.EventHandler(this.Button45Click);
                // 
                // pictureBox4
                // 
                this.pictureBox4.Location = new System.Drawing.Point(343, 15);
                this.pictureBox4.Name = "pictureBox4";
                this.pictureBox4.Size = new System.Drawing.Size(134, 134);
                this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox4.TabIndex = 59;
                this.pictureBox4.TabStop = false;
                // 
                // listBox4
                // 
                this.listBox4.FormattingEnabled = true;
                this.listBox4.Location = new System.Drawing.Point(3, 3);
                this.listBox4.Name = "listBox4";
                this.listBox4.Size = new System.Drawing.Size(253, 238);
                this.listBox4.TabIndex = 57;
                this.listBox4.SelectedIndexChanged += new System.EventHandler(this.ListBox4SelectedIndexChanged);
                // 
                // tabPage4
                // 
                this.tabPage4.Controls.Add(this.textBox7);
                this.tabPage4.Controls.Add(this.button46);
                this.tabPage4.Controls.Add(this.button47);
                this.tabPage4.Controls.Add(this.pictureBox5);
                this.tabPage4.Controls.Add(this.listBox5);
                this.tabPage4.Location = new System.Drawing.Point(4, 4);
                this.tabPage4.Name = "tabPage4";
                this.tabPage4.Size = new System.Drawing.Size(549, 293);
                this.tabPage4.TabIndex = 3;
                this.tabPage4.Text = "T-SHIRTS";
                this.tabPage4.UseVisualStyleBackColor = true;
                // 
                // textBox7
                // 
                this.textBox7.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox7.Location = new System.Drawing.Point(283, 155);
                this.textBox7.Multiline = true;
                this.textBox7.Name = "textBox7";
                this.textBox7.ReadOnly = true;
                this.textBox7.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox7.Size = new System.Drawing.Size(263, 86);
                this.textBox7.TabIndex = 68;
                // 
                // button46
                // 
                this.button46.Location = new System.Drawing.Point(283, 248);
                this.button46.Name = "button46";
                this.button46.Size = new System.Drawing.Size(263, 42);
                this.button46.TabIndex = 65;
                this.button46.Text = "Reset";
                this.button46.UseVisualStyleBackColor = true;
                this.button46.Click += new System.EventHandler(this.Button46Click);
                // 
                // button47
                // 
                this.button47.Location = new System.Drawing.Point(3, 248);
                this.button47.Name = "button47";
                this.button47.Size = new System.Drawing.Size(253, 42);
                this.button47.TabIndex = 64;
                this.button47.Text = "Randomize";
                this.button47.UseVisualStyleBackColor = true;
                this.button47.Click += new System.EventHandler(this.Button47Click);
                // 
                // pictureBox5
                // 
                this.pictureBox5.Location = new System.Drawing.Point(343, 15);
                this.pictureBox5.Name = "pictureBox5";
                this.pictureBox5.Size = new System.Drawing.Size(134, 134);
                this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox5.TabIndex = 63;
                this.pictureBox5.TabStop = false;
                // 
                // listBox5
                // 
                this.listBox5.FormattingEnabled = true;
                this.listBox5.Location = new System.Drawing.Point(3, 3);
                this.listBox5.Name = "listBox5";
                this.listBox5.Size = new System.Drawing.Size(253, 238);
                this.listBox5.TabIndex = 62;
                this.listBox5.SelectedIndexChanged += new System.EventHandler(this.ListBox5SelectedIndexChanged);
                // 
                // tabPage5
                // 
                this.tabPage5.Controls.Add(this.textBox8);
                this.tabPage5.Controls.Add(this.button48);
                this.tabPage5.Controls.Add(this.button49);
                this.tabPage5.Controls.Add(this.pictureBox6);
                this.tabPage5.Controls.Add(this.listBox6);
                this.tabPage5.Location = new System.Drawing.Point(4, 4);
                this.tabPage5.Name = "tabPage5";
                this.tabPage5.Size = new System.Drawing.Size(549, 293);
                this.tabPage5.TabIndex = 4;
                this.tabPage5.Text = "SHIRTS";
                this.tabPage5.UseVisualStyleBackColor = true;
                // 
                // textBox8
                // 
                this.textBox8.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox8.Location = new System.Drawing.Point(283, 155);
                this.textBox8.Multiline = true;
                this.textBox8.Name = "textBox8";
                this.textBox8.ReadOnly = true;
                this.textBox8.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox8.Size = new System.Drawing.Size(263, 86);
                this.textBox8.TabIndex = 68;
                // 
                // button48
                // 
                this.button48.Location = new System.Drawing.Point(283, 248);
                this.button48.Name = "button48";
                this.button48.Size = new System.Drawing.Size(263, 42);
                this.button48.TabIndex = 65;
                this.button48.Text = "Reset";
                this.button48.UseVisualStyleBackColor = true;
                this.button48.Click += new System.EventHandler(this.Button48Click);
                // 
                // button49
                // 
                this.button49.Location = new System.Drawing.Point(3, 248);
                this.button49.Name = "button49";
                this.button49.Size = new System.Drawing.Size(253, 42);
                this.button49.TabIndex = 64;
                this.button49.Text = "Randomize";
                this.button49.UseVisualStyleBackColor = true;
                this.button49.Click += new System.EventHandler(this.Button49Click);
                // 
                // pictureBox6
                // 
                this.pictureBox6.Location = new System.Drawing.Point(343, 15);
                this.pictureBox6.Name = "pictureBox6";
                this.pictureBox6.Size = new System.Drawing.Size(134, 134);
                this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox6.TabIndex = 63;
                this.pictureBox6.TabStop = false;
                // 
                // listBox6
                // 
                this.listBox6.FormattingEnabled = true;
                this.listBox6.Location = new System.Drawing.Point(3, 3);
                this.listBox6.Name = "listBox6";
                this.listBox6.Size = new System.Drawing.Size(253, 238);
                this.listBox6.TabIndex = 62;
                this.listBox6.SelectedIndexChanged += new System.EventHandler(this.ListBox6SelectedIndexChanged);
                // 
                // tabPage6
                // 
                this.tabPage6.Controls.Add(this.textBox9);
                this.tabPage6.Controls.Add(this.button50);
                this.tabPage6.Controls.Add(this.button51);
                this.tabPage6.Controls.Add(this.pictureBox7);
                this.tabPage6.Controls.Add(this.listBox7);
                this.tabPage6.Location = new System.Drawing.Point(4, 4);
                this.tabPage6.Name = "tabPage6";
                this.tabPage6.Size = new System.Drawing.Size(549, 293);
                this.tabPage6.TabIndex = 5;
                this.tabPage6.Text = "PANTS";
                this.tabPage6.UseVisualStyleBackColor = true;
                // 
                // textBox9
                // 
                this.textBox9.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox9.Location = new System.Drawing.Point(283, 155);
                this.textBox9.Multiline = true;
                this.textBox9.Name = "textBox9";
                this.textBox9.ReadOnly = true;
                this.textBox9.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox9.Size = new System.Drawing.Size(263, 86);
                this.textBox9.TabIndex = 68;
                // 
                // button50
                // 
                this.button50.Location = new System.Drawing.Point(283, 248);
                this.button50.Name = "button50";
                this.button50.Size = new System.Drawing.Size(263, 42);
                this.button50.TabIndex = 65;
                this.button50.Text = "Reset";
                this.button50.UseVisualStyleBackColor = true;
                this.button50.Click += new System.EventHandler(this.Button50Click);
                // 
                // button51
                // 
                this.button51.Location = new System.Drawing.Point(3, 248);
                this.button51.Name = "button51";
                this.button51.Size = new System.Drawing.Size(253, 42);
                this.button51.TabIndex = 64;
                this.button51.Text = "Randomize";
                this.button51.UseVisualStyleBackColor = true;
                this.button51.Click += new System.EventHandler(this.Button51Click);
                // 
                // pictureBox7
                // 
                this.pictureBox7.Location = new System.Drawing.Point(343, 15);
                this.pictureBox7.Name = "pictureBox7";
                this.pictureBox7.Size = new System.Drawing.Size(134, 134);
                this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox7.TabIndex = 63;
                this.pictureBox7.TabStop = false;
                // 
                // listBox7
                // 
                this.listBox7.FormattingEnabled = true;
                this.listBox7.Location = new System.Drawing.Point(3, 3);
                this.listBox7.Name = "listBox7";
                this.listBox7.Size = new System.Drawing.Size(253, 238);
                this.listBox7.TabIndex = 62;
                this.listBox7.SelectedIndexChanged += new System.EventHandler(this.ListBox7SelectedIndexChanged);
                // 
                // tabPage9
                // 
                this.tabPage9.Controls.Add(this.textBox10);
                this.tabPage9.Controls.Add(this.checkBox1);
                this.tabPage9.Controls.Add(this.button58);
                this.tabPage9.Controls.Add(this.button59);
                this.tabPage9.Controls.Add(this.pictureBox9);
                this.tabPage9.Controls.Add(this.listBox9);
                this.tabPage9.Location = new System.Drawing.Point(4, 4);
                this.tabPage9.Name = "tabPage9";
                this.tabPage9.Size = new System.Drawing.Size(549, 293);
                this.tabPage9.TabIndex = 8;
                this.tabPage9.Text = "EXTRA";
                this.tabPage9.UseVisualStyleBackColor = true;
                // 
                // textBox10
                // 
                this.textBox10.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox10.Location = new System.Drawing.Point(283, 155);
                this.textBox10.Multiline = true;
                this.textBox10.Name = "textBox10";
                this.textBox10.ReadOnly = true;
                this.textBox10.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox10.Size = new System.Drawing.Size(263, 86);
                this.textBox10.TabIndex = 71;
                // 
                // checkBox1
                // 
                this.checkBox1.Location = new System.Drawing.Point(3, 230);
                this.checkBox1.Name = "checkBox1";
                this.checkBox1.Size = new System.Drawing.Size(79, 17);
                this.checkBox1.TabIndex = 70;
                this.checkBox1.Text = "Show Hats";
                this.checkBox1.UseVisualStyleBackColor = true;
                this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
                // 
                // button58
                // 
                this.button58.Location = new System.Drawing.Point(283, 248);
                this.button58.Name = "button58";
                this.button58.Size = new System.Drawing.Size(263, 42);
                this.button58.TabIndex = 69;
                this.button58.Text = "Reset";
                this.button58.UseVisualStyleBackColor = true;
                this.button58.Click += new System.EventHandler(this.Button58Click);
                // 
                // button59
                // 
                this.button59.Location = new System.Drawing.Point(3, 248);
                this.button59.Name = "button59";
                this.button59.Size = new System.Drawing.Size(253, 42);
                this.button59.TabIndex = 68;
                this.button59.Text = "Randomize";
                this.button59.UseVisualStyleBackColor = true;
                this.button59.Click += new System.EventHandler(this.Button59Click);
                // 
                // pictureBox9
                // 
                this.pictureBox9.Location = new System.Drawing.Point(343, 15);
                this.pictureBox9.Name = "pictureBox9";
                this.pictureBox9.Size = new System.Drawing.Size(134, 134);
                this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox9.TabIndex = 67;
                this.pictureBox9.TabStop = false;
                // 
                // listBox9
                // 
                this.listBox9.FormattingEnabled = true;
                this.listBox9.Location = new System.Drawing.Point(3, 3);
                this.listBox9.Name = "listBox9";
                this.listBox9.Size = new System.Drawing.Size(253, 225);
                this.listBox9.TabIndex = 66;
                this.listBox9.SelectedIndexChanged += new System.EventHandler(this.ListBox9SelectedIndexChanged);
                // 
                // tabPage7
                // 
                this.tabPage7.Controls.Add(this.label8);
                this.tabPage7.Controls.Add(this.pictureBox10);
                this.tabPage7.Controls.Add(this.button60);
                this.tabPage7.Controls.Add(this.textBox1);
                this.tabPage7.Controls.Add(this.label7);
                this.tabPage7.Controls.Add(this.label6);
                this.tabPage7.Controls.Add(this.button55);
                this.tabPage7.Controls.Add(this.label5);
                this.tabPage7.Controls.Add(this.label4);
                this.tabPage7.Controls.Add(this.label3);
                this.tabPage7.Controls.Add(this.button54);
                this.tabPage7.Controls.Add(this.button53);
                this.tabPage7.Controls.Add(this.button52);
                this.tabPage7.Location = new System.Drawing.Point(4, 4);
                this.tabPage7.Name = "tabPage7";
                this.tabPage7.Size = new System.Drawing.Size(549, 293);
                this.tabPage7.TabIndex = 6;
                this.tabPage7.Text = "OTHER";
                this.tabPage7.UseVisualStyleBackColor = true;
                // 
                // button71
                // 
                this.button71.Location = new System.Drawing.Point(3, 268);
                this.button71.Name = "button71";
                this.button71.Size = new System.Drawing.Size(75, 22);
                this.button71.TabIndex = 13;
                this.button71.Text = "SAVE";
                this.button71.UseVisualStyleBackColor = true;
                this.button71.Click += new System.EventHandler(this.button71_Click);
                // 
                // label8
                // 
                this.label8.Location = new System.Drawing.Point(433, 6);
                this.label8.Name = "label8";
                this.label8.Size = new System.Drawing.Size(69, 29);
                this.label8.TabIndex = 12;
                this.label8.Text = "Custom Icon (Client Side)";
                this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // 
                // pictureBox10
                // 
                this.pictureBox10.Location = new System.Drawing.Point(410, 38);
                this.pictureBox10.Name = "pictureBox10";
                this.pictureBox10.Size = new System.Drawing.Size(118, 118);
                this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox10.TabIndex = 11;
                this.pictureBox10.TabStop = false;
                // 
                // button60
                // 
                this.button60.Location = new System.Drawing.Point(434, 162);
                this.button60.Name = "button60";
                this.button60.Size = new System.Drawing.Size(68, 23);
                this.button60.TabIndex = 10;
                this.button60.Text = "Browse...";
                this.button60.UseVisualStyleBackColor = true;
                this.button60.Click += new System.EventHandler(this.button60_Click);
                // 
                // button43
                // 
                this.button43.Location = new System.Drawing.Point(3, 243);
                this.button43.Name = "button43";
                this.button43.Size = new System.Drawing.Size(75, 22);
                this.button43.TabIndex = 1;
                this.button43.Text = "3D VIEW";
                this.button43.UseVisualStyleBackColor = true;
                this.button43.Click += new System.EventHandler(this.Button43Click);
                // 
                // textBox1
                // 
                this.textBox1.Location = new System.Drawing.Point(40, 260);
                this.textBox1.Name = "textBox1";
                this.textBox1.Size = new System.Drawing.Size(462, 20);
                this.textBox1.TabIndex = 9;
                this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                this.textBox1.TextChanged += new System.EventHandler(this.TextBox1TextChanged);
                // 
                // label7
                // 
                this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                this.label7.Location = new System.Drawing.Point(3, 230);
                this.label7.Name = "label7";
                this.label7.Size = new System.Drawing.Size(543, 2);
                this.label7.TabIndex = 8;
                // 
                // label6
                // 
                this.label6.Location = new System.Drawing.Point(140, 238);
                this.label6.Name = "label6";
                this.label6.Size = new System.Drawing.Size(257, 19);
                this.label6.TabIndex = 7;
                this.label6.Text = "Character Appearance URL (for clients that require it)";
                this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                // 
                // button55
                // 
                this.button55.Location = new System.Drawing.Point(11, 162);
                this.button55.Name = "button55";
                this.button55.Size = new System.Drawing.Size(367, 23);
                this.button55.TabIndex = 6;
                this.button55.Text = "Disable Icon/Enable Custom Icons";
                this.button55.UseVisualStyleBackColor = true;
                this.button55.Click += new System.EventHandler(this.Button55Click);
                // 
                // label5
                // 
                this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label5.Location = new System.Drawing.Point(229, 9);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(56, 19);
                this.label5.TabIndex = 5;
                this.label5.Text = "NBC";
                // 
                // label4
                // 
                this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label4.ForeColor = System.Drawing.Color.Red;
                this.label4.Location = new System.Drawing.Point(37, 178);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(465, 44);
                this.label4.TabIndex = 4;
                this.label4.Text = "NOTE: The icon will only function in a client with a custom scoreboard (I.E 2011E" +
        " or 2011M).";
                this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // 
                // label3
                // 
                this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label3.Location = new System.Drawing.Point(122, 9);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(111, 19);
                this.label3.TabIndex = 3;
                this.label3.Text = "Selected Icon:";
                // 
                // button54
                // 
                this.button54.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.button54.ImageKey = "OBC.png";
                this.button54.ImageList = this.imageList1;
                this.button54.Location = new System.Drawing.Point(260, 38);
                this.button54.Name = "button54";
                this.button54.Size = new System.Drawing.Size(118, 118);
                this.button54.TabIndex = 2;
                this.button54.Text = "OBC";
                this.button54.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                this.button54.UseVisualStyleBackColor = true;
                this.button54.Click += new System.EventHandler(this.Button54Click);
                // 
                // button53
                // 
                this.button53.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.button53.ImageKey = "TBC.png";
                this.button53.ImageList = this.imageList1;
                this.button53.Location = new System.Drawing.Point(136, 38);
                this.button53.Name = "button53";
                this.button53.Size = new System.Drawing.Size(118, 118);
                this.button53.TabIndex = 1;
                this.button53.Text = "TBC";
                this.button53.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                this.button53.UseVisualStyleBackColor = true;
                this.button53.Click += new System.EventHandler(this.Button53Click);
                // 
                // button52
                // 
                this.button52.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.button52.ImageKey = "BC.png";
                this.button52.ImageList = this.imageList1;
                this.button52.Location = new System.Drawing.Point(11, 38);
                this.button52.Name = "button52";
                this.button52.Size = new System.Drawing.Size(118, 118);
                this.button52.TabIndex = 0;
                this.button52.Text = "BC";
                this.button52.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                this.button52.UseVisualStyleBackColor = true;
                this.button52.Click += new System.EventHandler(this.Button52Click);
                // 
                // panel3
                // 
                this.panel3.Controls.Add(this.button41);
                this.panel3.Controls.Add(this.button83);
                this.panel3.Controls.Add(this.button82);
                this.panel3.Controls.Add(this.button42);
                this.panel3.Controls.Add(this.button81);
                this.panel3.Location = new System.Drawing.Point(110, 359);
                this.panel3.Name = "panel3";
                this.panel3.Size = new System.Drawing.Size(535, 62);
                this.panel3.TabIndex = 3;
                // 
                // button41
                // 
                this.button41.Location = new System.Drawing.Point(3, 33);
                this.button41.Name = "button41";
                this.button41.Size = new System.Drawing.Size(253, 29);
                this.button41.TabIndex = 55;
                this.button41.Text = "Randomize all 3";
                this.button41.UseVisualStyleBackColor = true;
                this.button41.Click += new System.EventHandler(this.Button41Click);
                // 
                // button83
                // 
                this.button83.Location = new System.Drawing.Point(296, 4);
                this.button83.Name = "button83";
                this.button83.Size = new System.Drawing.Size(56, 23);
                this.button83.TabIndex = 4;
                this.button83.Text = "HAT #3";
                this.button83.UseVisualStyleBackColor = true;
                this.button83.Click += new System.EventHandler(this.button83_Click);
                // 
                // button82
                // 
                this.button82.Location = new System.Drawing.Point(221, 4);
                this.button82.Name = "button82";
                this.button82.Size = new System.Drawing.Size(69, 23);
                this.button82.TabIndex = 58;
                this.button82.Text = "HAT #2";
                this.button82.UseVisualStyleBackColor = true;
                this.button82.Click += new System.EventHandler(this.button82_Click);
                // 
                // button42
                // 
                this.button42.Location = new System.Drawing.Point(269, 33);
                this.button42.Name = "button42";
                this.button42.Size = new System.Drawing.Size(263, 29);
                this.button42.TabIndex = 56;
                this.button42.Text = "Reset all 3";
                this.button42.UseVisualStyleBackColor = true;
                this.button42.Click += new System.EventHandler(this.Button42Click);
                // 
                // button81
                // 
                this.button81.Location = new System.Drawing.Point(159, 4);
                this.button81.Name = "button81";
                this.button81.Size = new System.Drawing.Size(56, 23);
                this.button81.TabIndex = 3;
                this.button81.Text = "HAT #1";
                this.button81.UseVisualStyleBackColor = true;
                this.button81.Click += new System.EventHandler(this.button81_Click);
                // 
                // label9
                // 
                this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                this.label9.Location = new System.Drawing.Point(3, 238);
                this.label9.Name = "label9";
                this.label9.Size = new System.Drawing.Size(75, 2);
                this.label9.TabIndex = 14;
                // 
                // CharacterCustomization
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.ClientSize = new System.Drawing.Size(665, 434);
                this.Controls.Add(this.panel3);
                this.Controls.Add(this.panel2);
                this.Controls.Add(this.panel1);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.MaximizeBox = false;
                this.Name = "CharacterCustomization";
                this.Text = "Avatar Customization";
                this.Closing += new System.ComponentModel.CancelEventHandler(this.CharacterCustomizationClose);
                this.Load += new System.EventHandler(this.CharacterCustomizationLoad);
                this.panel1.ResumeLayout(false);
                this.panel2.ResumeLayout(false);
                this.tabControl1.ResumeLayout(false);
                this.tabPage1.ResumeLayout(false);
                this.groupBox3.ResumeLayout(false);
                this.groupBox2.ResumeLayout(false);
                this.groupBox1.ResumeLayout(false);
                this.tabPage2.ResumeLayout(false);
                this.tabControl2.ResumeLayout(false);
                this.tabPage10.ResumeLayout(false);
                this.tabPage10.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
                this.tabPage11.ResumeLayout(false);
                this.tabPage11.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
                this.tabPage12.ResumeLayout(false);
                this.tabPage12.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
                this.tabPage8.ResumeLayout(false);
                this.tabPage8.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
                this.tabPage3.ResumeLayout(false);
                this.tabPage3.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
                this.tabPage4.ResumeLayout(false);
                this.tabPage4.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
                this.tabPage5.ResumeLayout(false);
                this.tabPage5.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
                this.tabPage6.ResumeLayout(false);
                this.tabPage6.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
                this.tabPage9.ResumeLayout(false);
                this.tabPage9.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
                this.tabPage7.ResumeLayout(false);
                this.tabPage7.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
                this.panel3.ResumeLayout(false);
                this.ResumeLayout(false);
#endif
#if RETAIL
            }
            else
            {
#endif
#if EDITORLAYOUT2
            this.components = new System.ComponentModel.Container();
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterCustomization));
                this.imageList1 = new System.Windows.Forms.ImageList(this.components);
                this.tabControl1 = new TabControlWithoutHeader(2);
                this.tabPage1 = new System.Windows.Forms.TabPage();
                this.groupBox3 = new System.Windows.Forms.GroupBox();
                this.button61 = new System.Windows.Forms.Button();
                this.button65 = new System.Windows.Forms.Button();
                this.button62 = new System.Windows.Forms.Button();
                this.button66 = new System.Windows.Forms.Button();
                this.button63 = new System.Windows.Forms.Button();
                this.button67 = new System.Windows.Forms.Button();
                this.button64 = new System.Windows.Forms.Button();
                this.button68 = new System.Windows.Forms.Button();
                this.groupBox2 = new System.Windows.Forms.GroupBox();
                this.button2 = new System.Windows.Forms.Button();
                this.button1 = new System.Windows.Forms.Button();
                this.button3 = new System.Windows.Forms.Button();
                this.button4 = new System.Windows.Forms.Button();
                this.button5 = new System.Windows.Forms.Button();
                this.button6 = new System.Windows.Forms.Button();
                this.groupBox1 = new System.Windows.Forms.GroupBox();
                this.button70 = new System.Windows.Forms.Button();
                this.button7 = new System.Windows.Forms.Button();
                this.button69 = new System.Windows.Forms.Button();
                this.button8 = new System.Windows.Forms.Button();
                this.button9 = new System.Windows.Forms.Button();
                this.button10 = new System.Windows.Forms.Button();
                this.button14 = new System.Windows.Forms.Button();
                this.button35 = new System.Windows.Forms.Button();
                this.button13 = new System.Windows.Forms.Button();
                this.button36 = new System.Windows.Forms.Button();
                this.button12 = new System.Windows.Forms.Button();
                this.button37 = new System.Windows.Forms.Button();
                this.button11 = new System.Windows.Forms.Button();
                this.button38 = new System.Windows.Forms.Button();
                this.button18 = new System.Windows.Forms.Button();
                this.button31 = new System.Windows.Forms.Button();
                this.button17 = new System.Windows.Forms.Button();
                this.button32 = new System.Windows.Forms.Button();
                this.button16 = new System.Windows.Forms.Button();
                this.button33 = new System.Windows.Forms.Button();
                this.button15 = new System.Windows.Forms.Button();
                this.button34 = new System.Windows.Forms.Button();
                this.button22 = new System.Windows.Forms.Button();
                this.button27 = new System.Windows.Forms.Button();
                this.button21 = new System.Windows.Forms.Button();
                this.button28 = new System.Windows.Forms.Button();
                this.button20 = new System.Windows.Forms.Button();
                this.button29 = new System.Windows.Forms.Button();
                this.button19 = new System.Windows.Forms.Button();
                this.button30 = new System.Windows.Forms.Button();
                this.button26 = new System.Windows.Forms.Button();
                this.button23 = new System.Windows.Forms.Button();
                this.button25 = new System.Windows.Forms.Button();
                this.button24 = new System.Windows.Forms.Button();
                this.button39 = new System.Windows.Forms.Button();
                this.button40 = new System.Windows.Forms.Button();
                this.label2 = new System.Windows.Forms.Label();
                this.label1 = new System.Windows.Forms.Label();
                this.tabPage2 = new System.Windows.Forms.TabPage();
                this.tabControl2 = new TabControlWithoutHeader(2);
                this.tabPage10 = new System.Windows.Forms.TabPage();
                this.textBox2 = new System.Windows.Forms.TextBox();
                this.listBox1 = new System.Windows.Forms.ListBox();
                this.pictureBox1 = new System.Windows.Forms.PictureBox();
                this.tabPage11 = new System.Windows.Forms.TabPage();
                this.textBox3 = new System.Windows.Forms.TextBox();
                this.listBox2 = new System.Windows.Forms.ListBox();
                this.pictureBox2 = new System.Windows.Forms.PictureBox();
                this.tabPage12 = new System.Windows.Forms.TabPage();
                this.textBox4 = new System.Windows.Forms.TextBox();
                this.listBox3 = new System.Windows.Forms.ListBox();
                this.pictureBox3 = new System.Windows.Forms.PictureBox();
                this.button42 = new System.Windows.Forms.Button();
                this.button41 = new System.Windows.Forms.Button();
                this.tabPage8 = new System.Windows.Forms.TabPage();
                this.textBox5 = new System.Windows.Forms.TextBox();
                this.button56 = new System.Windows.Forms.Button();
                this.button57 = new System.Windows.Forms.Button();
                this.pictureBox8 = new System.Windows.Forms.PictureBox();
                this.listBox8 = new System.Windows.Forms.ListBox();
                this.tabPage3 = new System.Windows.Forms.TabPage();
                this.textBox6 = new System.Windows.Forms.TextBox();
                this.button44 = new System.Windows.Forms.Button();
                this.button45 = new System.Windows.Forms.Button();
                this.pictureBox4 = new System.Windows.Forms.PictureBox();
                this.listBox4 = new System.Windows.Forms.ListBox();
                this.tabPage4 = new System.Windows.Forms.TabPage();
                this.textBox7 = new System.Windows.Forms.TextBox();
                this.button46 = new System.Windows.Forms.Button();
                this.button47 = new System.Windows.Forms.Button();
                this.pictureBox5 = new System.Windows.Forms.PictureBox();
                this.listBox5 = new System.Windows.Forms.ListBox();
                this.tabPage5 = new System.Windows.Forms.TabPage();
                this.textBox8 = new System.Windows.Forms.TextBox();
                this.button48 = new System.Windows.Forms.Button();
                this.button49 = new System.Windows.Forms.Button();
                this.pictureBox6 = new System.Windows.Forms.PictureBox();
                this.listBox6 = new System.Windows.Forms.ListBox();
                this.tabPage6 = new System.Windows.Forms.TabPage();
                this.textBox9 = new System.Windows.Forms.TextBox();
                this.button50 = new System.Windows.Forms.Button();
                this.button51 = new System.Windows.Forms.Button();
                this.pictureBox7 = new System.Windows.Forms.PictureBox();
                this.listBox7 = new System.Windows.Forms.ListBox();
                this.tabPage9 = new System.Windows.Forms.TabPage();
                this.textBox10 = new System.Windows.Forms.TextBox();
                this.checkBox1 = new System.Windows.Forms.CheckBox();
                this.button58 = new System.Windows.Forms.Button();
                this.button59 = new System.Windows.Forms.Button();
                this.pictureBox9 = new System.Windows.Forms.PictureBox();
                this.listBox9 = new System.Windows.Forms.ListBox();
                this.tabPage7 = new System.Windows.Forms.TabPage();
                this.button71 = new System.Windows.Forms.Button();
                this.label8 = new System.Windows.Forms.Label();
                this.pictureBox10 = new System.Windows.Forms.PictureBox();
                this.button60 = new System.Windows.Forms.Button();
                this.button43 = new System.Windows.Forms.Button();
                this.textBox1 = new System.Windows.Forms.TextBox();
                this.label7 = new System.Windows.Forms.Label();
                this.label6 = new System.Windows.Forms.Label();
                this.button55 = new System.Windows.Forms.Button();
                this.label5 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                this.label3 = new System.Windows.Forms.Label();
                this.button54 = new System.Windows.Forms.Button();
                this.button53 = new System.Windows.Forms.Button();
                this.button52 = new System.Windows.Forms.Button();
                this.tabControl1.SuspendLayout();
                this.tabPage1.SuspendLayout();
                this.groupBox3.SuspendLayout();
                this.groupBox2.SuspendLayout();
                this.groupBox1.SuspendLayout();
                this.tabPage2.SuspendLayout();
                this.tabControl2.SuspendLayout();
                this.tabPage10.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                this.tabPage11.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
                this.tabPage12.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
                this.tabPage8.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
                this.tabPage3.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
                this.tabPage4.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
                this.tabPage5.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
                this.tabPage6.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
                this.tabPage9.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
                this.tabPage7.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
                this.SuspendLayout();
                // 
                // imageList1
                // 
                this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
                this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
                this.imageList1.Images.SetKeyName(0, "BC.png");
                this.imageList1.Images.SetKeyName(1, "TBC.png");
                this.imageList1.Images.SetKeyName(2, "OBC.png");
                this.imageList1.ImageSize = new System.Drawing.Size(32,32);
                // 
                // tabControl1
                // 
                this.tabControl1.Controls.Add(this.tabPage1);
                this.tabControl1.Controls.Add(this.tabPage2);
                this.tabControl1.Controls.Add(this.tabPage8);
                this.tabControl1.Controls.Add(this.tabPage3);
                this.tabControl1.Controls.Add(this.tabPage4);
                this.tabControl1.Controls.Add(this.tabPage5);
                this.tabControl1.Controls.Add(this.tabPage6);
                this.tabControl1.Controls.Add(this.tabPage9);
                this.tabControl1.Controls.Add(this.tabPage7);
                this.tabControl1.Location = new System.Drawing.Point(1, 3);
                this.tabControl1.Multiline = true;
                this.tabControl1.Name = "tabControl1";
                this.tabControl1.SelectedIndex = 0;
                this.tabControl1.Size = new System.Drawing.Size(475, 267);
                this.tabControl1.TabIndex = 0;
                this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
                // 
                // tabPage1
                // 
                this.tabPage1.Controls.Add(this.groupBox3);
                this.tabPage1.Controls.Add(this.groupBox2);
                this.tabPage1.Controls.Add(this.groupBox1);
                this.tabPage1.Controls.Add(this.button39);
                this.tabPage1.Controls.Add(this.button40);
                this.tabPage1.Controls.Add(this.label2);
                this.tabPage1.Controls.Add(this.label1);
                this.tabPage1.Location = new System.Drawing.Point(4, 22);
                this.tabPage1.Name = "tabPage1";
                this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage1.Size = new System.Drawing.Size(467, 241);
                this.tabPage1.TabIndex = 0;
                this.tabPage1.Text = "BODY";
                this.tabPage1.UseVisualStyleBackColor = true;
                // 
                // groupBox3
                // 
                this.groupBox3.Controls.Add(this.button61);
                this.groupBox3.Controls.Add(this.button65);
                this.groupBox3.Controls.Add(this.button62);
                this.groupBox3.Controls.Add(this.button66);
                this.groupBox3.Controls.Add(this.button63);
                this.groupBox3.Controls.Add(this.button67);
                this.groupBox3.Controls.Add(this.button64);
                this.groupBox3.Controls.Add(this.button68);
                this.groupBox3.Location = new System.Drawing.Point(355, 163);
                this.groupBox3.Name = "groupBox3";
                this.groupBox3.Size = new System.Drawing.Size(109, 68);
                this.groupBox3.TabIndex = 60;
                this.groupBox3.TabStop = false;
                this.groupBox3.Text = "\'06 Color Presets";
                // 
                // button61
                // 
                this.button61.Location = new System.Drawing.Point(6, 16);
                this.button61.Name = "button61";
                this.button61.Size = new System.Drawing.Size(24, 23);
                this.button61.TabIndex = 51;
                this.button61.Text = "1";
                this.button61.UseVisualStyleBackColor = true;
                this.button61.Click += new System.EventHandler(this.button61_Click);
                // 
                // button65
                // 
                this.button65.Location = new System.Drawing.Point(81, 42);
                this.button65.Name = "button65";
                this.button65.Size = new System.Drawing.Size(24, 23);
                this.button65.TabIndex = 58;
                this.button65.Text = "8";
                this.button65.UseVisualStyleBackColor = true;
                this.button65.Click += new System.EventHandler(this.button65_Click);
                // 
                // button62
                // 
                this.button62.Location = new System.Drawing.Point(31, 16);
                this.button62.Name = "button62";
                this.button62.Size = new System.Drawing.Size(24, 23);
                this.button62.TabIndex = 52;
                this.button62.Text = "2";
                this.button62.UseVisualStyleBackColor = true;
                this.button62.Click += new System.EventHandler(this.button62_Click);
                // 
                // button66
                // 
                this.button66.Location = new System.Drawing.Point(56, 42);
                this.button66.Name = "button66";
                this.button66.Size = new System.Drawing.Size(24, 23);
                this.button66.TabIndex = 57;
                this.button66.Text = "7";
                this.button66.UseVisualStyleBackColor = true;
                this.button66.Click += new System.EventHandler(this.button66_Click);
                // 
                // button63
                // 
                this.button63.Location = new System.Drawing.Point(56, 16);
                this.button63.Name = "button63";
                this.button63.Size = new System.Drawing.Size(24, 23);
                this.button63.TabIndex = 53;
                this.button63.Text = "3";
                this.button63.UseVisualStyleBackColor = true;
                this.button63.Click += new System.EventHandler(this.button63_Click);
                // 
                // button67
                // 
                this.button67.Location = new System.Drawing.Point(31, 42);
                this.button67.Name = "button67";
                this.button67.Size = new System.Drawing.Size(24, 23);
                this.button67.TabIndex = 56;
                this.button67.Text = "6";
                this.button67.UseVisualStyleBackColor = true;
                this.button67.Click += new System.EventHandler(this.button67_Click);
                // 
                // button64
                // 
                this.button64.Location = new System.Drawing.Point(81, 16);
                this.button64.Name = "button64";
                this.button64.Size = new System.Drawing.Size(24, 23);
                this.button64.TabIndex = 54;
                this.button64.Text = "4";
                this.button64.UseVisualStyleBackColor = true;
                this.button64.Click += new System.EventHandler(this.button64_Click);
                // 
                // button68
                // 
                this.button68.Location = new System.Drawing.Point(6, 42);
                this.button68.Name = "button68";
                this.button68.Size = new System.Drawing.Size(24, 23);
                this.button68.TabIndex = 55;
                this.button68.Text = "5";
                this.button68.UseVisualStyleBackColor = true;
                this.button68.Click += new System.EventHandler(this.button68_Click);
                // 
                // groupBox2
                // 
                this.groupBox2.Controls.Add(this.button2);
                this.groupBox2.Controls.Add(this.button1);
                this.groupBox2.Controls.Add(this.button3);
                this.groupBox2.Controls.Add(this.button4);
                this.groupBox2.Controls.Add(this.button5);
                this.groupBox2.Controls.Add(this.button6);
                this.groupBox2.Location = new System.Drawing.Point(6, 6);
                this.groupBox2.Name = "groupBox2";
                this.groupBox2.Size = new System.Drawing.Size(162, 225);
                this.groupBox2.TabIndex = 50;
                this.groupBox2.TabStop = false;
                this.groupBox2.Text = "Body Parts";
                // 
                // button2
                // 
                this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(105)))), ((int)(((byte)(172)))));
                this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button2.Location = new System.Drawing.Point(46, 65);
                this.button2.Name = "button2";
                this.button2.Size = new System.Drawing.Size(68, 72);
                this.button2.TabIndex = 1;
                this.button2.UseVisualStyleBackColor = false;
                this.button2.Click += new System.EventHandler(this.Button2Click);
                // 
                // button1
                // 
                this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(205)))), ((int)(((byte)(47)))));
                this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button1.Location = new System.Drawing.Point(56, 15);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(47, 46);
                this.button1.TabIndex = 0;
                this.button1.UseVisualStyleBackColor = false;
                this.button1.Click += new System.EventHandler(this.Button1Click);
                // 
                // button3
                // 
                this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(205)))), ((int)(((byte)(47)))));
                this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button3.Location = new System.Drawing.Point(9, 65);
                this.button3.Name = "button3";
                this.button3.Size = new System.Drawing.Size(31, 72);
                this.button3.TabIndex = 2;
                this.button3.UseVisualStyleBackColor = false;
                this.button3.Click += new System.EventHandler(this.Button3Click);
                // 
                // button4
                // 
                this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(205)))), ((int)(((byte)(47)))));
                this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button4.Location = new System.Drawing.Point(120, 65);
                this.button4.Name = "button4";
                this.button4.Size = new System.Drawing.Size(31, 72);
                this.button4.TabIndex = 3;
                this.button4.UseVisualStyleBackColor = false;
                this.button4.Click += new System.EventHandler(this.Button4Click);
                // 
                // button5
                // 
                this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(189)))), ((int)(((byte)(71)))));
                this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button5.Location = new System.Drawing.Point(46, 143);
                this.button5.Name = "button5";
                this.button5.Size = new System.Drawing.Size(31, 70);
                this.button5.TabIndex = 4;
                this.button5.UseVisualStyleBackColor = false;
                this.button5.Click += new System.EventHandler(this.Button5Click);
                // 
                // button6
                // 
                this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(189)))), ((int)(((byte)(71)))));
                this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button6.Location = new System.Drawing.Point(83, 143);
                this.button6.Name = "button6";
                this.button6.Size = new System.Drawing.Size(31, 70);
                this.button6.TabIndex = 5;
                this.button6.UseVisualStyleBackColor = false;
                this.button6.Click += new System.EventHandler(this.Button6Click);
                // 
                // groupBox1
                // 
                this.groupBox1.Controls.Add(this.button70);
                this.groupBox1.Controls.Add(this.button7);
                this.groupBox1.Controls.Add(this.button69);
                this.groupBox1.Controls.Add(this.button8);
                this.groupBox1.Controls.Add(this.button9);
                this.groupBox1.Controls.Add(this.button10);
                this.groupBox1.Controls.Add(this.button14);
                this.groupBox1.Controls.Add(this.button35);
                this.groupBox1.Controls.Add(this.button13);
                this.groupBox1.Controls.Add(this.button36);
                this.groupBox1.Controls.Add(this.button12);
                this.groupBox1.Controls.Add(this.button37);
                this.groupBox1.Controls.Add(this.button11);
                this.groupBox1.Controls.Add(this.button38);
                this.groupBox1.Controls.Add(this.button18);
                this.groupBox1.Controls.Add(this.button31);
                this.groupBox1.Controls.Add(this.button17);
                this.groupBox1.Controls.Add(this.button32);
                this.groupBox1.Controls.Add(this.button16);
                this.groupBox1.Controls.Add(this.button33);
                this.groupBox1.Controls.Add(this.button15);
                this.groupBox1.Controls.Add(this.button34);
                this.groupBox1.Controls.Add(this.button22);
                this.groupBox1.Controls.Add(this.button27);
                this.groupBox1.Controls.Add(this.button21);
                this.groupBox1.Controls.Add(this.button28);
                this.groupBox1.Controls.Add(this.button20);
                this.groupBox1.Controls.Add(this.button29);
                this.groupBox1.Controls.Add(this.button19);
                this.groupBox1.Controls.Add(this.button30);
                this.groupBox1.Controls.Add(this.button26);
                this.groupBox1.Controls.Add(this.button23);
                this.groupBox1.Controls.Add(this.button25);
                this.groupBox1.Controls.Add(this.button24);
                this.groupBox1.Location = new System.Drawing.Point(174, 6);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(286, 155);
                this.groupBox1.TabIndex = 49;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "Part Colors";
                // 
                // button70
                // 
                this.button70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(122)))), ((int)(((byte)(89)))));
                this.button70.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button70.Location = new System.Drawing.Point(244, 97);
                this.button70.Name = "button70";
                this.button70.Size = new System.Drawing.Size(20, 20);
                this.button70.TabIndex = 33;
                this.button70.UseVisualStyleBackColor = false;
                this.button70.Click += new System.EventHandler(this.button70_Click);
                // 
                // button7
                // 
                this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
                this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button7.Location = new System.Drawing.Point(27, 19);
                this.button7.Name = "button7";
                this.button7.Size = new System.Drawing.Size(20, 20);
                this.button7.TabIndex = 6;
                this.button7.UseVisualStyleBackColor = false;
                this.button7.Click += new System.EventHandler(this.Button7Click);
                // 
                // button69
                // 
                this.button69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(112)))), ((int)(((byte)(160)))));
                this.button69.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button69.Location = new System.Drawing.Point(58, 97);
                this.button69.Name = "button69";
                this.button69.Size = new System.Drawing.Size(20, 20);
                this.button69.TabIndex = 32;
                this.button69.UseVisualStyleBackColor = false;
                this.button69.Click += new System.EventHandler(this.button69_Click);
                // 
                // button8
                // 
                this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(229)))), ((int)(((byte)(224)))));
                this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button8.Location = new System.Drawing.Point(58, 19);
                this.button8.Name = "button8";
                this.button8.Size = new System.Drawing.Size(20, 20);
                this.button8.TabIndex = 7;
                this.button8.UseVisualStyleBackColor = false;
                this.button8.Click += new System.EventHandler(this.Button8Click);
                // 
                // button9
                // 
                this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(165)))));
                this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button9.Location = new System.Drawing.Point(89, 19);
                this.button9.Name = "button9";
                this.button9.Size = new System.Drawing.Size(20, 20);
                this.button9.TabIndex = 8;
                this.button9.UseVisualStyleBackColor = false;
                this.button9.Click += new System.EventHandler(this.Button9Click);
                // 
                // button10
                // 
                this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(95)))), ((int)(((byte)(96)))));
                this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button10.Location = new System.Drawing.Point(120, 19);
                this.button10.Name = "button10";
                this.button10.Size = new System.Drawing.Size(20, 20);
                this.button10.TabIndex = 9;
                this.button10.UseVisualStyleBackColor = false;
                this.button10.Click += new System.EventHandler(this.Button10Click);
                // 
                // button14
                // 
                this.button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(42)))), ((int)(((byte)(53)))));
                this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button14.Location = new System.Drawing.Point(151, 19);
                this.button14.Name = "button14";
                this.button14.Size = new System.Drawing.Size(20, 20);
                this.button14.TabIndex = 10;
                this.button14.UseVisualStyleBackColor = false;
                this.button14.Click += new System.EventHandler(this.Button14Click);
                // 
                // button35
                // 
                this.button35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(185)))), ((int)(((byte)(145)))));
                this.button35.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button35.Location = new System.Drawing.Point(58, 123);
                this.button35.Name = "button35";
                this.button35.Size = new System.Drawing.Size(20, 20);
                this.button35.TabIndex = 37;
                this.button35.UseVisualStyleBackColor = false;
                this.button35.Click += new System.EventHandler(this.Button35Click);
                // 
                // button13
                // 
                this.button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(40)))), ((int)(((byte)(27)))));
                this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button13.Location = new System.Drawing.Point(182, 19);
                this.button13.Name = "button13";
                this.button13.Size = new System.Drawing.Size(20, 20);
                this.button13.TabIndex = 11;
                this.button13.UseVisualStyleBackColor = false;
                this.button13.Click += new System.EventHandler(this.Button13Click);
                // 
                // button36
                // 
                this.button36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(142)))), ((int)(((byte)(105)))));
                this.button36.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button36.Location = new System.Drawing.Point(27, 123);
                this.button36.Name = "button36";
                this.button36.Size = new System.Drawing.Size(20, 20);
                this.button36.TabIndex = 36;
                this.button36.UseVisualStyleBackColor = false;
                this.button36.Click += new System.EventHandler(this.Button36Click);
                // 
                // button12
                // 
                this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(205)))), ((int)(((byte)(47)))));
                this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button12.Location = new System.Drawing.Point(213, 19);
                this.button12.Name = "button12";
                this.button12.Size = new System.Drawing.Size(20, 20);
                this.button12.TabIndex = 12;
                this.button12.UseVisualStyleBackColor = false;
                this.button12.Click += new System.EventHandler(this.Button12Click);
                // 
                // button37
                // 
                this.button37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(92)))), ((int)(((byte)(69)))));
                this.button37.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button37.Location = new System.Drawing.Point(213, 97);
                this.button37.Name = "button37";
                this.button37.Size = new System.Drawing.Size(20, 20);
                this.button37.TabIndex = 35;
                this.button37.UseVisualStyleBackColor = false;
                this.button37.Click += new System.EventHandler(this.Button37Click);
                // 
                // button11
                // 
                this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(234)))), ((int)(((byte)(142)))));
                this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button11.Location = new System.Drawing.Point(244, 19);
                this.button11.Name = "button11";
                this.button11.Size = new System.Drawing.Size(20, 20);
                this.button11.TabIndex = 13;
                this.button11.UseVisualStyleBackColor = false;
                this.button11.Click += new System.EventHandler(this.Button11Click);
                // 
                // button38
                // 
                this.button38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(122)))), ((int)(((byte)(118)))));
                this.button38.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button38.Location = new System.Drawing.Point(182, 97);
                this.button38.Name = "button38";
                this.button38.Size = new System.Drawing.Size(20, 20);
                this.button38.TabIndex = 34;
                this.button38.UseVisualStyleBackColor = false;
                this.button38.Click += new System.EventHandler(this.Button38Click);
                // 
                // button18
                // 
                this.button18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(105)))), ((int)(((byte)(172)))));
                this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button18.Location = new System.Drawing.Point(27, 45);
                this.button18.Name = "button18";
                this.button18.Size = new System.Drawing.Size(20, 20);
                this.button18.TabIndex = 14;
                this.button18.UseVisualStyleBackColor = false;
                this.button18.Click += new System.EventHandler(this.Button18Click);
                // 
                // button31
                // 
                this.button31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(196)))), ((int)(((byte)(153)))));
                this.button31.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button31.Location = new System.Drawing.Point(151, 97);
                this.button31.Name = "button31";
                this.button31.Size = new System.Drawing.Size(20, 20);
                this.button31.TabIndex = 33;
                this.button31.UseVisualStyleBackColor = false;
                this.button31.Click += new System.EventHandler(this.Button31Click);
                // 
                // button17
                // 
                this.button17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(143)))), ((int)(((byte)(155)))));
                this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button17.Location = new System.Drawing.Point(58, 45);
                this.button17.Name = "button17";
                this.button17.Size = new System.Drawing.Size(20, 20);
                this.button17.TabIndex = 15;
                this.button17.UseVisualStyleBackColor = false;
                this.button17.Click += new System.EventHandler(this.Button17Click);
                // 
                // button32
                // 
                this.button32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(135)))), ((int)(((byte)(121)))));
                this.button32.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button32.Location = new System.Drawing.Point(120, 97);
                this.button32.Name = "button32";
                this.button32.Size = new System.Drawing.Size(20, 20);
                this.button32.TabIndex = 32;
                this.button32.UseVisualStyleBackColor = false;
                this.button32.Click += new System.EventHandler(this.Button32Click);
                // 
                // button16
                // 
                this.button16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(153)))), ((int)(((byte)(201)))));
                this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button16.Location = new System.Drawing.Point(89, 45);
                this.button16.Name = "button16";
                this.button16.Size = new System.Drawing.Size(20, 20);
                this.button16.TabIndex = 16;
                this.button16.UseVisualStyleBackColor = false;
                this.button16.Click += new System.EventHandler(this.Button16Click);
                // 
                // button33
                // 
                this.button33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
                this.button33.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button33.Location = new System.Drawing.Point(89, 97);
                this.button33.Name = "button33";
                this.button33.Size = new System.Drawing.Size(20, 20);
                this.button33.TabIndex = 31;
                this.button33.UseVisualStyleBackColor = false;
                this.button33.Click += new System.EventHandler(this.Button33Click);
                // 
                // button15
                // 
                this.button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(186)))), ((int)(((byte)(219)))));
                this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button15.Location = new System.Drawing.Point(120, 45);
                this.button15.Name = "button15";
                this.button15.Size = new System.Drawing.Size(20, 20);
                this.button15.TabIndex = 17;
                this.button15.UseVisualStyleBackColor = false;
                this.button15.Click += new System.EventHandler(this.Button15Click);
                // 
                // button34
                // 
                this.button34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(50)))), ((int)(((byte)(123)))));
                this.button34.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button34.Location = new System.Drawing.Point(27, 97);
                this.button34.Name = "button34";
                this.button34.Size = new System.Drawing.Size(20, 20);
                this.button34.TabIndex = 30;
                this.button34.UseVisualStyleBackColor = false;
                this.button34.Click += new System.EventHandler(this.Button34Click);
                // 
                // button22
                // 
                this.button22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(210)))), ((int)(((byte)(228)))));
                this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button22.Location = new System.Drawing.Point(151, 45);
                this.button22.Name = "button22";
                this.button22.Size = new System.Drawing.Size(20, 20);
                this.button22.TabIndex = 18;
                this.button22.UseVisualStyleBackColor = false;
                this.button22.Click += new System.EventHandler(this.Button22Click);
                // 
                // button27
                // 
                this.button27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(63)))), ((int)(((byte)(39)))));
                this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button27.Location = new System.Drawing.Point(244, 71);
                this.button27.Name = "button27";
                this.button27.Size = new System.Drawing.Size(20, 20);
                this.button27.TabIndex = 29;
                this.button27.UseVisualStyleBackColor = false;
                this.button27.Click += new System.EventHandler(this.Button27Click);
                // 
                // button21
                // 
                this.button21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(134)))), ((int)(((byte)(156)))));
                this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button21.Location = new System.Drawing.Point(182, 45);
                this.button21.Name = "button21";
                this.button21.Size = new System.Drawing.Size(20, 20);
                this.button21.TabIndex = 19;
                this.button21.UseVisualStyleBackColor = false;
                this.button21.Click += new System.EventHandler(this.Button21Click);
                // 
                // button28
                // 
                this.button28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(95)))), ((int)(((byte)(55)))));
                this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button28.Location = new System.Drawing.Point(213, 71);
                this.button28.Name = "button28";
                this.button28.Size = new System.Drawing.Size(20, 20);
                this.button28.TabIndex = 28;
                this.button28.UseVisualStyleBackColor = false;
                this.button28.Click += new System.EventHandler(this.Button28Click);
                // 
                // button20
                // 
                this.button20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(134)))), ((int)(((byte)(64)))));
                this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button20.Location = new System.Drawing.Point(213, 45);
                this.button20.Name = "button20";
                this.button20.Size = new System.Drawing.Size(20, 20);
                this.button20.TabIndex = 20;
                this.button20.UseVisualStyleBackColor = false;
                this.button20.Click += new System.EventHandler(this.Button20Click);
                // 
                // button29
                // 
                this.button29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(130)))));
                this.button29.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button29.Location = new System.Drawing.Point(182, 71);
                this.button29.Name = "button29";
                this.button29.Size = new System.Drawing.Size(20, 20);
                this.button29.TabIndex = 27;
                this.button29.UseVisualStyleBackColor = false;
                this.button29.Click += new System.EventHandler(this.Button29Click);
                // 
                // button19
                // 
                this.button19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(155)))), ((int)(((byte)(63)))));
                this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button19.Location = new System.Drawing.Point(244, 45);
                this.button19.Name = "button19";
                this.button19.Size = new System.Drawing.Size(20, 20);
                this.button19.TabIndex = 21;
                this.button19.UseVisualStyleBackColor = false;
                this.button19.Click += new System.EventHandler(this.Button19Click);
                // 
                // button30
                // 
                this.button30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(196)))), ((int)(((byte)(140)))));
                this.button30.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button30.Location = new System.Drawing.Point(151, 71);
                this.button30.Name = "button30";
                this.button30.Size = new System.Drawing.Size(20, 20);
                this.button30.TabIndex = 26;
                this.button30.UseVisualStyleBackColor = false;
                this.button30.Click += new System.EventHandler(this.Button30Click);
                // 
                // button26
                // 
                this.button26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(70)))), ((int)(((byte)(43)))));
                this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button26.Location = new System.Drawing.Point(27, 71);
                this.button26.Name = "button26";
                this.button26.Size = new System.Drawing.Size(20, 20);
                this.button26.TabIndex = 22;
                this.button26.UseVisualStyleBackColor = false;
                this.button26.Click += new System.EventHandler(this.Button26Click);
                // 
                // button23
                // 
                this.button23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(189)))), ((int)(((byte)(71)))));
                this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button23.Location = new System.Drawing.Point(120, 71);
                this.button23.Name = "button23";
                this.button23.Size = new System.Drawing.Size(20, 20);
                this.button23.TabIndex = 25;
                this.button23.UseVisualStyleBackColor = false;
                this.button23.Click += new System.EventHandler(this.Button23Click);
                // 
                // button25
                // 
                this.button25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(126)))), ((int)(((byte)(71)))));
                this.button25.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button25.Location = new System.Drawing.Point(58, 71);
                this.button25.Name = "button25";
                this.button25.Size = new System.Drawing.Size(20, 20);
                this.button25.TabIndex = 23;
                this.button25.UseVisualStyleBackColor = false;
                this.button25.Click += new System.EventHandler(this.Button25Click);
                // 
                // button24
                // 
                this.button24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(150)))), ((int)(((byte)(73)))));
                this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.button24.Location = new System.Drawing.Point(89, 71);
                this.button24.Name = "button24";
                this.button24.Size = new System.Drawing.Size(20, 20);
                this.button24.TabIndex = 24;
                this.button24.UseVisualStyleBackColor = false;
                this.button24.Click += new System.EventHandler(this.Button24Click);
                // 
                // button39
                // 
                this.button39.Location = new System.Drawing.Point(174, 182);
                this.button39.Name = "button39";
                this.button39.Size = new System.Drawing.Size(89, 49);
                this.button39.TabIndex = 48;
                this.button39.Text = "Randomize Colors";
                this.button39.UseVisualStyleBackColor = true;
                this.button39.Click += new System.EventHandler(this.Button39Click);
                // 
                // button40
                // 
                this.button40.Location = new System.Drawing.Point(264, 182);
                this.button40.Name = "button40";
                this.button40.Size = new System.Drawing.Size(90, 49);
                this.button40.TabIndex = 47;
                this.button40.Text = "Reset Colors";
                this.button40.UseVisualStyleBackColor = true;
                this.button40.Click += new System.EventHandler(this.Button40Click);
                // 
                // label2
                // 
                this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.label2.Location = new System.Drawing.Point(274, 163);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(80, 16);
                this.label2.TabIndex = 46;
                // 
                // label1
                // 
                this.label1.Location = new System.Drawing.Point(174, 164);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(105, 16);
                this.label1.TabIndex = 45;
                this.label1.Text = "SELECTED PART:";
                // 
                // tabPage2
                // 
                this.tabPage2.Controls.Add(this.tabControl2);
                this.tabPage2.Controls.Add(this.button42);
                this.tabPage2.Controls.Add(this.button41);
                this.tabPage2.Location = new System.Drawing.Point(4, 22);
                this.tabPage2.Name = "tabPage2";
                this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage2.Size = new System.Drawing.Size(467, 241);
                this.tabPage2.TabIndex = 1;
                this.tabPage2.Text = "HATS";
                this.tabPage2.UseVisualStyleBackColor = true;
                // 
                // tabControl2
                // 
                this.tabControl2.Controls.Add(this.tabPage10);
                this.tabControl2.Controls.Add(this.tabPage11);
                this.tabControl2.Controls.Add(this.tabPage12);
                this.tabControl2.Location = new System.Drawing.Point(0, 0);
                this.tabControl2.Name = "tabControl2";
                this.tabControl2.SelectedIndex = 0;
                this.tabControl2.Size = new System.Drawing.Size(471, 191);
                this.tabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
                this.tabControl2.TabIndex = 57;
                // 
                // tabPage10
                // 
                this.tabPage10.Controls.Add(this.textBox2);
                this.tabPage10.Controls.Add(this.listBox1);
                this.tabPage10.Controls.Add(this.pictureBox1);
                this.tabPage10.Location = new System.Drawing.Point(4, 22);
                this.tabPage10.Name = "tabPage10";
                this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage10.Size = new System.Drawing.Size(463, 165);
                this.tabPage10.TabIndex = 0;
                this.tabPage10.Text = "HAT 1";
                this.tabPage10.UseVisualStyleBackColor = true;
                // 
                // textBox2
                // 
                this.textBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox2.Location = new System.Drawing.Point(238, 94);
                this.textBox2.Multiline = true;
                this.textBox2.Name = "textBox2";
                this.textBox2.ReadOnly = true;
                this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox2.Size = new System.Drawing.Size(218, 68);
                this.textBox2.TabIndex = 51;
                // 
                // listBox1
                // 
                this.listBox1.FormattingEnabled = true;
                this.listBox1.Location = new System.Drawing.Point(2, 3);
                this.listBox1.Name = "listBox1";
                this.listBox1.Size = new System.Drawing.Size(219, 160);
                this.listBox1.TabIndex = 46;
                this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1SelectedIndexChanged);
                // 
                // pictureBox1
                // 
                this.pictureBox1.Location = new System.Drawing.Point(303, 3);
                this.pictureBox1.Name = "pictureBox1";
                this.pictureBox1.Size = new System.Drawing.Size(85, 85);
                this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox1.TabIndex = 50;
                this.pictureBox1.TabStop = false;
                // 
                // tabPage11
                // 
                this.tabPage11.Controls.Add(this.textBox3);
                this.tabPage11.Controls.Add(this.listBox2);
                this.tabPage11.Controls.Add(this.pictureBox2);
                this.tabPage11.Location = new System.Drawing.Point(4, 22);
                this.tabPage11.Name = "tabPage11";
                this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
                this.tabPage11.Size = new System.Drawing.Size(463, 165);
                this.tabPage11.TabIndex = 1;
                this.tabPage11.Text = "HAT 2";
                this.tabPage11.UseVisualStyleBackColor = true;
                // 
                // textBox3
                // 
                this.textBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox3.Location = new System.Drawing.Point(238, 94);
                this.textBox3.Multiline = true;
                this.textBox3.Name = "textBox3";
                this.textBox3.ReadOnly = true;
                this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox3.Size = new System.Drawing.Size(218, 68);
                this.textBox3.TabIndex = 52;
                // 
                // listBox2
                // 
                this.listBox2.FormattingEnabled = true;
                this.listBox2.Location = new System.Drawing.Point(2, 3);
                this.listBox2.Name = "listBox2";
                this.listBox2.Size = new System.Drawing.Size(219, 160);
                this.listBox2.TabIndex = 47;
                this.listBox2.SelectedIndexChanged += new System.EventHandler(this.ListBox2SelectedIndexChanged);
                // 
                // pictureBox2
                // 
                this.pictureBox2.Location = new System.Drawing.Point(303, 3);
                this.pictureBox2.Name = "pictureBox2";
                this.pictureBox2.Size = new System.Drawing.Size(85, 85);
                this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox2.TabIndex = 51;
                this.pictureBox2.TabStop = false;
                // 
                // tabPage12
                // 
                this.tabPage12.Controls.Add(this.textBox4);
                this.tabPage12.Controls.Add(this.listBox3);
                this.tabPage12.Controls.Add(this.pictureBox3);
                this.tabPage12.Location = new System.Drawing.Point(4, 22);
                this.tabPage12.Name = "tabPage12";
                this.tabPage12.Size = new System.Drawing.Size(463, 165);
                this.tabPage12.TabIndex = 2;
                this.tabPage12.Text = "HAT 3";
                this.tabPage12.UseVisualStyleBackColor = true;
                // 
                // textBox4
                // 
                this.textBox4.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox4.Location = new System.Drawing.Point(238, 94);
                this.textBox4.Multiline = true;
                this.textBox4.Name = "textBox4";
                this.textBox4.ReadOnly = true;
                this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox4.Size = new System.Drawing.Size(218, 68);
                this.textBox4.TabIndex = 55;
                // 
                // listBox3
                // 
                this.listBox3.FormattingEnabled = true;
                this.listBox3.Location = new System.Drawing.Point(2, 3);
                this.listBox3.Name = "listBox3";
                this.listBox3.Size = new System.Drawing.Size(219, 160);
                this.listBox3.TabIndex = 52;
                this.listBox3.SelectedIndexChanged += new System.EventHandler(this.ListBox3SelectedIndexChanged);
                // 
                // pictureBox3
                // 
                this.pictureBox3.Location = new System.Drawing.Point(303, 3);
                this.pictureBox3.Name = "pictureBox3";
                this.pictureBox3.Size = new System.Drawing.Size(85, 85);
                this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox3.TabIndex = 54;
                this.pictureBox3.TabStop = false;
                // 
                // button42
                // 
                this.button42.Location = new System.Drawing.Point(242, 193);
                this.button42.Name = "button42";
                this.button42.Size = new System.Drawing.Size(219, 42);
                this.button42.TabIndex = 56;
                this.button42.Text = "Reset all 3";
                this.button42.UseVisualStyleBackColor = true;
                this.button42.Click += new System.EventHandler(this.Button42Click);
                // 
                // button41
                // 
                this.button41.Location = new System.Drawing.Point(6, 193);
                this.button41.Name = "button41";
                this.button41.Size = new System.Drawing.Size(219, 42);
                this.button41.TabIndex = 55;
                this.button41.Text = "Randomize all 3";
                this.button41.UseVisualStyleBackColor = true;
                this.button41.Click += new System.EventHandler(this.Button41Click);
                // 
                // tabPage8
                // 
                this.tabPage8.Controls.Add(this.textBox5);
                this.tabPage8.Controls.Add(this.button56);
                this.tabPage8.Controls.Add(this.button57);
                this.tabPage8.Controls.Add(this.pictureBox8);
                this.tabPage8.Controls.Add(this.listBox8);
                this.tabPage8.Location = new System.Drawing.Point(4, 22);
                this.tabPage8.Name = "tabPage8";
                this.tabPage8.Size = new System.Drawing.Size(467, 241);
                this.tabPage8.TabIndex = 7;
                this.tabPage8.Text = "HEADS";
                this.tabPage8.UseVisualStyleBackColor = true;
                // 
                // textBox5
                // 
                this.textBox5.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox5.Location = new System.Drawing.Point(242, 114);
                this.textBox5.Multiline = true;
                this.textBox5.Name = "textBox5";
                this.textBox5.ReadOnly = true;
                this.textBox5.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox5.Size = new System.Drawing.Size(218, 68);
                this.textBox5.TabIndex = 66;
                // 
                // button56
                // 
                this.button56.Location = new System.Drawing.Point(242, 193);
                this.button56.Name = "button56";
                this.button56.Size = new System.Drawing.Size(219, 42);
                this.button56.TabIndex = 65;
                this.button56.Text = "Reset";
                this.button56.UseVisualStyleBackColor = true;
                this.button56.Click += new System.EventHandler(this.Button56Click);
                // 
                // button57
                // 
                this.button57.Location = new System.Drawing.Point(6, 193);
                this.button57.Name = "button57";
                this.button57.Size = new System.Drawing.Size(219, 42);
                this.button57.TabIndex = 64;
                this.button57.Text = "Randomize";
                this.button57.UseVisualStyleBackColor = true;
                this.button57.Click += new System.EventHandler(this.Button57Click);
                // 
                // pictureBox8
                // 
                this.pictureBox8.Location = new System.Drawing.Point(303, 9);
                this.pictureBox8.Name = "pictureBox8";
                this.pictureBox8.Size = new System.Drawing.Size(99, 99);
                this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox8.TabIndex = 63;
                this.pictureBox8.TabStop = false;
                // 
                // listBox8
                // 
                this.listBox8.FormattingEnabled = true;
                this.listBox8.Location = new System.Drawing.Point(6, 9);
                this.listBox8.Name = "listBox8";
                this.listBox8.Size = new System.Drawing.Size(219, 173);
                this.listBox8.TabIndex = 62;
                this.listBox8.SelectedIndexChanged += new System.EventHandler(this.ListBox8SelectedIndexChanged);
                // 
                // tabPage3
                // 
                this.tabPage3.Controls.Add(this.textBox6);
                this.tabPage3.Controls.Add(this.button44);
                this.tabPage3.Controls.Add(this.button45);
                this.tabPage3.Controls.Add(this.pictureBox4);
                this.tabPage3.Controls.Add(this.listBox4);
                this.tabPage3.Location = new System.Drawing.Point(4, 22);
                this.tabPage3.Name = "tabPage3";
                this.tabPage3.Size = new System.Drawing.Size(467, 241);
                this.tabPage3.TabIndex = 2;
                this.tabPage3.Text = "FACES";
                this.tabPage3.UseVisualStyleBackColor = true;
                // 
                // textBox6
                // 
                this.textBox6.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox6.Location = new System.Drawing.Point(242, 114);
                this.textBox6.Multiline = true;
                this.textBox6.Name = "textBox6";
                this.textBox6.ReadOnly = true;
                this.textBox6.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox6.Size = new System.Drawing.Size(218, 68);
                this.textBox6.TabIndex = 67;
                // 
                // button44
                // 
                this.button44.Location = new System.Drawing.Point(242, 193);
                this.button44.Name = "button44";
                this.button44.Size = new System.Drawing.Size(219, 42);
                this.button44.TabIndex = 61;
                this.button44.Text = "Reset";
                this.button44.UseVisualStyleBackColor = true;
                this.button44.Click += new System.EventHandler(this.Button44Click);
                // 
                // button45
                // 
                this.button45.Location = new System.Drawing.Point(6, 193);
                this.button45.Name = "button45";
                this.button45.Size = new System.Drawing.Size(219, 42);
                this.button45.TabIndex = 60;
                this.button45.Text = "Randomize";
                this.button45.UseVisualStyleBackColor = true;
                this.button45.Click += new System.EventHandler(this.Button45Click);
                // 
                // pictureBox4
                // 
                this.pictureBox4.Location = new System.Drawing.Point(303, 9);
                this.pictureBox4.Name = "pictureBox4";
                this.pictureBox4.Size = new System.Drawing.Size(99, 99);
                this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox4.TabIndex = 59;
                this.pictureBox4.TabStop = false;
                // 
                // listBox4
                // 
                this.listBox4.FormattingEnabled = true;
                this.listBox4.Location = new System.Drawing.Point(6, 9);
                this.listBox4.Name = "listBox4";
                this.listBox4.Size = new System.Drawing.Size(219, 173);
                this.listBox4.TabIndex = 57;
                this.listBox4.SelectedIndexChanged += new System.EventHandler(this.ListBox4SelectedIndexChanged);
                // 
                // tabPage4
                // 
                this.tabPage4.Controls.Add(this.textBox7);
                this.tabPage4.Controls.Add(this.button46);
                this.tabPage4.Controls.Add(this.button47);
                this.tabPage4.Controls.Add(this.pictureBox5);
                this.tabPage4.Controls.Add(this.listBox5);
                this.tabPage4.Location = new System.Drawing.Point(4, 22);
                this.tabPage4.Name = "tabPage4";
                this.tabPage4.Size = new System.Drawing.Size(467, 241);
                this.tabPage4.TabIndex = 3;
                this.tabPage4.Text = "T-SHIRTS";
                this.tabPage4.UseVisualStyleBackColor = true;
                // 
                // textBox7
                // 
                this.textBox7.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox7.Location = new System.Drawing.Point(242, 114);
                this.textBox7.Multiline = true;
                this.textBox7.Name = "textBox7";
                this.textBox7.ReadOnly = true;
                this.textBox7.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox7.Size = new System.Drawing.Size(218, 68);
                this.textBox7.TabIndex = 68;
                // 
                // button46
                // 
                this.button46.Location = new System.Drawing.Point(242, 193);
                this.button46.Name = "button46";
                this.button46.Size = new System.Drawing.Size(219, 42);
                this.button46.TabIndex = 65;
                this.button46.Text = "Reset";
                this.button46.UseVisualStyleBackColor = true;
                this.button46.Click += new System.EventHandler(this.Button46Click);
                // 
                // button47
                // 
                this.button47.Location = new System.Drawing.Point(6, 193);
                this.button47.Name = "button47";
                this.button47.Size = new System.Drawing.Size(219, 42);
                this.button47.TabIndex = 64;
                this.button47.Text = "Randomize";
                this.button47.UseVisualStyleBackColor = true;
                this.button47.Click += new System.EventHandler(this.Button47Click);
                // 
                // pictureBox5
                // 
                this.pictureBox5.Location = new System.Drawing.Point(303, 9);
                this.pictureBox5.Name = "pictureBox5";
                this.pictureBox5.Size = new System.Drawing.Size(99, 99);
                this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox5.TabIndex = 63;
                this.pictureBox5.TabStop = false;
                // 
                // listBox5
                // 
                this.listBox5.FormattingEnabled = true;
                this.listBox5.Location = new System.Drawing.Point(6, 9);
                this.listBox5.Name = "listBox5";
                this.listBox5.Size = new System.Drawing.Size(219, 173);
                this.listBox5.TabIndex = 62;
                this.listBox5.SelectedIndexChanged += new System.EventHandler(this.ListBox5SelectedIndexChanged);
                // 
                // tabPage5
                // 
                this.tabPage5.Controls.Add(this.textBox8);
                this.tabPage5.Controls.Add(this.button48);
                this.tabPage5.Controls.Add(this.button49);
                this.tabPage5.Controls.Add(this.pictureBox6);
                this.tabPage5.Controls.Add(this.listBox6);
                this.tabPage5.Location = new System.Drawing.Point(4, 22);
                this.tabPage5.Name = "tabPage5";
                this.tabPage5.Size = new System.Drawing.Size(467, 241);
                this.tabPage5.TabIndex = 4;
                this.tabPage5.Text = "SHIRTS";
                this.tabPage5.UseVisualStyleBackColor = true;
                // 
                // textBox8
                // 
                this.textBox8.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox8.Location = new System.Drawing.Point(242, 114);
                this.textBox8.Multiline = true;
                this.textBox8.Name = "textBox8";
                this.textBox8.ReadOnly = true;
                this.textBox8.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox8.Size = new System.Drawing.Size(218, 68);
                this.textBox8.TabIndex = 68;
                // 
                // button48
                // 
                this.button48.Location = new System.Drawing.Point(242, 193);
                this.button48.Name = "button48";
                this.button48.Size = new System.Drawing.Size(219, 42);
                this.button48.TabIndex = 65;
                this.button48.Text = "Reset";
                this.button48.UseVisualStyleBackColor = true;
                this.button48.Click += new System.EventHandler(this.Button48Click);
                // 
                // button49
                // 
                this.button49.Location = new System.Drawing.Point(6, 193);
                this.button49.Name = "button49";
                this.button49.Size = new System.Drawing.Size(219, 42);
                this.button49.TabIndex = 64;
                this.button49.Text = "Randomize";
                this.button49.UseVisualStyleBackColor = true;
                this.button49.Click += new System.EventHandler(this.Button49Click);
                // 
                // pictureBox6
                // 
                this.pictureBox6.Location = new System.Drawing.Point(303, 9);
                this.pictureBox6.Name = "pictureBox6";
                this.pictureBox6.Size = new System.Drawing.Size(99, 99);
                this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox6.TabIndex = 63;
                this.pictureBox6.TabStop = false;
                // 
                // listBox6
                // 
                this.listBox6.FormattingEnabled = true;
                this.listBox6.Location = new System.Drawing.Point(6, 9);
                this.listBox6.Name = "listBox6";
                this.listBox6.Size = new System.Drawing.Size(219, 173);
                this.listBox6.TabIndex = 62;
                this.listBox6.SelectedIndexChanged += new System.EventHandler(this.ListBox6SelectedIndexChanged);
                // 
                // tabPage6
                // 
                this.tabPage6.Controls.Add(this.textBox9);
                this.tabPage6.Controls.Add(this.button50);
                this.tabPage6.Controls.Add(this.button51);
                this.tabPage6.Controls.Add(this.pictureBox7);
                this.tabPage6.Controls.Add(this.listBox7);
                this.tabPage6.Location = new System.Drawing.Point(4, 22);
                this.tabPage6.Name = "tabPage6";
                this.tabPage6.Size = new System.Drawing.Size(467, 241);
                this.tabPage6.TabIndex = 5;
                this.tabPage6.Text = "PANTS";
                this.tabPage6.UseVisualStyleBackColor = true;
                // 
                // textBox9
                // 
                this.textBox9.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox9.Location = new System.Drawing.Point(242, 114);
                this.textBox9.Multiline = true;
                this.textBox9.Name = "textBox9";
                this.textBox9.ReadOnly = true;
                this.textBox9.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox9.Size = new System.Drawing.Size(218, 68);
                this.textBox9.TabIndex = 68;
                // 
                // button50
                // 
                this.button50.Location = new System.Drawing.Point(242, 193);
                this.button50.Name = "button50";
                this.button50.Size = new System.Drawing.Size(219, 42);
                this.button50.TabIndex = 65;
                this.button50.Text = "Reset";
                this.button50.UseVisualStyleBackColor = true;
                this.button50.Click += new System.EventHandler(this.Button50Click);
                // 
                // button51
                // 
                this.button51.Location = new System.Drawing.Point(6, 193);
                this.button51.Name = "button51";
                this.button51.Size = new System.Drawing.Size(219, 42);
                this.button51.TabIndex = 64;
                this.button51.Text = "Randomize";
                this.button51.UseVisualStyleBackColor = true;
                this.button51.Click += new System.EventHandler(this.Button51Click);
                // 
                // pictureBox7
                // 
                this.pictureBox7.Location = new System.Drawing.Point(303, 9);
                this.pictureBox7.Name = "pictureBox7";
                this.pictureBox7.Size = new System.Drawing.Size(99, 99);
                this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox7.TabIndex = 63;
                this.pictureBox7.TabStop = false;
                // 
                // listBox7
                // 
                this.listBox7.FormattingEnabled = true;
                this.listBox7.Location = new System.Drawing.Point(6, 9);
                this.listBox7.Name = "listBox7";
                this.listBox7.Size = new System.Drawing.Size(219, 173);
                this.listBox7.TabIndex = 62;
                this.listBox7.SelectedIndexChanged += new System.EventHandler(this.ListBox7SelectedIndexChanged);
                // 
                // tabPage9
                // 
                this.tabPage9.Controls.Add(this.textBox10);
                this.tabPage9.Controls.Add(this.checkBox1);
                this.tabPage9.Controls.Add(this.button58);
                this.tabPage9.Controls.Add(this.button59);
                this.tabPage9.Controls.Add(this.pictureBox9);
                this.tabPage9.Controls.Add(this.listBox9);
                this.tabPage9.Location = new System.Drawing.Point(4, 22);
                this.tabPage9.Name = "tabPage9";
                this.tabPage9.Size = new System.Drawing.Size(467, 241);
                this.tabPage9.TabIndex = 8;
                this.tabPage9.Text = "EXTRA";
                this.tabPage9.UseVisualStyleBackColor = true;
                // 
                // textBox10
                // 
                this.textBox10.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.textBox10.Location = new System.Drawing.Point(242, 114);
                this.textBox10.Multiline = true;
                this.textBox10.Name = "textBox10";
                this.textBox10.ReadOnly = true;
                this.textBox10.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                this.textBox10.Size = new System.Drawing.Size(218, 68);
                this.textBox10.TabIndex = 71;
                // 
                // checkBox1
                // 
                this.checkBox1.Location = new System.Drawing.Point(6, 170);
                this.checkBox1.Name = "checkBox1";
                this.checkBox1.Size = new System.Drawing.Size(79, 17);
                this.checkBox1.TabIndex = 70;
                this.checkBox1.Text = "Show Hats";
                this.checkBox1.UseVisualStyleBackColor = true;
                this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
                // 
                // button58
                // 
                this.button58.Location = new System.Drawing.Point(242, 193);
                this.button58.Name = "button58";
                this.button58.Size = new System.Drawing.Size(219, 42);
                this.button58.TabIndex = 69;
                this.button58.Text = "Reset";
                this.button58.UseVisualStyleBackColor = true;
                this.button58.Click += new System.EventHandler(this.Button58Click);
                // 
                // button59
                // 
                this.button59.Location = new System.Drawing.Point(6, 193);
                this.button59.Name = "button59";
                this.button59.Size = new System.Drawing.Size(219, 42);
                this.button59.TabIndex = 68;
                this.button59.Text = "Randomize";
                this.button59.UseVisualStyleBackColor = true;
                this.button59.Click += new System.EventHandler(this.Button59Click);
                // 
                // pictureBox9
                // 
                this.pictureBox9.Location = new System.Drawing.Point(303, 9);
                this.pictureBox9.Name = "pictureBox9";
                this.pictureBox9.Size = new System.Drawing.Size(99, 99);
                this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox9.TabIndex = 67;
                this.pictureBox9.TabStop = false;
                // 
                // listBox9
                // 
                this.listBox9.FormattingEnabled = true;
                this.listBox9.Location = new System.Drawing.Point(6, 9);
                this.listBox9.Name = "listBox9";
                this.listBox9.Size = new System.Drawing.Size(219, 160);
                this.listBox9.TabIndex = 66;
                this.listBox9.SelectedIndexChanged += new System.EventHandler(this.ListBox9SelectedIndexChanged);
                // 
                // tabPage7
                // 
                this.tabPage7.Controls.Add(this.button71);
                this.tabPage7.Controls.Add(this.label8);
                this.tabPage7.Controls.Add(this.pictureBox10);
                this.tabPage7.Controls.Add(this.button60);
                this.tabPage7.Controls.Add(this.button43);
                this.tabPage7.Controls.Add(this.textBox1);
                this.tabPage7.Controls.Add(this.label7);
                this.tabPage7.Controls.Add(this.label6);
                this.tabPage7.Controls.Add(this.button55);
                this.tabPage7.Controls.Add(this.label5);
                this.tabPage7.Controls.Add(this.label4);
                this.tabPage7.Controls.Add(this.label3);
                this.tabPage7.Controls.Add(this.button54);
                this.tabPage7.Controls.Add(this.button53);
                this.tabPage7.Controls.Add(this.button52);
                this.tabPage7.Location = new System.Drawing.Point(4, 22);
                this.tabPage7.Name = "tabPage7";
                this.tabPage7.Size = new System.Drawing.Size(467, 241);
                this.tabPage7.TabIndex = 6;
                this.tabPage7.Text = "OTHER";
                this.tabPage7.UseVisualStyleBackColor = true;
                // 
                // button71
                // 
                this.button71.Location = new System.Drawing.Point(365, 37);
                this.button71.Name = "button71";
                this.button71.Size = new System.Drawing.Size(99, 31);
                this.button71.TabIndex = 13;
                this.button71.Text = "Save Outfit";
                this.button71.UseVisualStyleBackColor = true;
                this.button71.Click += new System.EventHandler(this.button71_Click);
                // 
                // label8
                // 
                this.label8.Location = new System.Drawing.Point(293, 6);
                this.label8.Name = "label8";
                this.label8.Size = new System.Drawing.Size(69, 29);
                this.label8.TabIndex = 12;
                this.label8.Text = "Custom Icon (Client Side)";
                this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // 
                // pictureBox10
                // 
                this.pictureBox10.Location = new System.Drawing.Point(294, 37);
                this.pictureBox10.Name = "pictureBox10";
                this.pictureBox10.Size = new System.Drawing.Size(65, 64);
                this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pictureBox10.TabIndex = 11;
                this.pictureBox10.TabStop = false;
                // 
                // button60
                // 
                this.button60.Location = new System.Drawing.Point(294, 105);
                this.button60.Name = "button60";
                this.button60.Size = new System.Drawing.Size(68, 23);
                this.button60.TabIndex = 10;
                this.button60.Text = "Browse...";
                this.button60.UseVisualStyleBackColor = true;
                this.button60.Click += new System.EventHandler(this.button60_Click);
                // 
                // button43
                // 
                this.button43.Location = new System.Drawing.Point(365, 3);
                this.button43.Name = "button43";
                this.button43.Size = new System.Drawing.Size(99, 34);
                this.button43.TabIndex = 1;
                this.button43.Text = "Avatar 3D Preview";
                this.button43.UseVisualStyleBackColor = true;
                this.button43.Click += new System.EventHandler(this.Button43Click);
                // 
                // textBox1
                // 
                this.textBox1.Location = new System.Drawing.Point(113, 213);
                this.textBox1.Name = "textBox1";
                this.textBox1.Size = new System.Drawing.Size(241, 20);
                this.textBox1.TabIndex = 9;
                this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                this.textBox1.TextChanged += new System.EventHandler(this.TextBox1TextChanged);
                // 
                // label7
                // 
                this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                this.label7.Location = new System.Drawing.Point(3, 185);
                this.label7.Name = "label7";
                this.label7.Size = new System.Drawing.Size(461, 2);
                this.label7.TabIndex = 8;
                this.label7.Text = "label7";
                // 
                // label6
                // 
                this.label6.Location = new System.Drawing.Point(105, 191);
                this.label6.Name = "label6";
                this.label6.Size = new System.Drawing.Size(257, 19);
                this.label6.TabIndex = 7;
                this.label6.Text = "Character Appearance URL (for clients that require it)";
                this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                // 
                // button55
                // 
                this.button55.Location = new System.Drawing.Point(116, 94);
                this.button55.Name = "button55";
                this.button55.Size = new System.Drawing.Size(167, 34);
                this.button55.TabIndex = 6;
                this.button55.Text = "Disable Icon/ Enable Custom Icons";
                this.button55.UseVisualStyleBackColor = true;
                this.button55.Click += new System.EventHandler(this.Button55Click);
                // 
                // label5
                // 
                this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label5.Location = new System.Drawing.Point(231, 9);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(56, 19);
                this.label5.TabIndex = 5;
                this.label5.Text = "NBC";
                // 
                // label4
                // 
                this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label4.ForeColor = System.Drawing.Color.Red;
                this.label4.Location = new System.Drawing.Point(73, 125);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(316, 60);
                this.label4.TabIndex = 4;
                this.label4.Text = "NOTE: The icon will only function in a client with a custom scoreboard (I.E 2011E" +
        " or 2011M).";
                this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // 
                // label3
                // 
                this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label3.Location = new System.Drawing.Point(104, 9);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(131, 19);
                this.label3.TabIndex = 3;
                this.label3.Text = "Selected Icon:";
                // 
                // button54
                // 
                this.button54.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.button54.ImageKey = "OBC.png";
                this.button54.ImageList = this.imageList1;
                this.button54.Location = new System.Drawing.Point(231, 31);
                this.button54.Name = "button54";
                this.button54.Size = new System.Drawing.Size(52, 62);
                this.button54.TabIndex = 2;
                this.button54.Text = "OBC";
                this.button54.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                this.button54.UseVisualStyleBackColor = true;
                this.button54.Click += new System.EventHandler(this.Button54Click);
                // 
                // button53
                // 
                this.button53.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.button53.ImageKey = "TBC.png";
                this.button53.ImageList = this.imageList1;
                this.button53.Location = new System.Drawing.Point(174, 31);
                this.button53.Name = "button53";
                this.button53.Size = new System.Drawing.Size(52, 62);
                this.button53.TabIndex = 1;
                this.button53.Text = "TBC";
                this.button53.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                this.button53.UseVisualStyleBackColor = true;
                this.button53.Click += new System.EventHandler(this.Button53Click);
                // 
                // button52
                // 
                this.button52.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.button52.ImageKey = "BC.png";
                this.button52.ImageList = this.imageList1;
                this.button52.Location = new System.Drawing.Point(116, 31);
                this.button52.Name = "button52";
                this.button52.Size = new System.Drawing.Size(52, 62);
                this.button52.TabIndex = 0;
                this.button52.Text = "BC";
                this.button52.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                this.button52.UseVisualStyleBackColor = true;
                this.button52.Click += new System.EventHandler(this.Button52Click);
                // 
                // CharacterCustomization
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.SystemColors.ControlLightLight;
                this.ClientSize = new System.Drawing.Size(477, 272);
                this.Controls.Add(this.tabControl1);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.MaximizeBox = false;
                this.Name = "CharacterCustomization";
                this.Text = "Avatar Customization";
                this.Closing += new System.ComponentModel.CancelEventHandler(this.CharacterCustomizationClose);
                this.Load += new System.EventHandler(this.CharacterCustomizationLoad);
                this.tabControl1.ResumeLayout(false);
                this.tabPage1.ResumeLayout(false);
                this.groupBox3.ResumeLayout(false);
                this.groupBox2.ResumeLayout(false);
                this.groupBox1.ResumeLayout(false);
                this.tabPage2.ResumeLayout(false);
                this.tabControl2.ResumeLayout(false);
                this.tabPage10.ResumeLayout(false);
                this.tabPage10.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
                this.tabPage11.ResumeLayout(false);
                this.tabPage11.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
                this.tabPage12.ResumeLayout(false);
                this.tabPage12.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
                this.tabPage8.ResumeLayout(false);
                this.tabPage8.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
                this.tabPage3.ResumeLayout(false);
                this.tabPage3.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
                this.tabPage4.ResumeLayout(false);
                this.tabPage4.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
                this.tabPage5.ResumeLayout(false);
                this.tabPage5.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
                this.tabPage6.ResumeLayout(false);
                this.tabPage6.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
                this.tabPage9.ResumeLayout(false);
                this.tabPage9.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
                this.tabPage7.ResumeLayout(false);
                this.tabPage7.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
                this.ResumeLayout(false);
#endif
#if RETAIL
            }
#endif
        }
        private System.Windows.Forms.TabPage tabPage12;
		private System.Windows.Forms.TabPage tabPage11;
		private System.Windows.Forms.TabPage tabPage10;
		private TabControlWithoutHeader tabControl2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ListBox listBox9;
		private System.Windows.Forms.PictureBox pictureBox9;
		private System.Windows.Forms.Button button59;
		private System.Windows.Forms.Button button58;
		private System.Windows.Forms.TabPage tabPage9;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ListBox listBox8;
		private System.Windows.Forms.PictureBox pictureBox8;
		private System.Windows.Forms.Button button57;
		private System.Windows.Forms.Button button56;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.Button button52;
		private System.Windows.Forms.Button button53;
		private System.Windows.Forms.Button button54;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button55;
		private System.Windows.Forms.ListBox listBox7;
		private System.Windows.Forms.PictureBox pictureBox7;
		private System.Windows.Forms.Button button51;
		private System.Windows.Forms.Button button50;
		private System.Windows.Forms.ListBox listBox6;
		private System.Windows.Forms.PictureBox pictureBox6;
		private System.Windows.Forms.Button button49;
		private System.Windows.Forms.Button button48;
		private System.Windows.Forms.ListBox listBox5;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.Button button47;
		private System.Windows.Forms.Button button46;
		private System.Windows.Forms.ListBox listBox4;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.Button button45;
		private System.Windows.Forms.Button button44;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Button button43;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button40;
		private System.Windows.Forms.Button button39;
		private System.Windows.Forms.Button button24;
		private System.Windows.Forms.Button button25;
		private System.Windows.Forms.Button button23;
		private System.Windows.Forms.Button button26;
		private System.Windows.Forms.Button button30;
		private System.Windows.Forms.Button button19;
		private System.Windows.Forms.Button button29;
		private System.Windows.Forms.Button button20;
		private System.Windows.Forms.Button button28;
		private System.Windows.Forms.Button button21;
		private System.Windows.Forms.Button button27;
		private System.Windows.Forms.Button button22;
		private System.Windows.Forms.Button button34;
		private System.Windows.Forms.Button button15;
		private System.Windows.Forms.Button button33;
		private System.Windows.Forms.Button button16;
		private System.Windows.Forms.Button button32;
		private System.Windows.Forms.Button button17;
		private System.Windows.Forms.Button button31;
		private System.Windows.Forms.Button button18;
		private System.Windows.Forms.Button button38;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button37;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button36;
		private System.Windows.Forms.Button button13;
		private System.Windows.Forms.Button button35;
		private System.Windows.Forms.Button button14;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button button41;
		private System.Windows.Forms.Button button42;
		private System.Windows.Forms.ListBox listBox3;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private TabControlWithoutHeader tabControl1;
        private System.Windows.Forms.Button button60;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Button button62;
        private System.Windows.Forms.Button button61;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button65;
        private System.Windows.Forms.Button button66;
        private System.Windows.Forms.Button button63;
        private System.Windows.Forms.Button button67;
        private System.Windows.Forms.Button button64;
        private System.Windows.Forms.Button button68;
        private System.Windows.Forms.Button button70;
        private System.Windows.Forms.Button button69;
        private System.Windows.Forms.Button button71;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button73;
        private System.Windows.Forms.Button button72;
        private System.Windows.Forms.Button button78;
        private System.Windows.Forms.Button button77;
        private System.Windows.Forms.Button button76;
        private System.Windows.Forms.Button button75;
        private System.Windows.Forms.Button button74;
        private System.Windows.Forms.Button button80;
        private System.Windows.Forms.Button button79;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button83;
        private System.Windows.Forms.Button button82;
        private System.Windows.Forms.Button button81;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
    }
}
