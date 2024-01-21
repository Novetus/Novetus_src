#region Usings
using Novetus.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#endregion

#region Splash Compatibility Definition
public enum SplashCompatibility
{
    None,
    Normal,
    Stylish
}
#endregion

#region Splash Definition
public class Splash
{
    public Splash(string text, bool specialSplashMode = false)
    {
        if (text.Contains('|'))
        {
            TextArray = text.Split('|');
            SplashText = TextArray[0];
            SplashContext = TextArray[1];
            IsSpecialSplash = specialSplashMode;
        }
        else
        {
            SplashText = text;
            SplashContext = "";
        }

        if (SplashText.Contains("[normal]"))
        {
            Compatibility = SplashCompatibility.Normal;
        }
        else if (SplashText.Contains("[stylish]"))
        {
            Compatibility = SplashCompatibility.Stylish;
        }
        else
        {
            Compatibility = SplashCompatibility.None;
        }

        SplashText = DecodeSplashString(SplashText);
        SplashContext = DecodeSplashString(SplashContext);

        if (IsSpecialSplash && text.Contains('|'))
        {
            int index = 2;
            string date = "";
            if (index >= 0 && index < TextArray.Length)
            {
                date = TextArray[index];
            }
            else
            {
                date = SplashContext;
            }

            if (date.Contains('/'))
            {
                if (date.Contains('-'))
                {
                    string[] datesubs = date.Split('-');
                    SplashFirstAppearanceDate = ConvertStringToDate(datesubs[0]);
                    SplashEndAppearanceDate = ConvertStringToDate(datesubs[1]);

                    if (datesubs.ElementAtOrDefault(2) != null && datesubs.ElementAtOrDefault(3) != null)
                    {
                        SplashDateStopAppearingAllTheTime = ConvertStringToDate(datesubs[2]);
                        SplashDateStartToAppearLess = ConvertStringToDate(datesubs[3]);
                    }
                    else
                    {
                        SplashDateStopAppearingAllTheTime = null;
                        SplashDateStartToAppearLess = null;
                    }
                }
                else
                {
                    SplashFirstAppearanceDate = ConvertStringToDate(date);
                    SplashEndAppearanceDate = null;
                    SplashDateStartToAppearLess = null;
                    SplashDateStopAppearingAllTheTime = null;
                }

                SplashWeekday = null;
            }
            else
            {
                SplashWeekday = ConvertStringToDayOfWeek(date);
                SplashFirstAppearanceDate = null;
                SplashEndAppearanceDate = null;
                SplashDateStartToAppearLess = null;
                SplashDateStopAppearingAllTheTime = null;
            }

            if (date == SplashContext)
            {
                SplashContext = "";
            }
        }
    }

    public DateTime ConvertStringToDate(string date)
    {
        if (date.Contains('/'))
        {
            string[] subs = date.Split('/');
            return new DateTime(DateTime.Now.Year, ConvertSafe.ToInt32Safe(subs[0]), ConvertSafe.ToInt32Safe(subs[1]), CultureInfo.InvariantCulture.Calendar);
        }

        return DateTime.Now;
    }

    public DayOfWeek ConvertStringToDayOfWeek(string dayofweek)
    {
        DayOfWeek weekday = DayOfWeek.Sunday;

        switch (dayofweek)
        {
            case string monday when string.Compare(monday, "monday", true, CultureInfo.InvariantCulture) == 0:
                weekday = DayOfWeek.Monday;
                break;
            case string tuesday when string.Compare(tuesday, "tuesday", true, CultureInfo.InvariantCulture) == 0:
                weekday = DayOfWeek.Tuesday;
                break;
            case string wednesday when string.Compare(wednesday, "wednesday", true, CultureInfo.InvariantCulture) == 0:
                weekday = DayOfWeek.Wednesday;
                break;
            case string thursday when string.Compare(thursday, "thursday", true, CultureInfo.InvariantCulture) == 0:
                weekday = DayOfWeek.Thursday;
                break;
            case string friday when string.Compare(friday, "friday", true, CultureInfo.InvariantCulture) == 0:
                weekday = DayOfWeek.Friday;
                break;
            case string saturday when string.Compare(saturday, "saturday", true, CultureInfo.InvariantCulture) == 0:
                weekday = DayOfWeek.Saturday;
                break;
            default:
                break;
        }

        return weekday;
    }

    public static string DecodeSplashString(string text)
    {
        CryptoRandom random = new CryptoRandom();
        DateTime now = DateTime.Now;

        return text.Replace("%name%", GlobalVars.UserConfiguration.ReadSetting("PlayerName"))
            .Replace("%randomtext%", NovetusFuncs.RandomString(random.Next(2, (GlobalVars.UserConfiguration.ReadSettingInt("LauncherStyle") == (int)Settings.Style.Stylish ? 64 : 32))))
            .Replace("%version%", GlobalVars.ProgramInformation.Version)
            .Replace("%year%", now.Year.ToString())
            .Replace("%day%", now.Day.ToString())
            .Replace("%month%", now.Month.ToString())
            .Replace("%nextyear%", (now.Year + 1).ToString())
            .Replace("%newline%", "\n")
            .Replace("%branch%", GlobalVars.ProgramInformation.Branch)
            .Replace("[normal]", "")
            .Replace("[stylish]", "");
    }

