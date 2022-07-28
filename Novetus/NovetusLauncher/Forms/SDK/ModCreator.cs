using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public partial class ModCreator : Form
{
    public ModCreator()
    {
        InitializeComponent();
    }

    private void ModCreator_Load(object sender, EventArgs e)
    {
        CenterToScreen();
        ListFiles();
    }

    private void SavePackageButton_Click(object sender, EventArgs e)
    {
        AddonFilesListing.Items.Clear();
        ListFiles();
    }

    private void ListFiles()
    {
        if (File.Exists(GlobalPaths.ConfigDir + "\\InitialFileList.txt"))
        {
            Thread t = new Thread(FillFileListing);
            t.Start();
        }
        else
        {
            MessageBox.Show("The initial file list has not been generated. Please launch the Novetus Launcher to initalize it.\n\nNote: Use a fresh Novetus install for this process. Do NOT use a client with mods (Addon scripts, items, maps, etc.) already created, as they won't show up in the file listing. After initalizing a fresh copy of Novetus, you are free to build Mod Packages for it.", 
                "Mod Creator - Initial file list not found.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
    }

    private void FillFileListing()
    {
        string fileLoadString = "Loading files...";
        AddonFilesListing.Items.Add(fileLoadString);
        string[] files = GetUnlistedFiles();

        foreach (string file in files)
        {
            AddonFilesListing.Items.Add(file);
        }

        AddonFilesListing.Items.Remove(fileLoadString);
    }

    private string[] GetUnlistedFiles()
    {
        string filterPath = GlobalPaths.ConfigDir + @"\\" + GlobalPaths.InitialFileListIgnoreFilterName;
        string[] fileListToIgnore = File.ReadAllLines(filterPath);

        string initialFileListPath = GlobalPaths.ConfigDir + "\\InitialFileList.txt";
        string[] initalFileListLines = File.ReadAllLines(initialFileListPath);

        List<string> newArray = new List<string>();

        DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.BasePath);
        FileInfo[] Files = dinfo.GetFiles("*.*", SearchOption.AllDirectories);
        foreach (FileInfo file in Files)
        {
            DirectoryInfo localdinfo = new DirectoryInfo(file.DirectoryName);
            string directory = localdinfo.Name;
            if (!fileListToIgnore.Contains(file.Name, StringComparer.InvariantCultureIgnoreCase) &&
                !fileListToIgnore.Contains(directory, StringComparer.InvariantCultureIgnoreCase) &&
                !initalFileListLines.Contains(file.FullName, StringComparer.InvariantCultureIgnoreCase))
            {
                newArray.Add(file.FullName);
            }
            else
            {
                continue;
            }
        }

        return newArray.ToArray();
    }
}
