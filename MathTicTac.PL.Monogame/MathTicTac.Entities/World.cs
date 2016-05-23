using Config;

namespace MathTicTac.Entities
{
	public class World
	{
		public static readonly int  BigCellRowCount;
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
			}
			else
			{
				this.BigCells = bigCells;
			}

			this.Turn = CurrentPlayer.Player2;
		}
	}
}