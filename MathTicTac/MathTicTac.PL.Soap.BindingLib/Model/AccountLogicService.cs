using System;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Channels;
using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic;
using MathTicTac.DAL.Dao;
using MathTicTac.Enums;
using MathTicTac.PL.Soap.BindingLib.ServiceModels;
using MathTicTac.DTO;

namespace MathTicTac.PL.Soap.BindingLib.Model
{
	public class AccountLogicService : IAccountLogicService
	{
	    public AccountLogicService()
	    {
	        this.Log = new Logging("Account");
	    }

        private readonly IAccountLogic accLogic = new AccountLogic(new AccountDao());
        private Logging Log { get; set; }

		public ResponseResult Add(string name, string password)
		{
		    try
		    {
		        Log.MethodName = "Add";

		        Log.TimeLine(DateTime.Now);
		        Log.RequestStart();
		        Log.Data(
		            new string[] {"Username", "Password"},
		            new string[] {name, password});

		        var bindedAcc = new Account() {Username = name};
		        var result = this.accLogic.Add(bindedAcc, password);

		        Log.ResponceStart();
		        Log.Data(
		            new string[] {"Result"},
		            new string[] {result.ToString()});
		        Log.EndLine();

		        return result;
		    }
		    catch (Exception ex)
		    {
		        Log.LogError(ex);
                return ResponseResult.None;
		    }
		}

		public TypedResponce<AccountSM> Get(int id, string token)
		{
		    try
		    {
		        var ip = this.GetIP();

                Log.MethodName = "Get";

                Log.TimeLine(DateTime.Now);
                Log.RequestStart();
                Log.Data(
                    new string[] { "Id", "Token", "Ip" },
                    new string[] { id.ToString(), token, ip });

                var tempAcc = new Account();

                var resp = this.accLogic.Get(id, token, ip, out tempAcc);

                var result = new TypedResponce<AccountSM>()
                {
                    Value = this.AccountBind(tempAcc),
                    Responce = resp
                };

                Log.ResponceStart();
                Log.Data(
                    new string[]
                    {
                    "Result",
                    "Id",
                    "Username",
                    "Draw",
                    "Won",
                    "Lose"
                    },
                    new string[]
                    {
                    result.Responce.ToString(),
                    result.Value.Id.ToString(),
                    result.Value.Username,
                    result.Value.Draw.ToString(),
                    result.Value.Won.ToString(),
                    result.Value.Lose.ToString()
                    });
                Log.EndLine();

                return result;
            }
		    catch (Exception ex)
		    {
		        Log.LogError(ex);
		        return new TypedResponce<AccountSM>()
		        {
                    Value = null,
                    Responce = ResponseResult.None
		        };
		    }
		}

		public ResponseResult Login(string token)
		{
		    try
		    {
		        var ip = this.GetIP();

                Log.MethodName = "Login";

                Log.TimeLine(DateTime.Now);
                Log.RequestStart();
                Log.Data(
                    new string[] { "Token", "Ip" },
                    new string[] { token, ip });

                var result = this.accLogic.Login(token, ip);

                Log.ResponceStart();
                Log.Data(
                    new string[]
                    {
                    "Result"
                    },
                    new string[]
                    {
                    result.ToString()
                    });
                Log.EndLine();

                return result;
            }
		    catch (Exception ex)
		    {
		        Log.LogError(ex);
		        return ResponseResult.None;
		    }
            
		}

		public TypedResponce<string> Login(string identifier, string password)
        {
		    try
		    {
		        var ip = this.GetIP();

                Log.MethodName = "Login";

                Log.TimeLine(DateTime.Now);
                Log.RequestStart();
                Log.Data(
                    new string[] { "Identifier", "Password", "Ip" },
                    new string[] { identifier, password, ip });

                string token;
                var resp = this.accLogic.Login(identifier, password, ip, out token);

                var result = new TypedResponce<string>()
                {
                    Value = token,
                    Responce = resp
                };

                Log.ResponceStart();
                Log.Data(
                    new string[]
                    {
                    "Result",
                    "Token"
                    },
                    new string[]
                    {
                    result.Responce.ToString(),
                    result.Value
                    });
                Log.EndLine();

                return result;
            }
		    catch (Exception ex)
		    {
		        Log.LogError(ex);
                return new TypedResponce<string>()
                {
                    Responce = ResponseResult.None,
                    Value = null
                };
		    }
            
		}

		public ResponseResult Logout(string token)
		{
		    try
		    {
		        var ip = this.GetIP();

                Log.MethodName = "Logout";

                Log.TimeLine(DateTime.Now);
                Log.RequestStart();
                Log.Data(
                    new string[] { "Token", "Ip" },
                    new string[] { token, ip });

                var result = this.accLogic.Logout(token, ip);

                Log.ResponceStart();
                Log.Data(
                    new string[]
                    {
                    "Result"
                    },
                    new string[]
                    {
                    result.ToString()
                    });
                Log.EndLine();

                return result;
            }
		    catch (Exception ex)
		    {
                Log.LogError(ex);
                return ResponseResult.None;
            }
            
		}

	    private Account AccountSMBind(AccountSM item)
	    {
	        return new Account()
	        {
                Id = item.Id,
                Username = item.Username,
                Draw = item.Draw,
                Won = item.Won,
                Lose = item.Lose
            };
	    }

        private AccountSM AccountBind(Account item)
        {
            if (item == null)
            {
                return null;
            }

            return new AccountSM()
            {
                Id = item.Id,
                Username = item.Username,
                Draw = item.Draw,
                Won = item.Won,
                Lose = item.Lose
            };
        }

        private string GetIP()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint =
               prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;

            return ip;
        }
    }
}