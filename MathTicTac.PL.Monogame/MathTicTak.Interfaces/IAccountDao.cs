using MathTicTac.Entities;
using System;

namespace MathTicTak.Interfaces
{
	public interface IAccountDao
	{
		DateTime? AcceptToken(string token);

		bool Add(Account item, string password);

		Account Get(int id);

		void AddStatus(int id, GameStatus status);

		string CreateToken(int id);

		bool DeleteToken(string token);

		string GetUserPassword(int id);

		int GetUserIdByName(string name);

		int GetUserIdByToken(string token);

		bool TokenIpIsTrusted(string token, string ip);

		bool UpdateTokenDate(string token);
	}
}