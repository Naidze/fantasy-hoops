﻿using System;
using fantasy_hoops.Database;
using FluentScheduler;
using System.Linq;
using System.Threading.Tasks;
using fantasy_hoops.Jobs;
using fantasy_hoops.Services.Interfaces;

namespace fantasy_hoops
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry(GameContext context, IScoreService scoreService, IPushService pushService)
        {
            if (context.Teams.Count() < 30)
                Schedule(new RostersJob(pushService))
                    .WithName("rosterJob")
                    .ToRunNow();

            Task.Run(() => RostersJob.UpdateTeamColors(new GameContext()));

            if (!bool.Parse(Startup.Configuration["corona-suspended"]))
            {
                Schedule(new NextGameJob(scoreService, pushService, false))
                    .WithName("nextGameJob")
                    .ToRunNow();
            }
            NextGameJob.NEXT_GAME = new DateTime(2020, 04, 20);
            NextGameJob.NEXT_GAME_CLIENT = NextGameJob.NEXT_GAME;

            Schedule(new InjuriesJob(pushService))
                .WithName("injuriesJob")
                .ToRunNow()
                .AndEvery(10)
                .Minutes();

            Schedule(new PhotosJob())
                .WithName("photosJob")
                .ToRunEvery(1)
                .Days()
                .At(00, 04);
        }
    }
}
