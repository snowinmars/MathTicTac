using MathTicTac.Enums;

namespace MathTicTac.ServiceModels
{
	public class BigCellServiceModel
	{
		public BigCellServiceModel() : this(State.None,
						false,
						null)
		{
		}

		public BigCellServiceModel(State state, bool isFocus, CellServiceModel[,] cells)
		{
			this.Cells = cells;
			this.State = state;
			this.IsFocus = isFocus;
		}

		public CellServiceModel[,] Cells { get; private set; }
		public bool IsFocus { get; set; }
		public State State { get; set; }

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

		public static bool operator !=(BigCellServiceModel lhs, BigCellServiceModel rhs)
		    => !(lhs == rhs);

		public static bool operator ==(BigCellServiceModel lhs, BigCellServiceModel rhs)
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

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			BigCellServiceModel p = obj as BigCellServiceModel;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(BigCellServiceModel obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			return this == obj;
		}

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

		#endregion equals
	}
}