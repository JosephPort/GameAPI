using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Entities.DTO
{
    public class PostLeadboardDTO
    {
        public string SystemUID { get; set; } = string.Empty;
        public string ClassType { get; set; } = string.Empty;
        public float Time { get; set; }
    }
}