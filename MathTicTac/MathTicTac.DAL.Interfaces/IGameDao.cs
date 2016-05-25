using MathTicTac.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DAL.Interfaces
{
    public interface IGameDao
    {
        bool Add(DetailedWorld input);

        DetailedWorld GetGameState(int gameId);

        IEnumerable<DetailedGameInfo> GetAllGames(int userId);

        int GetGameCreatorId(int gameId);

        int GetGameGuestId(int gameId);

        bool Update(DetailedWorld gameWorld);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="userId"></param>
        /// <returns>1 - Owner, -1 - Guest, 0 - No match</returns>
        int GetGameRole(int gameId, int userId);
    }
}
