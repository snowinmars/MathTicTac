namespace MathTicTac.PL.Monogame.DTO
{
	using Entities;

	internal class WorldDTO
	{
		public int Id { get; set; }
		public BigCellDTO[,] BigCells { get; private set; }

		/// <summary>
		/// Coordinates of allowed bigcell
		/// </summary>
		public Coord TurnCoord { get; set; }

		public GameStatus Status { get; set; }

		public WorldDTO(int id) : this(id, null)
		{
		}

		public WorldDTO(int id, BigCellDTO[,] bigCells)
		{
			this.BigCells = bigCells;

			this.Id = id;
			this.TurnCoord = null;
		}

		public bool IsAllBigCellsFocused()
		{
			for (int i = 0; i < this.BigCells.GetLength(0); i++)
				for (int j = 0; j < this.BigCells.GetLength(1); j++)
				{
					if (!this.BigCells[i, j].IsFocus)
					{
						return false;
					}
				}

			return true;
		}

		internal void SetAllBigCellsToState(bool state)
		{
			for (int i = 0; i < this.BigCells.GetLength(0); i++)
				for (int j = 0; j < this.BigCells.GetLength(1); j++)
				{
					this.BigCells[i, j].IsFocus = state;
				}
		}
	}
}