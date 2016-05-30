using MathTicTac.ServiceModels;
using System.Collections.Generic;
using System.ServiceModel;

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

		bool RejectGame(string token, int gameId);

		bool Create(string player1Token, string player1Ip, string player2Identifier);

		/// <summary>
		/// Returns world copy from server for user with token token
		/// </summary>
		/// <param name="token">todo: describe token parameter on GetCurrentWorld</param>
		/// <param name="ip">todo: describe ip parameter on GetCurrentWorld</param>
		/// <param name="gameId">todo: describe gameId parameter on GetCurrentWorld</param>
		/// <returns></returns>
		WorldServiceModel GetCurrentWorld(string token, int gameId);

		IEnumerable<GameInfoServiceModel> GetAllActiveGames(string token);
	}
}