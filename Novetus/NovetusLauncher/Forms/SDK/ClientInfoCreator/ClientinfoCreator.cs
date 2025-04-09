#region Usings
using System;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Novetus.Core;
using NovetusLauncher.Forms.SDK.ClientInfoCreator;
#endregion

#region Client SDK
public partial class ClientinfoEditor : Form
{
	#region Private Variables
    private FileFormat.ClientInfo SelectedClientInfo = new FileFormat.ClientInfo();
	private string SelectedClientInfoPath = "";
	private bool Locked = false;
	public string RelativePath = "";
	public string curversion = "v3.1";
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
        bool IsVersion3 = false;
		bool LoadingException = false;

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
					cmdargsorclientoptions, commandargsver2, folders,
					usescustomname, customname, script, launchtime,
					revision;

				using (StreamReader reader = new StreamReader(ofd.FileName))
				{
					file = reader.ReadLine();
				}

				string ConvertedLine = "";

				try
				{
					IsVersion2 = true;
					ConvertedLine = SecurityFuncs.Decode(file, false);
				}
				catch (Exception)
				{
					label9.Text = "v1 (v1.1)";
					LoadingException = true;
                    ConvertedLine = SecurityFuncs.Decode(file, true);
				}

				string[] result = ConvertedLine.Split('|');
				usesplayername = SecurityFuncs.Decode(result[0]);
				usesid = SecurityFuncs.Decode(result[1]);
				warning = SecurityFuncs.Decode(result[2]);
				legacymode = SecurityFuncs.Decode(result[3]);
				clientmd5 = SecurityFuncs.Decode(result[4]);
				scriptmd5 = SecurityFuncs.Decode(result[5]);
				desc = SecurityFuncs.Decode(result[6]);
				locked = SecurityFuncs.Decode(result[7]);
				fix2007 = SecurityFuncs.Decode(result[8]);
				alreadyhassecurity = SecurityFuncs.Decode(result[9]);
				cmdargsorclientoptions = SecurityFuncs.Decode(result[10]);
				folders = "False";
				usescustomname = "False";
				customname = "";
				commandargsver2 = "";
				script = "";
				launchtime = "0.05";
				revision = "0";

                try
				{
					if (IsVersion2)
					{
						commandargsver2 = SecurityFuncs.Decode(result[11]);

						bool parsedValue;
						if (bool.TryParse(commandargsver2, out parsedValue))
						{
							folders = SecurityFuncs.Decode(result[11]);
							commandargsver2 = SecurityFuncs.Decode(result[12]);
							bool parsedValue2;
							if (bool.TryParse(commandargsver2, out parsedValue2))
							{
								usescustomname = SecurityFuncs.Decode(result[12]);
								customname = SecurityFuncs.Decode(result[13]);
								commandargsver2 = SecurityFuncs.Decode(result[14]);

								try
								{
                                    script = SecurityFuncs.Decode(result[15]);
                                    launchtime = SecurityFuncs.Decode(result[16]);
                                    
                                    //clearing script md5, we house the script now. 
                                    scriptmd5 = "";
                                    IsVersion3 = true;
                                }
								catch (Exception)
								{
                                    if (!label9.Text.Equals("v1 (v1.1)"))
                                    {
                                        label9.Text = "v2.3 (Last used in Snapshot v24.8790.39939.1)";
                                        LoadingException = true;
                                    }
                                }

								if (IsVersion3)
								{
									try
									{
                                        revision = SecurityFuncs.Decode(result[17]);
                                    }
                                    catch (Exception)
                                    {
                                        if (!label9.Text.Equals("v1 (v1.1)"))
                                        {
                                            label9.Text = "v3 (Last used in Snapshot v25.9216.36080.1)";
                                            LoadingException = true;
                                        }
                                    }
                                }
                            }
							else
                            {
								if (!label9.Text.Equals("v1 (v1.1)"))
								{
									label9.Text = "v2.2 (Last used in v1.3 v11.2021.1)";
                                    LoadingException = true;
                                }
							}
						}
						else
						{
							if (!label9.Text.Equals("v1 (v1.1)"))
							{
								label9.Text = "v2.1 (Last used in v1.3 Pre-Release 5)";
                                LoadingException = true;
                            }
						}
					}
				}
				catch (Exception)
				{
					if (!label9.Text.Equals("v1 (v1.1)"))
					{
						label9.Text = "v2 Alpha (Last used in v1.2 Snapshot 7440)";
						IsVersion2 = false;
                        IsVersion3 = false;
                        LoadingException = true;
                    }
				}

