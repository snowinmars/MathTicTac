using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic;
using MathTicTac.DTO;
using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MathTicTac.Tests.Logic
{
    public class GameLogicTests
    {
        private IAccountLogic accountLogic;
        private IGameLogic gameLogic;

        public GameLogicTests()
        {
            this.accountLogic = new AccountLogic(MoqInit.accDaoMock.Object);
            this.gameLogic = new GameLogic(MoqInit.gameDaoMock.Object, MoqInit.accDaoMock.Object);
        }

        [Fact]
        public void CreatingWorldWithCorrectInputData()
        {
            var result = gameLogic.Create("058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.1", "pr0gy");

            Assert.Equal(ResponseResult.Ok, result);
        }

        [Theory]
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.1", 69, GameStatus.EnemyTurn)]
        [InlineData("DAB93155-3737-44DD-8AC1-C81D7A23B712", "192.168.1.1", 69, GameStatus.ClientTurn)]
        public void GettingCurrentWorldByCorrectData(string token, string ip, int gameId, GameStatus avaitingRes)
        {
            World retVal;
            var result = gameLogic.GetCurrentWorld(token, ip, gameId, out retVal);

            Assert.Equal(avaitingRes, retVal.Status);
        }
    }
}
