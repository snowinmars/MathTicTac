using MathTicTac.Enums;
using System;

namespace MathTicTac.DTO
{
	public class GameInfo
	{
		public int ID { get; set; }
		public string OppositePlayerName { get; set; }
		public DateTime TimeOfCreation { get; set; }
		public GameStatusVM status { get; set; }
	}
}