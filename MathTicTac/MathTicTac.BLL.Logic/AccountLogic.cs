using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic.Additional;
using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Add(Account item, string password)
        {
            return this.accDao.Add(item, Security.GetPassHash(password));
        }

        public Account Get(int id)
        {
            if (id > 0)
            {
                return accDao.Get(id);
            }

            throw new ArgumentOutOfRangeException();
        }

        public bool Login(string token, string ip)
        {
            return Security.TokenIpPairIsValid(token, ip, accDao);
        }

        public string Login(string identifier, string password, string ip)
        {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException();
            }

            int userId = accDao.GetUserIdByIdentifier(identifier);

            if (userId == 0)
            {
                return null;
            }

            if (Security.GetPassHash(password) == accDao.GetUserPassword(userId))
            {
                string userToken = accDao.GetUserTokenById(userId);

                if (userToken != null)
                {
                    accDao.DeleteToken(userToken);
                }

                return accDao.CreateToken(userId, ip);
            }

            return null;
        }

        public bool Logout(string token, string ip)
        {
            if (Security.TokenIpPairIsValid(token, ip, accDao))
            {
                return accDao.DeleteToken(token);
            }

            return true;
        }
    }
}
