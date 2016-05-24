using MathTicTac.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.Logic.Additional
{
    internal static class Validation
    {
        internal static bool AccountValidation(Account input)
        {
            return input != null;
        }

        internal static bool MoveValidation(Move input)
        {
            if (string.IsNullOrWhiteSpace(input.Token) || string.IsNullOrWhiteSpace(input.IP))
            {
                return false;
            }

            return true;
        }
    }
}
