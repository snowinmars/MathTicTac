namespace MathTicTac.ServiceModels
{
	public class MoveServiceModel
	{
		public CoordServiceModel BigCellCoord { get; set; }
		public CoordServiceModel CellCoord { get; set; }
		public int GameId { get; set; }
		public string Token { get; set; }

		public MoveServiceModel(string token,
						int gameid,
						CoordServiceModel bigCellCoord,
						CoordServiceModel CellCoord)
		{
			this.Token = token;
			this.GameId = gameid;
			this.CellCoord = CellCoord;
			this.BigCellCoord = bigCellCoord;
		}
	}
}