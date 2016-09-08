using System.ServiceModel;
using MathTicTac.Enums;
using MathTicTac.PL.Soap.BindingLib.ServiceModels;

namespace MathTicTac.BLL.Interfaces
{
    [ServiceContract]
	public interface IAccountLogicService
	{
        [OperationContract]
		ResponseResult Add(AccountSM item, string password);

        [OperationContract]
        ResponseResult Get(int id, string token, string ip, out AccountSM account);
        
        [OperationContract(Name = "LoginById")]
        ResponseResult Login(string identifier, string password, string ip, out string token);

        [OperationContract(Name = "LoginByToken")]
        ResponseResult Login(string token, string ip);

        [OperationContract]
        ResponseResult Logout(string token, string ip);
	}
}