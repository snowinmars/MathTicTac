using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.PL.Monogame.Identity
{
	public class ApplicationUser : IUser
	{
		public string Id { get;  set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string Token { get; set; }
	}
}
