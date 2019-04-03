﻿using System.Net;
using System.Linq;
using System;
using Newtonsoft.Json.Linq;
using fantasy_hoops.Models;
using fantasy_hoops.Helpers;
using FluentScheduler;
using fantasy_hoops.Services;
using System.Globalization;
using System.Threading.Tasks;

namespace fantasy_hoops.Database
{
    public class StatsSeed
    {

        private static ScoreService _scoreService;

        public static void Initialize(GameContext context)
        {
            if (JobManager.RunningSchedules.Any(s => !s.Name.Equals("statsSeed")))
            {
                JobManager.AddJob(() => Initialize(context),
                s => s.WithName("statsSeed")
                .ToRunOnceIn(30)
                .Seconds());
                return;
            }

            _scoreService = new ScoreService();
            Task.Run(() => Calculate(context)).Wait();
        }

        private static JObject GetBoxscore(string url)
        {
            HttpWebResponse webResponse = CommonFunctions.GetResponse(url);
            string apiResponse = CommonFunctions.ResponseToString(webResponse);
            JObject json = JObject.Parse(apiResponse);
            return json;
        }

        private static bool IsFinished(GameContext context, JArray games)
        {
            if (games.Any(g => (int)g["statusNum"] != 3 || (bool)g["isGameActivated"]))
            {
                JobManager.AddJob(() => Initialize(context),
                s => s.WithName("statsSeed")
                .ToRunOnceIn(5)
                .Minutes());
                return false;
            }
            return true;
        }

        private static async Task Calculate(GameContext context)
        {
            string gameDate = CommonFunctions.UTCToEastern(NextGame.PREVIOUS_GAME).ToString("yyyyMMdd");
            JArray games = CommonFunctions.GetGames(gameDate);
            int countOfActivatedGames = 0;
            foreach (JObject game in games)
            {
                if (!(bool)game["isGameActivated"] && (int)game["statusNum"] != 3)
                    continue;

                countOfActivatedGames++;
                string bsUrl = "http://data.nba.net/10s/prod/v1/" + gameDate + "/" + game["gameId"] + "_boxscore.json";
                JObject boxscore = GetBoxscore(bsUrl);
                if (boxscore["stats"] == null)
                    continue;
                int hTeam = (int)boxscore["basicGameData"]["hTeam"]["teamId"];
                int vTeam = (int)boxscore["basicGameData"]["vTeam"]["teamId"];
                DateTime date = DateTime.ParseExact((string)boxscore["basicGameData"]["startDateEastern"], "yyyyMMdd", CultureInfo.InvariantCulture);
                var players = boxscore["stats"]["activePlayers"];
                Game gameObj = new Game
                {
                    Date = date,
                    HomeTeamID = hTeam,
                    HomeScore = (int)boxscore["basicGameData"]["hTeam"]["score"],
                    AwayTeamID = vTeam,
                    AwayScore = (int)boxscore["basicGameData"]["vTeam"]["score"]
                };
                context.Games.Add(gameObj);

                foreach (var player in (JArray)players)
                {
                    int oppId;

                    if (!context.Players.Any(x => x.NbaID.Equals((int)player["personId"])))
                        continue;
                    string score = "";
                    string liveToken = (int)game["statusNum"] != 3 ? ";LIVE" : "";
                    if ((int)player["teamId"] == hTeam)
                    {
                        oppId = vTeam;
                        score = (int)boxscore["basicGameData"]["vTeam"]["score"] + "-" + (int)boxscore["basicGameData"]["hTeam"]["score"] + ";vs" + liveToken;
                    }
                    else
                    {
                        oppId = hTeam;
                        score = (int)boxscore["basicGameData"]["vTeam"]["score"] + "-" + (int)boxscore["basicGameData"]["hTeam"]["score"] + ";@" + liveToken;
                    }
                    if ((string)player["min"] == null || ((string)player["min"]).Length == 0)
                        continue;
                    await AddToDatabase(context, player, gameObj.GameID, date, oppId, score);
                }
                await context.SaveChangesAsync();
            }

            if (!IsFinished(context, games))
            {
                int minutesDelay = countOfActivatedGames == 0 ? 5 : 1;
                JobManager.AddJob(() => Initialize(context),
                        s => s.WithName("statsSeed")
                        .ToRunOnceIn(minutesDelay).Minutes());
            }
            else
            {
                JobManager.AddJob(() => UserScoreSeed.Initialize(context),
                                s => s.WithName("userScore")
                                .ToRunNow());
            }
        }

