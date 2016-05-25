using MathTicTac.Enums;

namespace MathTicTac.DTO
{
	public class Cell
	{
		public Cell(State state)
		{
			this.State = state;
		}

		public State State { get; set; }
	}
}