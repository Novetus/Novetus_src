using NLog;
using System;
using System.Drawing;
using System.Drawing.Text;
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

            GlobalFuncs.ReadInfoFile(LocalPaths.InfoPath, true, LocalPaths.LauncherPath);

            GlobalFuncs.LogPrint("Loading Font...");
            try
            {
                PrivateFontCollection pfc = new PrivateFontCollection();
                string fontPath = LocalPaths.FixedDataDir + "\\BootstrapperFont.ttf";
                pfc.AddFontFile(fontPath);

                foreach (var fam in pfc.Families)
                {
                    VersionLabel.Font = new Font(fam, VersionLabel.Font.Size);
                    LaunchNovetusButton.Font = new Font(fam, VersionLabel.Font.Size);
                }
                GlobalFuncs.LogPrint("Font Loaded");
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
            }

            VersionLabel.Text = GlobalVars.ProgramInformation.Version.ToUpper();
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
