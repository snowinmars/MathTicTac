using MathTicTac.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MathTicTac.ServiceModels;
using MathTicTac.BLL.Interfaces;
using MathTicTac.DTO;

namespace MathTicTac.PL.RestService.Models
{
	public class GameService : IGameService
	{
		private IGameLogic gameLogic;

		public GameService(IGameLogic gameLogic)
		{
			this.gameLogic = gameLogic;
		}

		public bool Create(string player1Token, string player1Ip, string player2Identifier)
		{
			return gameLogic.Create(player1Token, player1Ip, player2Identifier);
		}

		public IEnumerable<GameInfoServiceModel> GetAllActiveGames(string token, string ip)
		{
			return gameLogic.GetAllActiveGames(token, ip)
				.Select((g) => new GameInfoServiceModel
						{
							ID = g.ID,
							OppositePlayerName = g.OppositePlayerName,
							status = g.status,
							TimeOfCreation = g.TimeOfCreation
						});
		}

		public WorldServiceModel GetCurrentWorld(string token, string ip, int gameId)
		{
			World world = gameLogic.GetCurrentWorld(token, ip, gameId);
			return Mapper.World2WorldSM(world);
		}

		public bool MakeMove(MoveServiceModel move)
		{
			Move newmove = Mapper.MoveSM2Move(move);
			return gameLogic.MakeMove(newmove);
		}

		public bool RejectGame(string token, string ip, int gameId)
		{
			throw new NotImplementedException();
		}
	}
}