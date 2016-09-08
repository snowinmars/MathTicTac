using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic.Additional;
using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.Enums;
using System;
using System.Linq;
using System.ServiceModel;

namespace MathTicTac.BLL.Logic
{
	public class AccountLogic : IAccountLogic
	{
		private IAccountDao accDao;

		public AccountLogic(IAccountDao accDao)
		{
			if (accDao == null)
			{
				throw new ArgumentNullException();
			}

			this.accDao = accDao;
		}

		public ResponseResult Add(Account item, string password)
		{
            if (this.accDao.Add(item, Security.GetPassHash(password)))
            {
                return ResponseResult.Ok;
            }

            return ResponseResult.None;
		}

		public ResponseResult Get(int id, string token, string ip, out Account account)
		{
            if (!Security.TokenIpPairIsValid(token, ip, accDao))
            {
                account = null;

                return ResponseResult.TokenInvalid;
            }

			if (id > 0)
			{
                account = accDao.Get(id);

                return ResponseResult.Ok;
			}

			throw new ArgumentOutOfRangeException();
		}

		public ResponseResult Login(string token, string ip)
		{
            if (Security.TokenIpPairIsValid(token, ip, accDao))
            {
                return ResponseResult.Ok;
            }

            return ResponseResult.TokenInvalid;
		}

		public ResponseResult Login(string identifier, string password, string ip, out string token)
		{
			if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(ip))
			{
				throw new ArgumentNullException();
			}

            token = null;

            int userId = accDao.GetUserIdByIdentifier(identifier);

			if (userId == 0)
			{
				return ResponseResult.AccountDataInvalid;
			}

            var tempPass = accDao.GetUserPassword(userId);

            if (tempPass != null && Security.GetPassHash(password).SequenceEqual(tempPass))
			{
				string userToken = accDao.GetUserTokenById(userId);

				if (userToken != null)
				{
					accDao.DeleteToken(userToken);
				}

                token = accDao.CreateToken(userId, ip);

                return ResponseResult.Ok;
			}

			return ResponseResult.AccountDataInvalid;
		}

		public ResponseResult Logout(string token, string ip)
		{
			if (Security.TokenIpPairIsValid(token, ip, accDao) && accDao.DeleteToken(token))
			{
				return ResponseResult.Ok;
			}

            return ResponseResult.TokenInvalid;
		}
	}
}