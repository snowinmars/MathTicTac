using MathTicTac.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathTicTac.DTO;

namespace MathTicTac.DAL.Dao
{
    public class GameDao : IGameDao
    {
        public bool Add(DetailedWorld input)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DetailedGameInfo> GetAllGames(int userId)
        {
            throw new NotImplementedException();
        }

        public DetailedWorld GetGameState(int gameId)
        {
            throw new NotImplementedException();
        }

        public bool Update(DetailedWorld gameWorld)
        {
            throw new NotImplementedException();
        }
    }
}
