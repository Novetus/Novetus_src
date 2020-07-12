#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
#endregion

namespace Novetus.ClientScriptTester
{
    #region LocalVars
    class LocalVars
    {
        public static List<string> SharedArgs = new List<string>();
        //a re-implementation of "Environment.NewLine" but with double spaces. Should be in NETExt, but we only use it here.
        public static String DoubleSpacedNewLine
        {
            get
            {
                Contract.Ensures(Contract.Result<String>() != null);
                return "\r\n\r\n";
            }
        }
    }
    #endregion
}
