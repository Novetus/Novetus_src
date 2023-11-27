
using System.Collections.Generic;

namespace NovetusLauncher
{
    #region LocalVars
    class LocalVars
    {
        #region Variables
        public static string prevsplash = "";
        public static bool launcherInitState = true;
        //hack for linux. store the command line variables locally.
        public static List<string> cmdLineArray = new List<string>();
        public static string cmdLineString = "";
        #endregion
    }
    #endregion
}
