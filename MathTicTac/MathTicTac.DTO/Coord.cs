namespace MathTicTac.DTO
{
	public class Coord
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Coord(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public Coord() : this(0, 0)
		{
		}

		#region equals

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Coord p = obj as Coord;
			if ((object)p == null)
			{
				return false;
			}

			return this.Equals(p);
		}

		public bool Equals(Coord obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			return this == obj;
		}

		public static bool operator ==(Coord lhs, Coord rhs)
		    => lhs.X == rhs.X &&
			lhs.Y == rhs.Y;

		public static bool operator !=(Coord lhs, Coord rhs)
		    => !(lhs == rhs);

		#endregion equals
	}
}
