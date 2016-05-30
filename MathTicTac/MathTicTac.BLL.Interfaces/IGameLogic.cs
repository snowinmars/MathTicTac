using MathTicTac.DTO;
using MathTicTac.Enums;
using System.Collections.Generic;

namespace MathTicTac.BLL.Interfaces
{
	public interface IGameLogic
	{
        /// <summary>
        /// Determinates, is player can make move to cell move.BigCellCoord and move.CellCoord
        /// If return true, server and client have to make this move.
        /// </summary>
        /// <param name="move"></param>
        /// <returns>true if move has been accepted, false if not (because of unknown error) or Auth exception if token and ip pair is invalid</returns>
        ResponseResult MakeMove(Move move);

        ResponseResult RejectGame(string token, string ip, int gameId);

        ResponseResult Create(string player1Token, string player1Ip, string player2Identifier);

        /// <summary>
        /// Returns world copy from server for user with token token
        /// </summary>
        /// <returns></returns>
        ResponseResult GetCurrentWorld(string token, string ip, int gameId, out World result);

		IEnumerable<GameInfo> GetAllActiveGames(string token, string ip);
	}
}