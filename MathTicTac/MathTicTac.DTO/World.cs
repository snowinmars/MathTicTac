using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DTO
{
    public class World
    {
        public int Id { get; set; }
        public BigCell[,] BigCells { get; private set; }

        public Coord NextMove { get; set; }

        public GameStatusVM Status { get; set; }

        public World(int id) : this(id, null)
        {
        }

        public World(int id, BigCell[,] bigCells)
        {
            this.BigCells = bigCells;

            this.Id = id;
        }
    }
}
