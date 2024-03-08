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

public class AwardBadge : IWebProxyExtension
{
    struct BadgeData
    {
        public long BadgeId;
        public string BadgeName;
        public string BadgeCreatorName;
    }

    private static readonly string BadgeDatabasePath = GlobalPaths.ConfigDir + "\\BadgeDatabase.ini";
    private static readonly string BadgeDatabaseSection = "BadgeDatabase";
    private string MetadataFileExtension = "_meta.ini";
    private INIFile ini = new INIFile(BadgeDatabasePath);

    void AddBadgeToDB(BadgeData data, bool Awarded = false)
    {
        CreateBadgeDatabaseIfNeeded();
        string BaseMapName = GlobalVars.UserConfiguration.ReadSetting("MapPathSnip").Replace(@"maps\\", "").Replace(".rbxl", "").Replace(".rbxlx", "").Replace(".bz2", "");
        if (GlobalVars.EasterEggMode)
        {
            BaseMapName = "Appreciation";
        }
        string BadgeName = (BaseMapName.Replace(" ", "-")) + "_" + data.BadgeId.ToString() + "_" + (data.BadgeName.Replace(" ", "-")) + "_" + (data.BadgeCreatorName.Replace(" ", "-"));
        ini.IniWriteValue(BadgeDatabaseSection, BadgeName, Awarded.ToString());
    }

    bool PlayerHasBadge(long BadgeID)
    {
        CreateBadgeDatabaseIfNeeded();

        if (ini.IniValueExists(BadgeID.ToString()))
        {
            string key = ini.IniGetKey(BadgeID.ToString());
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

    BadgeData LoadMetadata(long BadgeID) 
    {
        BadgeData result;
        result.BadgeId = BadgeID;
        result.BadgeName = BadgeID.ToString();
        result.BadgeCreatorName = "Unknown";
        string metaFile = (GlobalVars.UserConfiguration.ReadSetting("MapPath").Replace(".rbxl", "").Replace(".rbxlx", "").Replace(".bz2", "") + MetadataFileExtension);

        if (GlobalVars.EasterEggMode)
        {
            metaFile = GlobalPaths.DataDir + "\\Appreciation_meta.ini";
        }

        try
        {
            INIFile metaIni = new INIFile(metaFile, !(File.Exists(metaFile)));
            string section = BadgeID.ToString();
            
            string name = metaIni.IniReadValue(section, "BadgeName", "Unknown Badge #" + BadgeID.ToString());
            string creator = metaIni.IniReadValue(section, "BadgeCreatorName", "Unknown");
            result.BadgeName = name;
            result.BadgeCreatorName = creator;
        }
        catch (Exception)
        {
        }

        return result;
    }

    string GenerateBadgeString(string creatorName, string badgeName, long id)
    {
        if (PlayerHasBadge(id))
        {
            return "0";
        }

        return GlobalVars.UserConfiguration.ReadSetting("PlayerName") + " won " + creatorName + "'s \"" + badgeName + "\" award!";
    }

    public override string Name() 
    { 
        return "Badge Award API Extension";
    }

    public override string Version() 
    { 
        return "1.0.2";
    }

    public override string Author() 
    { 
        return "Bitl Development Studio"; 
    }

    public override void OnExtensionLoad() 
    { 
        CreateBadgeDatabaseIfNeeded();
    }

    public override bool IsValidURL(string absolutePath, string host) 
    { 
        return absolutePath.EndsWith("/game/badge/awardbadge.ashx");
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

        BadgeData meta = LoadMetadata(badgeid);

        string badgeAwardString = GenerateBadgeString(meta.BadgeCreatorName, meta.BadgeName, badgeid);
        AddBadgeToDB(meta, true);
        e.Ok(badgeAwardString, NetFuncs.GenerateHeaders(badgeAwardString.Length.ToString(), "text/plain"));
    }
}