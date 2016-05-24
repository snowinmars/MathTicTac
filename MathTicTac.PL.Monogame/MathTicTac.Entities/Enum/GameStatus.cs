namespace MathTicTac.Entities
{
	/// <summary>
	/// Player1 - game creator
	/// </summary>
	public enum GameStatus
	{
		None = 0,
		Accepted = 1,
		Dismissed = 2,
		Turn = 3,
		Awaiting = 4,
		Won = 5,
		Lose = 6,
		DeadHeat = 7,
	}
}