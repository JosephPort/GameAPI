using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPI.Data;
using GameAPI.Entities.DTO;
using GameAPI.Entities.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("api/leaderboard")]
    public class LeaderboardController : ControllerBase
    {
        private readonly DataContext _context;
        public LeaderboardController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("top-10/{classType}")]
        public async Task<IActionResult> GetLeaderboardType(string classType)
        {
            // Gets the top 10 leaderboard entries for the classType by doing an inner join on player and leaderboard tables
            var dbLeaderboard = await _context.Leaderboard.Join(_context.Player, l => l.PlayerID, p => p.PlayerID, (l, p) => new { l, p })
                .Where(l => l.l.Class == classType)
                .OrderBy(l => l.l.Time)
                .Select(l => new
                {
                    l.p.PlayerName,
                    l.l.Time
                })
                .Take(10)
                .ToListAsync();

            // If the leaderboard has no entries, return a not found (404)
            if (dbLeaderboard.Count == 0)
            {
                return NotFound("Leaderboard has no entries");
            }

            return Ok(dbLeaderboard);
        }

        [HttpGet("player/{classType}/{systemUID}")]
        public async Task<IActionResult> GetPlayerLeaderboardEntry(string classType, string systemUID)
        {
            // TODO: Find the player's leaderboard entry and return the nearest 10 entries (ex. 10-20, 40-50, etc.)
            return Ok("Not implemented yet");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddLeaderboardEntry(PostLeadboardDTO leaderboardRequest)
        {
            var dbPlayer = await _context.Player.FirstOrDefaultAsync(p => p.SystemUID == leaderboardRequest.SystemUID);

            if (dbPlayer is null)
            {
                return NotFound("Player not found");
            }

            // Gets the leaderboard entry if it exists using the playerID and classType
            var dbLeaderboardEntry = await _context.Leaderboard.FirstOrDefaultAsync(l => l.PlayerID == dbPlayer.PlayerID && l.Class == leaderboardRequest.ClassType);

            // If the leaderboard entry exists, update the time if the new time is faster
            if (dbLeaderboardEntry is not null)
            {
                if (leaderboardRequest.Time < dbLeaderboardEntry.Time)
                {
                    dbLeaderboardEntry.Time = leaderboardRequest.Time;
                    await _context.SaveChangesAsync();
                    return Ok("Leaderboard entry updated");
                }
                else
                {
                    return Ok("Leaderboard entry not updated");
                }
            }

            // If the leaderboard entry does not exist, create a new entry
            var leaderboardEntryToAdd = new Leaderboard
            {
                Player = dbPlayer,
                Class = leaderboardRequest.ClassType,
                Time = leaderboardRequest.Time
            };

            _context.Leaderboard.Add(leaderboardEntryToAdd);
            await _context.SaveChangesAsync();

            return Ok("Leaderboard entry added");
        }
    }
}