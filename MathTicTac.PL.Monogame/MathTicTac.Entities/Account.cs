namespace MathTicTac.Entities
{
	public class Account
	{
		public int id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }

        public int GamesWon { get; set; }
        public int GamesLose { get; set; }
        public int GamesDeadHeat { get; set; }
    }
}