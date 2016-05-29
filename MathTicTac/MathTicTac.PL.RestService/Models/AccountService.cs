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
	public class AccountService : IAccountService
	{
		private IAccountLogic accountLogic;

		public AccountService(IAccountLogic accountLogic)
		{
			this.accountLogic = accountLogic;
		}

		public bool Add(AccountServiceModel item, string password)
		{
			throw new NotImplementedException();

			//Account account = new Account();

			//accountLogic.Add()
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