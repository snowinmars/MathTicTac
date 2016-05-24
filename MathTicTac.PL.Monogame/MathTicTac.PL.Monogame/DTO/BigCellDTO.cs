namespace MathTicTac.PL.Monogame.DTO
{
	using Entities;
	using Microsoft.Xna.Framework;

	internal class BigCellDTO
	{
		public State State { get; set; }
		public bool IsFocus { get; set; }
		public CellDTO[,] Cells { get; private set; }
		public Vector2 Position { get; set; }

		public BigCellDTO() : this(State.None,
						false,
						null,
						new Vector2(0, 0))
		{
		}

		public BigCellDTO(State state, bool isFocus, CellDTO[,] cells, Vector2 position)
		{
			this.Cells = cells;
			this.State = state;
			this.IsFocus = isFocus;
			this.Position = position;
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

			BigCellDTO p = obj as BigCellDTO;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(BigCellDTO obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			return this == obj;
		}

		public static bool operator ==(BigCellDTO lhs, BigCellDTO rhs)
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

		public static bool operator !=(BigCellDTO lhs, BigCellDTO rhs)
		    => !(lhs == rhs);

		#endregion equals
	}
}