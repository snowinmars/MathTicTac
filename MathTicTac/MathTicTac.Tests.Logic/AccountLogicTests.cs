using System;
using Xunit;
using Moq;
using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.BLL.Logic;
using MathTicTac.BLL.Interfaces;
using MathTicTac.Enums;

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
        [InlineData("snow", "Pass", 13, ResponseResult.Ok)]
        public void AddingCorrectUser(string name, string password, int id, ResponseResult retVal)
        {
            Account newUser = new Account();
            newUser.Username = name;

            var result = accountLogic.Add(newUser, password);

            Assert.Equal<int>(id, newUser.Id);
            Assert.Equal(retVal, result);
        }

        [Theory]
        [InlineData(13, "058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.1", "snow")]
        public void GettingAccountByCoorectData(int id, string token, string ip, string res)
        {
            Account account;

            var result = accountLogic.Get(id, token, ip, out account);

            switch (result)
            {
                case ResponseResult.Ok:
                    Assert.Equal<string>(res, account.Username);
                    break;
                case ResponseResult.TokenInvalid:
                case ResponseResult.AccountDataInvalid:
                case ResponseResult.TurnUnavailiable:
                case ResponseResult.None:
                default:
                    throw new InvalidOperationException();
            }
        }

        [Theory]
        [InlineData("snow", "Pass", "192.168.0.1", "058F39A9-420B-4F22-9689-47E99BD7E876")]
        public void LoggingByCorrectIdPassIp(string identifier, string password, string ip, string testToken)
        {
            string token;

            var result = accountLogic.Login(identifier, password, ip, out token);

            Assert.Equal<string>(testToken, token);
            Assert.Equal(ResponseResult.Ok, result);
        }

        [Theory]
        [InlineData("pr0gy", "pass", "192.168.0.1")]
        public void LoggingByIncorrectIdPassIp(string identifier, string password, string ip)
        {
            string token;

            var result = accountLogic.Login(identifier, password, ip, out token);

            Assert.Equal<string>(null, token);
            Assert.Equal(ResponseResult.AccountDataInvalid, result);
        }

        [Theory]
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.1")]
        public void LoggingByCorrectTokenIpPair(string token, string ip)
        {
            var result = accountLogic.Login(token, ip);

            Assert.Equal(ResponseResult.Ok, result);
        }

        [Theory]
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E877", "192.168.0.1")]
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.2")]
        public void LoggingByIncorrectTokenIpPair(string token, string ip)
        {
            var result = accountLogic.Login(token, ip);

            Assert.Equal(ResponseResult.TokenInvalid, result);
        }

        [Theory]
        [InlineData("058F39A9-420B-4F22-9689-47E99BD7E876", "192.168.0.1")]
        public void LoggingOutByCorrectTokenIpPair(string token, string ip)
        {
            var result = accountLogic.Logout(token, ip);

            Assert.Equal(ResponseResult.Ok, result);
        }
    }
}
