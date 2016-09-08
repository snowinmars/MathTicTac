using System;
using System.ServiceModel;
using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic;
using MathTicTac.DAL.Dao;
using MathTicTac.Enums;
using MathTicTac.PL.Soap.BindingLib.ServiceModels;
using MathTicTac.DTO;

namespace MathTicTac.PL.Soap.BindingLib.Model
{
	public class AccountLogicService : IAccountLogicService
	{
		private readonly IAccountLogic accLogic = new AccountLogic(new AccountDao());

		public ResponseResult Add(AccountSM item, string password)
		{
            return this.accLogic.Add(this.AccountSMBind(item), password);
		}

		public ResponseResult Get(int id, string token, string ip, out AccountSM account)
		{
		    var tempAcc = new Account();

		    var result = this.accLogic.Get(id, token, ip, out tempAcc);

		    account = this.AccountBind(tempAcc);

		    return result;
		}

		public ResponseResult Login(string token, string ip)
		{
            return this.accLogic.Login(token, ip);
		}

		public ResponseResult Login(string identifier, string password, string ip, out string token)
		{
		    return this.accLogic.Login(identifier, password, ip, out token);
		}

		public ResponseResult Logout(string token, string ip)
		{
            return this.accLogic.Logout(token, ip);
		}

	    private Account AccountSMBind(AccountSM item)
	    {
	        return new Account()
	        {
                Id = item.Id,
                Username = item.Username,
                Draw = item.Draw,
                Won = item.Won,
                Lose = item.Lose
            };
	    }

        private AccountSM AccountBind(Account item)
        {
            if (item == null)
            {
                return null;
            }

            return new AccountSM()
            {
                Id = item.Id,
                Username = item.Username,
                Draw = item.Draw,
                Won = item.Won,
                Lose = item.Lose
            };
        }
    }
}