using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.Tests.Logic
{
    internal static class MoqInit
    {
        internal static Mock<IAccountDao> accDaoMock = new Mock<IAccountDao>();
        internal static Mock<IGameDao> gameDaoMock = new Mock<IGameDao>();

        /// <summary>
        /// World with enemy turn wich can take state of bigcell 2.2. All other big cells states are None.
        /// </summary>
        private static DetailedWorld worldId69;

        static MoqInit()
        {
            MoqInit.ReInit();

            #region AccountDao mock object

            accDaoMock.Setup(f
                    => f.Add(It.IsAny<Account>(), It.IsAny<byte[]>()))
                    .Returns<Account, byte[]>((x, y) => 
                    {
                        if (x.Username == "snow")
                        {
                            x.Id = 13; return true;
                        }
                        else if (x.Username == "pr0gy")
                        {
                            x.Id = 12; return true;
                        }

                        return false;
                    });

            accDaoMock.Setup(f
                => f.Get(It.Is<int>(x => x == 13)))
                .Returns<int>(id => 
                {
                    if (id == 13)
                    {
                        return new Account() { Id = 13, Username = "snow" };
                    }
                    else if (id == 12)
                    {
                        return new Account() { Id = 12, Username = "pr0gy" };
                    }

                    return null;
                });

            accDaoMock.Setup(f
                => f.GetUserTokenById(It.IsAny<int>()))
                .Returns<int>(x => 
                {
                    if (x == 13)
                    {
                        return "058F39A9-420B-4F22-9689-47E99BD7E876";
                    }
                    else if (x == 12)
                    {
                        return "DAB93155-3737-44DD-8AC1-C81D7A23B712";
                    }

                    return null;
                });

            accDaoMock.Setup(f
                => f.GetUserIdByIdentifier(It.IsAny<string>()))
                .Returns<string>(identifier => 
                {
                    if (identifier == "snow")
                    {
                        return 13;
                    }
                    else if (identifier == "pr0gy")
                    {
                        return 12;
                    }

                    return 0;
                });

            accDaoMock.Setup(f
                => f.GetUserIdByToken(It.IsAny<string>()))
                .Returns<string>(token =>
                {
                    if (token == "058F39A9-420B-4F22-9689-47E99BD7E876")
                    {
                        return 13;
                    }
                    else if (token == "DAB93155-3737-44DD-8AC1-C81D7A23B712")
                    {
                        return 12;
                    }

                    return 0;
                });

            accDaoMock.Setup(f
                => f.GetUserPassword(It.IsAny<int>()))
                .Returns<int>(id =>
                {
                    if (id == 13 || id == 12)
                    {
                        return new byte[] { 110, 148, 203, 180, 130, 40, 207, 26, 229, 46, 54, 234, 188, 182, 147, 155, 53, 11, 124, 180, 59, 185, 232, 130, 226, 198, 105, 69, 161, 182, 158, 37, 73, 172, 3, 140, 151, 188, 40, 113, 85, 100, 57, 216, 189, 121, 126, 155, 106, 130, 113, 94, 165, 196, 246, 245, 30, 208, 147, 142, 141, 139, 78, 156 };
                    }

                    return null;
                });

            accDaoMock.Setup(f
                => f.DeleteToken(It.IsAny<string>()))
                .Returns<string>(x => { return x == "058F39A9-420B-4F22-9689-47E99BD7E876" || x == "DAB93155-3737-44DD-8AC1-C81D7A23B712"; });

            accDaoMock.Setup(f
                => f.CreateToken(It.IsAny<int>(), It.IsAny<string>()))
                .Returns<int, string>((id, ip) => 
                {
                    if (id == 13 && ip == "192.168.0.1")
                    {
                        return "058F39A9-420B-4F22-9689-47E99BD7E876";
                    }
                    else if (id == 12 && ip == "192.168.1.1")
                    {
                        return "DAB93155-3737-44DD-8AC1-C81D7A23B712";
                    }

                    return null;
               });

            accDaoMock.Setup(f
                => f.AcceptToken(It.IsAny<string>()))
                .Returns<string>( token =>
                {
                    if (token == "058F39A9-420B-4F22-9689-47E99BD7E876"
                    || token == "DAB93155-3737-44DD-8AC1-C81D7A23B712")
                    {
                        return DateTime.Now.AddDays(-5);
                    }

                    return null;
                });

            accDaoMock.Setup(f
                => f.IsTokenIpTrusted(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((token, ip) => 
                {
                    if ((token == "058F39A9-420B-4F22-9689-47E99BD7E876" && ip == "192.168.0.1")
                    || (token == "DAB93155-3737-44DD-8AC1-C81D7A23B712" && ip == "192.168.1.1"))
                    {
                        return true;
                    }

                    return false;
                });

            #endregion

            #region GameDao mock object

            gameDaoMock.Setup(f
                    => f.Add(It.IsAny<DetailedWorld>()))
                    .Returns(true);

            gameDaoMock.Setup(f
                => f.GetGameState(It.IsAny<int>()))
                .Returns<int>(id => 
                {
                    if (id == 69)
                    {
                        return MoqInit.worldId69;
                    }

                    return null;
                });

            gameDaoMock.Setup(f
                => f.Update(It.IsAny<DetailedWorld>()))
                .Returns(true);

            #endregion
        }

        internal static void ReInit()
        {
            MoqInit.worldId69 = new DetailedWorld(3)
            {
                Id = 69,
                ClientId = 13,
                EnemyId = 12,
                LastBigCellMove = new Coord(0, 0),
                LastCellMove = new Coord(2, 2),
                Status = GameStatus.EnemyTurn,
            };

            MoqInit.worldId69.BigCells[0, 0].Cells[0, 0].State = State.Client;
            MoqInit.worldId69.BigCells[0, 0].Cells[0, 1].State = State.Client;
            MoqInit.worldId69.BigCells[0, 0].Cells[0, 2].State = State.Enemy;
            MoqInit.worldId69.BigCells[0, 0].Cells[1, 1].State = State.Enemy;
            MoqInit.worldId69.BigCells[0, 0].Cells[2, 2].State = State.Client;

            MoqInit.worldId69.BigCells[1, 0].Cells[1, 0].State = State.Client;
            MoqInit.worldId69.BigCells[1, 0].Cells[2, 2].State = State.Enemy;

            MoqInit.worldId69.BigCells[0, 1].Cells[0, 2].State = State.Client;
            MoqInit.worldId69.BigCells[0, 1].Cells[1, 2].State = State.Client;

            MoqInit.worldId69.BigCells[1, 1].Cells[0, 1].State = State.Enemy;
            MoqInit.worldId69.BigCells[1, 1].Cells[1, 1].State = State.Client;
            MoqInit.worldId69.BigCells[1, 1].Cells[2, 1].State = State.Client;
            MoqInit.worldId69.BigCells[1, 1].Cells[1, 2].State = State.Client;

            MoqInit.worldId69.BigCells[2, 1].Cells[0, 1].State = State.Enemy;

            MoqInit.worldId69.BigCells[0, 2].Cells[0, 0].State = State.Enemy;
            MoqInit.worldId69.BigCells[0, 2].Cells[0, 1].State = State.Client;
            MoqInit.worldId69.BigCells[0, 2].Cells[1, 1].State = State.Enemy;
            MoqInit.worldId69.BigCells[0, 2].Cells[2, 2].State = State.Client;

            MoqInit.worldId69.BigCells[1, 2].Cells[1, 0].State = State.Enemy;
            MoqInit.worldId69.BigCells[1, 2].Cells[1, 1].State = State.Enemy;

            MoqInit.worldId69.BigCells[2, 2].Cells[0, 0].State = State.Enemy;
            MoqInit.worldId69.BigCells[2, 2].Cells[0, 1].State = State.Enemy;
            MoqInit.worldId69.BigCells[2, 2].Cells[2, 2].State = State.Client;
        }
    }
}
