using MathTicTac.ServiceModels;
using Microsoft.Xna.Framework;

namespace MathTicTac.ViewModels
{
	public class CellViewModel : Button
	{
		public CellViewModel(State state, Coord position, int width, int height, string buttonText = "") : base(new Vector2(position.X, position.Y), width, height, buttonText)
		{
			this.State = state;
		}

		public State State { get; set; }

		#region equals

		public static bool operator !=(CellViewModel lhs, CellViewModel rhs)
		    => !(lhs == rhs);

		public static bool operator ==(CellViewModel lhs, CellViewModel rhs)
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

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			CellViewModel p = obj as CellViewModel;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(CellViewModel obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			return this == obj;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.State.GetHashCode();
		}

		#endregion equals
	}
}