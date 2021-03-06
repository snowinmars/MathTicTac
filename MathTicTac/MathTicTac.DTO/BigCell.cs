﻿using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DTO
{
    public class BigCell
    {
        public BigCell() : this(State.None,
                        false,
                        null)
        {
        }

        public BigCell(State state, bool isFocus, Cell[,] cells)
        {
            this.Cells = cells;
            this.State = state;
            this.IsFocus = isFocus;
        }

        public Cell[,] Cells { get; private set; }
        public bool IsFocus { get; set; }
        
        public State State { get; set; }
    }
}
