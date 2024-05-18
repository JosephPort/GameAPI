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
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly DataContext _context;

        public PlayerController(DataContext context)
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
    }
}