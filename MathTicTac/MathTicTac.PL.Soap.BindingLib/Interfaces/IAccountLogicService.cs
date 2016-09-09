using System.ServiceModel;
using MathTicTac.Enums;
using MathTicTac.PL.Soap.BindingLib.ServiceModels;

namespace MathTicTac.BLL.Interfaces
{
    [ServiceContract]
	public interface IAccountLogicService
	{
        [OperationContract]
		ResponseResult Add(string name, string password);

        [OperationContract]
        TypedResponce<AccountSM> Get(int id, string token);
        
        [OperationContract(Name = "LoginById")]
        TypedResponce<string> Login(string identifier, string password);

        [OperationContract(Name = "LoginByToken")]
        ResponseResult Login(string token);

        [OperationContract]
        ResponseResult Logout(string token);
	}
}