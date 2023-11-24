#region Usings
using NLog;
using Novetus.Core;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
#endregion

namespace NovetusLauncher
{
    #region Novetus Launcher Main Class
    internal sealed class NovetusLauncherEntryPoint
    {
        enum CMDState
        {
            CMDOpen,
            CMDOnly,
            CMDNone
        }

        static bool formsOpen = false;

        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            if (!Directory.Exists(GlobalPaths.LogDir))
            {
                Directory.CreateDirectory(GlobalPaths.LogDir);
            }

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = GlobalPaths.LogDir + "\\Launcher-log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".log" };
            config.AddRuleForAllLevels(logfile);
            LogManager.Configuration = config;

            //FileManagement.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName, 
                //GlobalPaths.ConfigDir + "\\" + GlobalPaths.TermListFileName);

            GlobalVars.ColorsLoaded = FileManagement.InitColors();

            bool isSDK = false;
            CMDState state = CMDState.CMDOpen;

            if (args.Length > 0)
            {
                CommandLineArguments.Arguments CommandLine = new CommandLineArguments.Arguments(args);

                if (CommandLine["sdk"] != null)
                {
                    isSDK = true;
                }

                if (CommandLine["cmdonly"] != null)
                {
                    state = CMDState.CMDOnly;
                    GlobalVars.isConsoleOnly = true;
                }

                if (CommandLine["nocmd"] != null)
                {
                    state = CMDState.CMDNone;
                }

                if (CommandLine["nofilelist"] != null)
                {
                    GlobalVars.NoFileList = true;
                }
            }

            LocalVars.cmdLineArray = args.ToList();

            foreach (string argString in LocalVars.cmdLineArray)
            {
                LocalVars.cmdLineString += argString;

                if (!argString.Equals(LocalVars.cmdLineArray.Last()))
                {
                    LocalVars.cmdLineString += " ";
                }
            }

            Run(isSDK, state);
        }

        static void CreateFiles()
        {
            if (!File.Exists(GlobalPaths.ConfigDir + "\\servers.txt"))
            {
                Util.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\servers.txt not found. Creating empty file.", 5);
                File.Create(GlobalPaths.ConfigDir + "\\servers.txt").Dispose();
            }
            if (!File.Exists(GlobalPaths.ConfigDir + "\\ports.txt"))
            {
                Util.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\ports.txt not found. Creating empty file.", 5);
                File.Create(GlobalPaths.ConfigDir + "\\ports.txt").Dispose();
            }

            FileManagement.CreateInitialFileListIfNeededMulti();
            FileManagement.CreateAssetCacheDirectories();
            NetFuncs.InitUPnP();
            DiscordRPC.StartDiscord();
        }

        static void Run(bool sdk = false, CMDState state = CMDState.CMDOpen)
        {
            try
            {
                while (!GlobalVars.AppClosed)
                {
                    System.Windows.Forms.Application.DoEvents();

                    if (!formsOpen)
                    {
                        if (state != CMDState.CMDNone)
                        {
                            NovetusConsole console = new NovetusConsole();
                            GlobalVars.consoleForm = console;
                            console.Show();
                        }

                        CreateFiles();

                        if (state != CMDState.CMDOnly)
                        {
                            if (!sdk)
                            {
                                switch ((Settings.Style)GlobalVars.UserConfiguration.ReadSettingInt("LauncherStyle"))
                                {
                                    case Settings.Style.Compact:
                                        LauncherFormCompact compact = new LauncherFormCompact();
                                        compact.Show();
                                        break;
                                    case Settings.Style.Extended:
                                        LauncherFormExtended extended = new LauncherFormExtended();
                                        extended.Show();
                                        break;
                                    case Settings.Style.Stylish:
                                    default:
                                        LauncherFormStylish stylish = new LauncherFormStylish();
                                        stylish.Show();
                                        break;
                                }
                            }
                            else
                            {
                                NovetusSDK sdkApp = new NovetusSDK(false);
                                sdkApp.Show();
                            }
                        }

                        formsOpen = true;
                    }

                    Thread.Sleep(1);
                }

                System.Windows.Forms.Application.Exit();
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
            }
        }
    }
	#endregion
}
