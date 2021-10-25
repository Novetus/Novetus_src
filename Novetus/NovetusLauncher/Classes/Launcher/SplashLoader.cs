#region Usings
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#endregion
#region Splash Reader
public static class SplashReader
{
    private static string RandomSplash()
    {
        CryptoRandom random = new CryptoRandom();
        string splash = "";

        try
        {
            string[] splashes = File.ReadAllLines(GlobalPaths.ConfigDir + "\\splashes.txt");

            try
            {
                bool checkStylishSplash = true;
                string generatedSplash = splashes[random.Next(0, splashes.Length - 1)];
                while (checkStylishSplash)
                {
                    if (generatedSplash.Contains("[stylish]"))
                    {
                        if (GlobalVars.UserConfiguration.LauncherStyle == Settings.Style.Stylish)
                        {
                            splash = generatedSplash.Replace("[stylish]", "");
                            checkStylishSplash = false;
                        }
                        else
                        {
                            generatedSplash = splashes[random.Next(0, splashes.Length - 1)];
                        }
                    }
                    else if (generatedSplash.Contains("[normal]"))
                    {
                        if (GlobalVars.UserConfiguration.LauncherStyle != Settings.Style.Stylish)
                        {
                            splash = generatedSplash.Replace("[normal]", "");
                            checkStylishSplash = false;
                        }
                        else
                        {
                            generatedSplash = splashes[random.Next(0, splashes.Length - 1)];
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
                    splash = "missingno";
                    return splash;
                }
            }
        }
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
            splash = "missingno";
            return splash;
        }

        string formattedsplash = splash
            .Replace("%name%", GlobalVars.UserConfiguration.PlayerName)
            .Replace("%randomtext%", SecurityFuncs.RandomString(random.Next(2, (GlobalVars.UserConfiguration.LauncherStyle == Settings.Style.Stylish ? 64 : 32))));

        return formattedsplash;
    }

    private static string GetSpecialSplash()
    {
        string returnsplash = "";

        if (GlobalVars.UserConfiguration.InitialBootup)
        {
            returnsplash = "Welcome to Novetus " + GlobalVars.ProgramInformation.Version + "!";
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
            DateTime now = DateTime.Now;

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
                                returnsplash = specialsplash.SplashText;
                                break;
                            }
                            else
                            {
                                returnsplash = "";
                                break;
                            }
                        }
                        else
                        {
                            returnsplash = specialsplash.SplashText;
                            break;
                        }
                    }
                }
                else
                {
                    if (now == specialsplash.SplashFirstAppearanceDate)
                    {
                        returnsplash = specialsplash.SplashText;
                        break;
                    }
                }
            }
            else if (specialsplash.SplashWeekday != null)
            {
                if (now.DayOfWeek == specialsplash.SplashWeekday)
                {
                    returnsplash = specialsplash.SplashText;
                    break;
                }
            }
        }

        return returnsplash;
    }

    public static string GetSplash()
    {
        string splash = GetSpecialSplash();

        if (string.IsNullOrWhiteSpace(splash))
        {
            splash = RandomSplash();
        }

        return splash;
    }
}
#endregion
