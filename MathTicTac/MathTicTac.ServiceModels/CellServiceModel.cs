
namespace MathTicTac.ServiceModels
{
	using Enums;
	using Microsoft.Xna.Framework;
	public class CellServiceModel
	{
		public CellServiceModel(State state)
		{
			this.State = state;
		}

		public State State { get; set; }

		#region equals

		public static bool operator !=(CellServiceModel lhs, CellServiceModel rhs)
		    => !(lhs == rhs);

		public static bool operator ==(CellServiceModel lhs, CellServiceModel rhs)
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

			CellServiceModel p = obj as CellServiceModel;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(CellServiceModel obj)
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