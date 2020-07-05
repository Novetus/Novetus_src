/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:04 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System.Text;
using System.Runtime.InteropServices;
using System;

//credit to BLaZiNiX
public class IniFile
{
	public string path;

	[DllImport("kernel32")]
	private static extern long WritePrivateProfileString(string section,
		string key, string val, string filePath);
	[DllImport("kernel32")]
	private static extern int GetPrivateProfileString(string section,
		string key, string def, StringBuilder retVal,
		int size, string filePath);

	/// <summary>
	/// INIFile Constructor.
	/// </summary>
	/// <PARAM name="INIPath"></PARAM>
	public IniFile(string INIPath)
	{
		path = INIPath;
	}
	/// <summary>
	/// Write Data to the INI File
	/// </summary>
	/// <PARAM name="Section"></PARAM>
	/// Section name
	/// <PARAM name="Key"></PARAM>
	/// Key Name
	/// <PARAM name="Value"></PARAM>
	/// Value Name
	public void IniWriteValue(string Section, string Key, string Value)
	{
		WritePrivateProfileString(Section, Key, Value, this.path);
	}
        
	/// <summary>
	/// Read Data Value From the Ini File
	/// </summary>
	/// <PARAM name="Section"></PARAM>
	/// <PARAM name="Key"></PARAM>
	/// <PARAM name="Default Value. Optional for creating values in case they are invalid."></PARAM>
	/// <returns></returns>
	public string IniReadValue(string Section, string Key, string DefaultValue = "")
	{
		try
		{
			StringBuilder temp = new StringBuilder(255);
			int i = GetPrivateProfileString(Section, Key, "", temp,
								  255, this.path);
			return temp.ToString();
		}
		catch (Exception)
		{
			IniWriteValue(Section, Key, DefaultValue);
			return IniReadValue(Section, Key);
        }
	}
}