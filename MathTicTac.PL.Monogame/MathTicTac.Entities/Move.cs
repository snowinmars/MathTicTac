﻿using System;

namespace MathTicTac.Entities
{
	[Serializable]
	public class Move
	{
		public string Token { get; set; }
		public int GameId { get; set; }
		public Coord BigCellCoord { get; set; }
		public Coord CellCoord { get; set; }

		public static string GetNewId()
		{
			return Guid.NewGuid().ToString();
		}

		public Move(string token, int gameid, Coord bigCellCoord, Coord CellCoord)
		{
			this.Token = token;
			this.GameId = gameid;
			this.CellCoord = CellCoord;
			this.BigCellCoord = bigCellCoord;
		}
	}
}