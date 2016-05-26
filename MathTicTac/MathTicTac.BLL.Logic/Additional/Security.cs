using MathTicTac.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.BLL.Logic.Additional
{
    internal static class Security
    {
        internal static SHA512 shaM { get; private set; } = new SHA512Managed();

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

        internal static byte[] GetPassHash(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            return Security.shaM.ComputeHash(input);
        }

        private static bool IsHashesEquals(byte[] lhs, byte[] rhs)
        {
            if ((lhs == null) || (rhs == null))
            {
                throw new ArgumentNullException("Hash is null");
            }

            if (lhs.Length != rhs.Length)
            {
                return false;
            }

            for (int i = 0; i < lhs.Length; i++)
            {
                if (lhs[i] != rhs[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static byte[] ComputeHash(this SHA512 shaM, string text)
        {
            if (shaM == null)
            {
                throw new ArgumentNullException(nameof(shaM), "SHA manager is null");
            }

            byte[] data = Encoding.Unicode.GetBytes(text);

            return shaM.ComputeHash(data);
        }
    }
}
