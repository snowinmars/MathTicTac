﻿using MathTicTac.Entities.Enum;
using System;

namespace MathTicTac.Entities
{
    public class GameInfo
    {
        public int ID { get; set; }
        public int OppositePlayerName { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public GameStatus status { get; set; }
    }
}