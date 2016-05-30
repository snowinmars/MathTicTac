using MathTicTac.Enums;
using System;

namespace MathTicTac.DTO
{
	public class DetailedGameInfo
	{
		public int ID { get; set; }
		public int ClientId { get; set; }
		public int EnemyId { get; set; }
		public DateTime TimeOfCreation { get; set; }
		public GameStatus status { get; set; }
	}
}