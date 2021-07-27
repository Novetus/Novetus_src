#region Usings
using System.IO;
using System.Linq;
using System.Xml.Serialization;
#endregion

#region Content Provider Options
public class Provider
{
    public string Name;
    public string URL;
    public string Icon;
}

[XmlRoot("ContentProviders")]
public class ContentProviders
{
    [XmlArray("Providers")]
    public Provider[] Providers;
}

public class OnlineClothing
{
    public static Provider[] GetContentProviders()
    {
        if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ContentProviders));

            FileStream fs = new FileStream(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName, FileMode.Open);
            ContentProviders providers;
            providers = (ContentProviders)serializer.Deserialize(fs);

            return providers.Providers;
        }
        else
        {
            return null;
        }
    }

    public static Provider FindContentProviderByName(Provider[] providers, string query)
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

    public static Provider FindContentProviderByURL(Provider[] providers, string query)
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
