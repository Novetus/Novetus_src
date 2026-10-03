using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Novetus.Core
{
    #region Content Provider Options
    public class ContentProvider
    {
        public string Name;
        public string URL;
        public string Icon;
        public bool SDKOnly;

        public ContentProvider(string szName, string szURL, string szIcon, bool bSDKOnly)
        {
            Name = szName;
            URL = szURL;
            Icon = szIcon;
            SDKOnly = bSDKOnly;
        }

        private static ContentProvider[] providers = new ContentProvider[]{
            new ContentProvider(
               "Roblox (Asset Delivery, HTTP)",
               "http://assetdelivery.roblox.com/v1/asset/?id=",
               "roblox.png",
               false),

            new ContentProvider(
               "Roblox (Asset Delivery, HTTPS, Incompatible w/ older clients)",
               "https://assetdelivery.roblox.com/v1/asset/?id=",
               "roblox.png",
               false),

            new ContentProvider(
               "Roblox Local (Web Proxy Required)", 
               "http://www.roblox.com/asset?id=", 
               "roblox.png", 
               false),

           new ContentProvider(
               "Imgur (HTTP)", 
               "http://i.imgur.com/", 
               "imgur.png", 
               false),

           new ContentProvider(
               "Imgur (HTTPS, Incompatible w/ older clients)", 
               "https://i.imgur.com/", 
               "imgur.png",
               false),

           new ContentProvider(
               "Novetus Assetdelivery Textures (HTTP)", 
               "http://raw.githubusercontent.com/Novetus/novetus-assetdelivery/master/textures/", 
               "novetustex.png",
               false),

           new ContentProvider(
               "Novetus Assetdelivery Textures (HTTPS)", 
               "https://raw.githubusercontent.com/Novetus/novetus-assetdelivery/master/textures/", 
               "novetustex.png",
               false),

           new ContentProvider(
               "Novetus Assetdelivery (HTTP)", 
               "http://raw.githubusercontent.com/Novetus/novetus-assetdelivery/master/", 
               "novetus.png",
               false),

           new ContentProvider(
               "Novetus Assetdelivery (HTTPS)", 
               "https://raw.githubusercontent.com/Novetus/novetus-assetdelivery/master/", 
               "novetus.png",
               false),
        };

        public static ContentProvider[] GetContentProviders()
        {
            return providers;
        }

        public static ContentProvider FindContentProviderByName(string query)
        {
            return providers.FirstOrDefault(item => query.Contains(item.Name));
        }

        public static ContentProvider FindContentProviderByURL(string query)
        {
            return providers.FirstOrDefault(item => query.Contains(item.URL));
        }
    }
    #endregion
}
