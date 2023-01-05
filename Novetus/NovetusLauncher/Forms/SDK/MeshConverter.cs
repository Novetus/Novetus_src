#region Usings
using Novetus.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
#endregion

public partial class MeshConverter : Form
{
    #region Private Variables
    //obj2mesh
    private OpenFileDialog MeshConverter_OpenOBJDialog;
    private string output;
    #endregion

    #region Constructor
    public MeshConverter()
    {
        InitializeComponent();

        //meshconverter
        MeshConverter_OpenOBJDialog = new OpenFileDialog()
        {
            FileName = "Select a .OBJ file",
            Filter = "Wavefront .obj file (*.obj)|*.obj",
            Title = "Open model .obj"
        };
    }
    #endregion

    #region Form Events

    #region Load/Close Events
    private void AssetSDK_Load(object sender, EventArgs e)
    {
        //MeshConverter
        MeshConverter_MeshVersionSelector.SelectedItem = "1.00";
    }

    void AssetSDK_Close(object sender, CancelEventArgs e)
    {
    }
    #endregion

    #region Mesh Converter
    private void MeshConverter_ConvertButton_Click(object sender, EventArgs e)
    {
        if (MeshConverter_OpenOBJDialog.ShowDialog() == DialogResult.OK)
        {
            MeshConverter_ProcessOBJ(GlobalPaths.DataDir + "\\ObjToRBXMesh.exe", MeshConverter_OpenOBJDialog.FileName);
        }
    }

    private void MeshConverter_ProcessOBJ(string EXEName, string FileName)
    {
        MeshConverter_StatusText.Text = "Loading utility...";
        Process proc = new Process();
        proc.StartInfo.FileName = EXEName;
        proc.StartInfo.Arguments = "\"" + FileName + "\" " + MeshConverter_MeshVersionSelector.Text;
        proc.StartInfo.CreateNoWindow = false;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.EnableRaisingEvents = true;
        proc.Start();
        MeshConverter_StatusText.Text = "Converting OBJ to Roblox Mesh v" + MeshConverter_MeshVersionSelector.Text + "...";
        output = proc.StandardOutput.ReadToEnd();
        if (proc.HasExited)
        {
            MeshConverter_StatusText.Invoke(new Action(() => { MeshConverter_StatusText.Text = "Ready"; }));
            string properName = Path.GetFileName(MeshConverter_OpenOBJDialog.FileName) + ".mesh";
            string message = "File " + properName + " created!";

            if (output.Contains("ERROR"))
            {
                string small_output = output.Substring(0, output.Length);
                message = "Error when creating file.\nOutput:\n" + small_output;
            }

            MessageBox.Show(message, "Mesh Converter - OBJ File Converted", MessageBoxButtons.OK, (output.Contains("ERROR")) ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }
    }
    #endregion

    #endregion
}
