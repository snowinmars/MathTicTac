using MathTicTac.DTO;
using MathTicTac.Enums;

namespace MathTicTac.BLL.Interfaces
{
	public interface IAccountLogic
	{
		ResponseResult Add(Account item, string password);

        ResponseResult Get(int id, string token, string ip, out Account account);

	    /// <summary>
	    /// Login user.
	    /// </summary>
	    /// <param name="identifier"></param>
	    /// <param name="password"></param>
	    /// <param name="ip"></param>
	    /// <param name="token"></param>
	    /// <returns>User's token</returns>
	    ResponseResult Login(string identifier, string password, string ip, out string token);

        ResponseResult Login(string token, string ip);

        ResponseResult Logout(string token, string ip);
	}
}