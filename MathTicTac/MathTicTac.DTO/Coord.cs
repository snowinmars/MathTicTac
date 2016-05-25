namespace MathTicTac.DTO
{
	public struct Coord
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Coord(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}
}