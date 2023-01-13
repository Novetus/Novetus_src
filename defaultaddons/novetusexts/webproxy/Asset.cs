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

    public override async Task OnRequest(object sender, SessionEventArgs e) 
    {
        string query = e.HttpClient.Request.RequestUri.Query;
        long id;
        if (!long.TryParse(NetFuncs.FindQueryString(query, "id"), out id))
        {
            e.Redirect("https://assetdelivery.roblox.com/v1/asset/" + query);
        }
        else
        {
            List<string> PathList = new List<string>((IEnumerable<string>)Directory.GetFiles(GlobalPaths.DataPath, id.ToString(), SearchOption.AllDirectories));

            if (PathList.Count > 0)
            {
                Util.ConsolePrint(Name() + ": Local asset for " + id.ToString() + " found. Using local asset.", 3);
                string First = PathList[0];
                byte[] numArray = await Task.Run(() => File.ReadAllBytes(First));
                e.Ok(numArray, NetFuncs.GenerateHeaders(((long) numArray.Length).ToString()));
            }
            else
            {
                e.Redirect("https://assetdelivery.roblox.com/v1/asset/" + query);
            }
        }
    }
}