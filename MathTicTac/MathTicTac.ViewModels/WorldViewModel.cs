using MathTicTac.Enums;
using MathTicTac.ServiceModels;

namespace MathTicTac.ViewModels
{
	public class WorldViewModel
	{
		public int Id { get; set; }

		public BigCellViewModel[,] BigCells { get; private set; }

		public LastTurnCoord LastTurnCoord { get; set; }

		public GameStatus Status { get; set; }

		public WorldViewModel(int id) : this(id, null)
		{
		}

		public WorldViewModel(int id, BigCellViewModel[,] bigCells)
		{
			this.BigCells = bigCells;
			this.Id = id;
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

		public void SetAllBigCellsToState(bool state)
		{
			for (int i = 0; i < this.BigCells.GetLength(0); i++)
				for (int j = 0; j < this.BigCells.GetLength(1); j++)
				{
					this.BigCells[i, j].IsFocus = state;
				}
		}
	}
}