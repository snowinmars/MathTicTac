
namespace MathTicTac.Entities
{
	public class World
	{
		public int Id { get; set; }
		public BigCell[,] BigCells { get; private set; }

		/// <summary>
		/// Coordinates of allowed bigcell
		/// </summary>
		public Coord TurnCoord { get; set; }

		public GameStatus Status { get; set; }

		public World(int id) : this(id, null)
		{
		}

		public World(int id, BigCell[,] bigCells)
		{
			
				this.BigCells = bigCells;

			this.Id = id;
			this.TurnCoord = null;
		}
	}
}