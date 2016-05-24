using Microsoft.Xna.Framework;

namespace MathTicTac.Entities
{
	public class Cell : Button
	{
		public State State { get; set; }

		public Cell(State state, Vector2 position, int width, int height, string buttonText = "") : base(position, width, height, buttonText)
		{
			this.State = state;
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