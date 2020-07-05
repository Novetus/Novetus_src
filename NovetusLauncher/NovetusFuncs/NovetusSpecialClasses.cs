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
 * info
 * reshade
 * 
 * change field names for all forms
 * Rewrite client launching into one function.
 */
public class Config
{
    public Config()
    {
        
    }


}

public class CustomizationConfig
{
  public CustomizationConfig()
  {
        Hat1 = "NoHat.rbxm";
        Hat2 = "NoHat.rbxm";
        Hat3 = "NoHat.rbxm";
        Face = "DefaultFace.rbxm";
        Head = "DefaultHead.rbxm";
        TShirt = "NoTShirt.rbxm";
        Shirt = "NoShirt.rbxm";
        Pants = "NoPants.rbxm";
        Icon = "NBC";
        Extra = "NoExtra.rbxm";
        HeadColorID = 24;
        TorsoColorID = 23;
        LeftArmColorID = 24;
        RightArmColorID = 24;
        LeftLegColorID = 119;
        RightLegColorID = 119;
        HeadColorString = "Color [A=255, R=245, G=205, B=47]";
        TorsoColorString = "Color [A=255, R=13, G=105, B=172]";
        LeftArmColorString = "Color [A=255, R=245, G=205, B=47]";
        RightArmColorString = "Color [A=255, R=245, G=205, B=47]";
        LeftLegColorString = "Color [A=255, R=164, G=189, B=71]";
        RightLegColorString = "Color [A=255, R=164, G=189, B=71]";
        ExtraSelectionIsHat = false;
        ShowHatsInExtra = false;
        CharacterID = "";
    }

    public string Hat1 { get; set; }
    public string Hat2 { get; set; }
    public string Hat3 { get; set; }
    public string Face { get; set; }
    public string Head { get; set; }
    public string TShirt { get; set; }
    public string Shirt { get; set; }
    public string Pants { get; set; }
    public string Icon { get; set; }
    public string Extra { get; set; }
    public int HeadColorID { get; set; }
    public int TorsoColorID { get; set; }
    public int LeftArmColorID { get; set; }
    public int RightArmColorID { get; set; }
    public int LeftLegColorID { get; set; }
    public int RightLegColorID { get; set; }
    public string HeadColorString { get; set; }
    public string TorsoColorString { get; set; }
    public string LeftArmColorString { get; set; }
    public string RightArmColorString { get; set; }
    public string LeftLegColorString { get; set; }
    public string RightLegColorString { get; set; }
    public bool ExtraSelectionIsHat { get; set; }
    public bool ShowHatsInExtra { get; set; }
    public string CharacterID { get; set; }
}

public class ProgramInfo
{
    public ProgramInfo()
    {

    }
}

public class ReShadeConfig
{
    public ReShadeConfig()
    {

    }
}
