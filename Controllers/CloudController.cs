using GameAPI.Data;
using GameAPI.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers
{
    [Route("cloud")]
    public class CloudController : Controller
    {
        private readonly DataContext _context;
        public CloudController(DataContext context)
        {
            _context = context;
        }

        // Saves the JSON string to the database of the associated device ID
        [HttpPost("save")]
        public async Task<IActionResult> SaveCloudSave(CloudSaveDTO cloudSave)
        {
            var dbPlayer = await _context.Player.FirstOrDefaultAsync(p => p.SystemUID == cloudSave.SystemUID);

            if (dbPlayer is null)
            {
                return BadRequest("Player not found");
            }

            // Updates the save string of the player to the client-side request

            _context.Player.Update(dbPlayer);
            await _context.SaveChangesAsync();

            return Ok("Cloud save updated successfully");
        }

        // Loads the JSON string from the database of the associated device ID
        [HttpGet("load/{systemUID}")]
        public async Task<IActionResult> GetCloudSave(string systemUID)
        {
            var dbPlayer = await _context.Player.FirstOrDefaultAsync(p => p.SystemUID == systemUID);

            if (dbPlayer is null)
            {
                return BadRequest("Player not found");
            }


            return Ok("save");
        }
    }
}