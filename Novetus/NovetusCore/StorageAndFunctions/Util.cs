#region Usings
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using NLog;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
#endregion

#region Utils

//This code was brought to you by:
//https://stackoverflow.com/questions/1926264/color-different-parts-of-a-richtextbox-string
//https://stackoverflow.com/questions/262280/how-can-i-know-if-a-process-is-running
//https://stackoverflow.com/questions/444798/case-insensitive-containsstring
//https://stackoverflow.com/questions/6084940/how-do-i-search-a-multi-dimensional-array
//https://www.dotnetperls.com/between-before-after
//https://stackoverflow.com/questions/12422619/can-i-disable-the-close-button-of-a-form-using-c
//https://stackoverflow.com/questions/9031537/really-simple-encryption-with-c-sharp-and-symmetricalgorithm

public static class Util
{
    #region Rich Text Box Extensions
    public static void AppendText(this RichTextBox box, string text, Color color)
    {
        box.SelectionStart = box.TextLength;
        box.SelectionLength = 0;

        box.SelectionColor = color;
        box.AppendText(text);
        box.SelectionColor = box.ForeColor;
    }
    #endregion

    #region Process Extensions
    public static bool IsRunning(this Process process)
    {
        try
        {
            Process.GetProcessById(process.Id);
        }
        catch (InvalidOperationException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }
        return true;
    }
    #endregion

