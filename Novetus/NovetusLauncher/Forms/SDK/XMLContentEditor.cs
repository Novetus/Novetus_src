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
    public PartColor[] XMLPartColorList;
    public Provider[] contentProviders;
    List<object> loaderList = new List<object>();
    XMLContentType ListType;
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
        SaveXML();
    }

    private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (XMLView.Rows.Count == 0)
        {
            MessageBox.Show("You cannot do this action because no file has been loaded.", "XML Content Editor - File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (!XMLView.Rows[XMLView.CurrentCell.RowIndex].IsNewRow)
        {
            XMLView.Rows.RemoveAt(XMLView.CurrentCell.RowIndex);
        }
    }

    private void insertRowToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (XMLView.Rows.Count == 0)
        {
            MessageBox.Show("You cannot do this action because no file has been loaded.", "XML Content Editor - File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (!XMLView.Rows[XMLView.CurrentCell.RowIndex].IsNewRow)
        {
            XMLView.Rows.Insert(XMLView.CurrentCell.RowIndex, 1);
        }
    }

    private void reloadCurrentFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (XMLView.Rows.Count == 0)
        {
            MessageBox.Show("You cannot do this action because no file has been loaded.", "XML Content Editor - File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        LoadXML(ListType);
    }

    //https://stackoverflow.com/questions/14431936/how-to-force-datagridviewcell-to-end-edit-when-row-header-is-clicked/14498870
    private void XMLView_MouseClick(object sender, MouseEventArgs e)
    {
        if (XMLView.HitTest(e.X, e.Y).Type == DataGridViewHitTestType.RowHeader)
        {
            XMLView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            XMLView.EndEdit();
        }
        else
        {
            XMLView.EditMode = DataGridViewEditMode.EditOnEnter;
        }
    }

    private void XMLContentEditor_OnClosing(object sender, FormClosingEventArgs e)
    {
        DialogResult res = MessageBox.Show("This file may have unsaved changes. Would you like to save?", "XML Content Editor - Save Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
        switch (res)
        {
            case DialogResult.Yes:
                SaveXML(true);
                break;
            case DialogResult.Cancel:
                e.Cancel = true;
                break;
            case DialogResult.No:
            default:
                break;
        }
    }

    //https://stackoverflow.com/questions/24083959/using-linq-to-get-datagridview-row-index-where-first-column-has-specific-value
    private void SearchButton_Click(object sender, EventArgs e)
    {
        string searchValue = SearchBar.Text;
        XMLView.ClearSelection();
        XMLView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        bool valueResult = false;
        try
        {
            int index = 0;
            var item = XMLView.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells[0].Value.ToString().Contains(searchValue, StringComparison.InvariantCultureIgnoreCase));
            if (item != null)
            {
                index = item.Index;
                XMLView.Rows[index].Selected = true;
                XMLView.FirstDisplayedScrollingRowIndex = index;
                valueResult = true;
            }
        }
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
        }
        finally
        {
            if (!valueResult)
            {
                MessageBox.Show("The item, '" + SearchBar.Text + "', was not found. Please try another term.", "XML Content Editor - Item Not Found");
            }
        }
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
                    MessageBox.Show("Cannot load the Content Provider list because the Content Provider XML file does not exist.", "XML Content Editor - Content Provider Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                loaderList.AddRange(contentProviders);
                break;
            case XMLContentType.PartColors:
                if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
                {
                    XMLPartColorList = PartColorLoader.GetPartColors();
                }
                else
                {
                    MessageBox.Show("Cannot load the Part Color list because the Part Color XML file does not exist.", "XML Content Editor - Part Color Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                loaderList.AddRange(XMLPartColorList);
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
                XMLView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                XMLView.Columns[1].Name = "URL";
                XMLView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                XMLView.Columns[2].Name = "Icon File";
                XMLView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                ListType = XMLContentType.ContentProviders;
            }
            else if (loaderList.OfType<PartColor>().Any())
            {
                XMLView.ColumnCount = 3;
                XMLView.Columns[0].Name = "Name";
                XMLView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                XMLView.Columns[1].Name = "ID";
                XMLView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                XMLView.Columns[2].Name = "RGB Value";
                XMLView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
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
                    string[] partColorRow = new string[] { pc.ColorRawName, pc.ColorID.ToString(), pc.ColorRGB };
                    XMLView.Rows.Add(partColorRow);
                }
            }
        }
        else
        {
            MessageBox.Show("Unable to load XML file information because no information exists in the XML file.", "XML Content Editor - File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SaveXML(bool noReload = false)
    {
        XMLView.EndEdit();

        //https://stackoverflow.com/questions/37145086/datagridview-remove-empty-rows-button
        for (int i = XMLView.Rows.Count - 1; i > -1; i--)
        {
            DataGridViewRow row = XMLView.Rows[i];
            if (!row.IsNewRow && row.Cells[0].Value == null)
            {
                XMLView.Rows.RemoveAt(i);
            }
        }

        List<Provider> providerList = new List<Provider>();
        List<PartColor> partColorList = new List<PartColor>();

        foreach (DataGridViewRow data in XMLView.Rows)
        {
            if (data.IsNewRow) continue;

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
                    pc.ColorRawName = data.Cells[0].Value.ToString();
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

        if (!noReload)
        {
            MessageBox.Show(fileName + " has been saved! The list will now reload.", "XML Content Editor - File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadXML(ListType);
        }
        else
        {
            MessageBox.Show(fileName + " has been saved!", "XML Content Editor - File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

