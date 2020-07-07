#region Usings
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#endregion

/*
 * change field names for all forms
 * Rewrite client launching into one function.
 * add regions to ALL classes.
 * maybe make enums print out the names in inis instead of the int value?
 */

#region Global Variables
public static class GlobalVars
{
    public static FileFormat.ProgramInfo ProgramInformation = new FileFormat.ProgramInfo();
    public static FileFormat.Config UserConfiguration = new FileFormat.Config();
    public static string IP = "localhost";
    public static string SharedArgs = "";
    public static readonly string ScriptName = "CSMPFunctions";
    public static readonly string ScriptGenName = "CSMPBoot";
    public static SimpleHTTPServer WebServer = null;
    public static bool IsWebServerOn = false;
    public static bool IsSnapshot = false;
    //misc vars
    public static string FullMapPath = "";
    //weebserver
    public static int WebServerPort = 40735;
    public static string LocalWebServerURI = "http://localhost:" + (WebServerPort).ToString();
    public static string WebServerURI = "http://" + IP + ":" + (WebServerPort).ToString();
    //config name
    public static readonly string ConfigName = "config.ini";
    public static string ConfigNameCustomization = "config_customization.ini";
    public static readonly string InfoName = "info.ini";
    //client shit
    public static FileFormat.ClientInfo SelectedClientInfo = new FileFormat.ClientInfo();
    public static string AddonScriptPath = "";
    //charcustom
    public static FileFormat.CustomizationConfig UserCustomization = new FileFormat.CustomizationConfig();
    public static string loadtext = "";
    public static string sololoadtext = "";
    //color menu.
    public static bool AdminMode = false;
    public static string important = "";
    //discord
    public static IDiscordRPC.RichPresence presence;
    public static string appid = "505955125727330324";
    public static string imagekey_large = "novetus_large";
    public static string image_ingame = "ingame_small";
    public static string image_inlauncher = "inlauncher_small";
    public static string image_instudio = "instudio_small";
    public static string image_incustomization = "incustomization_small";

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

    //task.delay is only available on net 4.5.......
    public static async void Delay(int miliseconds)
    {
        await TaskEx.Delay(miliseconds);
    }
}
#endregion
