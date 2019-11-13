using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovetusLauncher
{
    public partial class AssetLocalizer : Form
    {
        private RobloxXMLLocalizer.DLType currentType;

        public AssetLocalizer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RobloxXMLLocalizer.LoadRBXFile(currentType, label2.Text);
            }
            catch (Exception ex) when (!Env.Debugging)
            {
                MessageBox.Show("Error: Unable to localize the asset. " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                currentType = RobloxXMLLocalizer.DLType.RBXL; 
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                currentType = RobloxXMLLocalizer.DLType.RBXM;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                currentType = RobloxXMLLocalizer.DLType.Hat;
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                currentType = RobloxXMLLocalizer.DLType.Head;
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                currentType = RobloxXMLLocalizer.DLType.Face;
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                currentType = RobloxXMLLocalizer.DLType.TShirt;
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                currentType = RobloxXMLLocalizer.DLType.Shirt;
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                currentType = RobloxXMLLocalizer.DLType.Pants;
            }
        }

        private void AssetLocalizer_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "RBXL";

            if (!System.IO.Directory.Exists(GlobalVars.AssetCacheDirFonts))
            {
                System.IO.Directory.CreateDirectory(GlobalVars.AssetCacheDirFonts);
            }

            if (!System.IO.Directory.Exists(GlobalVars.AssetCacheDirSky))
            {
                System.IO.Directory.CreateDirectory(GlobalVars.AssetCacheDirSky);
            }

            if (!System.IO.Directory.Exists(GlobalVars.AssetCacheDirSounds))
            {
                System.IO.Directory.CreateDirectory(GlobalVars.AssetCacheDirSounds);
            }

            if (!System.IO.Directory.Exists(GlobalVars.AssetCacheDirTexturesGUI))
            {
                System.IO.Directory.CreateDirectory(GlobalVars.AssetCacheDirTexturesGUI);
            }
        }
    }
}
