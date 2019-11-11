using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovetusTest_RobloxFileDownloaderAndSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = GlobalVars.RootPath + "\\Test.rbxl";
            string exportpath = GlobalVars.RootPath + "\\Export";

            Console.WriteLine("Meshes");
            RobloxXMLLocalizer.DownloadFromNodes(path, GlobalVars.Fonts, 0, 0, exportpath, 0);
            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
