#region Usings
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
#endregion

#region Rich Text Box Extensions
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
#endregion

#region Process Extensions
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
#endregion

#region String Extensions
public static class StringExtensions
{
	public static bool Contains(this string source, string toCheck, StringComparison comp)
	{
		if (source == null)
			return false;
		return source.IndexOf(toCheck, comp) >= 0;
	}
}
#endregion

#region Array Helper
//credit to code4life
public static class ArrayHelper
{
    public static object FindInDimensions(this object[,] target,
      object searchTerm)
    {
        object result = null;
        var rowLowerLimit = target.GetLowerBound(0);
        var rowUpperLimit = target.GetUpperBound(0);

        var colLowerLimit = target.GetLowerBound(1);
        var colUpperLimit = target.GetUpperBound(1);

        for (int row = rowLowerLimit; row < rowUpperLimit; row++)
        {
            for (int col = colLowerLimit; col < colUpperLimit; col++)
            {
                // you could do the search here...
            }
        }

        return result;
    }
}
#endregion

#region Substring Extensions
//dotnetperls
static class SubstringExtensions
{
    /// <summary>
    /// Get string value between [first] a and [last] b.
    /// </summary>
    public static string Between(this string value, string a, string b)
    {
        int posA = value.IndexOf(a);
        int posB = value.LastIndexOf(b);
        if (posA == -1)
        {
            return "";
        }
        if (posB == -1)
        {
            return "";
        }
        int adjustedPosA = posA + a.Length;
        if (adjustedPosA >= posB)
        {
            return "";
        }
        return value.Substring(adjustedPosA, posB - adjustedPosA);
    }

    /// <summary>
    /// Get string value after [first] a.
    /// </summary>
    public static string Before(this string value, string a)
    {
        int posA = value.IndexOf(a);
        if (posA == -1)
        {
            return "";
        }
        return value.Substring(0, posA);
    }

    /// <summary>
    /// Get string value after [last] a.
    /// </summary>
    public static string After(this string value, string a)
    {
        int posA = value.LastIndexOf(a);
        if (posA == -1)
        {
            return "";
        }
        int adjustedPosA = posA + a.Length;
        if (adjustedPosA >= value.Length)
        {
            return "";
        }
        return value.Substring(adjustedPosA);
    }
}
#endregion

#region Tab Control without Header
//credit to https://stackoverflow.com/questions/23247941/c-sharp-how-to-remove-tabcontrol-border
public partial class TabControlWithoutHeader : TabControl
{
    int layoutval = 1;

    public TabControlWithoutHeader(int layout)
    {
        SetLayout(layout);
    }
    public TabControlWithoutHeader()
    {
        SetLayout(1);
    }

    private void SetLayout(int layout)
    {
        layoutval = layout;
        if (layoutval == 1)
        {
            if (!DesignMode) Multiline = true;
        }
    }

    protected override void WndProc(ref Message m)
    {
        if (layoutval == 2)
        {
            base.WndProc(ref m);
            return;
        }

        if (m.Msg == 0x1328 && !DesignMode)
            m.Result = new IntPtr(1);
        else
            base.WndProc(ref m);
    }
}
#endregion

#region Form Extensions
//https://stackoverflow.com/questions/12422619/can-i-disable-the-close-button-of-a-form-using-c
public static class FormExt
{
    [DllImport("user32")]
    public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("user32")]
    public static extern bool EnableMenuItem(IntPtr hMenu, uint itemId, uint uEnable);

    public static void DisableCloseButton(this Form form)
    {
        // The 1 parameter means to gray out. 0xF060 is SC_CLOSE.
        EnableMenuItem(GetSystemMenu(form.Handle, false), 0xF060, 1);
    }

    public static void EnableCloseButton(this Form form)
    {
        // The zero parameter means to enable. 0xF060 is SC_CLOSE.
        EnableMenuItem(GetSystemMenu(form.Handle, false), 0xF060, 0);
    }
}
#endregion

#region String Utilities
//https://stackoverflow.com/questions/9031537/really-simple-encryption-with-c-sharp-and-symmetricalgorithm
public static class StringUtil
{
    private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
    private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

    public static string Crypt(this string text)
    {
        SymmetricAlgorithm algorithm = DES.Create();
        ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
        byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
        byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
        return Convert.ToBase64String(outputBuffer);
    }

    public static string Decrypt(this string text)
    {
        SymmetricAlgorithm algorithm = DES.Create();
        ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
        byte[] inputbuffer = Convert.FromBase64String(text);
        byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
        return Encoding.Unicode.GetString(outputBuffer);
    }
}
#endregion