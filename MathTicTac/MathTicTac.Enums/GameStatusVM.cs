namespace MathTicTac.Enums
{
	public enum GameStatusVM
	{
		None = 0,

		//  завершено
		Victory = 1,

		Defeat = 2,
		Draw = 3,
		Rejected = 4,
		Query = 5,

		// не завершено
		ClientTurn = 6,

		EnemyTurn = 7,
	}
}