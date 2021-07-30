#region Usings
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
#endregion

namespace NovetusLauncher
{
    #region CustomGraphicsOptions
    public partial class CustomGraphicsOptions : Form
    {
        #region Private Variables
        private int QualityLevel = 0;
        private int MeshDetail = 100;
        private int ShadingQuality = 100;
        private int MaterialQuality = 0;
        private int AA = 0;
        private int AASamples = 1;
        private int Bevels = 0;
        private int Shadows_2008 = 0;
        private bool Shadows_2007 = false;
        private string Style_2007 = "";
        private static string ClientName = "";
        private FileFormat.ClientInfo info;
        #endregion

        #region Constructor
        public CustomGraphicsOptions()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void CustomGraphicsOptions_Load(object sender, EventArgs e)
        {
            ClientName = GlobalVars.UserConfiguration.SelectedClient;
            info = GlobalFuncs.GetClientInfoValues(ClientName);

            string terms = "_" + ClientName;
            bool hasFoundDir = false;
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
                    }
                    catch (Exception)
                    {
                        // try doing march 2007.
                        try
                        {
                            Shadows_2007 = Convert.ToBoolean(RobloxXML.GetRenderSettings(doc, "shadows", XMLTypes.Bool));
                        }
                        catch (Exception)
                        {
                            GraphicsShadows2007.Enabled = false;
                        }
                    }

                    if (GraphicsShadows2007.Enabled)
                    {
                        switch (Shadows_2007)
                        {
                            case false:
                                GraphicsShadows2007.SelectedIndex = 1;
                                break;
                            default:
                                GraphicsShadows2007.SelectedIndex = 0;
                                break;
                        }
                    }

                    try
                    {
                        bool checkSkin = RobloxXML.IsRenderSettingStringValid(doc, "_skinFile", XMLTypes.String);
                        if (checkSkin)
                        {
                            Style_2007 = RobloxXML.GetRenderSettings(doc, "_skinFile", XMLTypes.String).Replace(@"Styles\", "");
                            Style2007.Text = Style_2007;
                        }
                        else
                        {
                            Style2007.Enabled = false;
                            Style2007FolderFinder.Enabled = false;
                        }
                    }
                    catch (Exception)
                    {
                        Style2007.Enabled = false;
                        Style2007FolderFinder.Enabled = false;
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
                    hasFoundDir = true;
                }
            }

            if (!hasFoundDir)
            {
                MessageBox.Show("This client does not support setting adjustment through the Novetus Launcher.\nTry opening this client in ROBLOX Studio and adjust it through the settings in Tools -> Settings.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
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
                MessageBox.Show("This client does not support quality levels above 19.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Style2007_TextChanged(object sender, EventArgs e)
        {
            Style_2007 = Style2007.Text;
        }

        private void Style2007FolderFinder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", GlobalPaths.ClientDir.Replace(@"\\", @"\") + @"\" + ClientName + @"\Styles");
        }

        private void Styles2007Info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Make sure you place the styles you want in the Styles folder in " + GlobalPaths.ClientDir.Replace(@"\\", @"\") + @"\" + ClientName + @"\Styles." + Environment.NewLine + "If the files are not placed in this directory, they will not be loaded properly.\nThis client will accept .msstyles and .cjstyles files.", "Novetus - Styles Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GraphicsShadows2007_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (GraphicsShadows2007.SelectedIndex)
            {
                case 1:
                    Shadows_2007 = false;
                    break;
                default:
                    Shadows_2007 = true;
                    break;
            }
        }

        private void CustomGraphicsOptions_Close(object sender, FormClosingEventArgs e)
        {
            GlobalFuncs.ReadClientValues(ClientName, null);
            GlobalFuncs.ApplyClientSettings_custom(info, ClientName, MeshDetail, ShadingQuality, MaterialQuality,
                        AA, AASamples, Bevels, Shadows_2008, Shadows_2007, Style_2007, QualityLevel);
        }
        #endregion
    }
    #endregion
}
