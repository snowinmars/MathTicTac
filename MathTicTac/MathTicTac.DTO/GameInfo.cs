using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DTO
{
    public class GameInfo
    {
        public int ID { get; set; }
        public int OppositePlayerName { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public GameStatusVM status { get; set; }
    }
}
