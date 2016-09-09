using System.Collections.Generic;
using System.ServiceModel;
using MathTicTac.Enums;
using MathTicTac.PL.Soap.BindingLib.ServiceModels;

namespace MathTicTac.PL.Soap.BindingLib.Interfaces
{
    [ServiceContract]
    public interface IGameLogicService
	{
        [OperationContract]
        ResponseResult MakeMove(MoveSM move);

        [OperationContract]
        ResponseResult RejectGame(string token, string ip, int gameId);

        [OperationContract]
        ResponseResult Create(string player1Token, string player1Ip, string player2Identifier);

        [OperationContract]
        TypedResponce<WorldSM> GetCurrentWorld(string token, string ip, int gameId);

        [OperationContract]
        TypedResponce<List<GameInfoSM>> GetAllActiveGames(string token, string ip);
	}
}