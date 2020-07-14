#region Usings
using System;
using System.IO;
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

    public static string GetSplash()
    {
        DateTime today = DateTime.Now;
        string splash = "";

        switch (today)
        {
            case DateTime christmaseve when christmaseve.Month.Equals(12) && christmaseve.Day.Equals(24):
            case DateTime christmasday when christmasday.Month.Equals(12) && christmasday.Day.Equals(25):
                splash = "Merry Christmas!";
                break;
            case DateTime newyearseve when newyearseve.Month.Equals(12) && newyearseve.Day.Equals(31):
            case DateTime newyearsday when newyearsday.Month.Equals(1) && newyearsday.Day.Equals(1):
                splash = "Happy New Year!";
                break;
            case DateTime halloween when halloween.Month.Equals(10) && halloween.Day.Equals(31):
                splash = "Happy Halloween!";
                break;
            case DateTime bitlbirthday when bitlbirthday.Month.Equals(6) && bitlbirthday.Day.Equals(10):
                splash = "Happy Birthday, Bitl!";
                break;
            case DateTime robloxbirthday when robloxbirthday.Month.Equals(8) && robloxbirthday.Day.Equals(27):
                splash = "Happy Birthday, ROBLOX!";
                break;
            case DateTime novetusbirthday when novetusbirthday.Month.Equals(10) && novetusbirthday.Day.Equals(27):
                splash = "Happy Birthday, Novetus!";
                break;
            case DateTime leiferikson when leiferikson.Month.Equals(10) && leiferikson.Day.Equals(9):
                splash = "Happy Leif Erikson Day! HINGA DINGA DURGEN!";
                break;
            case DateTime smokeweedeveryday when smokeweedeveryday.Month.Equals(4) && smokeweedeveryday.Day.Equals(20):
                CryptoRandom random = new CryptoRandom();
                if (random.Next(0, 1) == 1)
                {
                    splash = "smoke weed every day";
                }
                else
                {
                    splash = "4/20 lol";
                }
                break;
            case DateTime erikismyhero when erikismyhero.Month.Equals(2) && erikismyhero.Day.Equals(11):
                splash = "RIP Erik Cassel";
                break;
            default:
                splash = RandomSplash();
                break;
        }

        return splash;
    }
}
#endregion
