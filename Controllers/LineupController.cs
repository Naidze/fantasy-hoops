﻿using fantasy_hoops.Database;
using fantasy_hoops.Models.ViewModels;
using fantasy_hoops.Repositories;
using fantasy_hoops.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using fantasy_hoops.Dtos;
using fantasy_hoops.Jobs;
using fantasy_hoops.Repositories.Interfaces;
using fantasy_hoops.Services.Interfaces;

namespace fantasy_hoops.Controllers
{
    [Route("api/[controller]")]
    public class LineupController : Controller
    {

        public readonly int MAX_PRICE = 300;

        private readonly ILineupService _lineupService;
        private readonly ILineupRepository _lineupRepository;

        public LineupController(ILineupService lineupService, ILineupRepository lineupRepository)
        {
            _lineupService = lineupService;
            _lineupRepository = lineupRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            return Ok(_lineupRepository.GetLineup(id));
        }

        [Authorize]
        [HttpPost("submit")]
        public IActionResult SubmitLineup([FromBody]SubmitLineupViewModel model)
        {
            string userId = User.Claims.ToList()[0].Value;
            model.UserID = userId;
            if (_lineupRepository.GetLineupPrice(model) > MAX_PRICE)
                return StatusCode(422, "Lineup price exceeds the budget! Lineup was not submitted.");
            if (!_lineupRepository.ArePricesCorrect(model))
                return StatusCode(422, "Wrong player prices! Lineup was not submitted.");
            if (!PlayersJob.PLAYER_POOL_DATE.Equals(NextGameJob.NEXT_GAME))
                return StatusCode(500, "Player pool not updated! Try again in a moment.");
            if (_lineupRepository.AreNotPlayingPlayers(model))
                return StatusCode(422, "Player pool is outdated! Refresh the page.");

            _lineupService.SubmitLineup(model);

            return Ok("Lineup was updated successfully");
        }

        [HttpGet("nextGame")]
        public IActionResult NextGame()
        {
            return Ok(new
            {
                nextGame = NextGameJob.NEXT_GAME_CLIENT,
                serverTime = DateTime.Now,
                playerPoolDate = PlayersJob.PLAYER_POOL_DATE
            });
        }

        [Authorize]
        [HttpGet("current")]
        public UserLeaderboardRecordDto GetUserCurrentLineup()
        {
            string userId = User.Claims.ToList()[0].Value;
            return _lineupRepository.GetUserCurrentLineup(userId);
        }

        [Authorize]
        [HttpGet("recent")]
        public List<UserLeaderboardRecordDto> GetRecentLineups([FromQuery] int start = 0, [FromQuery] int count = 5)
        {
            string userId = User.Claims.ToList()[0].Value;
            return _lineupRepository.GetRecentLineups(userId, start, count);
        }
    }
}
