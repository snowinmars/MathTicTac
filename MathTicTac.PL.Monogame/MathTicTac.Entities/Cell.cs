using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MathTicTac.Entities
{
	public class Cell
	{
		public State State { get; set; }
		public bool IsFocus { get; set; }

		public Cell(State state, bool isFocus)
		{
			this.State = state;
			this.IsFocus = isFocus;
		}

		#region equals

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Cell p = obj as Cell;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(Cell obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			return this == obj;
		}

		public static bool operator !=(Cell lhs, Cell rhs)
		    => !(lhs == rhs);

		public static bool operator ==(Cell lhs, Cell rhs)
		{
			object olhs = (object)lhs;
			object orhs = (object)rhs;

			if (olhs == null && orhs == null)
			{
				return true;
			}

			if (olhs == null ^ orhs == null)
			{
				return false;
			}

			return lhs.State == rhs.State;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.State.GetHashCode();
		}

		#endregion equals
	}
}