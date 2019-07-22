/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 10/31/2018
 * Time: 11:55 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using NovetusShared;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of ItemMaker.
	/// </summary>
	public partial class ItemMaker : Form
	{
		private static string url = "http://www.roblox.com/asset?id=";
		
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
				string version = (numericUpDown1.Value != 0) ? "&version=" + numericUpDown1.Value : "";
				
				System.Diagnostics.Process.Start(url + textBox2.Text + version);
				
				MessageBox.Show("In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, you'll have to download the meshes/textures from the links in the rbxm file, then replace the links in the file pointing to where they are using rbxasset://. Look at the directory in the 'shareddata/charcustom' folder that best suits your item type, then look at the rbxm for any one of the items. If you get a corrupted file, change the URL using the drop down box.\n\nIf you're trying to create a offline item. please use these file extension names when saving your files:\n.rbxm - ROBLOX Model/Item\n.mesh - ROBLOX Mesh\n.png - Texture/Icon\n.wav - Sound","Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch(Exception)
			{
				MessageBox.Show("Error: Unable to download the file. Try using a different file name or ID.","Novetus Item SDK | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.SelectedIndex == 0)
			{
				url = "http://www.roblox.com/asset?id=";
			}
			else if (comboBox1.SelectedIndex == 1)
			{
				url = "http://assetgame.roblox.com/asset/?id=";
			}				
		}
		
		void ItemMakerLoad(object sender, EventArgs e)
		{
			comboBox1.Text = "http://www.roblox.com/";			
		}
	}
}
