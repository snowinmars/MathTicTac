using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DTO
{
    public class Cell
    {
        public Cell(State state)
        {
            this.State = state;
        }

        public State State { get; set; }
    }
}
