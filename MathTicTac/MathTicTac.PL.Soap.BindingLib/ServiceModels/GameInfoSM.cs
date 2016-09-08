using System;
using MathTicTac.Enums;

namespace MathTicTac.PL.Soap.BindingLib.ServiceModels
{
	public class GameInfoSM
	{
		public int ID { get; set; }
		public string OppositePlayerName { get; set; }
		public DateTime TimeOfCreation { get; set; }
		public GameStatus status { get; set; }
	}
}