#region Usings
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region Asset Localizer
    public partial class AssetLocalizer : Form
    {
        #region Private Variables
        private RobloxFileType currentType;
        private string path;
        private string name;
        private string meshname;
        #endregion

        #region Constructor
        public AssetLocalizer()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog robloxFileDialog = SDKFuncs.LoadROBLOXFileDialog(currentType);

            if (robloxFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = robloxFileDialog.FileName;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentType = SDKFuncs.SelectROBLOXFileType(comboBox1.SelectedIndex);
        }

        private void AssetLocalizer_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = GlobalVars.UserConfiguration.AssetLocalizerSaveBackups;
            comboBox1.SelectedItem = "RBXL";
            comboBox2.SelectedItem = "None";

            if (Directory.Exists(LocalPaths.hatdirFonts))
            {
                DirectoryInfo dinfo = new DirectoryInfo(LocalPaths.hatdirFonts);
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

            LauncherFuncs.CreateAssetCacheDirectories();
        }

        // This event handler is where the time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            SDKFuncs.LocalizeAsset(currentType, worker, path, name, meshname);
        }

        // This event handler updates the progress.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label2.Text = SDKFuncs.GetProgressString(currentType, e.ProgressPercentage);
            progressBar1.Value = e.ProgressPercentage;
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (e)
            {
                case RunWorkerCompletedEventArgs can when can.Cancelled:
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           GlobalVars.UserConfiguration.AssetLocalizerSaveBackups = checkBox1.Checked;
        }
        #endregion
    }
    #endregion
}
