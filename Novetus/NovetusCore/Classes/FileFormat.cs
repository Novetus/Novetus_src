#region File Formats

public class FileFormat
{
    #region Client Information
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
            ClientLoadOptions = Settings.GraphicsOptions.ClientLoadOptions.Client_2008AndUp;
            CommandLineArgs = "";
        }

        public bool UsesPlayerName { get; set; }
        public bool UsesID { get; set; }
        public string Description { get; set; }
        public string Warning { get; set; }
        public bool LegacyMode { get; set; }
        public string ClientMD5 { get; set; }
        public string ScriptMD5 { get; set; }
        public bool Fix2007 { get; set; }
        public bool AlreadyHasSecurity { get; set; }
        public Settings.GraphicsOptions.ClientLoadOptions ClientLoadOptions { get; set; }
        public string CommandLineArgs { get; set; }
    }
    #endregion

    #region Configuration
    public class Config
    {
        public Config()
        {
            SelectedClient = "";
            Map = "";
            CloseOnLaunch = false;
            UserID = 0;
            PlayerName = "Player";
            PlayerTripcode = "";
            RobloxPort = 53640;
            PlayerLimit = 12;
            UPnP = false;
            DisabledItemMakerHelp = false;
            DiscordPresence = true;
            MapPath = "";
            MapPathSnip = "";
            GraphicsMode = Settings.GraphicsOptions.Mode.Automatic;
            ReShade = false;
            QualityLevel = Settings.GraphicsOptions.Level.Automatic;
            LauncherStyle = Settings.UIOptions.Style.Extended;
            ReShadeFPSDisplay = false;
            ReShadePerformanceMode = false;
            AssetLocalizerSaveBackups = true;
            AlternateServerIP = "";
            WebServerPort = 40735;
            WebServer = true;
            DisableReshadeDelete = false;
        }

        public string SelectedClient { get; set; }
        public string Map { get; set; }
        public bool CloseOnLaunch { get; set; }
        public int UserID { get; set; }
        public string PlayerName { get; set; }
        public string PlayerTripcode { get; set; }
        public int RobloxPort { get; set; }
        public int PlayerLimit { get; set; }
        public bool UPnP { get; set; }
        public bool DisabledItemMakerHelp { get; set; }
        public bool DiscordPresence { get; set; }
        public string MapPath { get; set; }
        public string MapPathSnip { get; set; }
        public Settings.GraphicsOptions.Mode GraphicsMode { get; set; }
        public bool ReShade { get; set; }
        public Settings.GraphicsOptions.Level QualityLevel { get; set; }
        public Settings.UIOptions.Style LauncherStyle { get; set; }
        public bool ReShadeFPSDisplay { get; set; }
        public bool ReShadePerformanceMode { get; set; }
        public bool AssetLocalizerSaveBackups { get; set; }
        public string AlternateServerIP { get; set; }
        public int WebServerPort { get; set; }
        public bool WebServer { get; set; }
        public bool DisableReshadeDelete { get; set; }
    }
    #endregion

    #region Customization Configuration
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
    #endregion

    #region Program Information
    public class ProgramInfo
    {
        public ProgramInfo()
        {
            Version = "";
            Branch = "";
            DefaultClient = "";
            RegisterClient1 = "";
            RegisterClient2 = "";
            DefaultMap = "";
        }

        public string Version { get; set; }
        public string Branch { get; set; }
        public string DefaultClient { get; set; }
        public string RegisterClient1 { get; set; }
        public string RegisterClient2 { get; set; }
        public string DefaultMap { get; set; }
    }
    #endregion
}
#endregion
