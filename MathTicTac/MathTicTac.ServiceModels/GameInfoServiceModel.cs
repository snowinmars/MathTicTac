using MathTicTac.Enums;
using System;

namespace MathTicTac.ServiceModels
{
	public class GameInfoServiceModel
	{
		public int ID { get; set; }
		public string OppositePlayerName { get; set; }
		public DateTime TimeOfCreation { get; set; }
		public GameStatus status { get; set; }
	}
}