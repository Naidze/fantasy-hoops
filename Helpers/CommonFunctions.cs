﻿using fantasy_hoops.Database;
using fantasy_hoops.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace fantasy_hoops.Helpers
{
	public class CommonFunctions
	{
		public static string SEASON_YEAR = GetSeasonYear();

		public static string LineupPositionsOrder = "PG|SG|SF|PF|C";

		public static DateTime UTCToEastern(DateTime UTC)
		{
			TimeZoneInfo eastern = TimeZoneInfo.FindSystemTimeZoneById(Environment.GetEnvironmentVariable("TIME_ZONE_ID"));
			return TimeZoneInfo.ConvertTimeFromUtc(UTC, eastern);
		}

		public static DateTime EasternToUTC(DateTime eastern)
		{
			TimeZoneInfo.ConvertTimeBySystemTimeZoneId(eastern, TimeZoneInfo.Local.Id);
			TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById((Environment.GetEnvironmentVariable("TIME_ZONE_ID")));
			return TimeZoneInfo.ConvertTimeToUtc(eastern, easternZone);
		}

		public static HttpWebResponse GetResponse(string url)
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "GET";
				request.KeepAlive = true;
				request.ContentType = "application/json";
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				return response;

			}
			catch (WebException)
			{
				return null;
			}
		}

		public static string ResponseToString(HttpWebResponse response)
		{
			string resp = "";
			using (StreamReader sr = new StreamReader(response.GetResponseStream()))
			{
				resp = sr.ReadToEnd();
			}
			return resp;
		}

		public static JArray GetGames(string date)
		{
			string url = "http://data.nba.net/10s/prod/v1/" + date + "/scoreboard.json";
			HttpWebResponse webResponse = GetResponse(url);
			if (webResponse == null)
				return null;
			string apiResponse = ResponseToString(webResponse);
			JObject json = JObject.Parse(apiResponse);
			return (JArray)json["games"];
		}

		public static int GetNextGame(int playerId)
		{
			string url = "http://data.nba.net/v2015/json/mobile_teams/nba/" + GetSeasonYear() + "/players/playercard_" + playerId + "_02.json";
			HttpWebResponse webResponse = GetResponse(url);
			if (webResponse == null)
				return -1;
			string apiResponse = ResponseToString(webResponse);
			JObject json = JObject.Parse(apiResponse);
			return (int)json["pl"]["ng"]["otid"];
		}

		public static int DaysInMonth()
		{
			int year = UTCToEastern(DateTime.UtcNow).Year;
			int month = UTCToEastern(DateTime.UtcNow).Month;
			return DateTime.DaysInMonth(year, month);
		}

		// Leaderboards and weekly scores
		public static DateTime GetDate(string type)
		{
			DateTime easternDate = UTCToEastern(DateTime.UtcNow);
			int dayOfWeek = (int)UTCToEastern(DateTime.UtcNow).DayOfWeek;
			int dayOfMonth = UTCToEastern(DateTime.UtcNow).Day;

			if (type.Equals("weekly"))
			{
				int dayOffset = dayOfWeek == 1
						? 7
						: dayOfWeek == 0 ? 6 : dayOfWeek - 1;

				return easternDate.AddDays(-dayOffset).Date;
			}
			if (type.Equals("monthly"))
			{
				int dayOffset = dayOfMonth == 1 ? DaysInMonth() : dayOfMonth - 1;
				return easternDate.AddDays(-dayOffset).Date;
			}
			return UTCToEastern(NextGame.PREVIOUS_GAME).Date;
		}

		public static string GetSeasonYear()
		{
			string url = "http://data.nba.net/10s/prod/v1/today.json";
			HttpWebResponse webResponse = GetResponse(url);
			if (webResponse == null)
				return null;
			string apiResponse = ResponseToString(webResponse);
			JObject json = JObject.Parse(apiResponse);
			return (string)json["seasonScheduleYear"];
		}

		public static int GetIso8601WeekOfYear(DateTime time)
		{
			// Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
			// be the same week# as whatever Thursday, Friday or Saturday are,
			// and we always get those right
			DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
			if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
			{
				time = time.AddDays(3);
			}

			// Return the week of our adjusted day
			return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
		}

        public static Team GetUnknownTeam(GameContext _context)
        {
            return _context.Teams.Where(t => t.NbaID == 0).FirstOrDefault();
        }
        public async static Task<string> GetImageAsBase64Url(string url)
        {
            using (var client = new HttpClient())
            {
                var bytes = await client.GetByteArrayAsync(url);
                return "image/jpeg;base64,     " + Convert.ToBase64String(bytes);
            }
        }
    }
}
