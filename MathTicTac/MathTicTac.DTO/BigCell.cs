using MathTicTac.Enums;

namespace MathTicTac.DTO
{
	public class BigCell
	{
		public BigCell(int dimension) : this(dimension, State.None, false)
		{
		}

		public BigCell(int dimension, State state, bool isFocus)
		{
            if (dimension > 0)
            {
                this.Cells = new Cell[dimension, dimension];
            }

            for (int cy = 0; cy < this.Cells.GetLength(1); cy++)
            {
                for (int cx = 0; cx < this.Cells.GetLength(0); cx++)
                {
                    this.Cells[cx, cy] = new Cell();
                }
            }

			this.State = state;
			this.IsFocus = isFocus;
		}

		public Cell[,] Cells { get; private set; }

		public bool IsFocus { get; set; }

		public State State { get; set; }
	}
}