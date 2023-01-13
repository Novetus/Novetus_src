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
    private string BadgeDatabaseSection = "BadgeDatabase";
    private string MetadataFileExtension = "_meta.ini";
    private INIFile ini = new INIFile(BadgeDatabasePath);

    public override string Name() 
    { 
        return "Badge Award Extension";
    }

    public override string Author() 
    { 
        return "Bitl"; 
    }

    void AddBadgeToDB(long BadgeID, bool Awarded = false)
    {
        CreateBadgeDatabaseIfNeeded();
        ini.IniWriteValue(BadgeDatabaseSection, BadgeID.ToString(), Awarded.ToString());
    }

    bool PlayerHasBadge(long BadgeID)
    {
        CreateBadgeDatabaseIfNeeded();

        if (ini.IniValueExists(BadgeID.ToString()))
        {
            string awarded = ini.IniReadValue(BadgeDatabaseSection, BadgeID.ToString(), "False");
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

    BadgeData LoadMetadata(long BadgeID) 
    {
        BadgeData result;
        result.BadgeId = BadgeID;
        result.BadgeName = BadgeID.ToString();
        result.BadgeCreatorName = "Unknown";
        string metaFile = (GlobalVars.UserConfiguration.MapPath.Replace(".rbxl", "").Replace(".rbxlx", "").Replace(".bz2", "") + MetadataFileExtension);

        if (GlobalVars.GameOpened == ScriptType.EasterEgg)
        {
            metaFile = ((GlobalPaths.DataDir + "\\Appreciation.rbxl").Replace(".rbxl", MetadataFileExtension));
        }

        if (File.Exists(metaFile))
        {
            try
            {
                INIFile metaIni = new INIFile(metaFile);
                string section = BadgeID.ToString();

                string name = metaIni.IniReadValue(section, "BadgeName", BadgeID.ToString());
                string creator = metaIni.IniReadValue(section, "BadgeCreatorName", "Unknown");
                result.BadgeName = name;
                result.BadgeCreatorName = creator;
            }
            catch (Exception)
            {
            }
        }

        return result;
    }

    public override bool IsValidURL(string absolutePath, string host) 
    { 
        return absolutePath.EndsWith("/game/badge/awardbadge.ashx");
    }

    string GenerateBadgeString(string creatorName, string badgeName, long id)
    {
        if (PlayerHasBadge(id))
        {
            return "0";
        }

        return GlobalVars.UserConfiguration.PlayerName + " won " + creatorName + "'s \"" + badgeName + "\" award!";
    }

    public override async Task OnRequest(object sender, SessionEventArgs e) 
    {
        await Util.Delay(1000);
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
        AddBadgeToDB(badgeid, true);
        e.Ok(badgeAwardString, NetFuncs.GenerateHeaders(badgeAwardString.Length.ToString()));
    }
}