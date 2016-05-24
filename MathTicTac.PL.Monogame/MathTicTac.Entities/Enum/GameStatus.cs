namespace MathTicTac.Entities.Enum
{
    /// <summary>
    /// Player1 - game creator
    /// </summary>
	public enum GameStatus
    {
        None = 0,
        Turn = 1,
        Awaiting = 2,
        Won = 3,
        Lose = 4,
        DeadHeat = 5
    }
}