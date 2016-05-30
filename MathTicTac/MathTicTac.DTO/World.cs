using MathTicTac.Enums;

namespace MathTicTac.DTO
{
	public class World
	{
		public int Id { get; set; }

		public BigCell[,] BigCells { get; private set; }

		public Coord LastBigCellMove { get; set; }
		public Coord LastCellMove { get; set; }

		public GameStatus Status { get; set; }

		public World() : this(null)
		{
		}

		public World(BigCell[,] bigCells)
		{
			this.BigCells = bigCells;
		}
	}
}