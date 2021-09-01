#region Usings
using System;
using System.Windows.Forms;
using System.ComponentModel;
#endregion

namespace NovetusURI
{
    #region Quick Configuration
    public partial class QuickConfigure : Form
	{
        #region Constructor
        public QuickConfigure()
		{
			InitializeComponent();
		}
        #endregion

        #region Form Events
        void QuickConfigureLoad(object sender, EventArgs e)
		{
			ReadConfigValues(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName);
			CenterToScreen();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			GeneratePlayerID();		
		}
		
		void ReadConfigValues(string cfgpath)
		{
			GlobalFuncs.Config(cfgpath, false);
            textBox2.Text = GlobalVars.UserConfiguration.UserID.ToString();
            label3.Text = GlobalVars.UserConfiguration.PlayerTripcode.ToString();
            textBox1.Text = GlobalVars.UserConfiguration.PlayerName;
		}
		
		void GeneratePlayerID()
		{
			GlobalFuncs.GeneratePlayerID();
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
			switch(GlobalVars.UserConfiguration.LauncherStyle)
            {
				case Settings.Style.Compact:
					CharacterCustomizationCompact ccustom2 = new CharacterCustomizationCompact();
					ccustom2.Show();
					break;
				case Settings.Style.Extended:
				default:
					CharacterCustomizationExtended ccustom = new CharacterCustomizationExtended();
					ccustom.Show();
					break;
			}
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void QuickConfigureClose(object sender, CancelEventArgs e)
		{
    		GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
			GlobalFuncs.ReadClientValues();
			LocalVars.ReadyToLaunch = true;
		}
        #endregion
    }
    #endregion
}
