using MathTicTac.Enums;

namespace MathTicTac.DTO
{
	public class World
	{
		public BigCell[,] BigCells { get; private set; }

		public Coord NextMove { get; set; }

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