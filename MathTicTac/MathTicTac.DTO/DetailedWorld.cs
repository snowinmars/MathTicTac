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

        public DetailedWorld(int dimension)
        {
            this.BigCells = new BigCell[dimension, dimension];

            for (int i = 0; i < this.BigCells.GetLength(0); i++)
            {
                for (int j = 0; j < this.BigCells.GetLength(1); j++)
                {
                    this.BigCells[i, j] = new BigCell(dimension);
                }
            }
        }
    }
}
