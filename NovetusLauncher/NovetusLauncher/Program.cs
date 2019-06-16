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
using NovetusShared;

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
			if (args.Length == 0)
			{
				Application.Run(new MainForm());
			}
			else
			{
				foreach (string s in args)
      			{
        			GlobalVars.SharedArgs = ProcessInput(s);
      			}
				
				if (GlobalVars.SharedArgs.Equals("-itemmaker"))
				{
					Application.Run(new ItemMaker());
				}
				else if (GlobalVars.SharedArgs.Equals("-clientinfo"))
				{
					Application.Run(new ClientinfoEditor());
				}
				else if (GlobalVars.SharedArgs.Equals("-quickconfigure"))
				{
					Application.Run(new QuickConfigure());
				}
				else if (GlobalVars.SharedArgs.Equals("-documentation"))
				{
					Application.Run(new ClientScriptDocumentation());
				}
				else
				{
					Application.Run(new LoaderForm());
				}
			}
		}
		
	}
}
