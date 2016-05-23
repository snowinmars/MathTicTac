using Config;

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

		public BigCell[,] BigCells { get; private set; }
		public CurrentPlayer Turn { get; set; }

		public World() : this(null)
		{
		}

		public World(BigCell[,] bigCells)
		{
			if (bigCells == null)
			{
				this.BigCells = new BigCell[BigCellRowCount, BigCellColumnCount];
				for (int i = 0; i < this.BigCells.GetLength(0); i++)
					for (int j = 0; j < this.BigCells.GetLength(1); j++)
					{
						this.BigCells[i,j] = new BigCell();
					}
			}
			else
			{
				this.BigCells = bigCells;
			}

			this.Turn = CurrentPlayer.Player2;
		}
	}
}