/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/13/2017
 * Time: 4:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using NovetusLauncher;

namespace NovetusURI
{
	/// <summary>
	/// Description of QuickConfigure.
	/// </summary>
	public partial class QuickConfigure : Form
	{
		public QuickConfigure()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void QuickConfigureLoad(object sender, EventArgs e)
		{
			string cfgpath = Directories.ConfigDir + "\\" + GlobalVars.ConfigName;
			if (!File.Exists(cfgpath))
			{
				LauncherFuncs.Config(cfgpath, true);
			}
			else
			{
				ReadConfigValues(cfgpath);
			}
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			GeneratePlayerID();		
		}
		
		void ReadConfigValues(string cfgpath)
		{
			LauncherFuncs.Config(cfgpath, false);
            textBox2.Text = GlobalVars.UserConfiguration.UserID.ToString();
            label3.Text = GlobalVars.UserConfiguration.PlayerTripcode.ToString();
            textBox1.Text = GlobalVars.UserConfiguration.PlayerName;
		}
		
		void GeneratePlayerID()
		{
			LauncherFuncs.GeneratePlayerID();
			textBox2.Text = GlobalVars.UserConfiguration.UserID.ToString();
		}

        void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.PlayerName = textBox1.Text;
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			int parsedValue;
			if (int.TryParse(textBox2.Text, out parsedValue))
			{
				if (textBox2.Text.Equals(""))
				{
					GlobalVars.UserConfiguration.UserID = 0;
				}
				else
				{
					GlobalVars.UserConfiguration.UserID = Convert.ToInt32(textBox2.Text);
				}
			}
			else
			{
				GlobalVars.UserConfiguration.UserID = 0;
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			CharacterCustomization ccustom = new CharacterCustomization();
			ccustom.Show();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void QuickConfigureClose(object sender, CancelEventArgs e)
		{
    		LauncherFuncs.Config(Directories.ConfigDir + "\\" + GlobalVars.ConfigName, true);
			LocalVars.ReadyToLaunch = true;
		}
	}
}
