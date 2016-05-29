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
                .Returns(new byte[] { 110, 148, 203, 180, 130, 40, 207, 26, 229, 46, 54, 234, 188, 182, 147, 155, 53, 11, 124, 180, 59, 185, 232, 130, 226, 198, 105, 69, 161, 182, 158, 37, 73, 172, 3, 140, 151, 188, 40, 113, 85, 100, 57, 216, 189, 121, 126, 155, 106, 130, 113, 94, 165, 196, 246, 245, 30, 208, 147, 142, 141, 139, 78, 156 });
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

            mock.Setup(f
                => f.AcceptToken(It.Is<string>(x => x == "058F39A9-420B-4F22-9689-47E99BD7E876")))
                .Returns(DateTime.Now.AddDays(-5));
            mock.Setup(f
                => f.AcceptToken(It.Is<string>(x => x != "058F39A9-420B-4F22-9689-47E99BD7E876")))
                .Returns<DateTime?>(null);

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
        // Correct data
        [InlineData("snow", "Pass", "192.168.0.1", "058F39A9-420B-4F22-9689-47E99BD7E876")]
        // Incorrect data
        [InlineData("pr0gy", "pass", "192.168.0.1", null)]
        public void LoggingByIdPassIp(string identifier, string password, string ip, string testRes)
        {
            var result = accountLogic.Login(identifier, password, ip);

            Assert.Equal<string>(testRes, result);
        }

        [Theory]
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.1")]
        public void LoggingByToken(string token, string ip)
        {
            var result = accountLogic.Login(token, ip);

            Assert.True(result);
        }
    }
}
