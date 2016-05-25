using MathTicTac.DTO;
using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DAL.Interfaces
{
    public interface IAccountDao
    {
        DateTime? AcceptToken(string token);

        bool Add(Account item, string password);

        Account Get(int id);

        void AddStatus(int id, GameResult result);

        string CreateToken(int id, string ip);

        bool DeleteToken(string token);

        string GetUserPassword(int id);

        int GetUserIdByIdentifier(string identifier);

        int GetUserIdByToken(string token);

        string GetUserNameById(int id);

        string GetUserTokenById(int id);

        bool IsTokenIpTrusted(string token, string ip);

        bool UpdateTokenDate(string token);
    }
}
