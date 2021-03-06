﻿using MathTicTac.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.PL.Interfaces
{
    public interface IAccountService
    {
        bool Add(AccountServiceModel item, string password);

        AccountServiceModel Get(int id);

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="password"></param>
        /// <returns>User's token</returns>
        string Login(string identifier, string password, string ip);

        bool Login(string token, string ip);

        bool Logout(string token, string ip);
    }
}
