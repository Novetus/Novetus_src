/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 10/31/2018
 * Time: 11:55 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of ItemMaker.
	/// </summary>
	public partial class ItemMaker : Form
	{
		private static string url = "http://www.roblox.com/asset?id=";
		private static bool isWebSite = false;
		
		public ItemMaker()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			try
			{
				string version = ((numericUpDown1.Value != 0) && (isWebSite != true)) ? "&version=" + numericUpDown1.Value : "";
                string fullURL = url + textBox2.Text + version;

                if (!isWebSite)
                {
                    if (!GlobalVars.DisabledHelp)
                    {
                        string helptext = "If you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - ROBLOX Model/Item\n.mesh - ROBLOX Mesh\n.png - Texture/Icon\n.wav - Sound";
                        MessageBox.Show(helptext, "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Downloader download = new Downloader(fullURL, textBox1.Text, "Roblox Model (*.rbxm)|*.rbxm|Roblox Mesh (*.mesh)|*.mesh|PNG Image (*.png)|*.png|WAV Sound (*.wav)|*.wav");

                    try
                    {
                        string helptext = "In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, you'll have to download the meshes/textures from the links in the rbxm file, then replace the links in the file pointing to where they are using rbxasset://. Look at the directory in the 'shareddata/charcustom' folder that best suits your item type, then look at the rbxm for any one of the items. If you get a corrupted file, change the URL using the drop down box.";
                        download.InitDownload((!GlobalVars.DisabledHelp) ? helptext : "");
                    }
                    catch (Exception ex) when (!Env.Debugging)
                    {
                        MessageBox.Show("Error: Unable to download the file. " +ex.Message, "Novetus Item SDK | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (!string.IsNullOrWhiteSpace(download.getDownloadOutcome()))
                    {
                        MessageBox.Show(download.getDownloadOutcome(), "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    System.Diagnostics.Process.Start(fullURL);

                    if (!GlobalVars.DisabledHelp)
                    {
                        string helptext = "In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, you'll have to download the meshes/textures from the links in the rbxm file, then replace the links in the file pointing to where they are using rbxasset://. Look at the directory in the 'shareddata/charcustom' folder that best suits your item type, then look at the rbxm for any one of the items. If you get a corrupted file, change the URL using the drop down box.\n\nIf you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - ROBLOX Model/Item\n.mesh - ROBLOX Mesh\n.png - Texture/Icon\n.wav - Sound";
                        MessageBox.Show(helptext, "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
			catch(Exception) when (!Env.Debugging)
            {
				MessageBox.Show("Error: Unable to download the file. Try using a different file name or ID.","Novetus Item SDK | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.SelectedIndex == 0)
			{
				url = "http://www.roblox.com/asset?id=";
				isWebSite = false;
			}
			else if (comboBox1.SelectedIndex == 1)
			{
				url = "http://assetgame.roblox.com/asset/?id=";
				isWebSite = false;
			}
			else if (comboBox1.SelectedIndex == 2)
			{
				url = "https://www.roblox.com/catalog/";
				isWebSite = true;
			}
			else if (comboBox1.SelectedIndex == 3)
			{
				url = "https://www.roblox.com/library/";
				isWebSite = true;
			}				
		}
		
		void ItemMakerLoad(object sender, EventArgs e)
		{
            string cfgpath = GlobalVars.ConfigDir + "\\config.ini";
            if (!File.Exists(cfgpath))
            {
                LauncherFuncs.WriteConfigValues(cfgpath);
            }
            else
            {
                LauncherFuncs.ReadConfigValues(cfgpath);
            }

            comboBox1.SelectedItem = "http://www.roblox.com/";
			isWebSite = false;
			
			if (GlobalVars.DisabledHelp == true)
			{
				checkBox1.Checked = true;
			}
			else if (GlobalVars.DisabledHelp == false)
			{
				checkBox1.Checked = false;
			}			
		}
		
		void ItemMakerClose(object sender, CancelEventArgs e)
        {
			LauncherFuncs.WriteConfigValues(GlobalVars.ConfigDir + "\\config.ini");
        }
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked == true)
			{
				GlobalVars.DisabledHelp = true;
			}
			else if (checkBox1.Checked == false)
			{
				GlobalVars.DisabledHelp = false;
			}
		}
	}
}
