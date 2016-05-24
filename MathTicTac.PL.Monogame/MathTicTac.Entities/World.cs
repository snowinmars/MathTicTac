using Config;
using MathTicTac.Entities.Enum;

namespace MathTicTac.Entities
{
	public class World
	{
		public static readonly int BigCellRowCount;
		public static readonly int BigCellColumnCount;

		static World()
		{
			BigCellColumnCount = MathTicTacConfiguration.BigCellColumnCount;
			BigCellRowCount = MathTicTacConfiguration.BigCellRowCount;
		}

		public int Id { get; set; }
		public BigCell[,] BigCells { get; private set; }

		/// <summary>
		/// Coordinates of allowed bigcell
		/// </summary>
		public Coord TurnCoord { get; set; }

		public GameStatus Status { get; set; }

		public World(int id) : this(id, null)
		{
		}

		public World(int id, BigCell[,] bigCells)
		{
			if (bigCells == null)
			{
				this.BigCells = new BigCell[BigCellRowCount, BigCellColumnCount];
				for (int i = 0; i < this.BigCells.GetLength(0); i++)
					for (int j = 0; j < this.BigCells.GetLength(1); j++)
					{
						this.BigCells[i, j] = new BigCell();
					}
			}
			else
			{
				this.BigCells = bigCells;
			}

			this.Id = id;
			this.TurnCoord = null;
		}
	}
}