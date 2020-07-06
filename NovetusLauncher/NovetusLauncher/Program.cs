/*
 * Created by SharpDevelop.
 * User: BITL-Gaming
 * Date: 10/7/2016
 * Time: 3:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace NovetusLauncher
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		static string ProcessInput(string s)
    	{
       		return s;
    	}
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			LauncherFuncs.ReadInfoFile(Directories.ConfigDir + "\\" + GlobalVars.InfoName);
			if (args.Length == 0)
			{
				//read from our config to determine which clients to load.
				LauncherFuncs.Config(Directories.ConfigDir + "\\" + GlobalVars.ConfigName, false);

				switch (GlobalVars.UserConfiguration.LauncherLayout)
                {
					case LauncherLayout.Compact:
						Application.Run(new MainForm_legacy());
						break;
					default:
						Application.Run(new MainForm());
						break;
				}
			}
			else
			{
				foreach (string s in args)
      			{
        			GlobalVars.SharedArgs = ProcessInput(s);
      			}

                if (GlobalVars.SharedArgs.Equals("-sdk"))
                {
                    Application.Run(new NovetusSDK());
                }
			}
		}
		
	}
}
