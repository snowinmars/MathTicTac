using System;
using System.Collections.Generic;
using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic;
using MathTicTac.BLL.Logic.Additional;
using MathTicTac.DAL.Dao;
using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.Enums;
using MathTicTac.PL.Soap.BindingLib.Interfaces;
using MathTicTac.PL.Soap.BindingLib.ServiceModels;

namespace MathTicTac.PL.Soap.BindingLib.Model
{
    public class GameLogicService : IGameLogicService
	{
		private const int dimension = 3;

        private static IAccountDao accDao = new AccountDao();
        private static IGameDao gameDao = new GameDao();
        
		private IGameLogic gameLogic = new GameLogic(gameDao, accDao);

		public ResponseResult Create(string player1Token, string player1Ip, string player2Identifier)
		{
		    return this.gameLogic.Create(player1Token, player1Ip, player2Identifier);
		}

		public TypedResponce<List<GameInfoSM>> GetAllActiveGames(string token, string ip)
		{
            IEnumerable<GameInfo> tempRes = new List<GameInfo>();

		    var curRes = this.gameLogic.GetAllActiveGames(token, ip, out tempRes);

            var tempResList = new List<GameInfoSM>();

		    foreach (var item in tempRes)
		    {
		        ((List<GameInfoSM>)tempResList).Add(this.GameInfoBind(item));
            }

            var result = new TypedResponce<List<GameInfoSM>>()
            {
                Value = tempResList,
                Responce = curRes
            };

		    return result;
		}

		public TypedResponce<WorldSM> GetCurrentWorld(string token, string ip, int gameId)
		{
            var tempRes = new World();

            var curRes = this.gameLogic.GetCurrentWorld(token, ip, gameId, out tempRes);

		    var result = new TypedResponce<WorldSM>()
		    {
		        Value = this.WorldBind(tempRes),
                Responce = curRes
            };

		    return result;
		}

		public ResponseResult MakeMove(MoveSM move)
		{
		    return this.gameLogic.MakeMove(this.MoveSMBind(move));
		}

		public ResponseResult RejectGame(string token, string ip, int gameId)
		{
		    return this.gameLogic.RejectGame(token, ip, gameId);
		}

        private GameInfoSM GameInfoBind(GameInfo item)
        {
            return new GameInfoSM()
            {
                ID = item.ID,
                OppositePlayerName = item.OppositePlayerName,
                TimeOfCreation = item.TimeOfCreation,
                status = item.status
            };
        }

        private WorldSM WorldBind(World item)
        {
            return new WorldSM(item.BigCells)
            {
                Id = item.Id,
                LastBigCellMove = item.LastBigCellMove,
                LastCellMove = item.LastCellMove,
                Status = item.Status,
            };
        }
        
        private Move MoveSMBind(MoveSM item)
        {
            return new Move(item.IP, item.Token, item.GameId, item.BigCellCoord, item.CellCoord);
        }
	}
}