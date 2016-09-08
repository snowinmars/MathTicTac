using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MathTicTac.PL.Soap.BindingLib.Model;

namespace MathTicTac.PL.Soap.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var accHost = new ServiceHost(typeof(AccountLogicService)))
            {
                using (var gameHost = new ServiceHost(typeof(GameLogicService)))
                {
                    accHost.Open();
                    gameHost.Open();

                    System.Console.WriteLine("Service started...{0}Press any key to stop...", Environment.NewLine);
                    System.Console.ReadLine();

                    accHost.Close();
                    gameHost.Close();

                    System.Console.WriteLine("Service stopped");
                    System.Console.ReadLine();
                }
            }
        }
    }
}
