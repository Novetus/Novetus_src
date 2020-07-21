#region Usings
using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
#endregion

namespace Novetus.ClientScriptTester
{
    #region ClientScript Tester
    static class ClientScriptTester
    {
        #region Exeption Helpers
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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = Assembly.GetExecutingAssembly().Location + "\\Tester-log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".log" };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;

            //https://stackify.com/csharp-catch-all-exceptions/
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                Logger log = LogManager.GetCurrentClassLogger();
                log.Error("EXEPTION THROWN: " + (!string.IsNullOrWhiteSpace(eventArgs.Exception.Message) ? eventArgs.Exception.Message : "N/A"));
                log.Error("EXCEPTION INFO: " + (eventArgs.Exception != null ? eventArgs.Exception.ToString() : "N/A"));
                log.Error("INNER EXCEPTION: " + (eventArgs.Exception.InnerException != null ? eventArgs.Exception.InnerException.ToString() : "N/A"));
                log.Error("STACK TRACE: " + (!string.IsNullOrWhiteSpace(eventArgs.Exception.StackTrace) ? eventArgs.Exception.StackTrace : "N/A"));
                log.Error("TARGET SITE: " + (eventArgs.Exception.TargetSite != null ? eventArgs.Exception.TargetSite.ToString() : "N/A"));
                log.Error("FOOTPRINTS: " + (!string.IsNullOrWhiteSpace(eventArgs.Exception.GetExceptionFootprints()) ? eventArgs.Exception.GetExceptionFootprints() : "N/A"));
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            foreach (string s in args)
            {
                LocalVars.SharedArgs.Add(s);
            }

            Application.Run(new ClientScriptTestForm());
        }
    }
    #endregion
}
