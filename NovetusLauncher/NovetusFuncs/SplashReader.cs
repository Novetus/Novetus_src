/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:04 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;
using System.IO;

public static class SplashReader
{
	private static string RandomSplash()
	{
		string[] splashes = File.ReadAllLines(GlobalVars.ConfigDir + "\\splashes.txt");
		string splash = "";
			
		try {
			splash = splashes[new CryptoRandom().Next(0, splashes.Length - 1)];
		} catch (Exception) when (!Env.Debugging) {
			try {
				splash = splashes[0];
			} catch (Exception) when (!Env.Debugging) {
				splash = "missingno";
				return splash;
			}
		}
    		
		string formattedsplash = splash.Replace("%name%", GlobalVars.PlayerName);
    		
		return formattedsplash;
	}
		
	private static bool IsTheSameDay(DateTime date1, DateTime date2)
	{
		return (date1.Month == date2.Month && date1.Day == date2.Day);
	}
		
	public static string GetSplash()
	{
		DateTime today = DateTime.Now;
		string splash = "";
			
		if (IsTheSameDay(today, new DateTime(today.Year, 12, 24)) || IsTheSameDay(today, new DateTime(today.Year, 12, 25))) {
			splash = "Merry Christmas!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 12, 31)) || IsTheSameDay(today, new DateTime(today.Year, 1, 1))) {
			splash = "Happy New Year!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 10, 31))) {
			splash = "Happy Halloween!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 6, 10))) {
			splash = "Happy Birthday, Bitl!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 8, 27))) {
			splash = "Happy Birthday, ROBLOX!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 10, 27))) {
			splash = "Happy Birthday, Novetus!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 2, 15))) {
			splash = "Happy Birthday, Carrot!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 6, 14))) {
			splash = "Happy Birthday, MAO!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 9, 15))) {
			splash = "Happy Birthday, Coke!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 5, 17))) {
			splash = "Happy Birthday, TheLivingBee!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 10, 9))) {
			splash = "Happy Leif Erikson Day! HINGA DINGA DURGEN!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 10, 10))) {
			splash = "I used to wonder what friendship could be!";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 4, 20))) {
			splash = "4/20 lol";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 4, 27))) {
			splash = "fluttershy is best pone";
		} else if (IsTheSameDay(today, new DateTime(today.Year, 2, 11))) {
			splash = "RIP Erik Cassel";
		} else {
			splash = RandomSplash();
		}
			
		return splash;
	}
}