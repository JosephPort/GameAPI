using GameAPI.Data;
using GameAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        Player? dbPlayer;
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("silent-login/{systemUID}")]
        public async Task<ActionResult> SilentLogin(string systemUID)
        {
            var player = await _context.Player.FirstOrDefaultAsync(p => p.SystemUID == systemUID);

            if (player is null)
            {
                return NotFound("Device is not recognized");
            }

            return Ok("Device recognized");
        }

        [HttpPost("login")]
        public async Task<ActionResult> PerformLogin(PlayerDTO playerRequest)
        {
            dbPlayer = await _context.Player.FirstOrDefaultAsync(p => p.Username == playerRequest.Username);

            if (dbPlayer is null || !BCrypt.Net.BCrypt.Verify(playerRequest.Password, dbPlayer.HashedPassword))
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok("User logged in");
        }

        [HttpPost("register")]
        public async Task<ActionResult> CreatePlayer(PlayerDTO playerRequest)
        {
            dbPlayer = await _context.Player.FirstOrDefaultAsync(p => p.Username == playerRequest.Username);

            if (dbPlayer is not null)
            {
                return BadRequest("Username already in use");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(playerRequest.Password);

            dbPlayer = new Player
            {
                SystemUID = playerRequest.SystemUID,
                Username = playerRequest.Username,
                HashedPassword = hashedPassword,
            };

            _context.Player.Add(dbPlayer);
            await _context.SaveChangesAsync();

            return Ok("Player registered");
        }
    }
}
