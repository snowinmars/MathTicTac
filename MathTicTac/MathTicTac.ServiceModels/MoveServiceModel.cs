namespace MathTicTac.ServiceModels
{
	using System;

	public class MoveServiceModel
	{
		public CoordServiceModel BigCellCoord { get; set; }
		public CoordServiceModel CellCoord { get; set; }
		public int GameId { get; set; }
		public string IP { get; set; }
		public string Token { get; set; }

		public static string GetNewId()
		{
			return Guid.NewGuid().ToString();
		}

		public MoveServiceModel(string ip, string token, int gameid, CoordServiceModel bigCellCoord, CoordServiceModel CellCoord)
		{
			this.Token = token;
			this.GameId = gameid;
			this.CellCoord = CellCoord;
			this.BigCellCoord = bigCellCoord;
			this.IP = ip;
		}
	}
}