using MathTicTac.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathTicTac.ServiceModels;

namespace MathTicTac.PL.Services
{
	public class GameService : IGameService
	{
		public bool Create(string player1Token, string player1Ip, string player2Identifier)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<GameInfoServiceModel> GetAllActiveGames(string token, string ip)
		{
			throw new NotImplementedException();
		}

		public WorldServiceModel GetCurrentWorld(string token, string ip, int gameId)
		{
			throw new NotImplementedException();
		}

		public bool MakeMove(MoveServiceModel move)
		{
			throw new NotImplementedException();
		}

		public bool RejectGame(string token, string ip, int gameId)
		{
			throw new NotImplementedException();
		}
	}
}
