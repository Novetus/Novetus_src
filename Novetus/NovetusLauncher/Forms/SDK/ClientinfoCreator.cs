#region Usings
using System;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
#endregion

#region Client SDK
public partial class ClientinfoEditor : Form
{
	#region Private Variables
    private FileFormat.ClientInfo SelectedClientInfo = new FileFormat.ClientInfo();
	private string SelectedClientInfoPath = "";
	private bool Locked = false;
	public string RelativePath = "";
	#endregion

	#region Constructor
	public ClientinfoEditor()
	{
		InitializeComponent();
	}
    #endregion

	#region Form Events
    void CheckBox1CheckedChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.UsesPlayerName = checkBox1.Checked;
	}
		
	void CheckBox2CheckedChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.UsesID = checkBox2.Checked;
	}
		
	void TextBox1TextChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.Description = textBox1.Text;
	}
		
	void ClientinfoCreatorLoad(object sender, EventArgs e)
	{
		checkBox4.Visible = GlobalVars.AdminMode;
		NewClientInfo();
	}
		
	void CheckBox3CheckedChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.LegacyMode = checkBox3.Checked;
	}
		
	void CheckBox4CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox4.Checked == true)
		{
			Locked = true;
		}
		else if (checkBox4.Checked == false && Locked == true)
		{
			Locked = true;
		}
	}
		
	void CheckBox6CheckedChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.Fix2007 = checkBox6.Checked;	
	}
		
	void CheckBox7CheckedChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.AlreadyHasSecurity = checkBox7.Checked;
	}

	void NewToolStripMenuItemClick(object sender, EventArgs e)
	{
		label9.Text = "Not Loaded";
		NewClientInfo();
	}
		
	void LoadToolStripMenuItemClick(object sender, EventArgs e)
	{
		bool IsVersion2 = false;

		using (var ofd = new OpenFileDialog())
		{
			ofd.Filter = "Novetus Clientinfo files (*.nov)|*.nov";
			ofd.FilterIndex = 1;
			ofd.FileName = "clientinfo.nov";
			ofd.Title = "Load clientinfo.nov";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				string file, usesplayername, usesid, warning, legacymode, clientmd5,
					scriptmd5, desc, locked, fix2007, alreadyhassecurity,
					cmdargsorclientoptions, commandargsver2, folders;

				using (StreamReader reader = new StreamReader(ofd.FileName))
				{
					file = reader.ReadLine();
				}

				string ConvertedLine = "";

				try
				{
					IsVersion2 = true;
					label9.Text = "v2 (v" + GlobalVars.ProgramInformation.Version + ")";
					ConvertedLine = SecurityFuncs.Base64DecodeNew(file);
				}
				catch (Exception ex)
				{
					GlobalFuncs.LogExceptions(ex);
					label9.Text = "v1 (v1.1)";
					ConvertedLine = SecurityFuncs.Base64DecodeOld(file);
				}

				string[] result = ConvertedLine.Split('|');
				usesplayername = SecurityFuncs.Base64Decode(result[0]);
				usesid = SecurityFuncs.Base64Decode(result[1]);
				warning = SecurityFuncs.Base64Decode(result[2]);
				legacymode = SecurityFuncs.Base64Decode(result[3]);
				clientmd5 = SecurityFuncs.Base64Decode(result[4]);
				scriptmd5 = SecurityFuncs.Base64Decode(result[5]);
				desc = SecurityFuncs.Base64Decode(result[6]);
				locked = SecurityFuncs.Base64Decode(result[7]);
				fix2007 = SecurityFuncs.Base64Decode(result[8]);
				alreadyhassecurity = SecurityFuncs.Base64Decode(result[9]);
				cmdargsorclientoptions = SecurityFuncs.Base64Decode(result[10]);
				folders = "";
				commandargsver2 = "";
				
				try
				{
					if (IsVersion2)
					{
						commandargsver2 = SecurityFuncs.Base64Decode(result[11]);

						bool parsedValue;
						if (bool.TryParse(commandargsver2, out parsedValue))
						{
							folders = SecurityFuncs.Base64Decode(result[11]);
							commandargsver2 = SecurityFuncs.Base64Decode(result[12]);
						}
						else
						{
							folders = "False";

							if (!label9.Text.Equals("v1 (v1.1)"))
							{
								label9.Text = "v2 (v1.3 Pre-Release 5)";
							}
						}
					}
				}
				catch (Exception ex)
				{
					GlobalFuncs.LogExceptions(ex);
					if (!label9.Text.Equals("v1 (v1.1)"))
					{
						label9.Text = "v2 (v1.2 Snapshot 7440)";
						IsVersion2 = false;
					}
				}

				if (!GlobalVars.AdminMode)
				{
					bool lockcheck = Convert.ToBoolean(locked);
					if (lockcheck)
					{
						NewClientInfo();
						MessageBox.Show("This client is locked and therefore it cannot be loaded.", "Novetus Launcher - Error when loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					else
					{
						Locked = lockcheck;
						checkBox4.Checked = Locked;
					}
				}
				else
				{
					Locked = Convert.ToBoolean(locked);
					checkBox4.Checked = Locked;
				}

				SelectedClientInfo.UsesPlayerName = Convert.ToBoolean(usesplayername);
				SelectedClientInfo.UsesID = Convert.ToBoolean(usesid);
				SelectedClientInfo.Warning = warning;
				SelectedClientInfo.LegacyMode = Convert.ToBoolean(legacymode);
				SelectedClientInfo.ClientMD5 = clientmd5;
				SelectedClientInfo.ScriptMD5 = scriptmd5;
				SelectedClientInfo.Description = desc;
				SelectedClientInfo.Fix2007 = Convert.ToBoolean(fix2007);
				SelectedClientInfo.AlreadyHasSecurity = Convert.ToBoolean(alreadyhassecurity);
				SelectedClientInfo.SeperateFolders = Convert.ToBoolean(folders);

				try
				{
					if (IsVersion2)
					{
						if (cmdargsorclientoptions.Equals("True") || cmdargsorclientoptions.Equals("False"))
						{
							label9.Text = "v2 (v1.2.3)";
							SelectedClientInfo.ClientLoadOptions = Settings.GetClientLoadOptionsForBool(Convert.ToBoolean(cmdargsorclientoptions));
						}
						else
						{
							SelectedClientInfo.ClientLoadOptions = (Settings.ClientLoadOptions)Convert.ToInt32(cmdargsorclientoptions);
						}
						SelectedClientInfo.CommandLineArgs = commandargsver2;
					}
				}
				catch (Exception ex)
				{
					GlobalFuncs.LogExceptions(ex);
					//Again, fake it.
					SelectedClientInfo.ClientLoadOptions = Settings.ClientLoadOptions.Client_2008AndUp;
					SelectedClientInfo.CommandLineArgs = cmdargsorclientoptions;
				}

				SelectedClientInfoPath = Path.GetDirectoryName(ofd.FileName);
			}
		}

		checkBox1.Checked = SelectedClientInfo.UsesPlayerName;
		checkBox2.Checked = SelectedClientInfo.UsesID;
		checkBox3.Checked = SelectedClientInfo.LegacyMode;
		checkBox5.Checked = SelectedClientInfo.SeperateFolders;
		checkBox6.Checked = SelectedClientInfo.Fix2007;
		checkBox7.Checked = SelectedClientInfo.AlreadyHasSecurity;

		comboBox1.SelectedIndex = (int)SelectedClientInfo.ClientLoadOptions;
		textBox1.Text = SelectedClientInfo.Description;
		textBox4.Text = SelectedClientInfo.CommandLineArgs;
		textBox5.Text = SelectedClientInfo.Warning;
	}

	void SaveToClientToolStripMenuItemClick(object sender, EventArgs e)
	{
		GenerateMD5s();

		if (!string.IsNullOrWhiteSpace(SelectedClientInfoPath))
		{
			string[] lines = {
					SecurityFuncs.Base64Encode(SelectedClientInfo.UsesPlayerName.ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.UsesID.ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.Warning.ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.LegacyMode.ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.ClientMD5.ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.ScriptMD5.ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.Description.ToString()),
					SecurityFuncs.Base64Encode(Locked.ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.Fix2007.ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.AlreadyHasSecurity.ToString()),
					SecurityFuncs.Base64Encode(((int)SelectedClientInfo.ClientLoadOptions).ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.SeperateFolders.ToString()),
					SecurityFuncs.Base64Encode(SelectedClientInfo.CommandLineArgs.ToString())
				};
			File.WriteAllText(SelectedClientInfoPath + "\\clientinfo.nov", SecurityFuncs.Base64Encode(string.Join("|", lines)));

			label9.Text = "v2 (v" + GlobalVars.ProgramInformation.Version + ")";

			MessageBox.Show(SelectedClientInfoPath + "\\clientinfo.nov saved!", "Novetus Client SDK - Clientinfo Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		else
        {
			MessageBox.Show("This client info file is not saved in your client's directory. Please save it in your client's directory before using.", "Novetus Client SDK - Error when saving to client.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
	}

	private void saveAsTextFileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		using (var sfd = new SaveFileDialog())
		{
			sfd.Filter = "Text file (*.txt)|*.txt";
			sfd.FilterIndex = 1;
			string filename = "clientinfo.txt";
			sfd.FileName = filename;
			sfd.Title = "Save " + filename;

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				string[] lines = {
					SelectedClientInfo.UsesPlayerName.ToString(),
					SelectedClientInfo.UsesID.ToString(),
					SelectedClientInfo.Warning.ToString(),
					SelectedClientInfo.LegacyMode.ToString(),
					SelectedClientInfo.ClientMD5.ToString(),
					SelectedClientInfo.ScriptMD5.ToString(),
					SelectedClientInfo.Description.ToString(),
					Locked.ToString(),
					SelectedClientInfo.Fix2007.ToString(),
					SelectedClientInfo.AlreadyHasSecurity.ToString(),
					((int)SelectedClientInfo.ClientLoadOptions).ToString(),
					SelectedClientInfo.SeperateFolders.ToString(),
					SelectedClientInfo.CommandLineArgs.ToString()
				};
				File.WriteAllLines(sfd.FileName, lines);
			}
		}
	}

	private void saveAsINIFileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		using (var sfd = new SaveFileDialog())
		{
			sfd.Filter = "Configuration File (*.ini)|*.ini";
			sfd.FilterIndex = 1;
			string filename = "clientinfo.ini";
			sfd.FileName = filename;
			sfd.Title = "Save " + filename;

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				//WRITE
				INIFile ini = new INIFile(sfd.FileName);

				string section = "ClientInfo";
				ini.IniWriteValue(section, "UsesPlayerName", SelectedClientInfo.UsesPlayerName.ToString());
				ini.IniWriteValue(section, "UsesID", SelectedClientInfo.UsesID.ToString());
				ini.IniWriteValue(section, "Warning", SelectedClientInfo.Warning.ToString());
				ini.IniWriteValue(section, "LegacyMode", SelectedClientInfo.LegacyMode.ToString());
				ini.IniWriteValue(section, "ClientMD5", SelectedClientInfo.ClientMD5.ToString());
				ini.IniWriteValue(section, "ScriptMD5", SelectedClientInfo.ScriptMD5.ToString());
				ini.IniWriteValue(section, "Description", SelectedClientInfo.Description.ToString());
				ini.IniWriteValue(section, "Locked", Locked.ToString());
				ini.IniWriteValue(section, "Fix2007", SelectedClientInfo.Fix2007.ToString());
				ini.IniWriteValue(section, "AlreadyHasSecurity", SelectedClientInfo.AlreadyHasSecurity.ToString());
				ini.IniWriteValue(section, "ClientLoadOptions", ((int)SelectedClientInfo.ClientLoadOptions).ToString());
				ini.IniWriteValue(section, "SeperateFolders", SelectedClientInfo.SeperateFolders.ToString());
				ini.IniWriteValue(section, "CommandLineArgs", SelectedClientInfo.CommandLineArgs.ToString());
			}
		}
	}

	void TextBox4TextChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.CommandLineArgs = textBox4.Text;		
	}
		
	void TextBox5TextChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.Warning = textBox5.Text;			
	}

    private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClientScriptDocumentation csd = new ClientScriptDocumentation();
        csd.Show();
    }

    //tags
    private void clientToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddClientinfoText("<client></client>");
    }

    private void serverToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddClientinfoText("<server></server>");
    }

    private void soloToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddClientinfoText("<solo></solo>");
    }

    private void studioToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddClientinfoText("<studio></studio>");
    }

    private void no3dToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddClientinfoText("<no3d></no3d>");
    }

	private void sharedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		AddClientinfoText("<shared></shared>");
	}

	private void validateToolStripMenuItem_Click(object sender, EventArgs e)
	{
		AddClientinfoText("<validate>[FILE PATH IN CLIENT DIRECTORY]|[FILE MD5]</validate>");
	}

	private void variableToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ToolStripMenuItem senderitem = (ToolStripMenuItem)sender;
        AddClientinfoText(senderitem.Text);
    }

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.ClientLoadOptions = (Settings.ClientLoadOptions)comboBox1.SelectedIndex;
		BeginInvoke(new Action(() => { comboBox1.Select(0, 0); }));
	}

	private void checkBox5_CheckedChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.SeperateFolders = checkBox5.Checked;
	}

	private void addValidateTagsForRelativePathToolStripMenuItem_click(object sender, EventArgs e)
	{
		ClientinfoCreatorValidatePathForm pathForm = new ClientinfoCreatorValidatePathForm(this);
		pathForm.ShowDialog();

		if (!string.IsNullOrWhiteSpace(SelectedClientInfoPath))
		{
			string fullpath = SelectedClientInfoPath + "\\" + RelativePath;

			if (Directory.Exists(fullpath))
			{
				DirectoryInfo dir = new DirectoryInfo(fullpath);
				FileInfo[] Files = dir.GetFiles("*.*");
				List<string> text = new List<string>();

				foreach (FileInfo file in Files)
				{
					string fileMD5 = SecurityFuncs.GenerateMD5(file.FullName);
					string filePathStrip = file.FullName.Replace(SelectedClientInfoPath, "");
					text.Add("<validate>" + filePathStrip.TrimStart('/', '\\') + "|" + fileMD5 + "</validate>");
				}

				string joined = string.Join("\r\n", text);

				AddClientinfoText(joined.Replace(@"\", "/"));
			}
			else
            {
				MessageBox.Show("The directory does not exist. Please use an existing directory path in your client's folder.", "Novetus Client SDK - Error when adding Validate tags.", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		else
		{
			MessageBox.Show("This client info file is not saved in your client's directory. Please save it in your client's directory before using.", "Novetus Client SDK - Error when adding Validate tags.", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
	#endregion

	#region Functions
	private void GenerateMD5s()
	{
		if (string.IsNullOrWhiteSpace(SelectedClientInfoPath))
		{
			MessageBox.Show("Please choose the folder where you would like to save your clientinfo file.", "Novetus Client SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

			FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				SelectedClientInfoPath = folderBrowserDialog1.SelectedPath;
			}
		}

		string ClientName = "";

		if (SelectedClientInfo.LegacyMode)
		{
			ClientName = "\\RobloxApp.exe";
		}
		else if (SelectedClientInfo.SeperateFolders)
		{
			ClientName = "\\client\\RobloxApp_client.exe";
		}
		else
		{
			ClientName = "\\RobloxApp_client.exe";
		}

		string ClientMD5 = File.Exists(SelectedClientInfoPath + ClientName) ? SecurityFuncs.GenerateMD5(SelectedClientInfoPath + ClientName) : "";

		if (!string.IsNullOrWhiteSpace(ClientMD5))
		{
			SelectedClientInfo.ClientMD5 = ClientMD5.ToUpper(CultureInfo.InvariantCulture);
		}
		else
		{
			MessageBox.Show("Cannot load '" + ClientName.Trim('/') + "'. Please make sure you saved the clientinfo.nov into the client directory and if the file exists.", "Novetus Client SDK - Error while generating MD5 for client", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		string ClientScriptMD5 = File.Exists(SelectedClientInfoPath + "\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") ? SecurityFuncs.GenerateMD5(SelectedClientInfoPath + "\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";

		if (!string.IsNullOrWhiteSpace(ClientScriptMD5))
		{
			SelectedClientInfo.ScriptMD5 = ClientScriptMD5.ToUpper(CultureInfo.InvariantCulture);
		}
		/*else
		{
			MessageBox.Show("Cannot load '" + GlobalPaths.ScriptName + ".lua'. Please make sure you saved the clientinfo.nov into the client directory and if the file exists.", "Novetus Client SDK - Error while generating MD5 for script", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}*/
	}

	private void AddClientinfoText(string text)
	{
		textBox4.Paste(text);
	}
		
	void NewClientInfo()
	{
		label9.Text = "Not Loaded";
		SelectedClientInfo = new FileFormat.ClientInfo();
		Locked = false;
		SelectedClientInfoPath = "";
		checkBox1.Checked = SelectedClientInfo.UsesPlayerName;
		checkBox2.Checked = SelectedClientInfo.UsesID;
		checkBox3.Checked = SelectedClientInfo.LegacyMode;
		checkBox4.Checked = Locked;
		checkBox5.Checked = SelectedClientInfo.SeperateFolders;
		checkBox6.Checked = SelectedClientInfo.Fix2007;
		checkBox7.Checked = SelectedClientInfo.AlreadyHasSecurity;
		comboBox1.SelectedIndex = (int)SelectedClientInfo.ClientLoadOptions;
		textBox1.Text = SelectedClientInfo.Description;
		textBox4.Text = SelectedClientInfo.CommandLineArgs;
		textBox5.Text = SelectedClientInfo.Warning;
	}
    #endregion
}
#endregion

