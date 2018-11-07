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

namespace NovetusLauncher
{
	/// <summary>
	/// Description of ItemMaker.
	/// </summary>
	public partial class ItemMaker : Form
	{
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
			WebClient Client = new WebClient();
			string version = (numericUpDown1.Value != 0) ? "&version=" + numericUpDown1.Value : "";
			Client.DownloadFile("http://www.roblox.com/asset?id=" + textBox2.Text + version, GlobalVars.BasePath + "\\" + textBox1.Text + ".rbxm");
			MessageBox.Show("Item downloaded into your Novetus directory! In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, you'll have to download the meshes/textures from the links in the rbxm file, then replace the links in the file pointing to where they are using rbxasset://. Look at the directory in the 'charcustom' folder that best suits your item type, then look at the rbxm for any one of the items.","Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
