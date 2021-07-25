#region Usings
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
#endregion
#region Splash Reader

public static class SplashReader
{
    private static string RandomSplash()
    {
        string[] splashes = File.ReadAllLines(GlobalPaths.ConfigDir + "\\splashes.txt");
        string splash = "";

        try
        {
            splash = splashes[new CryptoRandom().Next(0, splashes.Length - 1)];
        }
        catch (Exception)
        {
            try
            {
                splash = splashes[0];
            }
            catch (Exception)
            {
                splash = "missingno";
                return splash;
            }
        }

        CryptoRandom random = new CryptoRandom();

        string formattedsplash = splash
            .Replace("%name%", GlobalVars.UserConfiguration.PlayerName)
            .Replace("%randomtext%", SecurityFuncs.RandomString(random.Next(2, 32)));

        return formattedsplash;
    }

    private static string GetSpecialSplash()
    {
        string[] splashes = File.ReadAllLines(GlobalPaths.ConfigDir + "\\splashes-special.txt");
        string returnsplash = "";
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
