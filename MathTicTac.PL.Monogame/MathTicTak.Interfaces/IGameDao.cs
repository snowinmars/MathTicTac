using MathTicTac.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTak.Interfaces
{
    public interface IGameDao
    {
        bool Add(World gameWorld, int creatorId, int guestId);

        World GetGameState(int gameId);

        int GetGameCreatorId(int gameId);

        int GetGameGuestId(int gameId);

        bool Update(World gameWorld, bool Solved, bool ownerWins);
    }
}
