/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

public static class Env
{
#if DEBUG
    public static readonly bool Debugging = true;
#else
    public static readonly bool Debugging = false;
#endif
}

public static class GlobalVars
{
	public static readonly string RootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
	public static readonly string BasePath = RootPath.Replace(@"\", @"\\");
	public static readonly string DataPath = BasePath + @"\\shareddata";
	public static readonly string ConfigDir = BasePath + @"\\config";
    public static readonly string ConfigDirData = ConfigDir + @"\\data";
    public static readonly string ClientDir = BasePath + @"\\clients";
	public static readonly string MapsDir = BasePath + @"\\maps";
    public static readonly string MapsDirBase = "maps";
    public static string MapPath = "";
    public static string MapPathSnip = "";
    public static readonly string BaseGameDir = "rbxasset://../../../";
    public static readonly string SharedDataGameDir = BaseGameDir + "shareddata/";
    public static readonly string DirFonts = "\\fonts";
    public static readonly string DirSounds = "\\sounds";
    public static readonly string DirTextures = "\\textures";
    public static readonly string DirScripts = "\\scripts";

    public static readonly string FontsGameDir = "fonts/";
    public static readonly string SoundsGameDir = "sounds/";
    public static readonly string TexturesGameDir = "textures/";
    public static readonly string ScriptsGameDir = "scripts/";
    //customization
    public static readonly string CustomPlayerDir = DataPath + "\\charcustom";
    public static readonly string hatdir = CustomPlayerDir + "\\hats";
    public static readonly string facedir = CustomPlayerDir + "\\faces";
    public static readonly string headdir = CustomPlayerDir + "\\heads";
    public static readonly string tshirtdir = CustomPlayerDir + "\\tshirts";
    public static readonly string shirtdir = CustomPlayerDir + "\\shirts";
    public static readonly string pantsdir = CustomPlayerDir + "\\pants";
    public static readonly string extradir = CustomPlayerDir + "\\custom";

    public static readonly string CharCustomGameDir = SharedDataGameDir + "charcustom/";
    public static readonly string hatGameDir = CharCustomGameDir + "hats/";
    public static readonly string faceGameDir = CharCustomGameDir + "faces/";
    public static readonly string headGameDir = CharCustomGameDir + "heads/";
    public static readonly string tshirtGameDir = CharCustomGameDir + "tshirts/";
    public static readonly string shirtGameDir = CharCustomGameDir + "shirts/";
    public static readonly string pantsGameDir = CharCustomGameDir + "pants/";
    public static readonly string extraGameDir = CharCustomGameDir + "custom/";

    //item asset dirs
    public static readonly string hatdirFonts = hatdir + DirFonts;
    public static readonly string hatdirTextures = hatdir + DirTextures;
    public static readonly string hatdirSounds = hatdir + DirSounds;
    public static readonly string hatdirScripts = hatdir + DirScripts;
    public static readonly string facedirTextures = facedir + DirTextures;
    public static readonly string headdirFonts = headdir + DirFonts;
    public static readonly string headdirTextures = headdir + DirTextures;
    public static readonly string tshirtdirTextures = tshirtdir + DirTextures;
    public static readonly string shirtdirTextures = shirtdir + DirTextures;
    public static readonly string pantsdirTextures = pantsdir + DirTextures;
    public static readonly string extradirIcons = extradir + "\\icons";

    public static readonly string hatGameDirFonts = hatGameDir + FontsGameDir;
    public static readonly string hatGameDirTextures = hatGameDir + TexturesGameDir;
    public static readonly string hatGameDirSounds = hatGameDir + SoundsGameDir;
    public static readonly string hatGameDirScripts = hatGameDir + ScriptsGameDir;
    public static readonly string faceGameDirTextures = faceGameDir + TexturesGameDir;
    public static readonly string headGameDirFonts = headGameDir + FontsGameDir;
    public static readonly string headGameDirTextures = headGameDir + TexturesGameDir;
    public static readonly string tshirtGameDirTextures = tshirtGameDir + TexturesGameDir;
    public static readonly string shirtGameDirTextures = shirtGameDir + TexturesGameDir;
    public static readonly string pantsGameDirTextures = pantsGameDir + TexturesGameDir;
    //asset cache
    public static readonly string AssetCacheDir = DataPath + "\\assetcache";
    
    public static readonly string AssetCacheDirSky = AssetCacheDir + "\\sky";
    public static readonly string AssetCacheDirFonts = AssetCacheDir + DirFonts;
    public static readonly string AssetCacheDirSounds = AssetCacheDir + DirSounds;
    public static readonly string AssetCacheDirTextures = AssetCacheDir + DirTextures;
    public static readonly string AssetCacheDirTexturesGUI = AssetCacheDirTextures + "\\gui";
    public static readonly string AssetCacheDirScripts = AssetCacheDir + DirScripts;

    public static readonly string AssetCacheGameDir = SharedDataGameDir + "assetcache/";
    public static readonly string AssetCacheFontsGameDir = AssetCacheGameDir + FontsGameDir;
    public static readonly string AssetCacheSkyGameDir = AssetCacheGameDir + "sky/";
    public static readonly string AssetCacheSoundsGameDir = AssetCacheGameDir + SoundsGameDir;
    public static readonly string AssetCacheTexturesGameDir = AssetCacheGameDir + TexturesGameDir;
    public static readonly string AssetCacheTexturesGUIGameDir = AssetCacheTexturesGameDir + "gui/";
    public static readonly string AssetCacheScriptsGameDir = AssetCacheGameDir + ScriptsGameDir;

    //defs
    public static AssetCacheDef Fonts { get { return new AssetCacheDef("SpecialMesh", new string[] { "MeshId", "TextureId" }, new string[] { ".mesh", ".png" }, new string[] { AssetCacheDirFonts, AssetCacheDirTextures }, new string[] { AssetCacheFontsGameDir, AssetCacheTexturesGameDir }); } }
    public static AssetCacheDef Sky { get { return new AssetCacheDef("Sky", new string[] { "SkyboxBk", "SkyboxDn", "SkyboxFt", "SkyboxLf", "SkyboxRt", "SkyboxUp" }, new string[] { ".png" }, new string[] { AssetCacheDirSky }, new string[] { AssetCacheSkyGameDir }); } }
    public static AssetCacheDef Decal { get { return new AssetCacheDef("Decal", new string[] { "Texture" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
    public static AssetCacheDef Texture { get { return new AssetCacheDef("Texture", new string[] { "Texture" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
    public static AssetCacheDef HopperBin { get { return new AssetCacheDef("HopperBin", new string[] { "TextureId" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
    public static AssetCacheDef Tool { get { return new AssetCacheDef("Tool", new string[] { "TextureId" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
    public static AssetCacheDef Sound { get { return new AssetCacheDef("Sound", new string[] { "SoundId" }, new string[] { ".wav" }, new string[] { AssetCacheDirSounds }, new string[] { AssetCacheSoundsGameDir }); } }
    public static AssetCacheDef ImageLabel { get { return new AssetCacheDef("ImageLabel", new string[] { "Image" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
    public static AssetCacheDef Shirt { get { return new AssetCacheDef("Shirt", new string[] { "ShirtTemplate" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
    public static AssetCacheDef ShirtGraphic { get { return new AssetCacheDef("ShirtGraphic", new string[] { "Graphic" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
    public static AssetCacheDef Pants { get { return new AssetCacheDef("Pants", new string[] { "PantsTemplate" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
    public static AssetCacheDef Script { get { return new AssetCacheDef("Script", new string[] { "LinkedSource" }, new string[] { ".lua" }, new string[] { AssetCacheDirScripts }, new string[] { AssetCacheScriptsGameDir }); } }
    public static AssetCacheDef LocalScript { get { return new AssetCacheDef("LocalScript", new string[] { "LinkedSource" }, new string[] { ".lua" }, new string[] { AssetCacheDirScripts }, new string[] { AssetCacheScriptsGameDir }); } }
    //item defs below
    public static AssetCacheDef ItemHatFonts { get { return new AssetCacheDef("SpecialMesh", new string[] { "MeshId", "TextureId" }, new string[] { ".mesh", ".png" }, new string[] { hatdirFonts, hatdirTextures }, new string[] { hatGameDirFonts, hatGameDirTextures }); } }
    public static AssetCacheDef ItemHatSound { get { return new AssetCacheDef("Sound", new string[] { "SoundId" }, new string[] { ".wav" }, new string[] { hatdirSounds }, new string[] { hatGameDirSounds }); } }
    public static AssetCacheDef ItemHatScript { get { return new AssetCacheDef("Script", new string[] { "LinkedSource" }, new string[] { ".lua" }, new string[] { hatdirScripts }, new string[] { hatGameDirScripts }); } }
    public static AssetCacheDef ItemHatLocalScript { get { return new AssetCacheDef("LocalScript", new string[] { "LinkedSource" }, new string[] { ".lua" }, new string[] { hatdirScripts }, new string[] { hatGameDirScripts }); } }
    public static AssetCacheDef ItemHeadFonts { get { return new AssetCacheDef("SpecialMesh", new string[] { "MeshId", "TextureId" }, new string[] { ".mesh", ".png" }, new string[] { headdirFonts, headdirTextures }, new string[] { headGameDirFonts, headGameDirTextures }); } }
    public static AssetCacheDef ItemFaceTexture { get { return new AssetCacheDef("Decal", new string[] { "Texture" }, new string[] { ".png" }, new string[] { facedirTextures }, new string[] { faceGameDirTextures }); } }
    public static AssetCacheDef ItemShirtTexture { get { return new AssetCacheDef("Shirt", new string[] { "ShirtTemplate" }, new string[] { ".png" }, new string[] { shirtdirTextures }, new string[] { shirtGameDirTextures }); } }
    public static AssetCacheDef ItemTShirtTexture { get { return new AssetCacheDef("ShirtGraphic", new string[] { "Graphic" }, new string[] { ".png" }, new string[] { tshirtdirTextures }, new string[] { tshirtGameDirTextures }); } }
    public static AssetCacheDef ItemPantsTexture { get { return new AssetCacheDef("Pants", new string[] { "PantsTemplate" }, new string[] { ".png" }, new string[] { pantsdirTextures }, new string[] { pantsGameDirTextures }); } }

    public static string IP = "localhost";
    public static string Version = "";
	public static string SharedArgs = "";
	public static readonly string ScriptName = "CSMPFunctions";
	public static readonly string ScriptGenName = "CSMPBoot";
	public static SimpleHTTPServer WebServer = null;
	public static bool IsWebServerOn = false;
    public static bool IsSnapshot = false;
    //vars for loader
    public static bool ReadyToLaunch = false;
	//server settings.
	public static bool UPnP = false;
	public static string Map = "";
	public static string FullMapPath = "";
	public static int RobloxPort = 53640;
	public static int DefaultRobloxPort = 53640;
	public static int WebServer_Port = 40735;
	public static int PlayerLimit = 12;
	//player settings
	public static int UserID = 0;
	public static string PlayerName = "Player";
    public static string PlayerTripcode = "";
    //config name
    public static readonly string ConfigName = "config.ini";
    public static string ConfigNameCustomization = "config_customization.ini";
    //launcher settings.
    public static bool CloseOnLaunch = false;
	public static bool LocalPlayMode = false;
	//client shit
	public static string SelectedClient = "";
	public static string DefaultClient = "";
    public static string RegisterClient1 = "";
    public static string RegisterClient2 = "";
    public static string DefaultMap = "";
	public static bool UsesPlayerName = false;
	public static bool UsesID = true;
	public static string SelectedClientDesc = "";
	public static string Warning = "";
	public static bool LegacyMode = false;
	public static string SelectedClientMD5 = "";
	public static string SelectedClientScriptMD5 = "";
	public static bool FixScriptMapMode = false;
	public static bool AlreadyHasSecurity = false;
	public static string CustomArgs = "";
    public static string AddonScriptPath = "";
	//charcustom
	public static string Custom_Hat1ID_Offline = "NoHat.rbxm";
	public static string Custom_Hat2ID_Offline = "NoHat.rbxm";
	public static string Custom_Hat3ID_Offline = "NoHat.rbxm";
	public static string Custom_Face_Offline = "DefaultFace.rbxm";
	public static string Custom_Head_Offline = "DefaultHead.rbxm";
	public static string Custom_T_Shirt_Offline = "NoTShirt.rbxm";
	public static string Custom_Shirt_Offline = "NoShirt.rbxm";
	public static string Custom_Pants_Offline = "NoPants.rbxm";
	public static string Custom_Icon_Offline = "NBC";
	public static int HeadColorID = 24;
	public static int TorsoColorID = 23;
	public static int LeftArmColorID = 24;
	public static int RightArmColorID = 24;
	public static int LeftLegColorID = 119;
	public static int RightLegColorID = 119;
	public static string loadtext = "";
	public static string sololoadtext = "";
	public static string CharacterID = "";
	public static string Custom_Extra = "NoExtra.rbxm";
	public static bool Custom_Extra_ShowHats = false;
	public static bool Custom_Extra_SelectionIsHat = false;
	//color menu.
	public static string ColorMenu_HeadColor = "Color [A=255, R=245, G=205, B=47]";
	public static string ColorMenu_TorsoColor = "Color [A=255, R=13, G=105, B=172]";
	public static string ColorMenu_LeftArmColor = "Color [A=255, R=245, G=205, B=47]";
	public static string ColorMenu_RightArmColor = "Color [A=255, R=245, G=205, B=47]";
	public static string ColorMenu_LeftLegColor = "Color [A=255, R=164, G=189, B=71]";
	public static string ColorMenu_RightLegColor = "Color [A=255, R=164, G=189, B=71]";
	public static bool AdminMode = false;
	public static string important = "";
    //discord
    public static bool DiscordPresence = true;
    public static DiscordRpc.RichPresence presence;
	public static string appid = "505955125727330324";
	public static string imagekey_large = "novetus_large";
    public static string image_ingame = "ingame_small";
    public static string image_inlauncher = "inlauncher_small";
    public static string image_instudio = "instudio_small";
    public static string image_incustomization = "incustomization_small";
    //webserver
    public static string WebServerURI = "http://" + IP + ":" + (WebServer_Port).ToString();
	public static string LocalWebServerURI = "http://localhost:" + (WebServer_Port).ToString();
	public static string WebServer_CustomPlayerDir = WebServerURI + "/charcustom/";
	public static string WebServer_HatDir = WebServer_CustomPlayerDir + "hats/";
	public static string WebServer_FaceDir = WebServer_CustomPlayerDir + "faces/";
	public static string WebServer_HeadDir = WebServer_CustomPlayerDir + "heads/";
	public static string WebServer_TShirtDir = WebServer_CustomPlayerDir + "tshirts/";
	public static string WebServer_ShirtDir = WebServer_CustomPlayerDir + "shirts/";
	public static string WebServer_PantsDir = WebServer_CustomPlayerDir + "pants/";
	public static string WebServer_ExtraDir = WebServer_CustomPlayerDir + "custom/";
	//itemmaker
	public static bool DisabledHelp = false;
    //reshade
    public static bool ReShade = true;
    public static bool ReShadeFPSDisplay = false;
    public static bool ReShadePerformanceMode = false;
    //video
    public static int GraphicsMode = 1;
    public static bool Bevels = true;
    public static bool Shadows = true;
    public static int QualityLevel = 5;

    public static string MultiLine(params string[] args)
	{
		return string.Join(Environment.NewLine, args);
	}

    public static string RemoveEmptyLines(string lines)
    {
        return Regex.Replace(lines, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline).TrimEnd();
    }

    public static bool ProcessExists(int id)
    {
        return Process.GetProcesses().Any(x => x.Id == id);
    }
}