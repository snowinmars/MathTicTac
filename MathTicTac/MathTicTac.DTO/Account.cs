﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DTO
{
    public class Account
    {
        public string Username { get; private set; }
        public int Id { get; set; }
        public int Lose { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }

        public Account(string username)
        {
            this.Username = username;
        }
    }
}
