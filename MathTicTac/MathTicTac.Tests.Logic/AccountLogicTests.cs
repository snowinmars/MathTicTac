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
                .Returns<Account, byte[]>((x, y) 
                => {
                    x.Id = 13;
                    return true;
                });
            mock.Setup(f 
                => f.Get(It.IsAny<int>()))
                .Returns<int>(x
                => {
                    if (x == 13)
                    {
                        return new Account()
                        {
                            Id = 13,
                            Username = "snow"
                        };
                    }

                    return null;
                });

            this.accountLogic = new AccountLogic(mock.Object);
        }

        [Theory]
        [InlineData("Samir", "Pass")]
        [InlineData("pr0gy", "Pass")]
        public void AddingNewCorrectUserMustReturnTrueAndId(string name, string password)
        {
            Account newUser = new Account();
            newUser.Username = name;

            var result = accountLogic.Add(newUser, password);

            Assert.Equal<int>(13, newUser.Id);
            Assert.True(result);
        }

        [Theory]
        [InlineData(13, "snow")]
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
    }
}
