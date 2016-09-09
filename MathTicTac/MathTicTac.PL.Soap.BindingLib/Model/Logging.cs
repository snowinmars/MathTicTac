using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.PL.Soap.BindingLib.Model
{
    internal class Logging
    {
        public Logging(String logicName)
        {
            this.logicName = logicName;
            MethodName = "undefined";
        }

        public string MethodName { get; set; }

        private string logicName;

        public void LogError(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine(ex.Message);
        }

        public void RequestStart()
        {
            Console.WriteLine(this.MarkedDivider($"\"{this.logicName}\" logic \"{this.MethodName}\" method request", 1));
        }

        public void ResponceStart()
        {
            Console.WriteLine(this.MarkedDivider($"\"{this.logicName}\" logic \"{this.MethodName}\" method responce", 1));
        }

        public void Data(string[] titles, string[] values)
        {
            Console.WriteLine(this.MarkedDivider("Data", 2));

            for (int i = 0; i < titles.Length; i++)
            {
                Console.WriteLine($"      {titles[i]}: {values[i]}");
            }
        }

        public void EndLine()
        {
            Console.WriteLine(this.MarkedFinish("", 0));
            Console.WriteLine();
        }

        public void TimeLine(DateTime time)
        {
            Console.WriteLine();
            Console.WriteLine(this.MarkedFinish(time.ToString(), 0));
        }

        private string MarkedDivider(string mark, int level)
        {
            var result = "";

            for (int i = 0; i < level*2; i++)
            {
                result += "-";
            }

            result += mark;

            for (int i = 0; i < 60-mark.Length-level*2; i++)
            {
                result += "-";
            }

            return result;
        }

        private string MarkedFinish(string mark, int level)
        {
            var result = "";

            for (int i = 0; i < level * 2; i++)
            {
                result += "=";
            }

            result += mark;

            for (int i = 0; i < 60 - mark.Length - level * 2; i++)
            {
                result += "=";
            }

            return result;
        }
    }
}
