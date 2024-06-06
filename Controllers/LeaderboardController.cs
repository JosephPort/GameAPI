using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPI.Data;
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

        [HttpGet("{classType}")]
        public async Task<IActionResult> GetLeaderboardType(string classType)
        {
            var dbLeaderboard = await _context.Leaderboard.Join(_context.Player, l => l.PlayerID, p => p.PlayerID, (l, p) => new { l, p })
                .Where(l => l.l.Class == classType)
                .OrderByDescending(l => l.l.Time)
                .Select(l => new
                {
                    l.p.PlayerName,
                    l.l.Time
                })
                .ToListAsync();

            return Ok(dbLeaderboard);
        }
    }
}