namespace MathTicTac.ServiceModels
{
	public class CoordServiceModel
	{
		public int X { get; set; }
		public int Y { get; set; }

		public CoordServiceModel(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public CoordServiceModel() : this(0, 0)
		{
		}

		#region equals

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			CoordServiceModel p = obj as CoordServiceModel;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(CoordServiceModel obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			return this == obj;
		}

		public static bool operator ==(CoordServiceModel lhs, CoordServiceModel rhs)
		    => lhs.X == rhs.X &&
			lhs.Y == rhs.Y;

		public static bool operator !=(CoordServiceModel lhs, CoordServiceModel rhs)
		    => !(lhs == rhs);

		#endregion equals
	}
}
