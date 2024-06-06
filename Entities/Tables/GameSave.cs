namespace GameAPI.Entities.Tables
{
    public class GameSave
    {
        public int GameSaveID { get; set; }
        public int CashEarned { get; set; }
        public int CashSpent { get; set; }
        public int GoldEarned { get; set; }
        public int GoldSpent { get; set; }
        public int PlayerID { get; set; }
        public Player Player { get; set; } = new Player();
    }
}