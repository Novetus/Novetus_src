#region Usings
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using Novetus.Core;
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
			GlobalVars.Proxy.DoSetup();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			GeneratePlayerID();		
		}
		
		void ReadConfigValues(string cfgpath)
		{
			DontShowBox.Checked = !GlobalVars.UserConfiguration.ReadSettingBool("URIQuickConfigure");
			IDBox.Text = GlobalVars.UserConfiguration.ReadSetting("UserID");
            TripcodeLabel.Text = GlobalVars.PlayerTripcode.ToString();
            NameBox.Text = GlobalVars.UserConfiguration.ReadSetting("PlayerName");
		}
		
		void GeneratePlayerID()
		{
			NovetusFuncs.GeneratePlayerID();
			IDBox.Text = GlobalVars.UserConfiguration.ReadSetting("UserID");
		}

        void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.SaveSetting("PlayerName", NameBox.Text);
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			int parsedValue;
			if (int.TryParse(IDBox.Text, out parsedValue))
			{
				if (IDBox.Text.Equals(""))
				{
					GlobalVars.UserConfiguration.SaveSettingInt("UserID", 0);
				}
				else
				{
					GlobalVars.UserConfiguration.SaveSettingInt("UserID", ConvertSafe.ToInt32Safe(IDBox.Text));
				}
			}
			else
			{
				GlobalVars.UserConfiguration.SaveSettingInt("UserID", 0);
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			NovetusFuncs.LaunchCharacterCustomization();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}

		private void DontShowBox_CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.SaveSettingBool("URIQuickConfigure", !DontShowBox.Checked);
		}

		void QuickConfigureClose(object sender, CancelEventArgs e)
		{
			Client.ReadClientValues();
			LocalVars.ReadyToLaunch = true;
		}
        #endregion
    }
    #endregion
}