        private static async Task AddToDatabase(GameContext context, JToken player, int gameID, DateTime date, int oppId, string score)
        {
            Stats statsObj = new Stats
            {
                GameID = gameID,
                Date = date,
                OppID = oppId,
                Score = score,
                MIN = (string)player["min"],
                FGM = (int)player["fgm"],
                FGA = (int)player["fga"],
                FGP = (double)player["fgp"],
                TPM = (int)player["tpm"],
                TPA = (int)player["tpa"],
                TPP = (double)player["tpp"],
                FTM = (int)player["ftm"],
                FTA = (int)player["fta"],
                FTP = (double)player["ftp"],
                DREB = (int)player["defReb"],
                OREB = (int)player["offReb"],
                TREB = (int)player["totReb"],
                AST = (int)player["assists"],
                BLK = (int)player["blocks"],
                STL = (int)player["steals"],
                FLS = (int)player["pFouls"],
                TOV = (int)player["turnovers"],
                PTS = (int)player["points"]
            };

            var statsPlayer = context.Players
                .Where(x => x.NbaID == (int)player["personId"])
                .FirstOrDefault();

            if (statsPlayer == null)
                return;

            statsObj.PlayerID = statsPlayer.PlayerID;
            int playerPrice = _scoreService.GetPrice(statsPlayer);

            var dbStats = context.Stats
                    .Where(stats => stats.Date.Equals(date) && stats.PlayerID == statsPlayer.PlayerID)
                    .FirstOrDefault();

            if (dbStats == null)
            {
                statsObj.GS = _scoreService.GetGameScore(statsObj.PTS, statsObj.FGM, statsObj.DREB, statsObj.OREB,
                                statsObj.STL, statsObj.AST, statsObj.BLK, statsObj.FGA, statsObj.FTA - statsObj.FTM,
                                statsObj.FLS, statsObj.TOV);

                statsObj.FP = _scoreService.GetFantasyPoints(statsObj.PTS, statsObj.DREB, statsObj.OREB,
                                statsObj.AST, statsObj.STL, statsObj.BLK, statsObj.TOV);

                statsObj.Price = playerPrice;

                await context.Stats.AddAsync(statsObj);
            }
            else
            {
                dbStats.GameID = gameID;
                dbStats.Score = score;
                dbStats.MIN = statsObj.MIN;
                dbStats.FGM = statsObj.FGM;
                dbStats.FGA = statsObj.FGA;
                dbStats.FGP = statsObj.FGP;
                dbStats.TPM = statsObj.TPM;
                dbStats.TPA = statsObj.TPA;
                dbStats.TPP = statsObj.TPP;
                dbStats.FTM = statsObj.FTM;
                dbStats.FTA = statsObj.FTA;
                dbStats.FTP = statsObj.FTP;
                dbStats.DREB = statsObj.DREB;
                dbStats.OREB = statsObj.OREB;
                dbStats.TREB = statsObj.TREB;
                dbStats.AST = statsObj.AST;
                dbStats.BLK = statsObj.BLK;
                dbStats.STL = statsObj.STL;
                dbStats.FLS = statsObj.FLS;
                dbStats.TOV = statsObj.TOV;
                dbStats.PTS = statsObj.PTS;
                dbStats.GS = _scoreService.GetGameScore(statsObj.PTS, statsObj.FGM, statsObj.DREB, statsObj.OREB,
                                statsObj.STL, statsObj.AST, statsObj.BLK, statsObj.FGA, statsObj.FTA - statsObj.FTM,
                                statsObj.FLS, statsObj.TOV);

                dbStats.FP = _scoreService.GetFantasyPoints(statsObj.PTS, statsObj.DREB, statsObj.OREB,
                                statsObj.AST, statsObj.STL, statsObj.BLK, statsObj.TOV);

                dbStats.Price = playerPrice;
            }
        }
    }
}