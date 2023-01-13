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

public class StudioLaunchPage : IWebProxyExtension
{
    public override string Name() 
    { 
        return "Studio Launch Page Extension";
    }

    public override string Author() 
    { 
        return "Bitl"; 
    }

    public override bool IsValidURL(string absolutePath, string host) 
    { 
        return absolutePath.EndsWith("/ide/landing.aspx") || absolutePath.EndsWith("/my/places.aspx");
    }

    public override async Task OnRequest(object sender, SessionEventArgs e) 
    {
        e.Ok("Welcome to Novetus Studio version " + GlobalVars.ProgramInformation.Version);
    }
}