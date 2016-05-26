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

        bool Update(DetailedWorld gameWorld);
    }
}
