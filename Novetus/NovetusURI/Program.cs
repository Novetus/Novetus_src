using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovetusURI
{
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
                Application.Run(new Form1());
            }
            else
            {
                foreach (string s in args)
                {
                    GlobalVars.SharedArgs = ProcessInput(s);
                }

                Application.Run(new LoaderForm());
            }
        }

    }
}
