using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace NovetusLauncher
{
    public partial class Obj2MeshV1GUI : Form
    {
        private OpenFileDialog openFileDialog1;

        public Obj2MeshV1GUI()
        {
            InitializeComponent();

            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select a .OBJ file",
                Filter = "Wavefront .obj file (*.obj)|*.obj",
                Title = "Open model .obj"
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ProcessOBJ(GlobalVars.ConfigDir + "\\obj2meshv1.exe", openFileDialog1.FileName);
            }
        }

        private void ProcessOBJ(string EXEName, string FileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = EXEName;
            proc.StartInfo.Arguments = FileName;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.EnableRaisingEvents = true;
            proc.Exited += new EventHandler(OBJ2MeshV1Exited);
            proc.Start();
        }

        void OBJ2MeshV1Exited(object sender, EventArgs e)
        {
            string properName = Path.GetFileName(openFileDialog1.FileName) + ".mesh";
            MessageBox.Show("File " + properName + " created!");
        }
    }
}
