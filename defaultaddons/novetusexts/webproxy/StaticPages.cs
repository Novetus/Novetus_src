using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;
using Novetus.Core;

public class StaticPages : IWebProxyExtension
{
    public override string Name() 
    { 
        return "Static APIs Extension";
    }

    public override string Author() 
    { 
        return "Bitl"; 
    }

    static string GetStudioPageOutput()
    {
        return "Welcome to Novetus " + GlobalVars.ProgramInformation.Version + "!";
    }

    static string GetHelpPageOutput()
    {
        string path = GlobalPaths.NovetusExtsWebProxy + @"\\webpages\\Help.html";
        return File.ReadAllText(path);
    }

    Dictionary<string, string> staticPages = new Dictionary<string, string>()
    {
        {"/ide/landing.aspx", GetStudioPageOutput()},
        {"/discover", GetStudioPageOutput()},
        {"/my/places.aspx", GetStudioPageOutput()},
        {"/game/badge/isbadgedisabled.ashx", "0"},
        {"/game/help.aspx", GetHelpPageOutput()}
    }; 

    public override bool IsValidURL(string absolutePath, string host) 
    { 
        foreach(var item in staticPages.Keys)
        {
            if (absolutePath.StartsWith(item) || absolutePath.EndsWith(item))
            {
                return true;
            }
        }

        return false;
    }

    public override async Task OnRequest(object sender, SessionEventArgs e) 
    {
        string absPath = e.HttpClient.Request.RequestUri.AbsolutePath.ToLowerInvariant();

        foreach(var item in staticPages.Keys)
        {
            if (absPath.StartsWith(item) || absPath.EndsWith(item))
            {
                string result = staticPages[item];
                e.Ok(result, NetFuncs.GenerateHeaders(result.Length.ToString(), "text/html"));
            }
        }
    }
}