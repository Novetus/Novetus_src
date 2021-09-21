#region Usings
using System;
using System.Diagnostics;
using System.IO;
#endregion

#region Settings
public class Settings
{
    public enum Mode
    {
        Automatic = 0,
        OpenGLStable = 1,
        OpenGLExperimental = 2,
        DirectX = 3
    }

    public enum Level
    {
        Automatic = 0,
        VeryLow = 1,
        Low = 2,
        Medium = 3,
        High = 4,
        Ultra = 5,
        Custom = 6
    }

    public enum Style
    {
        None = 0,
        Extended = 1,
        Compact = 2,
        Stylish = 3
    }

    public enum ClientLoadOptions
    {
        Client_2007_NoGraphicsOptions = 0,
        Client_2007 = 1,
        Client_2008AndUp = 2,
        Client_2008AndUp_LegacyOpenGL = 3,
        Client_2008AndUp_QualityLevel21 = 4,
        Client_2008AndUp_NoGraphicsOptions = 5,
        Client_2008AndUp_ForceAutomatic = 6,
        Client_2008AndUp_ForceAutomaticQL21 = 7,
        Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL = 8
    }

    public static ClientLoadOptions GetClientLoadOptionsForBool(bool level)
    {
        switch (level)
        {
            case false:
                return ClientLoadOptions.Client_2008AndUp;
            default:
                return ClientLoadOptions.Client_2007_NoGraphicsOptions;
        }
    }

    public static string GetPathForClientLoadOptions(ClientLoadOptions level)
    {
        string localAppdataRobloxPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Roblox";
        string appdataRobloxPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Roblox";

        if (!Directory.Exists(localAppdataRobloxPath))
        {
            Directory.CreateDirectory(localAppdataRobloxPath);
        }

        if (!Directory.Exists(appdataRobloxPath))
        {
            Directory.CreateDirectory(appdataRobloxPath);
        }

        switch (level)
        {
            case ClientLoadOptions.Client_2008AndUp_QualityLevel21:
            case ClientLoadOptions.Client_2008AndUp_LegacyOpenGL:
            case ClientLoadOptions.Client_2008AndUp_NoGraphicsOptions:
            case ClientLoadOptions.Client_2008AndUp_ForceAutomatic:
            case ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21:
            case ClientLoadOptions.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
            case ClientLoadOptions.Client_2008AndUp:
                return localAppdataRobloxPath;
            default:
                return appdataRobloxPath;
        }
    }
}
#endregion