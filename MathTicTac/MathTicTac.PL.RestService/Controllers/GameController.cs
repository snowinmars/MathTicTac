using MathTicTac.PL.Interfaces;
using MathTicTac.ServiceModels;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace MathTicTac.PL.RestService.Controllers
{
	/// GET	   - READ
	/// POST   - ADD
	/// PUT    - UPDATE
	/// DELETE - DELETE
	public class GameController : ApiController
	{
		private IGameService gameService;

		public GameController(IGameService gameService)
		{
			this.gameService = gameService;
		}

		public IHttpActionResult Get(string token)
		{
			// TODO to ask
			// Have I return Created()? Y?
			// Answer: Have to
			//return Json(this.gameService.GetAllActiveGames(token));
			return Json(token);
		}

		public IHttpActionResult Get(string token, int gameId)
		{
			return Json(this.gameService.GetCurrentWorld(token, gameId));
		}

		public IHttpActionResult Put(MoveServiceModel move)
		{
			return Json(this.gameService.MakeMove(move));
		}

		public IHttpActionResult Post([FromBody]string player1Token, [FromBody]string player1Ip, [FromBody]string player2Identifier)
		{
			// TODO to Created()
			// TODO to ViewModels

			return Json(this.gameService.Create(player1Token, player1Ip, player2Identifier));
		}

		public IHttpActionResult Delete(string token, int gameId)
		{
			return Json(this.gameService.RejectGame(token, gameId));
		}
	}
}