using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic;
using MathTicTac.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
