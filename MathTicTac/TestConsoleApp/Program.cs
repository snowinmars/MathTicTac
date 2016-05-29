﻿using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic;
using MathTicTac.DAL.Dao;
using MathTicTac.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool testVal;
            string enteredValue;

            #region Layers init
            Console.WriteLine("============================================");
            Console.WriteLine("ACCOUNT LOGIC INIT...");
            Console.WriteLine("--------------------------------------------");
            IAccountLogic accLogic = new AccountLogic(new AccountDao());
            Console.WriteLine("Account logic init OK");
            Console.WriteLine("============================================");
            #endregion

            #region Adding function test
            Console.WriteLine("Press any key to test Adding func...");
            Console.ReadKey();
            Console.WriteLine("============================================");
            Console.WriteLine("ADDING NEW USER...");
            Console.WriteLine("--------------------------------------------");

            Account user = new Account();
            string name = "pr0gy";
            string password = "pass";
            user.Username = name;

            testVal = accLogic.Add(user, password);

            Console.WriteLine("Adding new user returns " + testVal.ToString().ToUpper());
            if (testVal)
            {
                Console.WriteLine("Added user ID is " + user.Id);
            }
            Console.WriteLine("============================================");
            #endregion

            #region Getting function test
            Console.WriteLine("Press any key to test Getting user by ID func...");
            Console.ReadKey();
            Console.WriteLine("============================================");

            int userId = 13;

            Console.WriteLine($"GETTING USER BY ID...");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"INPUT");
            Console.WriteLine($"INPUT");
            Console.WriteLine("--------------------------------------------");

            
            
            Account userResult = accLogic.Get(userId);

            if (userResult == null)
            {
                Console.WriteLine("Method returned NULL value.");
            }
            else
            {
                Console.WriteLine($"User name: {userResult.Username}; Games won: {userResult.Won}; Games lose: {userResult.Lose}; Games draw: {userResult.Draw}");
            }
            Console.WriteLine("============================================"); 
            #endregion


            Console.ReadKey();
        }
    }
}
