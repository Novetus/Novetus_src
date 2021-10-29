#region Usings
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#endregion

#region Splash Definition
public class Splash
{
    public Splash(string text)
    {
        if (text.Contains('|'))
        {
            string[] subs = text.Split('|');
            SplashText = subs[0];
            SplashContext = subs[1];

        }
        else
        {
            SplashText = text;
            SplashContext = "";
        }
    }

    //text
    public string SplashText { get; set; }
    //context
    public string SplashContext { get; set; }
}
#endregion

#region Special Splash Definition
public class SpecialSplash : Splash
{
    public SpecialSplash(string text) : base(text)
    {
        if (text.Contains('|'))
        {
            string[] subs = text.Split('|');
            int index = 2;
            string date = "";
            if (index >= 0 && index < subs.Length)
            {
                date = subs[index];
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
            return new DateTime(DateTime.Now.Year, Convert.ToInt32(subs[0]), Convert.ToInt32(subs[1]), CultureInfo.InvariantCulture.Calendar);
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
    //date we should start appearing
    public DateTime? SplashFirstAppearanceDate { get; set; }
    //date we should stop appearing
    public DateTime? SplashEndAppearanceDate { get; set; }
    public DateTime? SplashDateStopAppearingAllTheTime { get; set; }
    public DateTime? SplashDateStartToAppearLess { get; set; }
    //weekdays.
    public DayOfWeek? SplashWeekday { get; set; }
}
#endregion

#region Splash Reader
public static class SplashReader
{
    private static Splash RandomSplash()
    {
        CryptoRandom random = new CryptoRandom();
        Splash missingsplash = new Splash("missingno|No Splashes Found.");
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
                bool checkStylishSplash = true;
                Splash generatedSplash = splashes[random.Next(0, splashes.Count)];
                while (checkStylishSplash)
                {
                    if (generatedSplash.SplashText.Contains("[stylish]"))
                    {
                        if (GlobalVars.UserConfiguration.LauncherStyle == Settings.Style.Stylish)
                        {
                            generatedSplash.SplashText = generatedSplash.SplashText.Replace("[stylish]", "");
                            splash = generatedSplash;
                            checkStylishSplash = false;
                        }
                        else
                        {
                            generatedSplash = splashes[random.Next(0, splashes.Count)];
                        }
                    }
                    else if (generatedSplash.SplashText.Contains("[normal]"))
                    {
                        if (GlobalVars.UserConfiguration.LauncherStyle != Settings.Style.Stylish)
                        {
                            generatedSplash.SplashText = generatedSplash.SplashText.Replace("[normal]", "");
                            splash = generatedSplash;
                            checkStylishSplash = false;
                        }
                        else
                        {
                            generatedSplash = splashes[random.Next(0, splashes.Count)];
                        }
                    }
                    else
                    {
                        splash = generatedSplash;
                        checkStylishSplash = false;
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);

                try
                {
                    splash = splashes[0];
                }
                catch (Exception ex2)
                {
                    GlobalFuncs.LogExceptions(ex2);
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
            GlobalFuncs.LogExceptions(ex);
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
        Splash missingsplash = new Splash("missingno|No Splashes Found.");
        Splash returnsplash = missingsplash;
        DateTime now = DateTime.Now;

        if (GlobalVars.UserConfiguration.InitialBootup)
        {
            returnsplash = new Splash("Welcome to Novetus " + GlobalVars.ProgramInformation.Version + "!|Hi!");
            GlobalVars.UserConfiguration.InitialBootup = false;
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
            return returnsplash;
        }

        string[] splashes = File.ReadAllLines(GlobalPaths.ConfigDir + "\\splashes-special.txt");
        List<SpecialSplash> specialsplashes = new List<SpecialSplash>();
        
        foreach (var splash in splashes)
        {
            specialsplashes.Add(new SpecialSplash(splash));
        }

        foreach (var specialsplash in specialsplashes)
        {
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

    public static Splash GetSplash()
    {
        Splash splash = GetSpecialSplash();
        CryptoRandom random = new CryptoRandom();
        int randchance = random.Next(1, 5);

        if (splash.SplashText == "missingno" || randchance == 5)
        {
            splash = RandomSplash();
        }

        splash.SplashText = EncodeSplashString(splash.SplashText);
        splash.SplashContext = EncodeSplashString(splash.SplashContext);

        return splash;
    }

    public static string EncodeSplashString(string text)
    {
        CryptoRandom random = new CryptoRandom();
        DateTime now = DateTime.Now;

        return text.Replace("%name%", GlobalVars.UserConfiguration.PlayerName)
            .Replace("%randomtext%", SecurityFuncs.RandomString(random.Next(2, (GlobalVars.UserConfiguration.LauncherStyle == Settings.Style.Stylish ? 64 : 32))))
            .Replace("%version%", GlobalVars.ProgramInformation.Version)
            .Replace("%year%", now.Year.ToString())
            .Replace("%nextyear%", (now.Year + 1).ToString())
            .Replace("%day%", now.Day.ToString())
            .Replace("%month%", now.Month.ToString());
    }
}
#endregion
