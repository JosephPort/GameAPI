using System.Text.Json.Serialization;

namespace GameAPI.Entities.DTO
{
    public class PlayerDTO
    {
        public string SystemUID { get; set; } = string.Empty;
        public string PlayerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SaveJSONString { get; set; } = string.Empty;
    }
}
