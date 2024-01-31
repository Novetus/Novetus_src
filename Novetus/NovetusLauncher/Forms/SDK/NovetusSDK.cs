#region Usings
using Novetus.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#endregion

#region SDKApps
enum SDKApps
{
    ClientSDK,
    AssetFixer,
    AssetDownloader,
    MeshConverter,
    ItemCreationSDK,
    ModCreator,
    ClientScriptDoc,
    SplashTester,
    ScriptGenerator,
    LegacyPlaceConverter,
    DiogenesEditor,
    ClientScriptTester,
    XMLContentEditor,
    ClientSDKLegacy
}
#endregion

#region Novetus SDK Launcher
public partial class NovetusSDK : Form
{
    bool IsLauncher;

    #region Constructor
    public NovetusSDK(bool launcher = true)
    {
        IsLauncher = launcher;
        InitializeComponent();
    }
    #endregion

    #region Form Events
    private void NovetusSDK_Load(object sender, EventArgs e)
    {
        if (!File.Exists(GlobalPaths.DataDir + "\\RSG.exe"))
        {
            DisableApp(SDKApps.ScriptGenerator);
        }

        if (!File.Exists(GlobalPaths.DataDir + "\\Roblox_Legacy_Place_Converter.exe"))
        {
            DisableApp(SDKApps.LegacyPlaceConverter);
        }

        if (!Client.IsClientValid("ClientScriptTester"))
        {
            DisableApp(SDKApps.ClientScriptTester);
        }

        Text = "Novetus SDK " + GlobalVars.ProgramInformation.Version;
        label1.Text = GlobalVars.ProgramInformation.Version;
    }

    private void NovetusSDK_Close(object sender, CancelEventArgs e)
    {
        Client.ReadClientValues();
        if (!IsLauncher && !GlobalVars.AppClosed)
        {
            GlobalVars.AppClosed = true;
        }
    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = 0;

        if (listView1.SelectedIndices.Count > 0)
        {
            selectedIndex = listView1.SelectedIndices[0];
        }

        LaunchSDKAppByIndex(selectedIndex);
    }
    #endregion

    #region Functions
    void DisableApp(SDKApps app)
    {
        ListViewItem appItem = listView1.Items[(int)app];
        appItem.Text = appItem.Text + " (Disabled)";
    }

    void LaunchSDKAppByIndex(int index)
    {
        ListViewItem appItem = listView1.Items[index];

        if (appItem.Text.Contains("Disabled"))
        {
            MessageBox.Show("This application has been disabled.", "Novetus SDK - App Disabled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        SDKApps selectedApp = (SDKApps)index;

        switch (selectedApp)
        {
            case SDKApps.ClientSDK:
                ClientinfoEditor csdk = new ClientinfoEditor();
                csdk.Show();
                break;
            case SDKApps.AssetFixer:
                AssetFixer assetF = new AssetFixer();
                assetF.Show();
                break;
            case SDKApps.AssetDownloader:
                AssetDownloader assetD = new AssetDownloader();
                assetD.Show();
                break;
            case SDKApps.MeshConverter:
                MeshConverter mesh = new MeshConverter();
                mesh.Show();
                break;
            case SDKApps.ItemCreationSDK:
                ItemCreationSDK icsdk = new ItemCreationSDK();
                icsdk.Show();
                break;
            case SDKApps.ModCreator:
                ModCreator mod = new ModCreator();
                mod.Show();
                break;
            case SDKApps.ClientScriptDoc:
                ClientScriptDocumentation csd = new ClientScriptDocumentation();
                csd.Show();
                break;
            case SDKApps.SplashTester:
                SplashTester st = new SplashTester();
                st.Show();
                break;
            case SDKApps.ScriptGenerator:
                Process proc = new Process();
                proc.StartInfo.FileName = GlobalPaths.DataDir + "\\RSG.exe";
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                break;
            case SDKApps.LegacyPlaceConverter:
                Process proc2 = new Process();
                proc2.StartInfo.FileName = GlobalPaths.DataDir + "\\Roblox_Legacy_Place_Converter.exe";
                proc2.StartInfo.CreateNoWindow = false;
                proc2.StartInfo.UseShellExecute = false;
                proc2.Start();
                break;
            case SDKApps.DiogenesEditor:
                DiogenesEditor dio = new DiogenesEditor();
                dio.Show();
                break;
            case SDKApps.ClientScriptTester:
                MessageBox.Show("Note: If you want to test a specific way of loading a client, select the ClientScript Tester in the 'Versions' tab of the Novetus Launcher, then launch it through any way you wish.", "Novetus SDK - Client Script Tester Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Client.LaunchRBXClient("ClientScriptTester", ScriptType.Client, false, false, null);
                GlobalVars.GameOpened = ScriptType.None;
                break;
            case SDKApps.XMLContentEditor:
                XMLContentEditor xml = new XMLContentEditor();
                xml.Show();
                break;
            case SDKApps.ClientSDKLegacy:
                ClientinfoEditor cie = new ClientinfoEditor();
                cie.Show();
                break;
            default:
                break;
        }
    }
    #endregion
}
#endregion