    #region String Extensions
    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        if (source == null)
            return false;
        return source.IndexOf(toCheck, comp) >= 0;
    }
    #endregion

    #region Substring Extensions
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
    #endregion

    #region String Utilities
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
    #endregion

    #region Exception Helpers
    //https://github.com/AlexMelw/EasySharp/blob/master/NHelpers/ExceptionsDealing/Extensions/ExceptionExtensions.cs
    /// <summary>
    ///     Gets the entire stack trace consisting of exception's footprints (File, Method, LineNumber)
    /// </summary>
    /// <param name="exception">Source <see cref="Exception" /></param>
    /// <returns>
    ///     <see cref="string" /> that represents the entire stack trace consisting of exception's footprints (File,
    ///     Method, LineNumber)
    /// </returns>
    public static string GetExceptionFootprints(this Exception exception)
    {
        StackTrace stackTrace = new StackTrace(exception, true);
        StackFrame[] frames = stackTrace.GetFrames();

        if (ReferenceEquals(frames, null))
        {
            return string.Empty;
        }

        var traceStringBuilder = new StringBuilder();

        for (var i = 0; i < frames.Length; i++)
        {
            StackFrame frame = frames[i];

            if (frame.GetFileLineNumber() < 1)
                continue;

            traceStringBuilder.AppendLine($"File: {frame.GetFileName()}");
            traceStringBuilder.AppendLine($"Method: {frame.GetMethod().Name}");
            traceStringBuilder.AppendLine($"LineNumber: {frame.GetFileLineNumber()}");

            if (i == frames.Length - 1)
                break;

            traceStringBuilder.AppendLine(" ---> ");
        }

        string stackTraceFootprints = traceStringBuilder.ToString();

        if (string.IsNullOrWhiteSpace(stackTraceFootprints))
            return "NO DETECTED FOOTPRINTS";

        return stackTraceFootprints;
    }
    #endregion

    #region DirectoryInfo Extensions
    public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
    {
        if (extensions == null)
            throw new ArgumentNullException("extensions");
        IEnumerable<FileInfo> files = dir.EnumerateFiles();
        return files.Where(f => extensions.Contains(f.Extension));
    }
    #endregion

    #region DateTime Extensions
    //https://stackoverflow.com/questions/5672862/check-if-datetime-instance-falls-in-between-other-two-datetime-objects
    public static bool IsBetweenTwoDates(this DateTime dt, DateTime start, DateTime end)
    {
        return dt >= start && dt <= end;
    }
    #endregion

    #region Form Extensions
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
    #endregion

    #region Utility Functions
    private static DialogResult ShowOverrideWarning(string dest)
    {
        DialogResult box = MessageBox.Show("A file with a similar name was detected in the directory as '" + dest +
                        "'.\n\nWould you like to override it?", "Novetus - Override Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        return box;
    }

    public static void FixedFileCopy(string src, string dest, bool overwrite, bool overwritewarning = false)
    {
        if (File.Exists(dest))
        {
            if (overwrite && overwritewarning)
            {
                if (ShowOverrideWarning(dest) == DialogResult.No)
                {
                    return;
                }
            }

            File.SetAttributes(dest, FileAttributes.Normal);
        }

        File.Copy(src, dest, overwrite);
        File.SetAttributes(dest, FileAttributes.Normal);
    }

    public static void FixedFileDelete(string src)
    {
        if (File.Exists(src))
        {
            File.SetAttributes(src, FileAttributes.Normal);
            File.Delete(src);
        }
    }

    public static void FixedFileMove(string src, string dest, bool overwrite, bool overwritewarning = false)
    {
        if (src.Equals(dest))
            return;

        if (!File.Exists(dest))
        {
            File.SetAttributes(src, FileAttributes.Normal);
            File.Move(src, dest);
        }
        else
        {
            if (overwrite)
            {
                if (overwritewarning)
                {
                    if (ShowOverrideWarning(dest) == DialogResult.No)
                    {
                        return;
                    }
                }

                FixedFileDelete(dest);
                File.SetAttributes(src, FileAttributes.Normal);
                File.Move(src, dest);
            }
            else
            {
                throw new IOException("Cannot create a file when that file already exists. FixedFileMove cannot override files with overwrite disabled.");
            }
        }
    }

    //modified from the following:
    //https://stackoverflow.com/questions/28887314/performance-of-image-loading
    //https://stackoverflow.com/questions/2479771/c-why-am-i-getting-the-process-cannot-access-the-file-because-it-is-being-u
    public static Image LoadImage(string fileFullName, string fallbackFileFullName = "")
    {
        if (string.IsNullOrWhiteSpace(fileFullName))
            return null;

        Image image = null;

        try
        {
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(fileFullName)))
            {
                image = Image.FromStream(ms);
            }

            // PropertyItems seem to get lost when fileStream is closed to quickly (?); perhaps
            // this is the reason Microsoft didn't want to close it in the first place.
            PropertyItem[] items = image.PropertyItems;

            foreach (PropertyItem item in items)
            {
                image.SetPropertyItem(item);
            }
        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
            if (!string.IsNullOrWhiteSpace(fallbackFileFullName))
                image = LoadImage(fallbackFileFullName);
        }

        return image;
    }

    //https://social.msdn.microsoft.com/Forums/vstudio/en-US/b0c31115-f6f0-4de5-a62d-d766a855d4d1/directorygetfiles-with-searchpattern-to-get-all-dll-and-exe-files-in-one-call?forum=netfxbcl
    public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
    {
        string[] searchPatterns = searchPattern.Split('|');
        List<string> files = new List<string>();
        foreach (string sp in searchPatterns)
            files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
        files.Sort();
        return files.ToArray();
    }

    // Credit to Carrot for the original code. Rewote it to be smaller.
    public static string CryptStringWithByte(string word)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(word);
        string result = "";
        for (int i = 0; i < bytes.Length; i++) { result += Convert.ToChar(0x55 ^ bytes[i]); }
        return result;
    }

    //https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    //https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
    private static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
    public static string SizeSuffix(Int64 value, int decimalPlaces = 1)
    {
        if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
        if (value < 0) { return "-" + SizeSuffix(-value, decimalPlaces); }
        if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

        // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
        int mag = (int)Math.Log(value, 1024);

        // 1L << (mag * 10) == 2 ^ (10 * mag) 
        // [i.e. the number of bytes in the unit corresponding to mag]
        decimal adjustedSize = (decimal)value / (1L << (mag * 10));

        // make adjustment when the value is large enough that
        // it would round up to 1000 or more
        if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
        {
            mag += 1;
            adjustedSize /= 1024;
        }

        return string.Format("{0:n" + decimalPlaces + "} {1}",
            adjustedSize,
            SizeSuffixes[mag]);
    }

    //https://stackoverflow.com/questions/11927116/getting-files-recursively-skip-files-directories-that-cannot-be-read
    public static string[] FindAllFiles(string rootDir)
    {
        var pathsToSearch = new Queue<string>();
        var foundFiles = new List<string>();

        pathsToSearch.Enqueue(rootDir);

        while (pathsToSearch.Count > 0)
        {
            var dir = pathsToSearch.Dequeue();

            try
            {
                var files = Directory.GetFiles(dir);
                foreach (var file in Directory.GetFiles(dir))
                {
                    foundFiles.Add(file);
                }

                foreach (var subDir in Directory.GetDirectories(dir))
                {
                    pathsToSearch.Enqueue(subDir);
                }

            }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                LogExceptions(ex);
#else
		    catch (Exception)
		    {
#endif
            }
        }

        return foundFiles.ToArray();
    }

    //https://stackoverflow.com/questions/66667263/i-want-to-remove-special-characters-from-file-name-without-affecting-extension-i
    //https://stackoverflow.com/questions/3218910/rename-a-file-in-c-sharp

    public static bool FileHasInvalidChars(string path)
    {
        string fileName = Path.GetFileName(path);

        if (Regex.Match(fileName, @"[^\w-.'_!()& ]") != Match.Empty)
        {
            return true;
        }

        return false;
    }

    public static void RenameFileWithInvalidChars(string path)
    {
        try
        {
            if (!FileHasInvalidChars(path))
                return;

            string pathWithoutFilename = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            fileName = Regex.Replace(fileName, @"[^\w-.'_!()& ]", "");
            string finalPath = pathWithoutFilename + "\\" + fileName;

            FixedFileMove(path, finalPath, File.Exists(finalPath));
        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
        }
    }

    //http://stevenhollidge.blogspot.com/2012/06/async-taskdelay.html
    public static Task Delay(int milliseconds)
    {
        var tcs = new TaskCompletionSource<object>();
        new System.Threading.Timer(_ => tcs.SetResult(null)).Change(milliseconds, -1);
        return tcs.Task;
    }

    public static void LogPrint(string text, int type = 1)
    {
        Logger log = LogManager.GetCurrentClassLogger();

        switch (type)
        {
            case 2:
                log.Error(text);
                break;
            case 3:
                log.Warn(text);
                break;
            default:
                log.Info(text);
                break;
        }
    }

