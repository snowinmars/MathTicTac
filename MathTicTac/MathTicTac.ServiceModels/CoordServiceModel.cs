namespace MathTicTac.ServiceModels
{
	public struct CoordServiceModel
	{
		public int X { get; set; }
		public int Y { get; set; }

		public CoordServiceModel(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
        
	}
}
