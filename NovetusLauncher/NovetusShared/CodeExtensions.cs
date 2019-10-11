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
using System.Linq;

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