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
            this.accountLogic = new AccountLogic(MoqInit.accDaoMock.Object);
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
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.1", true)]
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E877", "192.168.0.1", false)]
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.2", false)]
        public void LoggingByToken(string token, string ip, bool retVal)
        {
            var result = accountLogic.Login(token, ip);

            Assert.Equal<bool>(retVal, result);
        }

        [Theory]
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.1", true)]
        public void LoggingOut(string token, string ip, bool retVal)
        {
            var result = accountLogic.Logout(token, ip);

            Assert.Equal<bool>(retVal, result);
        }
    }
}