                bool lockcheck = ConvertSafe.ToBooleanSafe(locked);
                if (lockcheck && !GlobalVars.AdminMode)
                {
                    NewClientInfo();
                    MessageBox.Show("This client is locked and therefore it cannot be loaded.", "Novetus Launcher - Error when loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //MessageBox.Show("This client is locked, which means it cannot be loaded in the Client SDK of older versions of Novetus. You cannot turn off the 'lock' setting in the client as a result.", "Novetus Launcher - Error when loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Locked = lockcheck;
                checkBox4.Checked = Locked;
                SelectedClientInfo.UsesPlayerName = ConvertSafe.ToBooleanSafe(usesplayername);
				SelectedClientInfo.UsesID = ConvertSafe.ToBooleanSafe(usesid);
				SelectedClientInfo.Warning = warning;
				SelectedClientInfo.LegacyMode = ConvertSafe.ToBooleanSafe(legacymode);
				SelectedClientInfo.ClientMD5 = clientmd5;
                SelectedClientInfo.ClientInfoRevision = ConvertSafe.ToInt32Safe(revision);
                SelectedClientInfo.ScriptMD5 = scriptmd5;
				SelectedClientInfo.Description = desc;
				SelectedClientInfo.Fix2007 = ConvertSafe.ToBooleanSafe(fix2007);
				SelectedClientInfo.AlreadyHasSecurity = ConvertSafe.ToBooleanSafe(alreadyhassecurity);
				SelectedClientInfo.SeperateFolders = ConvertSafe.ToBooleanSafe(folders);
				SelectedClientInfo.UsesCustomClientEXEName = ConvertSafe.ToBooleanSafe(usescustomname);
				SelectedClientInfo.CustomClientEXEName = customname;
				SelectedClientInfo.LaunchScript = script;
                SelectedClientInfo.ClientLaunchTime = ConvertSafe.ToDoubleSafe(launchtime);

                try
				{
					if (IsVersion2)
					{
						if (cmdargsorclientoptions.Equals("True") || cmdargsorclientoptions.Equals("False"))
						{
							label9.Text = "v2 (Last used in v1.2.3)";
                            LoadingException = true;
                            SelectedClientInfo.ClientLoadOptions = FileFormat.ClientInfo.GetClientLoadOptionsForBool(ConvertSafe.ToBooleanSafe(cmdargsorclientoptions));
						}
						else
						{
                            SelectedClientInfo.ClientLoadOptions = (FileFormat.ClientInfo.ClientLoadOptionsLegacy)ConvertSafe.ToInt32Safe(cmdargsorclientoptions);
						}
						SelectedClientInfo.CommandLineArgs = commandargsver2;
					}
                }
				catch (Exception)
				{
					//Again, fake it.
					SelectedClientInfo.ClientLoadOptions = FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp;
					SelectedClientInfo.CommandLineArgs = cmdargsorclientoptions;
                    LoadingException = true;
                }

				if (!LoadingException)
				{
					label9.Text = curversion + " (v" + GlobalVars.ProgramInformation.Version + ") Revision: " + SelectedClientInfo.ClientInfoRevision;
				}

                SelectedClientInfoPath = Path.GetDirectoryName(ofd.FileName);
			}
		}

