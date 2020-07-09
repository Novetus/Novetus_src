#region Usings
using System.IO;
#endregion

namespace NovetusLauncher
{
    #region Launcher Functions
    class LauncherFuncs
    {
        public static void CreateAssetCacheDirectories()
        {
            if (!Directory.Exists(LocalPaths.AssetCacheDirFonts))
            {
                Directory.CreateDirectory(LocalPaths.AssetCacheDirFonts);
            }

            if (!Directory.Exists(LocalPaths.AssetCacheDirSky))
            {
                Directory.CreateDirectory(LocalPaths.AssetCacheDirSky);
            }

            if (!Directory.Exists(LocalPaths.AssetCacheDirSounds))
            {
                Directory.CreateDirectory(LocalPaths.AssetCacheDirSounds);
            }

            if (!Directory.Exists(LocalPaths.AssetCacheDirTexturesGUI))
            {
                Directory.CreateDirectory(LocalPaths.AssetCacheDirTexturesGUI);
            }

            if (!Directory.Exists(LocalPaths.AssetCacheDirScripts))
            {
                Directory.CreateDirectory(LocalPaths.AssetCacheDirScripts);
            }
        }
    }
    #endregion
}
