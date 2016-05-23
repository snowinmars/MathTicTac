using Config;

namespace MathTicTac.Entities
{
	[]
	public class BigCell
	{
		public State State { get; set; }
		public bool IsFocus { get; set; }
		public Cell[,] Cells { get; private set; }

		public static readonly int CellRowCount;
		public static readonly int CellColumnCount;

		static BigCell()
		{
			CellRowCount = MathTicTacConfiguration.CellRowCount;
			CellColumnCount = MathTicTacConfiguration.CellColumnCount;
		}

		public BigCell() : this(State.None, false, null)
		{
		}

		public BigCell(State state, bool isFocus, Cell[,] cells)
		{
			if (cells == null)
			{
				this.Cells = new Cell[5, 5]; // TODO to consts
			}
			else
			{
				this.Cells = cells;
			}

			this.State = state;
			this.IsFocus = isFocus;
		}

		public bool IsFilled()
		{
			foreach (var item in this.Cells)
			{
				if (item.State == State.None)
				{
					return false;
				}
			}

			return true;
		}

		#region equals

		public override int GetHashCode()
		{
			int hash = 0;

			foreach (var item in this.Cells)
			{
				hash ^= item.GetHashCode();
			}

			hash ^= this.IsFocus.GetHashCode();

			hash ^= this.State.GetHashCode();

			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			BigCell p = obj as BigCell;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(BigCell obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			return this == obj;
		}

		public static bool operator ==(BigCell lhs, BigCell rhs)
		{
			if ((lhs == null) && (rhs == null))
			{
				return true;
			}

			if ((lhs == null) ^ (rhs == null))
			{
				return false;
			}

			bool output = lhs.IsFocus == rhs.IsFocus &&
					lhs.State == rhs.State;

			if (!output) // small optimization
			{
				return false;
			}

			// checking cells euqals

			bool isCellsEquals = false;

			if (lhs.Cells.Length == rhs.Cells.Length)
			{
				for (int i = 0; i < lhs.Cells.GetLength(0); i++)
				{
					for (int j = 0; j < lhs.Cells.GetLength(1); j++)
					{
						if (lhs.Cells[i, j] != rhs.Cells[i, j])
						{
							isCellsEquals = false;
							goto cyclebreak;
						}
					}
				}

				isCellsEquals = true;

				cyclebreak:
				;
			}

			return output && isCellsEquals;
		}

		public static bool operator !=(BigCell lhs, BigCell rhs)
			=> !(lhs == rhs);

		#endregion equals
	}
}