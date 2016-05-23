﻿using MathTicTac.Entities;
using MathTicTac.Entities.Enum;
using System.Collections.Generic;

namespace MathTicTak.Interfaces
{
	public interface IGameLogic
	{
		/// <summary>
		/// Determinates, is player can make move to cell move.BigCellCoord and move.CellCoord
		/// If return true, server and client have to make this move.
		/// </summary>
		/// <param name="move"></param>
		/// <returns></returns>
		bool MakeMove(Move move);

		void CreateNewGame(string player1Token, int player2Id);

		/// <summary>
		/// Determinates, who wins
		/// </summary>
		/// <returns></returns>
		GameResult CheckGameResult();

		/// <summary>
		/// Returns world copy from server for user with token token
		/// </summary>
		/// <returns></returns>
		World GetCurrentState(string token, int gameId);

        IEnumerable<GameInfo> GetAllActiveGames(string token);
	}
}