    public string SplashText { get; set; }
    public string SplashContext { get; set; }
    public SplashCompatibility Compatibility { get; set; }
    public bool IsSpecialSplash { get; set; }
    public string[] TextArray { get; }
    public DateTime? SplashFirstAppearanceDate { get; set; }
    public DateTime? SplashEndAppearanceDate { get; set; }
    public DateTime? SplashDateStopAppearingAllTheTime { get; set; }
    public DateTime? SplashDateStartToAppearLess { get; set; }
    public DayOfWeek? SplashWeekday { get; set; }
}
#endregion

#region Splash Reader
public static class SplashReader
{
    private static string missingno = "missingno|No Splashes Found.";

    private static Splash RandomSplash()
    {
        CryptoRandom random = new CryptoRandom();
        Splash missingsplash = new Splash(missingno);
        Splash splash = missingsplash;

        try
        {
            string[] filelines = File.ReadAllLines(GlobalPaths.ConfigDir + "\\splashes.txt");
            List<Splash> splashes = new List<Splash>();

            foreach (var line in filelines)
            {
                splashes.Add(new Splash(line));
            }

            try
            {
                splash = splashes[random.Next(0, splashes.Count)];
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                try
                {
                    splash = splashes[0];
                }
                catch (Exception ex2)
                {
                    Util.LogExceptions(ex2);
                    if (splash.SplashText != missingsplash.SplashText)
                    {
                        splash = missingsplash;
                    }
                    return splash;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
            if (splash.SplashText != missingsplash.SplashText)
            {
                splash = missingsplash;
            }
            return splash;
        }

        return splash;
    }

    private static Splash GetSpecialSplash()
    {
        Splash missingsplash = new Splash(missingno);
        Splash returnsplash = missingsplash;
        DateTime now = DateTime.Now;

        if (GlobalVars.ProgramInformation.InitialBootup)
        {
            returnsplash = new Splash("Welcome to Novetus " + GlobalVars.ProgramInformation.Version + "!|Hi!");
            FileManagement.TurnOffInitialSequence();
            return returnsplash;
        }

        string[] splashes = File.ReadAllLines(GlobalPaths.ConfigDir + "\\splashes-special.txt");
        List<Splash> specialsplashes = new List<Splash>();
        
        foreach (var splash in splashes)
        {
            specialsplashes.Add(new Splash(splash, true));
        }

        foreach (var specialsplash in specialsplashes)
        {
            if (specialsplash.Compatibility == SplashCompatibility.Stylish)
            {
                if (GlobalVars.UserConfiguration.ReadSettingInt("LauncherStyle") != (int)Settings.Style.Stylish)
                {
                    continue;
                }
            }
            else if (specialsplash.Compatibility == SplashCompatibility.Normal)
            {
                if (GlobalVars.UserConfiguration.ReadSettingInt("LauncherStyle") == (int)Settings.Style.Stylish)
                {
                    continue;
                }
            }

            if (specialsplash.SplashFirstAppearanceDate != null)
            {
                if (specialsplash.SplashEndAppearanceDate != null)
                {
                    if (now.IsBetweenTwoDates(specialsplash.SplashFirstAppearanceDate.Value, specialsplash.SplashEndAppearanceDate.Value))
                    {
                        if (specialsplash.SplashDateStopAppearingAllTheTime != null && specialsplash.SplashDateStartToAppearLess != null)
                        {
                            CryptoRandom random2 = new CryptoRandom();
                            int chance = (now.Day > specialsplash.SplashDateStartToAppearLess.Value.Day) ? 1 : 2;
                            int randnum2 = (now.Day > specialsplash.SplashDateStopAppearingAllTheTime.Value.Day) ? random2.Next(0, chance) : 1;
                            if (randnum2 > 0)
                            {
                                returnsplash = specialsplash;
                                break;
                            }
                            else
                            {
                                returnsplash = missingsplash;
                                break;
                            }
                        }
                        else
                        {
                            returnsplash = specialsplash;
                            break;
                        }
                    }
                }
                else
                {
                    if (now.Month == specialsplash.SplashFirstAppearanceDate.Value.Month && 
                        now.Day == specialsplash.SplashFirstAppearanceDate.Value.Day)
                    {
                        returnsplash = specialsplash;
                        break;
                    }
                }
            }
            else if (specialsplash.SplashWeekday != null)
            {
                if (now.DayOfWeek == specialsplash.SplashWeekday)
                {
                    returnsplash = specialsplash;
                    break;
                }
            }
        }

        return returnsplash;
    }

    private static Splash GetSpecialOrNormalSplash()
    {
        Splash splash = GetSpecialSplash();
        CryptoRandom random = new CryptoRandom();
        int randchance = random.Next(1, 5);

        if (splash.SplashText == "missingno" || randchance == 5)
        {
            splash = RandomSplash();
        }

        return splash;
    }

    public static Splash GetSplash()
    {
        Splash generatedSplash = GetSpecialOrNormalSplash();

        bool checkStylishSplash = true;
        while (checkStylishSplash)
        {
            if (generatedSplash.Compatibility == SplashCompatibility.Stylish)
            {
                if (GlobalVars.UserConfiguration.ReadSettingInt("LauncherStyle") == (int)Settings.Style.Stylish)
                {
                    checkStylishSplash = false;
                }
                else
                {
                    generatedSplash = GetSpecialOrNormalSplash();
                }
            }
            else if (generatedSplash.Compatibility == SplashCompatibility.Normal)
            {
                if (GlobalVars.UserConfiguration.ReadSettingInt("LauncherStyle") != (int)Settings.Style.Stylish)
                {
                    checkStylishSplash = false;
                }
                else
                {
                    generatedSplash = GetSpecialOrNormalSplash();
                }
            }
            else
            {
                checkStylishSplash = false;
            }
        }

        return generatedSplash;
    }
}
#endregion
