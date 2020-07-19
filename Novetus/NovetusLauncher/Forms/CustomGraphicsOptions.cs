using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NovetusLauncher
{
    public partial class CustomGraphicsOptions : Form
    {
        private int QualityLevel = 0;
        private int MeshDetail = 100;
        private int ShadingQuality = 100;
        private int MaterialQuality = 0;
        private int AA = 0;
        private int AASamples = 1;
        private int Bevels = 0;
        private int Shadows_2008 = 0;
        private bool Shadows_2007 = false;
        private static string ClientName = "";
        private FileFormat.ClientInfo info;

        public CustomGraphicsOptions()
        {
            InitializeComponent();
        }

        private void CustomGraphicsOptions_Load(object sender, EventArgs e)
        {
            ClientName = GlobalVars.UserConfiguration.SelectedClient;
            info = GlobalFuncs.GetClientInfoValues(ClientName);

            string terms = "_" + ClientName;
            string[] dirs = Directory.GetFiles(GlobalPaths.ConfigDirClients);

            foreach (string dir in dirs)
            {
                if (dir.Contains(terms) && !dir.Contains("_default"))
                {
                    string oldfile = "";
                    string fixedfile = "";
                    XDocument doc = null;

                    try
                    {
                        oldfile = File.ReadAllText(dir);
                        fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile));
                        doc = XDocument.Parse(fixedfile);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    //read a few handpicked things to not take up memory

                    try
                    {
                        MeshDetail = Convert.ToInt32(RobloxXML.GetRenderSettings(doc, "maxMeshDetail", XMLTypes.Float));
                        GraphicsMeshQuality.Value = MeshDetail;
                    }
                    catch (Exception)
                    {
                        GraphicsMeshQuality.Enabled = false;
                    }

                    try
                    {
                        ShadingQuality = Convert.ToInt32(RobloxXML.GetRenderSettings(doc, "maxShadingQuality", XMLTypes.Float));
                        GraphicsShadingQuality.Value = ShadingQuality;
                    }
                    catch (Exception)
                    {
                        GraphicsShadingQuality.Enabled = false;
                    }

                    try
                    {
                        MaterialQuality = Convert.ToInt32(RobloxXML.GetRenderSettings(doc, "WoodQuality", XMLTypes.Token));
                        GraphicsMaterialQuality.SelectedIndex = MaterialQuality;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            MaterialQuality = Convert.ToInt32(RobloxXML.GetRenderSettings(doc, "TrussDetail", XMLTypes.Token));
                            GraphicsMaterialQuality.SelectedIndex = MaterialQuality;
                        }
                        catch (Exception)
                        {
                            GraphicsMaterialQuality.Enabled = false;
                        }
                    }

                    try
                    {
                        AA = Convert.ToInt32(RobloxXML.GetRenderSettings(doc, "Antialiasing", XMLTypes.Token));
                        GraphicsAntiAliasing.SelectedIndex = AA;
                    }
                    catch (Exception)
                    {
                        GraphicsAntiAliasing.Enabled = false;
                    }

                    try
                    {
                        AASamples = Convert.ToInt32(RobloxXML.GetRenderSettings(doc, "AASamples", XMLTypes.Token));

                        switch (AASamples)
                        {
                            case 4:
                                GraphicsAASamples.SelectedIndex = 1;
                                break;
                            case 8:
                                GraphicsAASamples.SelectedIndex = 2;
                                break;
                            default:
                                GraphicsAASamples.SelectedIndex = 0;
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        GraphicsAASamples.Enabled = false;
                    }

                    try
                    {
                        Bevels = Convert.ToInt32(RobloxXML.GetRenderSettings(doc, "Bevels", XMLTypes.Token));
                        GraphicsBevels.SelectedIndex = Bevels;
                    }
                    catch (Exception)
                    {
                        GraphicsBevels.Enabled = false;
                    }

                    try
                    {
                        Shadows_2008 = Convert.ToInt32(RobloxXML.GetRenderSettings(doc, "Shadow", XMLTypes.Token));
                        GraphicsShadows2008.SelectedIndex = Shadows_2008;
                    }
                    catch (Exception)
                    {
                        GraphicsShadows2008.Enabled = false;
                    }

                    try
                    {
                        Shadows_2007 = Convert.ToBoolean(RobloxXML.GetRenderSettings(doc, "Shadows", XMLTypes.Bool));
                        GraphicsShadows2007.Checked = Shadows_2007;
                    }
                    catch (Exception)
                    {
                        GraphicsShadows2007.Enabled = false;
                    }

                    try
                    {
                        QualityLevel = Convert.ToInt32(RobloxXML.GetRenderSettings(doc, "QualityLevel", XMLTypes.Token));
                        GraphicsLevel.Value = QualityLevel;
                    }
                    catch (Exception)
                    {
                        GraphicsLevel.Enabled = false;
                    }

                    doc = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        private void GraphicsLevel_ValueChanged(object sender, EventArgs e)
        {
            if (GraphicsLevel.Value > 19 && (info.ClientLoadOptions != Settings.GraphicsOptions.ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21 ||
                info.ClientLoadOptions != Settings.GraphicsOptions.ClientLoadOptions.Client_2008AndUp_QualityLevel21))
            {
                GraphicsLevel.Value = 19;
            }

            QualityLevel = Convert.ToInt32(GraphicsLevel.Value);
        }

        private void GraphicsLevel_Click(object sender, EventArgs e)
        {
            if (GraphicsLevel.Value > 19 && (info.ClientLoadOptions != Settings.GraphicsOptions.ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21 ||
                info.ClientLoadOptions != Settings.GraphicsOptions.ClientLoadOptions.Client_2008AndUp_QualityLevel21))
            {
                MessageBox.Show("This client does not support quality levels above 19.");
            }
        }

        private void GraphicsMeshQuality_ValueChanged(object sender, EventArgs e)
        {
            MeshDetail = Convert.ToInt32(GraphicsMeshQuality.Value);
        }

        private void GraphicsShadingQuality_ValueChanged(object sender, EventArgs e)
        {
            ShadingQuality = Convert.ToInt32(GraphicsShadingQuality.Value);
        }

        private void GraphicsMaterialQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GraphicsMaterialQuality.Enabled)
            {
                GraphicsAntiAliasing.SelectedIndex = 0;
            }

            MaterialQuality = GraphicsMaterialQuality.SelectedIndex;
        }

        private void GraphicsAntiAliasing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GraphicsAntiAliasing.Enabled)
            {
                GraphicsAntiAliasing.SelectedIndex = 0;
            }

            AA = GraphicsAntiAliasing.SelectedIndex;
        }

        private void GraphicsAASamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (GraphicsAASamples.SelectedIndex)
            {
                case 1:
                    AASamples = 4;
                    break;
                case 2:
                    AASamples = 8;
                    break;
                default:
                    AASamples = 1;
                    break;
            }
        }

        private void GraphicsBevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GraphicsBevels.Enabled)
            {
                GraphicsBevels.SelectedIndex = 0;
            }

            Bevels = GraphicsBevels.SelectedIndex;
        }

        private void GraphicsShadows2008_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GraphicsShadows2008.Enabled)
            {
                GraphicsShadows2008.SelectedIndex = 0;
            }

            Shadows_2008 = GraphicsShadows2008.SelectedIndex;
        }

        private void GraphicsShadows2007_CheckedChanged(object sender, EventArgs e)
        {
            if (!GraphicsShadows2007.Enabled)
            {
                GraphicsShadows2007.Checked = false;
            }

            Shadows_2007 = GraphicsShadows2007.Checked;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            GlobalFuncs.ReadClientValues(ClientName, null);
            GlobalFuncs.ApplyClientSettings_custom(info, ClientName, MeshDetail, ShadingQuality, MaterialQuality,
                        AA, AASamples, Bevels, Shadows_2008, Shadows_2007, QualityLevel);
            MessageBox.Show("Custom settings applied!");
            Close();
        }
    }
}
