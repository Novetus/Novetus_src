#region Usings
using System;
using System.Windows.Forms;
#endregion

namespace Novetus.ClientScriptTester
{
    #region ClientScript Tester
    static class ClientScriptTester
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
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
