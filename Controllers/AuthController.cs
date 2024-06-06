using GameAPI.Data;
using GameAPI.Entities.DTO;
using GameAPI.Entities.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        public AuthController(DataContext context)
        {
            _context = context;
        }

        // Registers user by adding them to the database
        [HttpPost("register")]
        public async Task<IActionResult> Register(PlayerDTO player)
        {
            // Finds if email exists since it needs to be unique
            var dbPlayer = await _context.Player.FirstOrDefaultAsync(p => p.Email == player.Email);

            if (dbPlayer is not null){
                return BadRequest("Email already exists");
            }

            // TODO: Add salt to password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(player.Password);

            // Creates a new player object to add to the database
            var playerToAdd = new Player
            {
                SystemUID = player.SystemUID,
                Email = player.Email,
                HashedPassword = hashedPassword,
            };

            _context.Player.Add(playerToAdd);
            await _context.SaveChangesAsync();

            return Ok("Player registered successfully");
        }

        // Logs in the player and if successful links the device to the player in the database
        [HttpPost("login")]
        public async Task<IActionResult> Login(PlayerDTO player)
        {
            var dbPlayer = await _context.Player.FirstOrDefaultAsync(p => p.Email == player.Email);

            // Checks if the player is found and if the password is correct
            if (dbPlayer is null || !BCrypt.Net.BCrypt.Verify(player.Password, dbPlayer.HashedPassword)){
                return BadRequest("Invalid email or password");
            }

            // Updates the systemUID of the player to the device that made the client-side request
            dbPlayer.SystemUID = player.SystemUID;

            _context.Player.Update(dbPlayer);
            await _context.SaveChangesAsync();

            return Ok("Player logged in successfully");
        }

        // Checks if the device ID making the request is found in the database, if not then device has not been used before
        [HttpGet("silent-login/{systemUID}")]
        public async Task<IActionResult> SilentLogin(string systemUID)
        {
            var dbPlayer = await _context.Player.FirstOrDefaultAsync(p => p.SystemUID == systemUID);

            if (dbPlayer is null){
                return NotFound("Player not found");
            }

            return Ok("Player found");
        }
    }
}