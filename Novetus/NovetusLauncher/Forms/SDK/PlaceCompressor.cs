#region Usings
using Novetus.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
#endregion

public partial class PlaceCompressor : Form
{
    private bool successful;
    private string currFile;
    public PlaceCompressor()
    {
        InitializeComponent();
        processStatus.Text = "Idle";
        currentFile.Text = "No place selected!";
    }

    private void selectButton_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        {
            ofd.Filter = "Roblox Level (*.rbxl)|*.rbxl|Roblox Level|*.rbxlx";
            ofd.FilterIndex = 1;
            ofd.Title = "Load Roblox Level...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentFile.Text = ofd.SafeFileName;
                currFile = Path.GetFullPath(ofd.FileName);
                selectButton.Enabled = false;
                processStatus.Text = "Compressing...";
                Util.ConsolePrint("Beginning compression of " + ofd.SafeFileName, 3);
                try
                {
                    Util.Compress(currFile);
                    successful = true;
                }
                catch (Exception ex)
                {
                    successful = false;
                    Util.ConsolePrint("Something went wrong while compressing: " + ex.Message, 2);
                    MessageBox.Show("Something went wrong while compressing the Place: \n" + ex.Message, "RBLX Compressor - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
                finally
                {
                    selectButton.Enabled = true;
                    if (!successful) { processStatus.Text = "Error"; }
                    else
                    { 
                        processStatus.Text = "Compression Finished!";
                        Util.ConsolePrint("Compression Finished", 3);
                    }
                }
            }
        }
    }

    private void sourceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("https://github.com/IDeletedSystem64/rblx-compressor");
    }
}

