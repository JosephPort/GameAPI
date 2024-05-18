using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string SystemUID { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}