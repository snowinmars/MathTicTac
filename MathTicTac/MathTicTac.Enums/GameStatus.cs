namespace MathTicTac.Enums
{
	/// <summary>
	/// Determinates, what status current game has.
	/// </summary>
	public enum GameStatus
	{
		None = 0,

		/// <summary>
		/// Victory of current client. Game was ended
		/// </summary>
		Victory = 1,

		/// <summary>
		/// Defeat of current client. Game was ended
		/// </summary>
		Defeat = 2,

		/// <summary>
		/// Game was ended
		/// </summary>
		Draw = 3,

		/// <summary>
		/// Game was rejected by enemy player. Game was ended
		/// </summary>
		Rejected = 4,

		/// <summary>
		/// Awaiting of enemy player to accept the game invite. Game was not ended
		/// </summary>
		Query = 5,

		/// <summary>
		/// Current client is thinking about turn. Game was not ended
		/// </summary>
		ClientTurn = 6,

		/// <summary>
		/// Enemy player is thinking about turn. Game was not ended
		/// </summary>
		EnemyTurn = 7,
	}
}