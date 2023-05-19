using Novetus.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        AuthorBox.Text = GlobalVars.UserConfiguration.ReadSetting("PlayerName");
        CenterToScreen();
        ListFiles();
    }

    private async void SavePackageButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ModNameBox.Text))
        {
            MessageBox.Show("Please specify a mod name.", "Mod Creator - No Mod Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(AuthorBox.Text))
        {
            MessageBox.Show("Please specify the mod's author.", "Mod Creator - No Mod Author", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (ModFilesListing.SelectedItems.Count <= 0)
        {
            MessageBox.Show("Please select the files you wish to include in your mod.", "Mod Creator - No Mod Files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string[] selectedFileList = ModFilesListing.SelectedItems.OfType<string>().ToArray();

        ModManager manager = new ModManager(ModManager.ModMode.ModCreation);
        await manager.CreateModPackage(selectedFileList, 
            ModNameBox.Text,
            AuthorBox.Text,
            DescBox.Text);

        if (!string.IsNullOrWhiteSpace(manager.getOutcome()))
        {
            MessageBoxIcon boxicon = MessageBoxIcon.Information;

            if (manager.getOutcome().Contains("Error"))
            {
                boxicon = MessageBoxIcon.Error;
            }

            MessageBox.Show(manager.getOutcome(), "Mod Creator - Mod Created", MessageBoxButtons.OK, boxicon);
        }

        ModNameBox.Text = "";
        DescBox.Text = "";
        RefreshFiles();
    }

    private void RefreshFileListButton_Click(object sender, EventArgs e)
    {
        RefreshFiles();
    }

    private void RefreshFiles()
    {
        ModFilesListing.Items.Clear();
        ListFiles();
    }

    private void ListFiles()
    {
        if (File.Exists(GlobalPaths.ConfigDir + "\\InitialFileList.txt"))
        {
            Thread t = new Thread(FillFileListing);
            t.IsBackground = true;
            t.Start();
        }
        else
        {
            MessageBox.Show("The initial file list has not been generated. Please launch the Novetus Launcher to initalize it or remove -nofilelist from the command line parameters.\n\nNote: Use a fresh Novetus install for this process. Do NOT use a client with mods (Addon scripts, items, maps, etc.) already created, as they won't show up in the file listing. After initalizing a fresh copy of Novetus, you are free to build Mod Packages for it.", 
                "Mod Creator - Initial file list not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
    }

    private void FillFileListing()
    {
        string fileLoadString = "Loading files...";
        ModFilesListing.Items.Add(fileLoadString);
        string[] files = GetUnlistedFiles();

        foreach (string file in files)
        {
            ModFilesListing.Items.Add(file);
        }

        ModFilesListing.Items.Remove(fileLoadString);
    }

    private string[] GetUnlistedFiles()
    {
        string filterPath = GlobalPaths.ConfigDir + @"\\" + GlobalPaths.InitialFileListIgnoreFilterName;
        string[] fileListToIgnore = File.ReadAllLines(filterPath);

        string initialFileListPath = GlobalPaths.ConfigDir + "\\InitialFileList.txt";
        string[] initalFileListLines = File.ReadAllLines(initialFileListPath);

        List<string> newArray = new List<string>();

        DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.RootPath);
        FileInfo[] Files = dinfo.GetFiles("*.*", SearchOption.AllDirectories);
        foreach (FileInfo file in Files)
        {
            DirectoryInfo localdinfo = new DirectoryInfo(file.DirectoryName);
            string directory = localdinfo.Name;
            if (!fileListToIgnore.Contains(file.Name, StringComparer.InvariantCultureIgnoreCase) &&
                !fileListToIgnore.Contains(directory, StringComparer.InvariantCultureIgnoreCase) &&
                !initalFileListLines.Contains(file.FullName, StringComparer.InvariantCultureIgnoreCase))
            {
                string fixedFileName = file.FullName.Replace(GlobalPaths.RootPath, "");
                newArray.Add(fixedFileName);
            }
            else
            {
                continue;
            }
        }

        return newArray.ToArray();
    }
}
