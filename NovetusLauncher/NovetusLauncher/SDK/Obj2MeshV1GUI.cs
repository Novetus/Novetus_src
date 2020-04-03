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
                ProcessOBJ(GlobalVars.ConfigDirData + "\\RBXMeshConverter.exe", openFileDialog1.FileName);
            }
        }

        private void ProcessOBJ(string EXEName, string FileName)
        {
            label4.Text = "Loading utility...";
            Process proc = new Process();
            proc.StartInfo.FileName = EXEName;
            proc.StartInfo.Arguments = "-f " + FileName + " -v " + numericUpDown1.Value;
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.UseShellExecute = false;
            proc.EnableRaisingEvents = true;
            proc.Exited += new EventHandler(OBJ2MeshV1Exited);
            proc.Start();
            label4.Text = "Converting OBJ to ROBLOX Mesh v" + numericUpDown1.Value + "...";
        }

        void OBJ2MeshV1Exited(object sender, EventArgs e)
        {
            label4.Text = "Ready";
            string properName = Path.GetFileName(openFileDialog1.FileName) + ".mesh";
            MessageBox.Show("File " + properName + " created!");
        }
    }
}
