using MathTicTac.DTO;
using MathTicTac.Enums;

namespace MathTicTac.PL.Soap.BindingLib.ServiceModels
{
	public class WorldSM
	{
		public int Id { get; set; }

		public BigCell[,] BigCells { get; private set; }

		public Coord LastBigCellMove { get; set; }
		public Coord LastCellMove { get; set; }

		public GameStatus Status { get; set; }

		public WorldSM() : this(null)
		{
		}

		public WorldSM(BigCell[,] bigCells)
		{
			this.BigCells = bigCells;
		}
	}
}