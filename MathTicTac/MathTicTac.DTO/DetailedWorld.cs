using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DTO
{
    public class DetailedWorld
    {
        public int Id { get; set; }
        public BigCell[,] BigCells { get; private set; }

        public Coord LastBigCellMove { get; set; }
        public Coord LastCellMove { get; set; }

        public int ClientId { get; set; }
        public int EnemyId { get; set; }

        public GameStatusVM Status { get; set; }

        public DetailedWorld(int id) : this(id, null)
        {
        }

        public DetailedWorld(int id, BigCell[,] bigCells)
        {
            this.BigCells = bigCells;

            this.Id = id;
        }
    }
}
