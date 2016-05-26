using MathTicTac.PL.Interfaces;
using MathTicTac.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

		public IHttpActionResult Get([FromBody]string token, [FromBody]string ip)
		{
			// TODO to ask
			// Have I return Created()? Y?
			return Json(this.gameService.GetAllActiveGames(token, ip));
		}

		public IHttpActionResult Get([FromBody]string token, [FromBody]string ip, [FromBody]int gameId)
		{
			return Json(this.gameService.GetCurrentWorld(token, ip, gameId));
		}

		public IHttpActionResult Put([FromBody]MoveServiceModel move)
		{
			return Json(this.gameService.MakeMove(move));
		}

		public IHttpActionResult Post([FromBody]string player1Token, [FromBody]string player1Ip, [FromBody]string player2Identifier)
		{
			return Json(this.gameService.Create(player1Token, player1Ip, player2Identifier));
		}

		public IHttpActionResult Delete ([FromBody]string token, [FromBody]string ip, [FromBody]int gameId)
		{
			return Json(this.gameService.RejectGame(token, ip, gameId));
		}
    }
}
