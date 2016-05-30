using MathTicTac.Enums;
using MathTicTac.ServiceModels;

namespace MathTicTac.ViewModels
{
	public class BigCellViewModel
	{
		public BigCellViewModel() : this(State.None,
						false,
						null,
						new CoordServiceModel(0, 0))
		{
		}

		public BigCellViewModel(State state,
						bool isFocus,
						CellViewModel[,] cells,
						CoordServiceModel position)
		{
			this.Cells = cells;
			this.State = state;
			this.IsFocus = isFocus;
			this.Position = position;
		}

		public CellViewModel[,] Cells { get; private set; }

		/// <summary>
		/// Determinates, can player make a move to this big cell or not
		/// </summary>
		public bool IsFocus { get; set; }

		/// <summary>
		/// Position in pixels
		/// </summary>
		public CoordServiceModel Position { get; set; }

		public State State { get; set; }

		/// <summary>
		/// Is all small cells in this big cell have state != State.None
		/// </summary>
		/// <returns></returns>
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

		public static bool operator !=(BigCellViewModel lhs, BigCellViewModel rhs)
		    => !(lhs == rhs);

		public static bool operator ==(BigCellViewModel lhs, BigCellViewModel rhs)
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

			BigCellViewModel p = obj as BigCellViewModel;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(BigCellViewModel obj)
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