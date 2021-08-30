#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
#endregion

#region XMLContentType
enum XMLContentType
{
    ContentProviders,
    PartColors
}
#endregion

#region XML Content Editor
public partial class XMLContentEditor : Form
{
    #region Private Variables
    public PartColor[] PartColorList;
    public Provider[] contentProviders;
    List<object> loaderList = new List<object>();
    XMLContentType ListType;
    BindingSource XMLDataBinding;
    #endregion

    #region Constructor
    public XMLContentEditor()
    {
        InitializeComponent();
    }
    #endregion

    #region Form Events
    private void contentProvidersToolStripMenuItem_Click(object sender, EventArgs e)
    {
        LoadXML(XMLContentType.ContentProviders);
    }

    private void partColorsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        LoadXML(XMLContentType.PartColors);
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        List<Provider> providerList = new List<Provider>();
        List<PartColor> partColorList = new List<PartColor>();

        foreach (DataGridViewRow data in XMLView.Rows)
        {
            if (data.IsNewRow) continue;

            //https://stackoverflow.com/questions/8255186/how-to-check-empty-and-null-cells-in-datagridview-using-c-sharp
            for (int i = 0; i < data.Cells.Count; i++)
            {
                if (data.Cells[i].Value == null || data.Cells[i].Value == DBNull.Value || string.IsNullOrWhiteSpace(data.Cells[i].Value.ToString()))
                {
                    continue;
                }
            }

            switch (ListType)
            {
                case XMLContentType.ContentProviders:
                    Provider pro = new Provider();
                    pro.Name = data.Cells[0].Value.ToString();
                    pro.URL = data.Cells[1].Value.ToString();
                    pro.Icon = data.Cells[2].Value.ToString();
                    providerList.Add(pro);
                    break;
                case XMLContentType.PartColors:
                    PartColor pc = new PartColor();
                    pc.ColorName = data.Cells[0].Value.ToString();
                    pc.ColorID = Convert.ToInt32(data.Cells[1].Value);
                    pc.ColorRGB = data.Cells[2].Value.ToString();
                    partColorList.Add(pc);
                    break;
                default:
                    break;
            }
        }

        //https://stackoverflow.com/questions/2129414/how-to-insert-xml-comments-in-xml-serialization
        switch (ListType)
        {
            case XMLContentType.ContentProviders:
                ContentProviders providers = new ContentProviders();
                providers.Providers = providerList.ToArray();

                XmlSerializer ser = new XmlSerializer(typeof(ContentProviders));

                using (FileStream fs = new FileStream(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName, FileMode.Create))
                {
                    XmlWriter writer = XmlWriter.Create(fs, new XmlWriterSettings { Indent = true });
                    writer.WriteStartDocument();
                    writer.WriteComment(GenerateComment("content providers"));
                    ser.Serialize(writer, providers);
                    writer.WriteEndDocument();
                    writer.Flush();
                }
                break;
            case XMLContentType.PartColors:
                PartColors partColors = new PartColors();
                partColors.ColorList = partColorList.ToArray();

                XmlSerializer ser2 = new XmlSerializer(typeof(PartColors));

                using (FileStream fs = new FileStream(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName, FileMode.Create))
                {
                    XmlWriter writer = XmlWriter.Create(fs, new XmlWriterSettings { Indent = true });
                    writer.WriteStartDocument();
                    writer.WriteComment(GenerateComment("part colors"));
                    ser2.Serialize(writer, partColors);
                    writer.WriteEndDocument();
                    writer.Flush();
                }
                break;
            default:
                break;
        }

        providerList.Clear();
        partColorList.Clear();

        string fileName = "";
        switch (ListType)
        {
            case XMLContentType.ContentProviders:
                fileName = GlobalPaths.ContentProviderXMLName;
                break;
            case XMLContentType.PartColors:
                fileName = GlobalPaths.PartColorXMLName;
                break;
            default:
                break;
        }

        MessageBox.Show(fileName + " has been saved! The list will now reload.", "XML Content Editor - File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadXML(ListType);
    }
    #endregion

    #region Functions
    private void LoadXML(XMLContentType type)
    {
        loaderList.Clear();

        switch (type)
        {
            case XMLContentType.ContentProviders:
                if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
                {
                    contentProviders = OnlineClothing.GetContentProviders();
                }
                else
                {
                    MessageBox.Show("Cannot load the Content Provider list because the Content Provider XML file does not exist", "XML Content Editor - Content Provider Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                loaderList.AddRange(contentProviders);
                break;
            case XMLContentType.PartColors:
                if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
                {
                    PartColorList = PartColorLoader.GetPartColors();
                }
                else
                {
                    MessageBox.Show("Cannot load the Part Color list because the Part Color XML file does not exist", "XML Content Editor - Part Color Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                loaderList.AddRange(PartColorList);
                break;
            default:
                break;
        }

        XMLView.Rows.Clear();
        XMLView.Columns.Clear();

        if (loaderList.Count > 0)
        {
            if (loaderList.OfType<Provider>().Any())
            {
                XMLView.ColumnCount = 3;
                XMLView.Columns[0].Name = "Name";
                XMLView.Columns[1].Name = "URL";
                XMLView.Columns[2].Name = "Icon File";
                ListType = XMLContentType.ContentProviders;
            }
            else if (loaderList.OfType<PartColor>().Any())
            {
                XMLView.ColumnCount = 3;
                XMLView.Columns[0].Name = "Name";
                XMLView.Columns[1].Name = "ID";
                XMLView.Columns[2].Name = "RGB Value";
                ListType = XMLContentType.PartColors;
            }

            foreach (var obj in loaderList)
            {
                if (obj is Provider)
                {
                    Provider pro = obj as Provider;
                    string[] providerRow = new string[] { pro.Name, pro.URL, pro.Icon };
                    XMLView.Rows.Add(providerRow);
                }
                else if (obj is PartColor)
                {
                    PartColor pc = obj as PartColor;
                    string[] partColorRow = new string[] { pc.ColorName, pc.ColorID.ToString(), pc.ColorRGB };
                    XMLView.Rows.Add(partColorRow);
                }
            }
        }
        else
        {
            MessageBox.Show("Unable to load XML file information because no information exists in the XML file.", "XML Content Editor - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    //this is completely fucking dumb.
    private string GenerateComment(string add)
    {
        return "Novetus reads through this file in order to grab " + add + " for the Avatar Customization.";
    }
    #endregion
}
#endregion