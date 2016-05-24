using MathTicTak.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathTicTac.Entities;
using MathTicTac.Logic.Additional;
using Config;

namespace MathTicTac.Logic
{
    public class AccountLogic : IAccountLogic
    {
        private IAccountDao dao;

        public AccountLogic(IAccountDao dao)
        {
            if (dao == null)
            {
                throw new ArgumentNullException();
            }

            this.dao = dao;
        }

        public bool Add(Account item, string password)
        {
            if (Validation.AccountValidation(item) && !String.IsNullOrWhiteSpace(password))
            {
                return this.dao.Add(item, Security.GetHashFromString(password));
            }

            return false;
        }

        public Account Get(int id)
        {
            if (id > 0)
            {
                return dao.Get(id);
            }

            throw new ArgumentOutOfRangeException();
        }

        public bool Login(string token, string ip)
        {
            return Security.TokenIpPairIsValid(token, ip, dao);
        }

        public string Login(string identifier, string password, string ip)
        {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException();
            }

            int userId = dao.GetUserIdByIdentifier(identifier);

            if (userId == 0)
            {
                return null;
            }

            if (Security.GetHashFromString(password) == dao.GetUserPassword(userId))
            {
                string userToken = dao.GetUserTokenById(userId);

                if (userToken != null)
                {
                    dao.DeleteToken(userToken);
                }

                return dao.CreateToken(userId, ip);
            }

            return null;
        }

        public bool Logout(string token, string ip)
        {
            if (Security.TokenIpPairIsValid(token, ip, dao))
            {
                return dao.DeleteToken(token);
            }

            return true;
        }
    }
}
