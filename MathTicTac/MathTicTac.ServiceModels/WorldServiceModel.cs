using MathTicTac.Enums;

namespace MathTicTac.ServiceModels
{
	public class WorldServiceModel
	{
		public int Id { get; set; }
		public BigCellServiceModel[,] BigCells { get; private set; }

		public GameStatusVM Status { get; set; }

		public WorldServiceModel(int id) : this(id, null)
		{
		}

		public WorldServiceModel(int id, BigCellServiceModel[,] bigCells)
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