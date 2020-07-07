using NovetusLauncher.SDK;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NovetusLauncher
{
    public partial class NovetusSDK : Form
    {
        public NovetusSDK()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItemMaker im = new ItemMaker();
            im.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientinfoEditor cie = new ClientinfoEditor();
            cie.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientScriptDocumentation csd = new ClientScriptDocumentation();
            csd.Show();
        }

        private void NovetusSDK_Load(object sender, EventArgs e)
        {
            Text = "Novetus SDK " + GlobalVars.ProgramInformation.Version;
            label1.Text = GlobalVars.ProgramInformation.Version;
        }

        private void NovetusSDK_Close(object sender, CancelEventArgs e)
        {
            LauncherFuncs.Config(Directories.ConfigDir + "\\" + GlobalVars.ConfigName, true);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 1:
                    ClientinfoEditor cie = new ClientinfoEditor();
                    cie.Show();
                    break;
                case 2:
                    ClientScriptDocumentation csd = new ClientScriptDocumentation();
                    csd.Show();
                    break;
                case 3:
                    AssetLocalizer al = new AssetLocalizer();
                    al.Show();
                    break;
                case 4:
                    SplashTester st = new SplashTester();
                    st.Show();
                    break;
                case 5:
                    Obj2MeshV1GUI obj = new Obj2MeshV1GUI();
                    obj.Show();
                    break;
                case 6:
                    Process proc = new Process();
                    proc.StartInfo.FileName = Directories.ConfigDirData + "\\RSG.exe";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                    break;
                case 7:
                    Process proc2 = new Process();
                    proc2.StartInfo.FileName = Directories.ConfigDirData + "\\Roblox_Legacy_Place_Converter.exe";
                    proc2.StartInfo.CreateNoWindow = false;
                    proc2.StartInfo.UseShellExecute = false;
                    proc2.Start();
                    break;
                case 8:
                    DiogenesEditor dio = new DiogenesEditor();
                    dio.Show();
                    break;
                default:
                    ItemMaker im = new ItemMaker();
                    im.Show();
                    break;
            }
        }
    }
}
