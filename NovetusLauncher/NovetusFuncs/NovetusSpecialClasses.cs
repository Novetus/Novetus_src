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

public class ClientInfo
{
    public ClientInfo()
    {
        UsesPlayerName = false;
        UsesID = true;
        Description = "";
        Warning = "";
        LegacyMode = false;
        ClientMD5 = "";
        ScriptMD5 = "";
        Fix2007 = false;
        AlreadyHasSecurity = false;
        NoGraphicsOptions = false;
        CommandLineArgs = "";
    }

    public bool UsesPlayerName  { get; set; }
    public bool UsesID  { get; set; }
    public string Description { get; set; }
    public string Warning  { get; set; }
    public bool LegacyMode { get; set; }
    public string ClientMD5 { get; set; }
    public string ScriptMD5 { get; set; }
    public bool Fix2007 { get; set; }
    public bool AlreadyHasSecurity { get; set; }
    public bool NoGraphicsOptions { get; set; }
    public string CommandLineArgs { get; set; }
}

/*
 * Finish classes for:
 * 
 * config
 * customization
 * info
 * reshade
 * 
 * also change field names for all forms and config read/writes
 * replace huge if statements with switch statements
 */
public class Config
{
}

public class CustomizationConfig
{
}

public class ProgramInfo
{
}

public class ReShadeConfig
{
}
