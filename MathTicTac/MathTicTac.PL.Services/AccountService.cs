using MathTicTac.PL.Interfaces;
using MathTicTac.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.PL.Services
{
	public class AccountService : IAccountService
	{
		public bool Add(AccountServiceModel item, string password)
		{
			throw new NotImplementedException();
		}

		public AccountServiceModel Get(int id)
		{
			throw new NotImplementedException();
		}

		public bool LoginByToken(string token, string ip)
		{
			throw new NotImplementedException();
		}

		public string LoginByUserName(string identifier, string password, string ip)
		{
			throw new NotImplementedException();
		}

		public bool Logout(string token, string ip)
		{
			throw new NotImplementedException();
		}
	}
}
