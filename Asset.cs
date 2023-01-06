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

    public override bool IsValidURL(string absolutePath, string host) 
    { 
        return (absolutePath.EndsWith("/asset") || absolutePath.EndsWith("/asset/"));
    }

    public override async Task OnRequest(object sender, SessionEventArgs e) 
    {
        string query = e.HttpClient.Request.RequestUri.Query;
        long id;
        if (!long.TryParse(NetFuncs.FindQueryString(query, "id"), out id))
        {
            Util.ConsolePrint(Name() + ": Redirecting " + query, 3);
            e.Redirect("https://assetdelivery.roblox.com/v1/asset/" + query);
        }
        else
        {
            List<string> PathList = new List<string>((IEnumerable<string>)Directory.GetFiles(GlobalPaths.DataPath, id.ToString(), SearchOption.AllDirectories));

            if (PathList.Count > 0)
            {
                Util.ConsolePrint(Name() + ": Local assets for " + id.ToString() + " found. Redirecting " + query, 3);
                string First = PathList[0];
                byte[] numArray = await Task.Run(() => File.ReadAllBytes(First));
                e.Ok(numArray, (IEnumerable<HttpHeader>) new List<HttpHeader>()
                {
                    new HttpHeader("Content-Length", ((long) numArray.Length).ToString()),
                    new HttpHeader("Cache-Control", "no-cache")
                });
            }
            else
            {
                Util.ConsolePrint(Name() + ": No local assets for " + id.ToString() + ". Redirecting " + query, 5);
                e.Redirect("https://assetdelivery.roblox.com/v1/asset/" + query);
            }
        }
    }
}