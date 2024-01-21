#region Usings
using System;
using System.Collections.Generic;
using System.IO;
#endregion

namespace Novetus.Core
{
    #region Text Line Remover and Friends
    // modified from https://stackoverflow.com/questions/668907/how-to-delete-a-line-from-a-text-file-in-c/668914#668914

    public static class TextLineRemover
    {
        public static void RemoveTextLines(IList<string> linesToRemove, string filename, string tempFilename)
        {
            // Initial values
            int lineNumber = 0;
            int linesRemoved = 0;
            DateTime startTime = DateTime.Now;

            // Read file
            using (var sr = new StreamReader(filename))
            {
                // Write new file
                using (var sw = new StreamWriter(tempFilename))
                {
                    // Read lines
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineNumber++;
                        // Look for text to remove
                        if (!ContainsString(line, linesToRemove))
                        {
                            // Keep lines that does not match
                            sw.WriteLine(line);
                        }
                        else
                        {
                            // Ignore lines that DO match
                            linesRemoved++;
                            InvokeOnRemovedLine(new RemovedLineArgs
                            {
                                RemovedLine = line,
                                RemovedLineNumber = lineNumber
                            });
                        }
                    }
                }
            }

            //FixedFileMove deletes the original file and moves the temp file in.
            IOSafe.File.Move(tempFilename, filename, true);

            // Final calculations
            DateTime endTime = DateTime.Now;
            InvokeOnFinished(new FinishedArgs
            {
                LinesRemoved = linesRemoved,
                TotalLines = lineNumber,
                TotalTime = endTime.Subtract(startTime)
            });
        }

        private static bool ContainsString(string line, IEnumerable<string> linesToRemove)
        {
            foreach (var lineToRemove in linesToRemove)
            {
                if (line.Contains(lineToRemove))
                    return true;
            }
            return false;
        }

        public static event RemovedLine OnRemovedLine;
        public static event Finished OnFinished;

        public static void InvokeOnFinished(FinishedArgs args)
        {
            OnFinished?.Invoke(null, args);
        }

        public static void InvokeOnRemovedLine(RemovedLineArgs args)
        {
            OnRemovedLine?.Invoke(null, args);
        }
    }

    public delegate void Finished(object sender, FinishedArgs args);

    public struct FinishedArgs
    {
        public int TotalLines { get; set; }
        public int LinesRemoved { get; set; }
        public TimeSpan TotalTime { get; set; }
    }

    public delegate void RemovedLine(object sender, RemovedLineArgs args);

    public struct RemovedLineArgs
    {
        public string RemovedLine { get; set; }
        public int RemovedLineNumber { get; set; }
    }
    #endregion
}
