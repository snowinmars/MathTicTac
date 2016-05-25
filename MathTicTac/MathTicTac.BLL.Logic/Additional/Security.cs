using MathTicTac.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.BLL.Logic.Additional
{
    internal static class Security
    {
        internal static bool TokenIpPairIsValid(string token, string ip, IAccountDao accDao)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException();
            }

            DateTime? tokenDate = accDao.AcceptToken(token);

            if (tokenDate != null && tokenDate.Value.AddDays(15) > DateTime.Now &&
                accDao.IsTokenIpTrusted(token, ip))
            {
                accDao.UpdateTokenDate(token);
                return true;
            }
            else if (tokenDate != null)
            {
                accDao.DeleteToken(token);
            }

            return false;
        }

        internal static string GetPassHash(string input)
        {
            return input;
        }
    }
}
