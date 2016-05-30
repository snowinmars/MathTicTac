using MathTicTac.BLL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.PL.Interfaces;
using MathTicTac.ServiceModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
			return this.gameLogic.Create(player1Token, player1Ip, player2Identifier);
		}

		public IEnumerable<GameInfoServiceModel> GetAllActiveGames(string token)
		{
			string ip = HttpContext.Current.Request.UserHostAddress;

			return this.gameLogic.GetAllActiveGames(token, ip)
				.Select((g) => new GameInfoServiceModel
				{
					ID = g.ID,
					OppositePlayerName = g.OppositePlayerName,
					status = g.status,
					TimeOfCreation = g.TimeOfCreation
				});
		}

		public WorldServiceModel GetCurrentWorld(string token, int gameId)
		{
			string ip = HttpContext.Current.Request.UserHostAddress;

			World world = this.gameLogic.GetCurrentWorld(token, ip, gameId);
			return Mapper.World2WorldSM(world);
		}

		public bool MakeMove(MoveServiceModel move)
		{
			string ip = HttpContext.Current.Request.UserHostAddress;

			Move newmove = Mapper.MoveSM2Move(move);
			newmove.IP = ip;

			return this.gameLogic.MakeMove(newmove);
		}

		public bool RejectGame(string token, int gameId)
		{
			string ip = HttpContext.Current.Request.UserHostAddress;

			return this.gameLogic.RejectGame(token, ip, gameId);
		}
	}
}