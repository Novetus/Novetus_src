using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Novetus.Bootstrapper
{
    public partial class NovetusLaunchForm : Form
    {
        public NovetusLaunchForm()
        {
            InitializeComponent();
        }

        private void NovetusLaunchForm_Load(object sender, EventArgs e)
        {
            //use novetus font for label!!
            //https://stackoverflow.com/questions/1297264/using-custom-fonts-on-a-label-on-winforms

            GlobalFuncs.ReadInfoFile(LocalPaths.InfoPath, true, LocalPaths.LauncherPath);

            PrivateFontCollection pfc = new PrivateFontCollection();
            int fontLength = Properties.Resources.Montserrat_SemiBold.Length;
            byte[] fontdata = Properties.Resources.Montserrat_SemiBold;
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);

            VersionLabel.Font = new Font(pfc.Families[0], VersionLabel.Font.Size);
            VersionLabel.Text = GlobalVars.ProgramInformation.Version.ToUpper();

            LaunchNovetusButton.Font = new Font(pfc.Families[0], VersionLabel.Font.Size);

            CenterToScreen();
        }

        private void LaunchNovetusButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName);
            Close();
        }

        private void LaunchSDKButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName, "-sdk");
            Close();
        }

        private void CMDButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.CMDName, ArgBox.Text);
            Close();
        }

        private void CMDHelpButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.CMDName, "-help");
        }

        private void DependencyInstallerButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplicationExt(GlobalPaths.BasePathLauncher, LocalPaths.DependencyLauncherName);
            Close();
        }

        private void URIButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.URIName);
            Close();
        }
    }
}
