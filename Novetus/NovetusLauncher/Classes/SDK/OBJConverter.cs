#region Usings
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Loaders;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#endregion

#region Icon Loader

public class OBJConverter
{
    private OpenFileDialog openFileDialog1;
    private string installOutcome = "";

    public OBJConverter()
    {
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select an .obj file",
            Filter = "Wavefront OBJ file (*.obj)|*.obj",
            Title = "Open .obj"
        };
    }

    public void setInstallOutcome(string text)
    {
        installOutcome = text;
    }

    public string getInstallOutcome()
    {
        return installOutcome;
    }

    public void ConvertOBJ()
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            using (Stream str = openFileDialog1.OpenFile())
            {
                Directory.SetCurrentDirectory(Path.GetDirectoryName(openFileDialog1.FileName));
                var materialStreamProvider = new MaterialStreamProvider();
                var objLoaderFactory = new ObjLoaderFactory();
                var objLoader = objLoaderFactory.Create(materialStreamProvider);
                var fileStream = materialStreamProvider.Open(openFileDialog1.FileName);
                var result = objLoader.Load(fileStream);

                string testString = "";
                testString += "version 1.00\n";
                testString += result.Groups.First().Faces.Count + "\n";
                foreach (Vertex vert in result.Vertices)
                {
                    testString += "[" + (vert.X * 0.5) + "," + (vert.Y * 0.5) + "," + (vert.Z * 0.5) + "]";
                    foreach (Normal norm in result.Normals)
                    {
                        testString += "[" + norm.X + "," + norm.Y + "," + norm.Z + "]";

                        //this is dumb
                        if (result.Textures.Count > 0)
                        {
                            foreach (Texture tex in result.Textures)
                            {
                                testString += "[" + tex.X + "," + tex.Y + ",0]";
                            }
                        }
                        else
                        {
                            testString += "[0,0,0]";
                        }
                    }
                }

                MessageBox.Show(testString);
                Clipboard.SetText(testString);

                Directory.SetCurrentDirectory(GlobalPaths.RootPath);
                str.Close();
            }
        }
    }
}
#endregion