#region Usings
using System;
using System.Windows.Forms;
#endregion

#region Item SDK
    public partial class ItemMaker : Form
	{
        #region Private Variables
        private string url = "http://www.roblox.com/asset?id=";
		private bool isWebSite = false;
        #endregion

        #region Constructor
        public ItemMaker()
		{
			InitializeComponent();
		}
        #endregion

        #region Form Events
        void Button1Click(object sender, EventArgs e)
		{
			SDKFuncs.StartItemDownload(textBox1.Text, url, textBox2.Text, Convert.ToInt32(numericUpDown1.Value), isWebSite);
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (comboBox1.SelectedIndex)
			{
				case 1:
					url = "http://assetgame.roblox.com/asset/?id=";
					isWebSite = false;
					break;
				case 2:
					url = "https://assetdelivery.roblox.com/v1/asset/?id=";
					isWebSite = false;
					break;
				case 3:
					url = "https://www.roblox.com/catalog/";
					isWebSite = true;
					break;
				case 4:
					url = "https://www.roblox.com/library/";
					isWebSite = true;
					break;
				default:
					url = "http://www.roblox.com/asset?id=";
					isWebSite = false;
					break;
			}
		}
		
		void ItemMakerLoad(object sender, EventArgs e)
		{
            comboBox1.SelectedItem = "http://www.roblox.com/";
			isWebSite = false;

			checkBox1.Checked = GlobalVars.UserConfiguration.DisabledItemMakerHelp;
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.DisabledItemMakerHelp = checkBox1.Checked;
		}
        #endregion
    }
    #endregion