#if LAUNCHER || CMD || URI || BASICLAUNCHER
    public static void LogExceptions(Exception ex)
    {
        LogPrint("EXCEPTION|MESSAGE: " + (ex.Message != null ? ex.Message.ToString() : "N/A"), 2);
        LogPrint("EXCEPTION|STACK TRACE: " + (!string.IsNullOrWhiteSpace(ex.StackTrace) ? ex.StackTrace : "N/A"), 2);
        LogPrint("EXCEPTION|ADDITIONAL INFO: " + (ex != null ? ex.ToString() : "N/A"), 2);
    }
#endif

    //https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request

    private static string HttpGetInternal(string uri)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
    public static string HttpGet(string uri)
    {
        int tries = 0;
        int triesMax = 5;
        string exceptionMessage = "";

        while (tries < triesMax)
        {
            tries++;
            try
            {
                return HttpGetInternal(uri);
            }
            catch (Exception ex)
            {
#if URI || LAUNCHER || CMD || BASICLAUNCHER
                LogExceptions(ex);
#endif
                exceptionMessage = ex.Message;
                continue;
            }
        }

        return "ERROR: " + exceptionMessage;
    }

    public static void DrawBorderSimple(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style, int width)
    {
        //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        ControlPaint.DrawBorder(graphics, bounds,
            color, width, style,
            color, width, style,
            color, width, style,
            color, width, style);
    }

    public static bool IsIPValid(string IP)
    {
        IPAddress address;
        if (IPAddress.TryParse(IP, out address))
        {
            switch (address.AddressFamily)
            {
                case System.Net.Sockets.AddressFamily.InterNetwork:
                    return true;
                case System.Net.Sockets.AddressFamily.InterNetworkV6:
                default:
                    break;
            }
        }

        return false;
    }

    //converted from https://facreationz.wordpress.com/2014/12/11/c-know-if-running-under-wine/
    public static bool IsWineRunning()
    {
        string processName = "winlogon";
        var p = Process.GetProcessesByName(processName).Count();
        return (p <= 0);
    }

    public static void ConsolePrint(string text, int type, bool notime = false, bool noLog = false)
    {
        if (!notime)
        {
            ConsoleText("[" + DateTime.Now.ToShortTimeString() + "] - ", ConsoleColor.White);
        }

        switch (type)
        {
            case 2:
                ConsoleText(text, ConsoleColor.Red);
                if (!noLog)
                    LogPrint(text, 2);
                break;
            case 3:
                ConsoleText(text, ConsoleColor.Green);
                if (!noLog)
                    LogPrint(text);
                break;
            case 4:
                ConsoleText(text, ConsoleColor.Cyan);
                if (!noLog)
                    LogPrint(text);
                break;
            case 5:
                ConsoleText(text, ConsoleColor.Yellow);
                if (!noLog)
                    LogPrint(text, 3);
                break;
            case 1:
            default:
                ConsoleText(text, ConsoleColor.White);
                if (!noLog)
                    LogPrint(text);
                break;
        }

        ConsoleText(Environment.NewLine, ConsoleColor.White);
    }

    public static void ConsoleText(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
    }
    #endregion
}
#endregion

#region Tab Control without Header
//https://stackoverflow.com/questions/23247941/c-sharp-how-to-remove-tabcontrol-border

public partial class TabControlWithoutHeader : TabControl
{
    public TabControlWithoutHeader()
    {
        if (!DesignMode) Multiline = true;
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == 0x1328 && !DesignMode)
            m.Result = new IntPtr(1);
        else
            base.WndProc(ref m);
    }
}
#endregion