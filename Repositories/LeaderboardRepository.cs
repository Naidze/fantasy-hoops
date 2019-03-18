﻿using fantasy_hoops.Database;
using fantasy_hoops.Helpers;
using fantasy_hoops.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace fantasy_hoops.Repositories
{
    public class LeaderboardRepository : ILeaderboardRepository
    {

        private readonly GameContext _context;

        public LeaderboardRepository(GameContext context)
        {
            _context = context;
        }

        public IEnumerable<object> GetPlayerLeaderboard(int from, int limit, string type)
        {
            DateTime date = CommonFunctions.GetDate(type);

            return _context.Players
                .Select(x => new
                {
                    x.PlayerID,
                    x.NbaID,
                    x.FullName,
                    x.FirstName,
                    x.LastName,
                    x.AbbrName,
                    x.Position,
                    teamColor = x.Team.Color,
                    FP = x.Stats
                        .Where(y => y.Date >= date)
                        .Select(y => y.FP).Sum()
                })
                .OrderByDescending(x => x.FP)
                .Skip(from)
                .Take(limit);
        }

        public IEnumerable<object> GetUserLeaderboard(int from, int limit, string type, string date)
        {
            date = date.Length == 0
                ? DateTime.UtcNow < NextGame.PREVIOUS_LAST_GAME
                    ? NextGame.PREVIOUS_GAME.AddDays(-1).ToString("yyyyMMdd")
                    : NextGame.PREVIOUS_GAME.ToString("yyyyMMdd")
                : date;

            DateTime dateTime = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
            if (type.Equals("daily"))
            {
                var dailyStats = _context.Stats.Where(stats => stats.Date >= dateTime.Date && stats.Date <= dateTime.AddDays(1).Date);

                return _context.UserLineups
                .Where(lineup => lineup.IsCalculated && lineup.Date >= dateTime.Date && lineup.Date <= dateTime.AddDays(1).Date)
                .OrderByDescending(lineup => lineup.FP)
                .Select(lineup => new
                {
                    lineup.UserID,
                    lineup.User.UserName,
                    longDate = lineup.Date.ToString("yyyy-MM-dd"),
                    shortDate = lineup.Date.ToString("MMM. dd"),
                    lineup.Date,
                    lineup.FP,
                    lineup = _context.Players
                        .Where(player =>
                            player.PlayerID == lineup.PgID
                            || player.PlayerID == lineup.SgID
                            || player.PlayerID == lineup.SfID
                            || player.PlayerID == lineup.PfID
                            || player.PlayerID == lineup.CID)
                        .Select(player => new
                        {
                            player.NbaID,
                            player.Position,
                            teamColor = player.Team.Color,
                            player.FullName,
                            player.FirstName,
                            player.LastName,
                            player.AbbrName,
                            FP = dailyStats.Where(stats => stats.PlayerID == player.PlayerID)
                                .Select(stats => stats.FP).FirstOrDefault()
                        }).OrderBy(p => Array.IndexOf(CommonFunctions.PlayersOrder, p.Position))
                })
                .Skip(from)
                .Take(limit);
            }
            return _context.UserLineups
                .Where(lineup => lineup.IsCalculated && lineup.Date >= CommonFunctions.GetDate(type))
                .GroupBy(lineup => lineup.UserID)
                .Select(lineup => new
                {
                    lineup.First().UserID,
                    lineup.First().User.UserName,
                    longDate = lineup.First().Date.ToString("yyyy-MM-dd"),
                    shortDate = lineup.First().Date.ToString("MMM. dd"),
                    lineup.First().Date,
                    FP = Math.Round(lineup.Sum(res => res.FP), 1)
                })
                .OrderByDescending(lineup => lineup.FP)
                .Skip(from)
                .Take(limit);
        }

        public IEnumerable<object> GetFriendsLeaderboard(string id, int from, int limit, string type, string date)
        {
            date = date.Length == 0
                ? DateTime.UtcNow < NextGame.PREVIOUS_LAST_GAME
                    ? NextGame.PREVIOUS_GAME.AddDays(-1).ToString("yyyyMMdd")
                    : NextGame.PREVIOUS_GAME.ToString("yyyyMMdd")
                : date;

            var loggedInUser = _context.Users.Where(u => u.Id.Equals(id));
            var friendsOnly = _context.FriendRequests
                .Include(u => u.Sender)
                .Where(f => f.ReceiverID.Equals(loggedInUser.FirstOrDefault().Id) && f.Status == Models.RequestStatus.ACCEPTED)
                .Select(u => u.Sender)
                .Union(_context.FriendRequests
                .Include(u => u.Receiver)
                .Where(f => f.SenderID.Equals(loggedInUser.FirstOrDefault().Id) && f.Status == Models.RequestStatus.ACCEPTED)
                .Select(u => u.Receiver)).Concat(loggedInUser);

            //DateTime date = CommonFunctions.GetDate(type);
            DateTime dateTime = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);

            if (type.Equals("daily"))
            {
                var dailyStats = _context.Stats.Where(stats => stats.Date >= dateTime.Date && stats.Date <= dateTime.AddDays(1).Date);

                return friendsOnly
                    .SelectMany(user => user.UserLineups)
                    .Where(lineup => lineup.Date >= dateTime && lineup.Date <= dateTime.AddDays(1).Date && lineup.IsCalculated)
                .Select(lineup => new
                {
                    lineup.UserID,
                    lineup.User.UserName,
                    longDate = lineup.Date.ToString("yyyy-MM-dd"),
                    shortDate = lineup.Date.ToString("MMM. dd"),
                    lineup.Date,
                    lineup.FP,
                    lineup = _context.Players
                        .Where(player =>
                            player.PlayerID == lineup.PgID
                            || player.PlayerID == lineup.SgID
                            || player.PlayerID == lineup.SfID
                            || player.PlayerID == lineup.PfID
                            || player.PlayerID == lineup.CID)
                        .Select(player => new
                        {
                            player.NbaID,
                            player.Position,
                            teamColor = player.Team.Color,
                            player.FullName,
                            player.FirstName,
                            player.LastName,
                            player.AbbrName,
                            FP = player.Stats.Where(stats => stats.Date.Date == lineup.Date.Date)
                                .Select(stats => stats.FP).FirstOrDefault()
                        }).OrderBy(p => Array.IndexOf(CommonFunctions.PlayersOrder, p.Position))
                })
                .Where(x => x.lineup.Count() > 0)
                .OrderByDescending(x => x.FP)
                .Skip(from)
                .Take(limit);
            }
            return friendsOnly
                .Select(user => new
                {
                    userID = user.Id,
                    user.UserName,
                    FP = Math.Round(user.UserLineups
                        .Where(lineup => lineup.Date >= CommonFunctions.GetDate(type) && lineup.IsCalculated)
                        .Select(lineup => lineup.FP).Sum(), 1),
                    gamesPlayed = user.UserLineups
                        .Where(lineup => lineup.Date >= CommonFunctions.GetDate(type) && lineup.IsCalculated)
                        .Count()
                })
                .Where(user => user.gamesPlayed > 0)
                .OrderByDescending(lineup => lineup.FP)
                .Skip(from)
                .Take(limit);
        }

        public IQueryable<object> GetSeasonLineups()
        {
            return _context.UserLineups
                .OrderByDescending(lineup => lineup.FP)
                .Take(10)
                .Select(lineup => new
                {
                    lineup.UserID,
                    lineup.User.UserName,
                    longDate = lineup.Date.ToString("yyyy-MM-dd"),
                    shortDate = lineup.Date.ToString("MMM. dd"),
                    lineup.Date,
                    lineup.FP,
                    lineup = _context.Players
                        .Where(player =>
                            player.PlayerID == lineup.PgID
                            || player.PlayerID == lineup.SgID
                            || player.PlayerID == lineup.SfID
                            || player.PlayerID == lineup.PfID
                            || player.PlayerID == lineup.CID)
                        .Select(player => new
                        {
                            player.NbaID,
                            player.Position,
                            teamColor = player.Team.Color,
                            player.FullName,
                            player.FirstName,
                            player.LastName,
                            player.AbbrName,
                            FP = player.Stats.Where(stats => stats.Date.Date == lineup.Date.Date)
                                .Select(stats => stats.FP).FirstOrDefault()
                        }).OrderBy(p => Array.IndexOf(CommonFunctions.PlayersOrder, p.Position))
                });
        }

        public IQueryable<object> GetSeasonPlayers()
        {
            return _context.Stats
                .OrderByDescending(s => s.FP)
                .Take(10)
                .Select(p => new
                {
                    longDate = p.Date.ToString("yyyy-MM-dd"),
                    shortDate = p.Date.ToString("MMM. dd"),
                    p.Player.NbaID,
                    teamColor = p.Player.Team.Color,
                    p.Player.FullName,
                    p.Player.AbbrName,
                    p.FP

                });
        }
    }
}
