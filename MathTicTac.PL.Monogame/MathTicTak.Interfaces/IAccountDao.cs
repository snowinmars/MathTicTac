using MathTicTac.Entities;
using MathTicTac.Entities.Enum;

namespace MathTicTak.Interfaces
{
	public interface IAccountDao
	{
		bool AcceptToken(int id, string token);

        bool Add(Account item, string password);

        Account Get(int id);

        void AddStatus(int id, GameResult result);
        
        string CreateToken(int id);

        bool DeactivateToken(int id);

        bool TokenIsValid(string token);

        string GetUserPassword(int id);

        int GetUserIdByName(string name);

        int GetUserIdByToken(string token);
    }
}