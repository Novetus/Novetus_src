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
#if !BASICLAUNCHER
using NLog;
#endif
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Management;
#if !BASICLAUNCHER
using Mono.Nat;
#endif
#endregion

namespace Novetus.Core
{
    #region Utils

    public static class Util
    {
        #region Utility Functions

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
#if URI || LAUNCHER || BASICLAUNCHER
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
#if URI || LAUNCHER || BASICLAUNCHER
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

                IOSafe.File.Move(path, finalPath, File.Exists(finalPath));
            }
#if URI || LAUNCHER || BASICLAUNCHER
            catch (Exception ex)
            {
                LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
            }
        }

        public static void LogPrint(string text, int type = 1)
        {
            //TODO, remove nlog support for bootstrapper completely. this is a temp fix.
#if !BASICLAUNCHER
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
#endif
        }

        public static void LogExceptions(Exception ex)
        {
            string message = (ex.Message != null ? ex.Message.ToString() : "N/A");

            ConsolePrint(ex.Source + " Exception: " + message, 2, true);

#if LAUNCHER || URI || BASICLAUNCHER
            LogPrint("EXCEPTION|MESSAGE: " + message, 2);
            LogPrint("EXCEPTION|STACK TRACE: " + (!string.IsNullOrWhiteSpace(ex.StackTrace) ? ex.StackTrace : "N/A"), 2);
            LogPrint("EXCEPTION|ADDITIONAL INFO: " + (ex != null ? ex.ToString() : "N/A"), 2);
#endif
        }

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

        public static void ConsolePrint(string text, int type = 1, bool noLog = false, bool scrollDown = true)
        {


            switch (type)
            {
                case 0:
                    ConsoleText(text, ConsoleColor.Black, true);
                    break;
                case 2:
                    ConsoleText(text, ConsoleColor.Red, true);
                    if (!noLog)
                        LogPrint(text, 2);
                    break;
                case 3:
                    ConsoleText(text, ConsoleColor.Green, true);
                    if (!noLog)
                        LogPrint(text);
                    break;
                case 4:
                    ConsoleText(text, ConsoleColor.Cyan, true);
                    if (!noLog)
                        LogPrint(text);
                    break;
                case 5:
                    ConsoleText(text, ConsoleColor.Yellow, true);
                    if (!noLog)
                        LogPrint(text, 3);
                    break;
                case 1:
                default:
                    ConsoleText(text, ConsoleColor.White, true);
                    if (!noLog)
                        LogPrint(text);
                    break;
            }

#if LAUNCHER
            if (GlobalVars.consoleForm != null)
            {
                if (GlobalVars.consoleForm.InvokeRequired)
                    return;

                FormPrint(text, type, GlobalVars.consoleForm.ConsoleBox, scrollDown);
            }
#endif
        }

        public static void ConsolePrintMultiLine(string text, int type = 1, bool noLog = false)
        {
            try
            {
                string[] NewlineChars = { Environment.NewLine, "\n" };
                string[] lines = text.Split(NewlineChars, StringSplitOptions.None);
                ConsolePrintMultiLine(lines, type, noLog);
            }
            catch (Exception e)
            {
#if URI || LAUNCHER || BASICLAUNCHER
                LogExceptions(e);
#endif
            }
        }

        public static void ConsolePrintMultiLine(ICollection<string> textColection, int type = 1, bool noLog = false)
        {
            if (!textColection.Any())
                return;

            if (textColection.Count == 1)
            {
                ConsolePrint(textColection.First(), type, noLog);
                return;
            }

            foreach (string text in textColection)
            {
                ConsolePrint(text, type, noLog);
            }
        }

        private static void FormPrint(string text, int type, RichTextBox box, bool scrollDown = true)
        {
            if (box == null)
                return;

            foreach (string line in box.Lines)
            {
                Regex.Replace(line, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            }

            box.AppendText("\r\n", Color.White);

            switch (type)
            {
                case 1:
                    box.AppendText(text, Color.White);
                    break;
                case 2:
                    box.AppendText(text, Color.Red);
                    break;
                case 3:
                    box.AppendText(text, Color.Lime);
                    break;
                case 4:
                    box.AppendText(text, Color.Aqua);
                    break;
                case 5:
                    box.AppendText(text, Color.Yellow);
                    break;
                case 0:
                default:
                    box.AppendText(text, Color.Black);
                    break;
            }

            if (scrollDown)
            {
                box.SelectionStart = box.Text.Length;
                box.ScrollToCaret();
            }
        }

        private static void ConsoleText(string text, ConsoleColor color, bool newLine = false)
        {
            Console.ForegroundColor = color;
            if (newLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
        }

        public static void ReadTextFileWithColor(string path, bool scrollDown = true)
        {
            var lines = File.ReadLines(path);
            foreach (var line in lines)
            {
                try
                {
                    string[] vals = line.Split('|');
                    ConsolePrint(vals[0], ConvertSafe.ToInt32Safe(vals[1]), true, scrollDown);
                }
                catch (Exception)
                {
                    ConsolePrint(line, 1, true, scrollDown);
                }
            }
        }

        //Modified from https://stackoverflow.com/questions/4286487/is-there-any-lorem-ipsum-generator-in-c
        public static string LoremIpsum(int minWords, int maxWords,
        int minSentences, int maxSentences,
        int numParagraphs)
        {

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
        "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
        "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences)
                + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            StringBuilder result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {
                result.Append("lorem ipsum ");
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result.Append(" "); }
                        result.Append(words[rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
            }

            return result.ToString();
        }

#if LAUNCHER
        //https://stackoverflow.com/questions/30687987/unable-to-decompress-bz2-file-has-orginal-file-using-dotnetzip-library
        public static string Compress(string sourceFile, bool forceOverwrite)
        {
            var outFname = sourceFile + ".bz2";

            if (File.Exists(outFname))
            {
                if (forceOverwrite)
                    File.Delete(outFname);
                else
                    return null;
            }
            long rowCount = 0;
            var output = File.Create(outFname);

            try
            {
                using (StreamReader reader = new StreamReader(sourceFile))
                {
                    using (var compressor = new Ionic.BZip2.ParallelBZip2OutputStream(output))
                    {
                        StreamWriter writer = new StreamWriter(compressor, Encoding.UTF8);
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            writer.WriteLine(line);
                            rowCount++;
                        }

                        writer.Close();
                        compressor.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (output != null)
                    output = null;
            }

            //     Pump(fs, compressor);

            return outFname;
        }

        public static string Decompress(string sourceFile, bool forceOverwrite)
        {
            var outFname = sourceFile.Replace(".bz2", "");
            if (File.Exists(outFname))
            {
                if (forceOverwrite)
                    File.Delete(outFname);
                else
                    return null;
            }

            using (Stream fs = File.OpenRead(sourceFile),
                   output = File.Create(outFname),
                   decompressor = new Ionic.BZip2.BZip2InputStream(fs))
                Pump(decompressor, output);

            return outFname;
        }

        private static void Pump(Stream src, Stream dest)
        {
            byte[] buffer = new byte[2048];
            int n;
            while ((n = src.Read(buffer, 0, buffer.Length)) > 0)
                dest.Write(buffer, 0, n);
        }
#endif
        #endregion
    }
    #endregion
}