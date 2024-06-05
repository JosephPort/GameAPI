namespace GameAPI.Entities.Tables
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string SystemUID { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string SaveJSONString { get; set; } = string.Empty;
    }
}