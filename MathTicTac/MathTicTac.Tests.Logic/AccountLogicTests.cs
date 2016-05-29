using System;
using Xunit;
using Moq;
using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.BLL.Logic;
using MathTicTac.BLL.Interfaces;

namespace MathTicTac.Tests.Logic
{
    public class AccountLogicTests
    {
        private IAccountLogic accountLogic;

        public AccountLogicTests()
        {
            Mock<IAccountDao> mock = new Mock<IAccountDao>();

            mock.Setup(f 
                => f.Add(It.IsAny<Account>(), It.IsAny<byte[]>()))
                .Returns<Account, byte[]>((x, y) => { x.Id = 13; return true; });
            mock.Setup(f
                => f.Add(It.Is<Account>(x => x == null), It.IsAny<byte[]>()))
                .Returns(false);

            mock.Setup(f 
                => f.Get(It.Is<int>(x => x == 13)))
                .Returns(new Account() { Id = 13, Username = "snow" });
            mock.Setup(f
                => f.Get(It.Is<int>(x => x != 13)))
                .Returns<Account>(null);

            mock.Setup(f
                => f.GetUserTokenById(It.Is<int>(x => x == 13)))
                .Returns<int>(x => { return "058F39A9-420B-4F22-9689-47E99BD7E876"; });
            mock.Setup(f
                => f.GetUserTokenById(It.Is<int>(x => x != 13)))
                .Returns<int>(x => { return null; });

            mock.Setup(f
                => f.GetUserIdByIdentifier(It.Is<string>(x => x == "snow")))
                .Returns(13);
            mock.Setup(f
                => f.GetUserIdByIdentifier(It.Is<string>(x => x != "snow")))
                .Returns(0);

            mock.Setup(f
                => f.GetUserPassword(It.Is<int>(x => x == 13)))
                .Returns(new byte[] { (byte)110, (byte)148, (byte)203, (byte)180, (byte)130, (byte)40, (byte)207, (byte)26, (byte)229, (byte)46, (byte)54, (byte)234, (byte)188, (byte)182, (byte)147, (byte)155, (byte)53, (byte)11, (byte)124, (byte)180, (byte)59, (byte)185, (byte)232, (byte)130, (byte)226, (byte)198, (byte)105, (byte)69, (byte)161, (byte)182, (byte)158, (byte)37, (byte)73, (byte)172, (byte)3, (byte)140, (byte)151, (byte)188, (byte)40, (byte)113, (byte)85, (byte)100, (byte)57, (byte)216, (byte)189, (byte)121, (byte)126, (byte)155, (byte)106, (byte)130, (byte)113, (byte)94, (byte)165, (byte)196, (byte)246, (byte)245, (byte)30, (byte)208, (byte)147, (byte)142, (byte)141, (byte)139, (byte)78, (byte)156 });
            mock.Setup(f
                => f.GetUserPassword(It.Is<int>(x => x != 13)))
                .Returns<byte[]>(null);

            mock.Setup(f
                => f.DeleteToken(It.IsAny<string>()))
                .Returns<string>(x => { return x == "058F39A9-420B-4F22-9689-47E99BD7E876"; });

            mock.Setup(f
                => f.CreateToken(It.Is<int>(x => x == 13), It.Is<string>(x => x == "192.168.0.1")))
                .Returns("058F39A9-420B-4F22-9689-47E99BD7E876");
            mock.Setup(f
                => f.CreateToken(It.Is<int>(x => x != 13), It.IsAny<string>()))
                .Returns<string>(null);

            this.accountLogic = new AccountLogic(mock.Object);
        }

        [Theory]
        // Correct name and password
        [InlineData("snow", "Pass", 13)]
        public void AddingUser(string name, string password, int id)
        {
            Account newUser = new Account();
            newUser.Username = name;

            var result = accountLogic.Add(newUser, password);

            Assert.Equal<int>(id, newUser.Id);
            Assert.True(result);
        }

        [Theory]
        // Correct ID
        [InlineData(13, "snow")]
        // Incorrect ID (Returns null)
        [InlineData(15, null)]
        public void GettingAccountById(int id, string res)
        {
            var result = accountLogic.Get(id);

            if (res != null)
            {
                Assert.Equal<string>(res, result.Username);
            }
            else
            {
                Assert.True(result == null);
            }
        }

        [Theory]

        [InlineData("snow", "Pass", "192.168.0.1", "058F39A9-420B-4F22-9689-47E99BD7E876")]
        public void LoggingByIdPassIp(string identifier, string password, string ip, string testRes)
        {
            var result = accountLogic.Login(identifier, password, ip);

            Assert.Equal<string>(testRes, result);
        }
    }
}
