using System;
using MathTicTac.BLL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.PL.Interfaces;
using MathTicTac.ServiceModels;

namespace MathTicTac.PL.RestService.Models
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

		public bool LoginByToken(string token)
		{
			throw new NotImplementedException();
		}

		public string LoginByUserName(string identifier, string password)
		{
			throw new NotImplementedException();
		}

		public bool Logout(string token)
		{
			throw new NotImplementedException();
		}
	}
}