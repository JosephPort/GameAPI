using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Entities.DTO
{
    public class CloudSaveDTO
    {
        public string SystemUID { get; set; } = string.Empty;
        public string SaveJSONString { get; set; } = string.Empty;
    }
}