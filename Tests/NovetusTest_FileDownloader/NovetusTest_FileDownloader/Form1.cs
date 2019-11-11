using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovetusTest_FileDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Downloader download = new Downloader(textBox2.Text, textBox1.Text, "Roblox Model (*.rbxm)|*.rbxm|Roblox Mesh (*.mesh)|*.mesh|PNG Image (*.png)|*.png|WAV Sound (*.wav)|*.wav", progressBar1);
            
            try
            {
                download.InitDownload(" This was a test download.");
            }
            catch (Exception)
            {
            }

            if (!string.IsNullOrWhiteSpace(download.getDownloadOutcome()))
            {
                MessageBox.Show(download.getDownloadOutcome());
            }
        }
    }
}
