using MathTicTac.ServiceModels;
using System.ServiceModel;

namespace MathTicTac.PL.Interfaces
{
	[ServiceContract]
	public interface IAccountService
	{
		[OperationContract]
		bool Add(AccountServiceModel item, string password);

		[OperationContract]
		AccountServiceModel Get(int id);

		[OperationContract]
		bool LoginByToken(string token, string ip);

		/// <summary>
		/// Login user.
		/// </summary>
		/// <param name="identifier"></param>
		/// <param name="password"></param>
		/// <param name="ip">todo: describe ip parameter on Login</param>
		/// <returns>User's token</returns>
		[OperationContract]
		string LoginByUserName(string identifier, string password, string ip);

		[OperationContract]
		bool Logout(string token, string ip);
	}
}