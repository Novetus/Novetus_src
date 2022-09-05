#region File Formats

using System.Diagnostics;

public class FileFormat
{
    #region Client Information
    public class ClientInfo
    {
        public ClientInfo()
        {
            UsesPlayerName = true;
            UsesID = true;
            Description = "";
            Warning = "";
            LegacyMode = false;
            ClientMD5 = "";
            ScriptMD5 = "";
            Fix2007 = false;
            AlreadyHasSecurity = false;
            ClientLoadOptions = Settings.ClientLoadOptions.Client_2008AndUp;
            SeperateFolders = false;
            UsesCustomClientEXEName = false;
            CustomClientEXEName = "";
            CommandLineArgs = "%args%";
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
        public bool SeperateFolders { get; set; }
        public bool UsesCustomClientEXEName { get; set; }
        public string CustomClientEXEName { get; set; }
        public Settings.ClientLoadOptions ClientLoadOptions { get; set; }
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
            RobloxPort = 53640;
            PlayerLimit = 12;
            UPnP = false;
            DisabledAssetSDKHelp = false;
            DiscordPresence = true;
            MapPath = "";
            MapPathSnip = "";
            GraphicsMode = Settings.Mode.Automatic;
            ReShade = false;
            QualityLevel = Settings.Level.Automatic;
            LauncherStyle = Settings.Style.Stylish;
            ReShadeFPSDisplay = false;
            ReShadePerformanceMode = false;
            AssetSDKFixerSaveBackups = true;
            AlternateServerIP = "";
            DisableReshadeDelete = false;
            ShowServerNotifications = false;
            ServerBrowserServerName = "Novetus";
            ServerBrowserServerAddress = "localhost";
            Priority = ProcessPriorityClass.RealTime;
            FirstServerLaunch = true;
            NewGUI = false;
            URIQuickConfigure = true;
            BootstrapperShowUI = true;
        }

        public string SelectedClient { get; set; }
        public string Map { get; set; }
        public bool CloseOnLaunch { get; set; }
        public int UserID { get; set; }
        public string PlayerName { get; set; }
        public int RobloxPort { get; set; }
        public int PlayerLimit { get; set; }
        public bool UPnP { get; set; }
        public bool DisabledAssetSDKHelp { get; set; }
        public bool DiscordPresence { get; set; }
        public string MapPath { get; set; }
        public string MapPathSnip { get; set; }
        public Settings.Mode GraphicsMode { get; set; }
        public bool ReShade { get; set; }
        public Settings.Level QualityLevel { get; set; }
        public Settings.Style LauncherStyle { get; set; }
        public bool ReShadeFPSDisplay { get; set; }
        public bool ReShadePerformanceMode { get; set; }
        public bool AssetSDKFixerSaveBackups { get; set; }
        public string AlternateServerIP { get; set; }
        public bool DisableReshadeDelete { get; set; }
        public bool ShowServerNotifications { get; set; }
        public string ServerBrowserServerName { get; set; }
        public string ServerBrowserServerAddress { get; set; }
        public ProcessPriorityClass Priority { get; set; }
        public bool FirstServerLaunch { get; set; }
        public bool NewGUI { get; set; }
        public bool URIQuickConfigure { get; set; }
        public bool BootstrapperShowUI { get; set; }
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
            IsLite = false;
            InitialBootup = true;
        }

        public string Version { get; set; }
        public string Branch { get; set; }
        public string DefaultClient { get; set; }
        public string RegisterClient1 { get; set; }
        public string RegisterClient2 { get; set; }
        public string DefaultMap { get; set; }
        public bool IsLite { get; set; }
        public bool InitialBootup { get; set; }
    }
    #endregion
}
#endregion
