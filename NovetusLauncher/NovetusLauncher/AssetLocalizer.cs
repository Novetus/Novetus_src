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
                RobloxXMLLocalizer.LoadRBXFile(currentType);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Unable to localize the asset. " + ex.Message, "Novetus Asset Localizer | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                currentType = RobloxXMLLocalizer.DLType.XML; 
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                currentType = RobloxXMLLocalizer.DLType.Hat;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                currentType = RobloxXMLLocalizer.DLType.Head;
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                currentType = RobloxXMLLocalizer.DLType.Face;
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                currentType = RobloxXMLLocalizer.DLType.TShirt;
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                currentType = RobloxXMLLocalizer.DLType.Shirt;
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                currentType = RobloxXMLLocalizer.DLType.Pants;
            }
        }
    }
}
