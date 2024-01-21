using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;

// Original code made by Matt, originally intended for Sodikm 1.2.
// Slight modifications and cleanup for Novetus by Bitl.
namespace Novetus.Core
{
    /// <summary>
    /// VC++ redists to check
    /// </summary>
    public enum VCPPRedist
    {
        /// <summary>
        /// Don't check redist
        /// </summary>
        None,

        /// <summary>
        /// VC++ 2005 redist
        /// </summary>
        [Description("Visual C++ 2005 SP1 Redistributables")]
        VCPP2005,

        /// <summary>
        /// VC++ 2008 redist
        /// </summary>
        [Description("Visual C++ 2008 Redistributables")]
        VCPP2008,

        /// <summary>
        /// VC++ 2012 redist
        /// </summary>
        [Description("Visual C++ 2012 Redistributables")]
        VCPP2012
    }

    public class VCPPRedistInstallationDetector
    {
        /// <summary>
        /// Which key in "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Installer\"
        /// </summary>
        private enum RedistKeyLocation
        {
            /// <summary>
            /// VC++2010 and below
            /// </summary>
            Products,

            /// <summary>
            /// VC++2012 and above
            /// </summary>
            Dependencies
        }

        /// <summary>
        /// Information about where the redist is
        /// </summary>
        private struct RedistInformation
        {
            /// <summary>
            /// Key location
            /// </summary>
            public RedistKeyLocation Location { get; }

            /// <summary>
            /// Optional?
            /// </summary>
            public bool Optional { get; }

            /// <summary>
            /// Possible keys
            /// </summary>
            public string[] Keys { get; }

            public RedistInformation(RedistKeyLocation location, string[] keys, bool optional)
            {
                Location = location;
                Keys = keys;
                Optional = optional;
            }
        }

        /// <summary>
        /// VC++ redist enum to redist infos. <br/>
        /// Value is a list because VC++2012 has possible two redist keys for some reason. <br/>
        /// Installer keys for VC redists can be found at https://stackoverflow.com/a/34209692.
        /// </summary>
        private static Dictionary<VCPPRedist, RedistInformation> _VCRedistToRedistKeysMap = new Dictionary<VCPPRedist, RedistInformation>()
        {
            [VCPPRedist.VCPP2005] = new RedistInformation(RedistKeyLocation.Products, new[] { "b25099274a207264182f8181add555d0", "c1c4f01781cc94c4c8fb1542c0981a2a" }, false),
            [VCPPRedist.VCPP2008] = new RedistInformation(RedistKeyLocation.Products, new[] { "6E815EB96CCE9A53884E7857C57002F0", "6F9E66FF7E38E3A3FA41D89E8A906A4A", "D20352A90C039D93DBF6126ECE614057" }, false),
            [VCPPRedist.VCPP2012] = new RedistInformation(RedistKeyLocation.Dependencies, new[] { "{33d1fd90-4274-48a1-9bc1-97e33d9c2d6f}", "{95716cce-fc71-413f-8ad5-56c2892d4b3a}" }, true)
        };

        /// <summary>
        /// Cached installation results.
        /// </summary>
        private static Dictionary<VCPPRedist, bool> _VCRedistResults = new Dictionary<VCPPRedist, bool>()
        {
            [VCPPRedist.None] = true
        };

        /// <summary>
        /// Checks if redist exists.
        /// </summary>
        /// <param name="information">Redist information</param>
        /// <returns>Exists</returns>
        private static bool CheckIfInstallerKeyExists(RedistInformation information)
        {
            string path = information.Location.ToString();

            foreach (string key in information.Keys)
            {
                using RegistryKey redist = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\Installer\" + path + @"\" + key);

                if (redist != null)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if redist is optional
        /// </summary>
        /// <param name="information">Redist information</param>
        /// <returns>Optional</returns>
        private static bool CheckIfOptional(RedistInformation information)
        {
            return information.Optional;
        }

        /// <summary>
        /// Check if a VC++ redist is installed
        /// </summary>
        /// <param name="redist">VC++ redist version</param>
        /// <returns>Is installed</returns>
        public static bool IsInstalled(VCPPRedist redist) => _VCRedistResults[redist];

        public static string GetNameForRedist(VCPPRedist redist)
        {
            switch(redist)
            {
                case VCPPRedist.VCPP2005:
                    return "Visual C++ 2005 SP1 Redistributables (32-bit)";
                case VCPPRedist.VCPP2008:
                    return "Visual C++ 2008 Redistributables (32-bit)";
                case VCPPRedist.VCPP2012:
                    return "Visual C++ 2012 Redistributables (32-bit)";
                case VCPPRedist.None:
                default:
                    return "Generic Redistributables";
            }
        }

        /// <summary>
        /// Checks for all keys
        /// </summary>
        static VCPPRedistInstallationDetector()
        {
            //TODO: make clients detect if a vc++ install is required upon launch.
            foreach (var kvPair in _VCRedistToRedistKeysMap)
            {
                VCPPRedist redist = kvPair.Key;
                RedistInformation information = kvPair.Value;

                bool installed = CheckIfInstallerKeyExists(information);

                // if we're optional, lie.
                if (!installed && CheckIfOptional(information))
                {
                    installed = true;
                }

                _VCRedistResults[redist] = installed;
            }
        }
    }
}
