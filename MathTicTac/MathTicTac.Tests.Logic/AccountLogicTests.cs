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
                => f.Add(It.IsAny<Account>(), It.IsAny<byte[]>())).Returns<Account, byte[]>((x, y) 
                => 
                {
                    x.Id = 13;
                    return true;
                });

            this.accountLogic = new AccountLogic(mock.Object);
        }

        [Theory]
        [InlineData("Samir", "")]
        public void AddingNewCorrectUserMustReturnTrueAndId(string name, string password)
        {
            Account newUser = new Account();
            newUser.Username = name;

            var result = accountLogic.Add(newUser, password);

            Assert.Equal<int>(13, newUser.Id);
            Assert.True(result);
        }
    }
}
