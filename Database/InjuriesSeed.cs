﻿using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using fantasy_hoops.Models;
using Microsoft.EntityFrameworkCore;
using fantasy_hoops.Helpers;
using fantasy_hoops.Models.Notifications;
using FluentScheduler;
using fantasy_hoops.Services;
using WebPush;
using fantasy_hoops.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace fantasy_hoops.Database
{
    public class InjuriesSeed : IJob
    {
        private readonly GameContext _context;
        private static readonly Stack<InjuryPushNotificationViewModel> lineupsAffected = new Stack<InjuryPushNotificationViewModel>();

        public InjuriesSeed(GameContext context)
        {
            _context = context;
        }

        private static JArray GetInjuries()
        {
            HttpWebResponse webResponse = CommonFunctions.GetResponse("https://www.fantasylabs.com/api/players/news/2/");
            string myResponse = CommonFunctions.ResponseToString(webResponse);
            JArray injuries = JArray.Parse(myResponse);
            return injuries;
        }

        private async Task AddToDatabaseAsync(JToken injury, DateTime? dateModified)
        {
            Player injuryPlayer = _context.Players.Where(x => x.NbaID == (int)injury["PrimarySourceKey"]).FirstOrDefault();

            if (injuryPlayer == null)
                return;

            var injuryObj = new Injury
            {
                Title = injury.Value<string>("Title") != null ? (string)injury["Title"] : null,
                Status = injury.Value<string>("PlayerStatus") != null ? (string)injury["PlayerStatus"] : null,
                InjuryTitle = injury.Value<string>("Injury") != null ? (string)injury["Injury"] : null,
                Description = injury.Value<string>("News") != null ? (string)injury["News"] : null,
                Date = dateModified,
                Link = injury.Value<string>("Link") != null ? (string)injury["Link"] : null
            };

            var dbInjury = _context.Injuries
                    .Where(inj => inj.Player.NbaID == (int)injury["PrimarySourceKey"])
                    .FirstOrDefault();


            if (dbInjury == null)
            {
                injuryObj.Player = injuryPlayer;
                injuryObj.PlayerID = injuryPlayer.PlayerID;
                await _context.Injuries.AddAsync(injuryObj);
                _context.SaveChanges();
                injuryPlayer.InjuryID = injuryObj.InjuryID;
                injuryPlayer.Injury = injuryObj;
            }
            else
            {
                dbInjury.Title = injuryObj.Title;
                dbInjury.Status = injuryObj.Status;
                dbInjury.InjuryTitle = injuryObj.InjuryTitle;
                dbInjury.Description = injuryObj.Description;
                dbInjury.Date = injuryObj.Date;
                dbInjury.Link = injuryObj.Link;
            }

            string statusBefore = injuryPlayer.Injury.Status;
            string statusAfter = injuryObj.Status;

            if (!statusBefore.Equals(statusAfter))
                await UpdateNotifications(injuryObj, statusBefore, statusAfter);

            injuryPlayer.Injury.Status = injuryObj.Status;
            injuryPlayer.Injury.Date = dateModified;
        }

        private async Task UpdateNotifications(Injury injury, string statusBefore, string statusAfter)
        {
            foreach (var lineup in _context.UserLineups
                            .Where(x => x.Date.Equals(CommonFunctions.UTCToEastern(NextGame.NEXT_GAME).Date)
                            && (x.PgID == injury.PlayerID
                                    || x.SgID == injury.PlayerID
                                    || x.SfID == injury.PlayerID
                                    || x.PfID == injury.PlayerID
                                    || x.CID == injury.PlayerID)))
            {
                lineupsAffected.Push(new InjuryPushNotificationViewModel
                {
                    UserID = lineup.UserID,
                    StatusBefore = statusBefore,
                    StatusAfter = statusAfter,
                    FullName = injury.Player.FullName,
                    PlayerNbaID = injury.Player.NbaID
                });
                var inj = new InjuryNotification
                {
                    UserID = lineup.UserID,
                    ReadStatus = false,
                    DateCreated = DateTime.UtcNow,
                    PlayerID = injury.PlayerID,
                    InjuryStatus = injury.Status,
                    InjuryDescription = injury.InjuryTitle
                };

                if (!_context.InjuryNotifications
                .Any(x => x.InjuryStatus.Equals(inj.InjuryStatus)
                                                                                && x.PlayerID == inj.PlayerID))
                    await _context.InjuryNotifications.AddAsync(inj);
            }
        }

        private async Task SendPushNotifications()
        {
            while (lineupsAffected.Count > 0)
            {
                var lineup = lineupsAffected.Pop();
                PushNotificationViewModel notification =
                                new PushNotificationViewModel(lineup.FullName,
                                                string.Format("Status changed from {0} to {1}!", lineup.StatusBefore, lineup.StatusAfter))
                                {
                                    Image = Environment.GetEnvironmentVariable("IMAGES_SERVER_NAME") + "/content/images/players/" + lineup.PlayerNbaID + ".png",
                                    Actions = new List<NotificationAction> { new NotificationAction("lineup", "🤾🏾‍♂️ Lineup") }
                                };
                await PushService.Instance.Value.Send(lineup.UserID, notification);
            }
        }

        public void Execute()
        {
            int seasonYear = int.Parse(CommonFunctions.SEASON_YEAR);
            IEnumerable<JToken> injuries = GetInjuries()
                .Where(inj => inj.Value<string>("ModifiedDate") == null
                    || DateTime.Parse(inj.Value<string>("ModifiedDate")) >= new DateTime(seasonYear - 1, 8, 1)).AsEnumerable();
            foreach (JToken injury in injuries)
            {
                int NbaID;
                if (injury.Value<int?>("PrimarySourceKey") == null)
                    continue;
                NbaID = (int)injury["PrimarySourceKey"];

                DateTime? dateModified = new DateTime?();
                if (injury.Value<string>("ModifiedDate") != null)
                {
                    dateModified = DateTime.Parse(injury["ModifiedDate"].ToString()).AddHours(4);
                    dateModified = dateModified.Value.IsDaylightSavingTime()
                        ? dateModified.Value.AddHours(-1)
                        : dateModified;
                }

                if (_context.Injuries
                    .Where(inj => inj.Player.NbaID == NbaID)
                    .Any(inj => inj.Status.Equals((string)injury["PlayerStatus"])
                        && dateModified.Equals(inj.Date)))
                    continue;

                try
                {
                    AddToDatabaseAsync(injury, dateModified).Wait();
                }
                catch (Exception)
                {
                    continue;
                }
            }
            _context.SaveChanges();
            Task.Run(() => SendPushNotifications());
        }
    }
}