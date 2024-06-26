namespace GameAPI.Entities.Tables
{
    public class Leaderboard
    {
        public int LeaderboardID { get; set; }
        public string Class { get; set; } = string.Empty;
        public float Time { get; set; }
        public int PlayerID { get; set; }
        public Player Player { get; set; } = new Player();
    }
}