namespace MathTicTac.DTO
{
	public class Move
	{
		public string Token { get; set; }
		public string IP { get; set; }
		public int GameId { get; set; }
		public Coord BigCellCoord { get; set; }
		public Coord CellCoord { get; set; }

		public Move(string ip,
				string token,
				int gameid,
				Coord bigCellCoord,
				Coord cellCoord)
		{
			this.Token = token;
			this.GameId = gameid;
			this.CellCoord = cellCoord;
			this.BigCellCoord = bigCellCoord;
			this.IP = ip;
		}
	}
}