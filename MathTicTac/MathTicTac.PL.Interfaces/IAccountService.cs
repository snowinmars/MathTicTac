using MathTicTac.ServiceModels;

namespace MathTicTac.PL.Interfaces
{
	public interface IAccountService
	{
		bool Add(AccountServiceModel item, string password);

		AccountServiceModel Get(int id);

		bool LoginByToken(string token, string ip);

		/// <summary>
		/// Login user.
		/// </summary>
		/// <param name="identifier"></param>
		/// <param name="password"></param>
		/// <param name="ip">todo: describe ip parameter on Login</param>
		/// <returns>User's token</returns>
		string LoginByUserName(string identifier, string password, string ip);

		bool Logout(string token, string ip);
	}
}