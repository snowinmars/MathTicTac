using MathTicTac.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.PL.Interfaces
{
    public interface IGameService
    {
        /// <summary>
        /// Determinates, is player can make move to cell move.BigCellCoord and move.CellCoord
        /// If return true, server and client have to make this move.
        /// </summary>
        /// <param name="move"></param>
        /// <returns>true if move has been accepted, false if not (because of unknown error) or Auth exception if token and ip pair is invalid</returns>
        bool MakeMove(MoveServiceModel move);

        bool RejectGame(string token, string ip, int gameId);

        bool Create(string player1Token, string player1Ip, string player2Identifier);

        /// <summary>
        /// Returns world copy from server for user with token token
        /// </summary>
        /// <returns></returns>
        WorldServiceModel GetCurrentWorld(string token, string ip, int gameId);

        IEnumerable<GameInfoServiceModel> GetAllActiveGames(string token, string ip);
    }
}
