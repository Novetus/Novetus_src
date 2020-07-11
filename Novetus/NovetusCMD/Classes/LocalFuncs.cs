#region Usings
using System.Diagnostics;
using System.Linq;
#endregion

namespace NovetusCMD
{
    #region LocalFuncs
    public class LocalFuncs
    {
        public static bool ProcessExists(int id)
        {
            return Process.GetProcesses().Any(x => x.Id == id);
        }
    }
    #endregion
}
