using Config;
using MathTicTak.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.Logic.Additional
{
    internal static class Security
    {
        internal static string GetHashFromString(string input)
        {
            return input;
        }

        internal static bool TokenIpPairIsValid(string token, string ip, IAccountDao dao)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException();
            }

            DateTime? tokenDate = dao.AcceptToken(token);

            if (tokenDate != null && tokenDate.Value.AddDays(MathTicTacConfiguration.tokenExpirationDays) > DateTime.Now &&
                dao.IsTokenIpTrusted(token, ip))
            {
                dao.UpdateTokenDate(token);
                return true;
            }
            else if (tokenDate != null)
            {
                dao.DeleteToken(token);
            }

            return false;
        }
    }
}
