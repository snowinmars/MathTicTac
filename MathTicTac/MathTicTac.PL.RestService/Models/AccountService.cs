using MathTicTac.BLL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.PL.Interfaces;
using MathTicTac.ServiceModels;
using System;

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
			Account account = Mapper.AccountSM2Account(item);

			return this.accountLogic.Add(account, password);
		}

		public AccountServiceModel Get(int id)
		{
			Account account = this.accountLogic.Get(id);
			return Mapper.Account2AccountSM(account);
		}

		public bool LoginByToken(string token, string ip)
		{
			return this.accountLogic.Login(token, ip);
		}

		public string LoginByUserName(string identifier, string password, string ip)
		{
			return this.accountLogic.Login(identifier, password, ip);
		}

		public bool Logout(string token, string ip)
		{
			return this.accountLogic.Logout(token, ip);
		}
	}
}