using MathTicTac.Entities;

namespace MathTicTak.Interfaces
{
	public interface IAccountLogic
	{
		/// <summary>
		/// Checks, is token is avaliable for user.
		/// </summary>
		/// <param name="id">User's id</param>
		/// <param name="token">User's token</param>
		/// <returns></returns>
		bool AcceptToken(int id, string token);

		bool Add(Account item, string password);

		Account Get(int id);

		/// <summary>
		/// Login user.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="password"></param>
		/// <returns>User's token</returns>
		string Login(int id, string password);

		bool Logout(int id);

		bool TokenIsValid(string token);

		//bool Remove(int id);

		//bool Update(Account item, string oldPassword, string newPassword = null);
	}
}