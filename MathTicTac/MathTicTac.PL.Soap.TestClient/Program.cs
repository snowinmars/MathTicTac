using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathTicTac.PL.Soap.TestClient.AccountService;

namespace MathTicTac.PL.Soap.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started...");
            Console.ReadLine();

            Console.WriteLine("Account Add test...");
            var user = AccountAdd("NewOne", "pass");
            Console.WriteLine("Created user with ID {0}", user.Id);

            Console.WriteLine("Ended.");
            Console.ReadLine();
        }

        public static AccountSM AccountAdd(string name, string pass)
        {
            var user = new AccountSM()
            {
                Username = name,
            };

            using (var service = new AccountService.AccountLogicServiceClient())
            {
                service.Add(user, pass);
            }

            return user;
        }
    }
}
