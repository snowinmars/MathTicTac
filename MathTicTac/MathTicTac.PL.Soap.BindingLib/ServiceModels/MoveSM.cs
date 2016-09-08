using MathTicTac.DTO;

namespace MathTicTac.PL.Soap.BindingLib.ServiceModels
{
	public class MoveSM
	{
		public string Token { get; set; }
		public string IP { get; set; }
		public int GameId { get; set; }
		public Coord BigCellCoord { get; set; }
		public Coord CellCoord { get; set; }
	}
}