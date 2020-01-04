using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovetusLauncher
{
    public partial class AssetLocalizer : Form
    {
        private RobloxXMLLocalizer.DLType currentType;
        private string path;
        private string name;

        public AssetLocalizer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = (currentType == RobloxXMLLocalizer.DLType.RBXL) ? "ROBLOX Level (*.rbxl)|*.rbxl" : "ROBLOX Model (*.rbxm)|*.rbxm",
                Title = "Open ROBLOX level or model"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;

                backgroundWorker1.RunWorkerAsync();
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

            if (!Directory.Exists(GlobalVars.AssetCacheDirFonts))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirFonts);
            }

            if (!Directory.Exists(GlobalVars.AssetCacheDirSky))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirSky);
            }

            if (!Directory.Exists(GlobalVars.AssetCacheDirSounds))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirSounds);
            }

            if (!Directory.Exists(GlobalVars.AssetCacheDirTexturesGUI))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirTexturesGUI);
            }

            if (!Directory.Exists(GlobalVars.AssetCacheDirScripts))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirScripts);
            }
        }

        private string GetProgressString(int percent)
        {
            string progressString = "";

            switch (currentType)
            {
                case RobloxXMLLocalizer.DLType.RBXL:
                    switch (percent)
                    {
                        case 0:
                            progressString = "Backing up RBXL...";
                            break;
                        case 5:
                            progressString = "Downloading RBXL Meshes and Textures...";
                            break;
                        case 10:
                            progressString = "Downloading RBXL Skybox Textures...";
                            break;
                        case 15:
                            progressString = "Downloading RBXL Decal Textures...";
                            break;
                        case 20:
                            progressString = "Downloading RBXL Textures...";
                            break;
                        case 25:
                            progressString = "Downloading RBXL Tool Textures...";
                            break;
                        case 30:
                            progressString = "Downloading RBXL HopperBin Textures...";
                            break;
                        case 40:
                            progressString = "Downloading RBXL Sounds...";
                            break;
                        case 50:
                            progressString = "Downloading RBXL GUI Textures...";
                            break;
                        case 60:
                            progressString = "Downloading RBXL Shirt Textures...";
                            break;
                        case 65:
                            progressString = "Downloading RBXL T-Shirt Textures...";
                            break;
                        case 70:
                            progressString = "Downloading RBXL Pants Textures...";
                            break;
                        case 80:
                            progressString = "Downloading RBXL Linked Scripts...";
                            break;
                        case 90:
                            progressString = "Downloading RBXL Linked LocalScripts...";
                            break;
                    }
                    break;
                case RobloxXMLLocalizer.DLType.RBXM:
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading RBXL Meshes and Textures...";
                            break;
                        case 10:
                            progressString = "Downloading RBXL Skybox Textures...";
                            break;
                        case 15:
                            progressString = "Downloading RBXL Decal Textures...";
                            break;
                        case 20:
                            progressString = "Downloading RBXL Textures...";
                            break;
                        case 25:
                            progressString = "Downloading RBXL Tool Textures...";
                            break;
                        case 30:
                            progressString = "Downloading RBXL HopperBin Textures...";
                            break;
                        case 40:
                            progressString = "Downloading RBXL Sounds...";
                            break;
                        case 50:
                            progressString = "Downloading RBXL GUI Textures...";
                            break;
                        case 60:
                            progressString = "Downloading RBXL Shirt Textures...";
                            break;
                        case 65:
                            progressString = "Downloading RBXL T-Shirt Textures...";
                            break;
                        case 70:
                            progressString = "Downloading RBXL Pants Textures...";
                            break;
                        case 80:
                            progressString = "Downloading RBXL Linked Scripts...";
                            break;
                        case 90:
                            progressString = "Downloading RBXL Linked LocalScripts...";
                            break;
                    }
                    break;
                case RobloxXMLLocalizer.DLType.Hat:
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Hat Meshes and Textures...";
                            break;
                        case 25:
                            progressString = "Downloading Hat Sounds...";
                            break;
                        case 50:
                            progressString = "Downloading Hat Linked Scripts...";
                            break;
                        case 75:
                            progressString = "Downloading Hat Linked LocalScripts...";
                            break;
                    }
                    break;
                case RobloxXMLLocalizer.DLType.Head:
                    //meshes
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Head Meshes and Textures...";
                            break;
                    }
                    break;
                case RobloxXMLLocalizer.DLType.Face:
                    //decal
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Face Textures...";
                            break;
                    }
                    break;
                case RobloxXMLLocalizer.DLType.TShirt:
                    //texture
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading T-Shirt Textures...";
                            break;
                    }
                    break;
                case RobloxXMLLocalizer.DLType.Shirt:
                    //texture
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Shirt Textures...";
                            break;
                    }
                    break;
                case RobloxXMLLocalizer.DLType.Pants:
                    //texture
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Pants Textures...";
                            break;
                    }
                    break;
                default:
                    progressString = "Idle";
                    break;
            }

            return progressString + " " + percent.ToString() + "%";
        }

        // This event handler is where the time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                switch (currentType)
                {
                    case RobloxXMLLocalizer.DLType.RBXL:
                        //backup the original copy
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxl", " BAK.rbxl"));
                        }
                        catch (Exception) when (!Env.Debugging)
                        {
                            worker.ReportProgress(100);
                        }
                        //meshes
                        worker.ReportProgress(5);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Fonts);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Fonts, 1, 1, 1, 1);
                        //skybox
                        worker.ReportProgress(10);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 1, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 2, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 3, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 4, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 5, 0, 0, 0);
                        //decal
                        worker.ReportProgress(15);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Decal);
                        //texture
                        worker.ReportProgress(20);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Texture);
                        //tools and hopperbin
                        worker.ReportProgress(25);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Tool);
                        worker.ReportProgress(30);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.HopperBin);
                        //sound
                        worker.ReportProgress(40);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sound);
                        worker.ReportProgress(50);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ImageLabel);
                        //clothing
                        worker.ReportProgress(60);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Shirt);
                        worker.ReportProgress(65);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ShirtGraphic);
                        worker.ReportProgress(70);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Pants);
                        //scripts
                        worker.ReportProgress(80);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Script);
                        worker.ReportProgress(90);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.LocalScript);
                        worker.ReportProgress(100);
                        break;
                    case RobloxXMLLocalizer.DLType.RBXM:
                        //meshes
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Fonts);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Fonts, 1, 1, 1, 1);
                        //skybox
                        worker.ReportProgress(10);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 1, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 2, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 3, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 4, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sky, 5, 0, 0, 0);
                        //decal
                        worker.ReportProgress(15);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Decal);
                        //texture
                        worker.ReportProgress(20);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Texture);
                        //tools and hopperbin
                        worker.ReportProgress(25);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Tool);
                        worker.ReportProgress(30);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.HopperBin);
                        //sound
                        worker.ReportProgress(40);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Sound);
                        worker.ReportProgress(50);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ImageLabel);
                        //clothing
                        worker.ReportProgress(60);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Shirt);
                        worker.ReportProgress(65);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ShirtGraphic);
                        worker.ReportProgress(70);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Pants);
                        //scripts
                        worker.ReportProgress(80);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Script);
                        worker.ReportProgress(90);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.LocalScript);
                        worker.ReportProgress(100);
                        break;
                    case RobloxXMLLocalizer.DLType.Hat:
                        //meshes
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemHatFonts, name);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemHatFonts, 1, 1, 1, 1, name);
                        worker.ReportProgress(25);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemHatSound);
                        //scripts
                        worker.ReportProgress(50);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Script);
                        worker.ReportProgress(75);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.LocalScript);
                        worker.ReportProgress(100);
                        break;
                    case RobloxXMLLocalizer.DLType.Head:
                        //meshes
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemHeadFonts, name);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemHeadFonts, 1, 1, 1, 1, name);
                        worker.ReportProgress(100);
                        break;
                    case RobloxXMLLocalizer.DLType.Face:
                        //decal
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemFaceTexture, name);
                        worker.ReportProgress(100);
                        break;
                    case RobloxXMLLocalizer.DLType.TShirt:
                        //texture
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemTShirtTexture, name);
                        worker.ReportProgress(100);
                        break;
                    case RobloxXMLLocalizer.DLType.Shirt:
                        //texture
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemShirtTexture, name);
                        worker.ReportProgress(100);
                        break;
                    case RobloxXMLLocalizer.DLType.Pants:
                        //texture
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemPantsTexture, name);
                        worker.ReportProgress(100);
                        break;
                    default:
                        worker.ReportProgress(100);
                        break;
                }
            }
            catch (Exception ex) when (!Env.Debugging)
            {
                MessageBox.Show("Error: Unable to localize the asset. " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // This event handler updates the progress.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label2.Text = GetProgressString(e.ProgressPercentage);
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                label2.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                label2.Text = "Error: " + e.Error.Message;
            }
            else
            {
                label2.Text = "Done!";
            }
        }

        void AssetLocalizer_Close(object sender, CancelEventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            name = textBox1.Text;
        }
    }
}
