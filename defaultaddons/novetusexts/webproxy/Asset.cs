using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;
using Novetus.Core;

public class Asset : IWebProxyExtension
{
    public override string Name() 
    { 
        return "Asset Redirection Extension";
    }

    public override string Author() 
    { 
        return "Bitl"; 
    }

    public override bool IsValidURL(string absolutePath, string host) 
    { 
        return (absolutePath.EndsWith("/asset") || absolutePath.EndsWith("/asset/"));
    }

    async void RedirectLocalAsset(List<string> pathList, string id, SessionEventArgs e)
    {
        if (pathList.Count <= 0)
            return;

        if (string.IsNullOrWhiteSpace(id))
            return;
        
        string First = pathList[0];
        byte[] numArray = await Task.Run(() => File.ReadAllBytes(First));
        e.Ok(numArray, NetFuncs.GenerateHeaders(((long) numArray.Length).ToString()));
    }

    bool CanRedirectLocalAsset(string path, long id, SessionEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        if (id == null)
            return false;
        
        string idString = id.ToString();
        List<string> PathList = new List<string>((IEnumerable<string>)Directory.GetFiles(path, idString, SearchOption.AllDirectories));

        if (PathList.Count > 0)
        {
            RedirectLocalAsset(PathList, idString, e);
            return true;
        }
        
        return false;
    }

    public override async Task OnRequest(object sender, SessionEventArgs e) 
    {
        string query = e.HttpClient.Request.RequestUri.Query;
        string url = "https://assetdelivery.roblox.com/v1/asset/" + query;
        long id;
        if (!long.TryParse(NetFuncs.FindQueryString(query, "id"), out id))
        {
            e.Redirect(url);
        }
        else
        {
            if (!CanRedirectLocalAsset(GlobalPaths.DataPath, id, e))
            {
                //Util.ConsolePrint(Name() + ": Cannot find " + id.ToString() + " in " + GlobalPaths.DataPath + ". Checking client assets.", 5);
                if (!CanRedirectLocalAsset(GlobalPaths.AssetsPath, id, e))
                {
                    //Util.ConsolePrint(Name() + ": Cannot find " + id.ToString() + " in " + GlobalPaths.AssetsPath + ". Redirecting.", 2);
                    e.Redirect(url);

                    Downloader download = new Downloader(url, id.ToString());

                    download.filePath = GlobalPaths.AssetCacheDirAssets;
                    download.showErrorInfo = false;
                    download.overwrite = false;
                    download.InitDownloadDirect("");

                    if (!download.getDownloadOutcome().Contains("Error"))
                    {
                        Util.ConsolePrint(Name() + ": Localization of " + id.ToString() + " successful. Asset will be local on next launch.", 3);
                    }
                }
            }
        }
    }
}