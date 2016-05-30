using MathTicTac.DTO;
using System.Collections.Generic;

namespace MathTicTac.DAL.Interfaces
{
	public interface IGameDao
	{
		bool Add(DetailedWorld input);

		DetailedWorld GetGameState(int gameId);

		IEnumerable<DetailedGameInfo> GetAllGames(int userId);

		bool Update(DetailedWorld gameWorld);
	}
}