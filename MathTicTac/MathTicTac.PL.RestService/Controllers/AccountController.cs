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
	public class AccountController : ApiController
    {
		private IAccountService accountService;

		public AccountController(IAccountService accountService)
		{
			this.accountService = accountService;
		}

		public IHttpActionResult Get([FromBody] int id)
		{
			return Json(this.accountService.Get(id));
		}

		public IHttpActionResult Post([FromBody]AccountServiceModel item, [FromBody]string password)
		{
			return Json(this.accountService.Add(item, password));
		}

		public IHttpActionResult Post([FromBody]string token, [FromBody]string ip)
		{
			return Json(this.accountService.LoginByToken(token, ip));
		}

		public IHttpActionResult Post([FromBody]string identifier, [FromBody]string password, [FromBody]string ip)
		{
			return Json(this.accountService.LoginByUserName(identifier, password, ip));
		}


		// TODO to ask. Is it has to be post? If yes, how to rename? Like an usual action?
		public IHttpActionResult Delete([FromBody]string token, [FromBody]string ip)
		{
			return Json(this.accountService.Logout(token, ip));
		}
			 
    }
}