		LoadUIElements();
	}

	void SaveToClientToolStripMenuItemClick(object sender, EventArgs e)
	{
		GenerateMD5s();

		if (!string.IsNullOrWhiteSpace(SelectedClientInfoPath))
		{
            SelectedClientInfo.ClientInfoRevision++;

            string[] lines = {
					SecurityFuncs.Encode(SelectedClientInfo.UsesPlayerName.ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.UsesID.ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.Warning.ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.LegacyMode.ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.ClientMD5.ToString()),
					// for compatibility
					SecurityFuncs.Encode("null"),
					SecurityFuncs.Encode(SelectedClientInfo.Description.ToString()),
					SecurityFuncs.Encode(Locked.ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.Fix2007.ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.AlreadyHasSecurity.ToString()),
					SecurityFuncs.Encode(((int)SelectedClientInfo.ClientLoadOptions).ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.SeperateFolders.ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.UsesCustomClientEXEName.ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.CustomClientEXEName.ToString()),
					SecurityFuncs.Encode(SelectedClientInfo.CommandLineArgs.ToString()),
                    SecurityFuncs.Encode(SelectedClientInfo.LaunchScript.ToString()),
                    SecurityFuncs.Encode(SelectedClientInfo.ClientLaunchTime.ToString()),
                    SecurityFuncs.Encode(SelectedClientInfo.ClientInfoRevision.ToString())
                };
			File.WriteAllText(SelectedClientInfoPath + "\\clientinfo.nov", SecurityFuncs.Encode(string.Join("|", lines)));

			label9.Text = curversion + " (v" + GlobalVars.ProgramInformation.Version + ") Revision: " + SelectedClientInfo.ClientInfoRevision;

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
			string filename = "clientinfo";
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
					"null",
					SelectedClientInfo.Description.ToString(),
					Locked.ToString(),
					SelectedClientInfo.Fix2007.ToString(),
					SelectedClientInfo.AlreadyHasSecurity.ToString(),
					((int)SelectedClientInfo.ClientLoadOptions).ToString(),
					SelectedClientInfo.SeperateFolders.ToString(),
					SelectedClientInfo.UsesCustomClientEXEName.ToString(),
					SelectedClientInfo.CustomClientEXEName.ToString(),
					SelectedClientInfo.CommandLineArgs.ToString(),
                    SelectedClientInfo.LaunchScript.ToString(),
                    SelectedClientInfo.ClientLaunchTime.ToString(),
                    SelectedClientInfo.ClientInfoRevision.ToString()
                };
				File.WriteAllLines(sfd.FileName, lines);
			}
		}
	}

    private void jSONToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var sfd = new SaveFileDialog())
        {
            sfd.Filter = "Configuration File (*.json)|*.json";
            sfd.FilterIndex = 1;
            string filename = "clientinfo";
            sfd.FileName = filename;
            sfd.Title = "Save " + filename;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //WRITE
                string section = "ClientInfo";
                JSONFile json = new JSONFile(sfd.FileName, section);
                
                json.JsonWriteValue(section, "UsesPlayerName", SelectedClientInfo.UsesPlayerName.ToString());
                json.JsonWriteValue(section, "UsesID", SelectedClientInfo.UsesID.ToString());
                json.JsonWriteValue(section, "Warning", SelectedClientInfo.Warning.ToString());
                json.JsonWriteValue(section, "LegacyMode", SelectedClientInfo.LegacyMode.ToString());
                json.JsonWriteValue(section, "ClientMD5", SelectedClientInfo.ClientMD5.ToString());
                json.JsonWriteValue(section, "ScriptMD5", "null");
                json.JsonWriteValue(section, "Description", SelectedClientInfo.Description.ToString());
                json.JsonWriteValue(section, "Locked", Locked.ToString());
                json.JsonWriteValue(section, "Fix2007", SelectedClientInfo.Fix2007.ToString());
                json.JsonWriteValue(section, "AlreadyHasSecurity", SelectedClientInfo.AlreadyHasSecurity.ToString());
                json.JsonWriteValue(section, "ClientLoadOptions", ((int)SelectedClientInfo.ClientLoadOptions).ToString());
                json.JsonWriteValue(section, "SeperateFolders", SelectedClientInfo.SeperateFolders.ToString());
                json.JsonWriteValue(section, "UsesCustomClientEXEName", SelectedClientInfo.UsesCustomClientEXEName.ToString());
                json.JsonWriteValue(section, "CustomClientEXEName", SelectedClientInfo.CustomClientEXEName.ToString());
                json.JsonWriteValue(section, "CommandLineArgs", SelectedClientInfo.CommandLineArgs.ToString());
                json.JsonWriteValue(section, "LaunchScript", SelectedClientInfo.LaunchScript.ToString());
                json.JsonWriteValue(section, "ClientLaunchTime", SelectedClientInfo.ClientLaunchTime.ToString());
                json.JsonWriteValue(section, "ClientInfoRevision", SelectedClientInfo.ClientInfoRevision.ToString());
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
        AddClientinfoText("client=");
    }

    private void serverToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddClientinfoText("server=");
    }

    private void soloToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddClientinfoText("solo=");
    }

    private void studioToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddClientinfoText("studio=");
    }

    private void no3dToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddClientinfoText("no3d=");
    }

	private void sharedToolStripMenuItem_Click(object sender, EventArgs e)
	{
		AddClientinfoText("shared=");
	}

	private void validateToolStripMenuItem_Click(object sender, EventArgs e)
	{
		AddClientinfoText("validate=[FILE PATH IN CLIENT DIRECTORY]|[FILE MD5]");
	}

	private void variableToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ToolStripMenuItem senderitem = (ToolStripMenuItem)sender;
        AddClientinfoText(senderitem.Text);
    }

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.ClientLoadOptions = (FileFormat.ClientInfo.ClientLoadOptionsLegacy)comboBox1.SelectedIndex;
		BeginInvoke(new Action(() => { comboBox1.Select(0, 0); }));
	}

	private void checkBox5_CheckedChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.SeperateFolders = checkBox5.Checked;
	}

	private void checkBox8_CheckedChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.UsesCustomClientEXEName = checkBox8.Checked;
		textBox2.Enabled = checkBox8.Checked;
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
		SelectedClientInfo.CustomClientEXEName = textBox2.Text;
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
					text.Add("validate=" + filePathStrip.TrimStart('/', '\\') + "|" + fileMD5);
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

	void signScriptManuallyModernClientsToolStripMenuItem_click(object sender, EventArgs e)
	{
		using (var ofd = new OpenFileDialog())
		{
			ofd.Filter = "Lua Script (*.lua)|*.lua";
			ofd.FilterIndex = 1;
			ofd.Title = "Load Lua Script";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				bool newFormat = false;

				DialogResult res = MessageBox.Show("Would you like to use the newer client signing format featured in newer clients (2014+)?", "Novetus Client SDK - Use New Format?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
				
				if (res == DialogResult.Yes)
                {
					newFormat = true;
				}

				Script.Generator.SignGeneratedScript(ofd.FileName, newFormat);

				MessageBox.Show("Script signed!", "Novetus Client SDK - Script Signed", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}

    private void LoadScriptEditor_Click(object sender, EventArgs e)
    {
        using (var kvl = new ClientInfoCreatorScriptEditor(SelectedClientInfo.LaunchScript))
        {
            if (kvl.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(kvl.scriptText))
                {
					SelectedClientInfo.LaunchScript = kvl.scriptText;
                }
            }
        }
    }
    private void textBox3_TextChanged(object sender, EventArgs e)
    {
        SelectedClientInfo.ClientLaunchTime = ConvertSafe.ToDoubleSafe(textBox3.Text);
    }

    private void exportScriptToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var sfd = new SaveFileDialog())
        {
            sfd.Filter = "Lua Script (*.lua)|*.lua";
            sfd.FilterIndex = 1;
            string filename = "script";
            sfd.FileName = filename;
            sfd.Title = "Export Client Lua Script";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.AppendAllText(sfd.FileName, SelectedClientInfo.LaunchScript);
            }
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
		else if (SelectedClientInfo.UsesCustomClientEXEName)
        {
			ClientName = @"\\" + SelectedClientInfo.CustomClientEXEName;
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
		LoadUIElements();
	}

	void LoadUIElements()
    {
		checkBox1.Checked = SelectedClientInfo.UsesPlayerName;
		checkBox2.Checked = SelectedClientInfo.UsesID;
		checkBox3.Checked = SelectedClientInfo.LegacyMode;
		checkBox5.Checked = SelectedClientInfo.SeperateFolders;
		checkBox6.Checked = SelectedClientInfo.Fix2007;
		checkBox7.Checked = SelectedClientInfo.AlreadyHasSecurity;
		checkBox8.Checked = SelectedClientInfo.UsesCustomClientEXEName;
		if (checkBox8.Checked)
		{
			textBox2.Enabled = true;
			textBox2.Text = SelectedClientInfo.CustomClientEXEName;
		}
		else
		{
			textBox2.Enabled = false;
		}

		comboBox1.SelectedIndex = (int)SelectedClientInfo.ClientLoadOptions;
		textBox1.Text = SelectedClientInfo.Description;
		textBox4.Text = SelectedClientInfo.CommandLineArgs;
		textBox5.Text = SelectedClientInfo.Warning;
        textBox3.Text = SelectedClientInfo.ClientLaunchTime.ToString();
    }
    #endregion
}
#endregion

