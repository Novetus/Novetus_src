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

        switch (today.DayOfWeek)
        {
            case DayOfWeek.Thursday:
                CryptoRandom random = new CryptoRandom();
                int randnum = random.Next(0, 2);
                if (randnum == 1)
                {
                    splash = "Happy Out-of-Touch Thursday!";
                }
                else if (randnum == 2)
                {
                    splash = "You're out of touch, I'm out of time!";
                }
                else
                {
                    splash = "But I'm out of my head when you're not around!";
                }
                goto End;
            default:
                break;
        }

        switch (today.Month)
        {
            case 1:
                if (today.Day.Equals(1))
                {
                    splash = "Happy New Year!";
                }
                else
                {
                    goto default;
                }
                break;
            case 2:
                if (today.Day.Equals(11))
                {
                    splash = "RIP Erik Cassel";
                }
                else
                {
                    goto default;
                }
                break;
            case 4:
                if (today.Day.Equals(20))
                {
                    CryptoRandom random = new CryptoRandom();
                    int randnum = random.Next(0, 1);
                    if (randnum == 1)
                    {
                        splash = "smoke weed every day";
                    }
                    else
                    {
                        splash = "4/20 lol";
                    }
                }
                else
                {
                    goto default;
                }
                break;
            case 6:
                if (today.Day.Equals(10))
                {
                    splash = "Happy Birthday, Bitl!";
                    break;
                }
                else
                {
                    CryptoRandom random2 = new CryptoRandom();
                    int chance = (today.Day > 15) ? 1 : 2;
                    int randnum2 = (today.Day > 7) ? random2.Next(0, chance) : 1;
                    if (randnum2 > 0)
                    {
                        splash = "Happy Pride Month!";
                        break;
                    }
                    else
                    {
                        goto default;
                    }
                }
            case 9:
                if (today.Day.Equals(1))
                {
                    splash = "Happy Birthday, Roblox!";
                }
                else
                {
                    goto default;
                }
                break;
            case 10:
                if (today.Day.Equals(9))
                {
                    splash = "Happy Leif Erikson Day! HINGA DINGA DURGEN!";
                }
                else if (today.Day.Equals(27))
                {
                    splash = "Happy Birthday, Novetus!";
                }
                else if (today.Day.Equals(31))
                {
                    splash = "Happy Halloween!";
                }
                else
                {
                    goto default;
                }
                break;
            case 12:
                if (today.Day.Equals(24) || today.Day.Equals(25))
                {
                    splash = "Merry Christmas!";
                }
                else if (today.Day.Equals(31))
                {
                    splash = "Happy New Year!";
                }
                else
                {
                    goto default;
                }
                break;
            default:
                splash = RandomSplash();
                break;
        }

End:
        return splash;
    }
}
#endregion
