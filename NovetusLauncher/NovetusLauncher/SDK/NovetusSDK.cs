using System;
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
            string[] lines = File.ReadAllLines(GlobalVars.ConfigDir + "\\info.txt"); //File is in System.IO
            GlobalVars.IsSnapshot = Convert.ToBoolean(lines[5]);
            if (GlobalVars.IsSnapshot == true)
            {
                GlobalVars.Version = lines[6].Replace("%version%", lines[0]).Replace("%build%", Assembly.GetExecutingAssembly().GetName().Version.Build.ToString()).Replace("%revision%", Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString());
                string[] changelogedit = File.ReadAllLines(GlobalVars.BasePath + "\\changelog.txt");
                if (!changelogedit[0].Equals(GlobalVars.Version))
                {
                    changelogedit[0] = GlobalVars.Version;
                    File.WriteAllLines(GlobalVars.BasePath + "\\changelog.txt", changelogedit);
                }
            }
            else
            {
                GlobalVars.Version = lines[0];
            }
            Text = "Novetus SDK " + GlobalVars.Version;
            label1.Text = GlobalVars.Version;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                ItemMaker im = new ItemMaker();
                im.Show();
            }
            else if (listBox1.SelectedIndex == 1)
            {
                ClientinfoEditor cie = new ClientinfoEditor();
                cie.Show();
            }
            else if (listBox1.SelectedIndex == 2)
            {
                ClientScriptDocumentation csd = new ClientScriptDocumentation();
                csd.Show();
            }
            else if (listBox1.SelectedIndex == 3)
            {
                AssetLocalizer al = new AssetLocalizer();
                al.Show();
            }
            else if (listBox1.SelectedIndex == 4)
            {
                SplashTester st = new SplashTester();
                st.Show();
            }
            else if (listBox1.SelectedIndex == 5)
            {
                Obj2MeshV1GUI obj = new Obj2MeshV1GUI();
                obj.Show();
            }
            else if (listBox1.SelectedIndex == 6)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = GlobalVars.ConfigDir + "\\RSG_1_4.exe";
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
            }
        }
    }
}
