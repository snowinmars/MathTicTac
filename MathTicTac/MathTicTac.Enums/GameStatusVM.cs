using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.Enums
{
	public enum GameStatusVM
	{
		None = 0,
		
		//  завершено
		Win = 1,
		Lose = 2,
		Draw = 3,
		Dismissed = 4,

		// не завершено
		ClientTurn = 5,
		EnemyTurn = 6,
	}
}
