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
        return "Bitl Development Studio"; 
    }
    
    public override string Version() 
    { 
        return "1.1.0";
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
    
    static string GetVideoPageOutput()
    {
        return "Your video has been saved in your My Videos/Roblox folder!";
    }
    
    static string GetImagePageOutput()
    {
        return "Your screenshot has been saved in your My Pictures/Roblox folder!";
    }
    
    static string GetReportOutput()
    {
        return "You can't report people in Novetus.\nContact your server administrator regarding problematic players.";
    }

    Dictionary<string, string> staticPages = new Dictionary<string, string>()
    {
        {"/analytics/measurement.ashx", ""},
        {"/error/lua.ashx", ""},
        {"/error/dmp.ashx", ""},
        {"/error/grid.ashx", ""},
        {"/game/cdn.ashx", ""},
        {"/game/joinrate.ashx", ""},
        {"/game/placevisit.ashx", ""},
        {"/game/clientpresence.ashx", ""},
        {"/game/validate-machine", "{\"success\":true}"},
        {"/game/logout.aspx", ""},
        {"/game/keeppingeralive.ashx", ""},
        {"/game/keepalivepinger.ashx", ""},
        {"/game/report-stats", ""},
        {"/game/gamepass/gamepasshandler.ashx", "<Value Type=\"boolean\">false</Value>"},
        {"/analytics/contentprovider.ashx", ""},
        {"/abusereport/ingamechat.aspx", GetReportOutput()},
        {"/uploadmedia/postimage.aspx", GetImagePageOutput()},
        {"/uploadmedia/uploadvideo.aspx", GetVideoPageOutput()},
        {"/asset/getscriptstate.ashx", "0 0 0 0"},
        {"/login/negotiate.ashx", ""},
        {"/gametransactions/getpendingtransactions", "[]"},
        {"/points/get-awardable-points", "{\"points\":\"0\"}"},
        {"/universes/validate-place-join", "true"},
        {"/persistence/getv2", "{\"data\":[]}"},
        {"/persistence/getsortedvalues", "{\"data\":{\"Entries\":[],\"ExclusiveStartKey\":null}}"},
        {"/persistence/increment", "{\"data\":null}"},
        {"/persistence/set", "{\"data\":null}"},
        {"/v1.1/counters/increment", ""},
        {"/v1.0/multiincrement/", ""},
        {"/getallowedsecurityversions", "{\"data\":[]}"},
        {"/getallowedmd5hashes", "{\"data\":[]}"},
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