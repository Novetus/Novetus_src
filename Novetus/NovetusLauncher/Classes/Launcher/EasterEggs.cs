using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#region Special Splash Definition
public class SpecialSplash
{
    public SpecialSplash(string text)
    {
        if (text.Contains('|'))
        {
            string[] subs = text.Split('|');
            SplashText = subs[0];
            string date = subs[1];
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

    //text
    public string SplashText { get; set; }
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

#region Special Names Definition
public class SpecialName
{
    public SpecialName(string text)
    {
        if (text.Contains('|'))
        {
            string[] subs = text.Split('|');
            NameText = subs[0];
            NameID = Convert.ToInt32(subs[1]);
        }
    }

    //text
    public string NameText { get; set; }
    //id
    public int NameID { get; set; }
}
#endregion
