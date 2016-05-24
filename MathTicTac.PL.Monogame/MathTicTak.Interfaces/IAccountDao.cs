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

		string CreateToken(int id, string ip);

		bool DeleteToken(string token);

		string GetUserPassword(int id);

		int GetUserIdByIdentifier(string identifier);

		int GetUserIdByToken(string token);

        string GetUserTokenById(int id);

        bool IsTokenIpTrusted(string token, string ip);

		bool UpdateTokenDate(string token);
	}
}