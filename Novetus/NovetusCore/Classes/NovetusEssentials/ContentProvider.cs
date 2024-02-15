using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Novetus.Core
{
    #region Content Provider Options
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentProvider
    {
        [JsonProperty]
        [JsonRequired]
        public string Name;
        [JsonProperty]
        [JsonRequired]
        public string URL;
        [JsonProperty]
        [JsonRequired]
        public string Icon;

        public static ContentProvider[] GetContentProviders()
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                List<ContentProvider> providers = JsonConvert.DeserializeObject<List<ContentProvider>>(File.ReadAllText(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName));
                return providers.ToArray();
            }
            else
            {
                return null;
            }
        }

        public static ContentProvider FindContentProviderByName(ContentProvider[] providers, string query)
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                return providers.SingleOrDefault(item => query.Contains(item.Name));
            }
            else
            {
                return null;
            }
        }

        public static ContentProvider FindContentProviderByURL(ContentProvider[] providers, string query)
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                return providers.SingleOrDefault(item => query.Contains(item.URL));
            }
            else
            {
                return null;
            }
        }
    }
    #endregion
}
