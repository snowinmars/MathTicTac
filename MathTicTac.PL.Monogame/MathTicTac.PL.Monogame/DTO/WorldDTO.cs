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
	}
}