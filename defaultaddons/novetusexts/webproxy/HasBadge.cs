using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Collections.Generic;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;
using Novetus.Core;

public class HasBadge : IWebProxyExtension
{
    private static readonly string BadgeDatabasePath = GlobalPaths.ConfigDir + "\\BadgeDatabase.ini";
    private static readonly string BadgeDatabaseSection = "BadgeDatabase";
    private INIFile ini = new INIFile(BadgeDatabasePath, false);

    public override string Name() 
    { 
        return "Badge Checker API Extension";
    }
    
    public override string Version() 
    { 
        return "1.0.1";
    }

    public override string Author() 
    { 
        return "Bitl Development Studio"; 
    }

    bool PlayerHasBadge(long BadgeID)
    {
        CreateBadgeDatabaseIfNeeded();

        if (ini.IniValueExists(BadgeID.ToString()))
        {
            string BaseMapName = GlobalVars.UserConfiguration.ReadSetting("MapPathSnip").Replace(@"maps\\", "").Replace(".rbxl", "").Replace(".rbxlx", "").Replace(".bz2", "");
            if (GlobalVars.EasterEggMode)
            {
                BaseMapName = "Appreciation";
            }
            string BadgeName = (BaseMapName.Replace(" ", "-")) + "_" + BadgeID.ToString();
            string key = ini.IniGetKey(BadgeName);
            string awarded = ini.IniReadValue(BadgeDatabaseSection, key, "False");
            return Convert.ToBoolean(awarded);
        }

        return false;
    }

    void CreateBadgeDatabaseIfNeeded()
    {
        if (!File.Exists(BadgeDatabasePath))
        {
            Util.ConsolePrint("WARNING - " + BadgeDatabasePath + " not found. Creating empty badge database.", 5);
            File.Create(BadgeDatabasePath).Dispose();
        }
    }

    public override void OnExtensionLoad() 
    { 
        CreateBadgeDatabaseIfNeeded();
    }

    public override bool IsValidURL(string absolutePath, string host) 
    { 
        return absolutePath.EndsWith("/game/badge/hasbadge.ashx");
    }

    public override async Task OnRequest(object sender, SessionEventArgs e) 
    {
        string query = e.HttpClient.Request.RequestUri.Query;
        long badgeid = 0;
        long userid = 0;
        if (!long.TryParse(NetFuncs.FindQueryString(query, "badgeid"), out badgeid) && 
            !long.TryParse(NetFuncs.FindQueryString(query, "userid"), out userid))
        {
            e.GenericResponse("", HttpStatusCode.BadRequest);
            return;
        }

        string hasBadgeResult = PlayerHasBadge(badgeid) ? "Success" : "Failure";
        e.Ok(hasBadgeResult, NetFuncs.GenerateHeaders(hasBadgeResult.Length.ToString(), "text/plain"));
    }
}