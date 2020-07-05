using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace NovetusLauncher
{
    public partial class AssetLocalizer : Form
    {
        private DLType currentType;
        private string path;
        private string name;
        private string meshname;

        public AssetLocalizer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = (currentType == DLType.RBXL) ? "ROBLOX Level (*.rbxl)|*.rbxl" : "ROBLOX Model (*.rbxm)|*.rbxm",
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
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    currentType = DLType.RBXM;
                    break;
                case 2:
                    currentType = DLType.Hat;
                    break;
                case 3:
                    currentType = DLType.Head;
                    break;
                case 4:
                    currentType = DLType.Face;
                    break;
                case 5:
                    currentType = DLType.Shirt;
                    break;
                case 6:
                    currentType = DLType.TShirt;
                    break;
                case 7:
                    currentType = DLType.Pants;
                    break;
                default:
                    currentType = DLType.RBXL;
                    break;
            }
        }

        private void AssetLocalizer_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "RBXL";
            comboBox2.SelectedItem = "None";

            if (Directory.Exists(GlobalVars.hatdirFonts))
            {
                DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.hatdirFonts);
                FileInfo[] Files = dinfo.GetFiles("*.mesh");
                foreach (FileInfo file in Files)
                {
                    if (file.Name.Equals(String.Empty))
                    {
                        continue;
                    }

                    comboBox2.Items.Add(file.Name);
                }
            }

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
                case DLType.RBXL:
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
                case DLType.RBXM:
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
                case DLType.Hat:
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
                case DLType.Head:
                    //meshes
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Head Meshes and Textures...";
                            break;
                    }
                    break;
                case DLType.Face:
                    //decal
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Face Textures...";
                            break;
                    }
                    break;
                case DLType.TShirt:
                    //texture
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading T-Shirt Textures...";
                            break;
                    }
                    break;
                case DLType.Shirt:
                    //texture
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Shirt Textures...";
                            break;
                    }
                    break;
                case DLType.Pants:
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
                    case DLType.RBXL:
                        //backup the original copy
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxl", " BAK.rbxl"));
                        }
                        catch (Exception)
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
                    case DLType.RBXM:
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
                    case DLType.Hat:
                        //meshes
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemHatFonts, name, meshname);
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
                    case DLType.Head:
                        //meshes
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemHeadFonts, name);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemHeadFonts, 1, 1, 1, 1, name);
                        worker.ReportProgress(100);
                        break;
                    case DLType.Face:
                        //decal
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemFaceTexture, name);
                        worker.ReportProgress(100);
                        break;
                    case DLType.TShirt:
                        //texture
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemTShirtTexture, name);
                        worker.ReportProgress(100);
                        break;
                    case DLType.Shirt:
                        //texture
                        worker.ReportProgress(0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.ItemShirtTexture, name);
                        worker.ReportProgress(100);
                        break;
                    case DLType.Pants:
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
            switch (e)
            {
                case RunWorkerCompletedEventArgs can when can.Cancelled == true:
                    label2.Text = "Canceled!";
                    break;
                case RunWorkerCompletedEventArgs err when err.Error != null:
                    label2.Text = "Error: " + e.Error.Message;
                    break;
                default:
                    label2.Text = "Done!";
                    break;
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "None")
            {
                meshname = "";
            }
            else
            {
                meshname = comboBox2.SelectedItem.ToString();
            }
        }
    }
}
