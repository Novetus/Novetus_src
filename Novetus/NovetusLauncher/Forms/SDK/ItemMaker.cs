#region Usings
using System;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
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
			SDKFuncs.SelectItemDownloadURL(comboBox1.SelectedIndex, url, isWebSite);
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
}
