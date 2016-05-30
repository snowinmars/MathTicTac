using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
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

        static MoqInit()
        {
            accDaoMock.Setup(f
                => f.Add(It.IsAny<Account>(), It.IsAny<byte[]>()))
                .Returns<Account, byte[]>((x, y) => { x.Id = 13; return true; });
            accDaoMock.Setup(f
                => f.Add(It.Is<Account>(x => x == null), It.IsAny<byte[]>()))
                .Returns(false);

            accDaoMock.Setup(f
                => f.Get(It.Is<int>(x => x == 13)))
                .Returns(new Account() { Id = 13, Username = "snow" });
            accDaoMock.Setup(f
                => f.Get(It.Is<int>(x => x != 13)))
                .Returns<Account>(null);

            accDaoMock.Setup(f
                => f.GetUserTokenById(It.Is<int>(x => x == 13)))
                .Returns<int>(x => { return "058F39A9-420B-4F22-9689-47E99BD7E876"; });
            accDaoMock.Setup(f
                => f.GetUserTokenById(It.Is<int>(x => x != 13)))
                .Returns<int>(x => { return null; });

            accDaoMock.Setup(f
                => f.GetUserIdByIdentifier(It.Is<string>(x => x == "snow")))
                .Returns(13);
            accDaoMock.Setup(f
                => f.GetUserIdByIdentifier(It.Is<string>(x => x != "snow")))
                .Returns(0);

            accDaoMock.Setup(f
                => f.GetUserPassword(It.Is<int>(x => x == 13)))
                .Returns(new byte[] { 110, 148, 203, 180, 130, 40, 207, 26, 229, 46, 54, 234, 188, 182, 147, 155, 53, 11, 124, 180, 59, 185, 232, 130, 226, 198, 105, 69, 161, 182, 158, 37, 73, 172, 3, 140, 151, 188, 40, 113, 85, 100, 57, 216, 189, 121, 126, 155, 106, 130, 113, 94, 165, 196, 246, 245, 30, 208, 147, 142, 141, 139, 78, 156 });
            accDaoMock.Setup(f
                => f.GetUserPassword(It.Is<int>(x => x != 13)))
                .Returns<byte[]>(null);

            accDaoMock.Setup(f
                => f.DeleteToken(It.IsAny<string>()))
                .Returns<string>(x => { return x == "058F39A9-420B-4F22-9689-47E99BD7E876"; });

            accDaoMock.Setup(f
                => f.CreateToken(It.Is<int>(x => x == 13), It.Is<string>(x => x == "192.168.0.1")))
                .Returns("058F39A9-420B-4F22-9689-47E99BD7E876");
            accDaoMock.Setup(f
                => f.CreateToken(It.Is<int>(x => x != 13), It.IsAny<string>()))
                .Returns<string>(null);

            accDaoMock.Setup(f
                => f.AcceptToken(It.Is<string>(x => x == "058F39A9-420B-4F22-9689-47E99BD7E876")))
                .Returns(DateTime.Now.AddDays(-5));
            accDaoMock.Setup(f
                => f.AcceptToken(It.Is<string>(x => x != "058F39A9-420B-4F22-9689-47E99BD7E876")))
                .Returns<DateTime?>(null);

            accDaoMock.Setup(f
                => f.IsTokenIpTrusted(It.Is<string>(x => x == "058F39A9-420B-4F22-9689-47E99BD7E876"), It.Is<string>(x => x == "192.168.0.1")))
                .Returns(true);
            accDaoMock.Setup(f
                => f.IsTokenIpTrusted(It.Is<string>(x => x != "058F39A9-420B-4F22-9689-47E99BD7E876"), It.Is<string>(x => x != "192.168.0.1")))
                .Returns(false);
        }
    }
}
