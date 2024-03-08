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

public class UploadWarnings : IWebProxyExtension
{
    public override string Name() 
    { 
        return "Upload Dialog Warnings Extension";
    }

    public override string Version() 
    { 
        return "1.0.1";
    }

    public override string Author() 
    { 
        return "Bitl Development Studio"; 
    }

    public override bool IsValidURL(string absolutePath, string host) 
    { 
        return absolutePath.EndsWith("/uploadmedia/postimage.aspx") || absolutePath.EndsWith("/uploadmedia/uploadvideo.aspx");
    }

    public override async Task OnRequest(object sender, SessionEventArgs e) 
    {
        string absPath = e.HttpClient.Request.RequestUri.AbsolutePath.ToLowerInvariant();

        string type = "video";
        string folder = "My Videos";

        if (absPath.EndsWith("/uploadmedia/postimage.aspx"))
        {
            type = "screenshot";
            folder = "My Pictures";
        }

        string result = "Your " + type + " was saved! Look in the Roblox folder in your " + folder + " folder!";
        e.Ok(result, NetFuncs.GenerateHeaders(result.Length.ToString(), "text/plain"));
    }
}