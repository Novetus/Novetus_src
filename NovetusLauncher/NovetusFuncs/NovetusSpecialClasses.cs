public class AssetCacheDef
{
    public AssetCacheDef(string clas, string[] id, string[] ext, 
        string[] dir, string[] gamedir)
    {
        Class = clas;
        Id = id;
        Ext = ext;
        Dir = dir;
        GameDir = gamedir;
    }

    public string Class { get; set; }
    public string[] Id { get; set; }
    public string[] Ext { get; set; }
    public string[] Dir { get; set; }
    public string[] GameDir { get; set; }
}

//maybe...
public class Client
{
    public Client(bool playername, bool playerid, string description, 
        string warning, bool legacymode, string clientmd5, string scriptmd5,
        bool fix2007, bool hassecurity, bool nographicsoptions, string commandlineargs)
    {
        UsesPlayerName = playername;
        UsesID = playerid;
        Description = description;
        Warning = warning;
        LegacyMode = legacymode;
        ClientMD5 = clientmd5;
        ScriptMD5 = scriptmd5;
        Fix2007 = fix2007;
        HasSecurity = hassecurity;
        NoGraphicsOptions = nographicsoptions;
        CommandLineArgs = commandlineargs;
    }

    public bool UsesPlayerName  { get; set; }
    public bool UsesID  { get; set; }
    public string Description { get; set; }
    public string Warning  { get; set; }
    public bool LegacyMode { get; set; }
    public string ClientMD5 { get; set; }
    public string ScriptMD5 { get; set; }
    public bool Fix2007 { get; set; }
    public bool HasSecurity { get; set; }
    public bool NoGraphicsOptions { get; set; }
    public string CommandLineArgs { get; set; }
}