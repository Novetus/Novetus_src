/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:00 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;

public static class RichTextBoxExtensions
{
	public static void AppendText(this RichTextBox box, string text, Color color)
	{
		box.SelectionStart = box.TextLength;
		box.SelectionLength = 0;

		box.SelectionColor = color;
		box.AppendText(text);
		box.SelectionColor = box.ForeColor;
	}
}
	
public static class ProcessExtensions
{
	public static bool IsRunning(this Process process)
	{
		try {
			Process.GetProcessById(process.Id);
		} catch (InvalidOperationException) {
			return false;
		} catch (ArgumentException) {
			return false;
		}
		return true;
	}
}
	
public static class StringExtensions
{
	public static bool Contains(this string source, string toCheck, StringComparison comp)
	{
		if (source == null)
			return false;
		return source.IndexOf(toCheck, comp) >= 0;
	}
}

public static class ZipArchiveExtensions
{
    public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite)
    {
        if (!overwrite)
        {
            archive.ExtractToDirectory(destinationDirectoryName);
            return;
        }

        DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
        string destinationDirectoryFullPath = di.FullName;

        foreach (ZipArchiveEntry file in archive.Entries)
        {
            string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

            if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
            }

            if (file.Name == "")
            {// Assuming Empty for Directory
                Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                continue;
            }
            file.ExtractToFile(completeFileName, true);
        }
    }
}