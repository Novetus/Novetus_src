#region Usings
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
#endregion

#region Novetus SDK Launcher
public partial class NovetusSDK : Form
{
    #region Constructor
    public NovetusSDK()
    {
        InitializeComponent();
    }
    #endregion

    #region Form Events
    private void NovetusSDK_Load(object sender, EventArgs e)
    {
        Text = "Novetus SDK " + GlobalVars.ProgramInformation.Version;
        label1.Text = GlobalVars.ProgramInformation.Version;
    }

    private void NovetusSDK_Close(object sender, CancelEventArgs e)
    {
        GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LaunchSDKAppByIndex(listBox1.SelectedIndex);
    }
    #endregion

    #region Functions
    public static void LaunchSDKAppByIndex(int index)
    {
        SDKApps selectedApp = SDKFuncs.GetSDKAppForIndex(index);

        switch (selectedApp)
        {
            case SDKApps.ClientSDK:
                ClientinfoEditor cie = new ClientinfoEditor();
                cie.Show();
                break;
            case SDKApps.ClientScriptDoc:
                ClientScriptDocumentation csd = new ClientScriptDocumentation();
                csd.Show();
                break;
            case SDKApps.AssetLocalizer:
                AssetLocalizer al = new AssetLocalizer();
                al.Show();
                break;
            case SDKApps.SplashTester:
                SplashTester st = new SplashTester();
                st.Show();
                break;
            case SDKApps.Obj2MeshV1GUI:
                Obj2MeshV1GUI obj = new Obj2MeshV1GUI();
                obj.Show();
                break;
            case SDKApps.ScriptGenerator:
                Process proc = new Process();
                proc.StartInfo.FileName = GlobalPaths.ConfigDirData + "\\RSG.exe";
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                break;
            case SDKApps.LegacyPlaceConverter:
                Process proc2 = new Process();
                proc2.StartInfo.FileName = GlobalPaths.ConfigDirData + "\\Roblox_Legacy_Place_Converter.exe";
                proc2.StartInfo.CreateNoWindow = false;
                proc2.StartInfo.UseShellExecute = false;
                proc2.Start();
                break;
            case SDKApps.DiogenesEditor:
                DiogenesEditor dio = new DiogenesEditor();
                dio.Show();
                break;
            case SDKApps.ClientScriptTester:
                MessageBox.Show("Note: If you want to test a specific way of loading a client, select the ClientScript Tester in the 'Versions' tab of the Novetus Launcher, then launch it through any way you wish.");
#if LAUNCHER
                GlobalFuncs.LaunchRBXClient("ClientScriptTester", ScriptType.Client, false, false, null, null);
#else
                GlobalFuncs.LaunchRBXClient("ClientScriptTester", ScriptType.Client, false, false, null);
#endif
                break;
            default:
                ItemMaker im = new ItemMaker();
                im.Show();
                break;
        }
    }
    #endregion
}
#endregion
