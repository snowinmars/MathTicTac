using MathTicTac.Entities;
using System.Collections.Generic;

namespace MathTicTak.Interfaces
{
	public interface IGameDao
	{
		bool Add(int creatorId, int guestId);

		World GetGameState(int gameId);

        IEnumerable<GameInfo> GetAllActiveGames(int userId);

        int GetGameCreatorId(int gameId);

		int GetGameGuestId(int gameId);

		bool Update(World gameWorld, bool Solved = false, int winnerId = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="userId"></param>
        /// <returns>1 - Owner, -1 - Guest, 0 - No match</returns>
        int GetGameRole(int gameId, int userId);
	}
}