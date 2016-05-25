﻿using MathTicTac.DTO;

namespace MathTicTac.BLL.Interfaces
{
	public interface IAccountLogic
	{
		bool Add(Account item, string password);

		Account Get(int id);

		/// <summary>
		/// Login user.
		/// </summary>
		/// <param name="identifier"></param>
		/// <param name="password"></param>
		/// <returns>User's token</returns>
		string Login(string identifier, string password, string ip);

		bool Login(string token, string ip);

		bool Logout(string token, string ip);
	}
}