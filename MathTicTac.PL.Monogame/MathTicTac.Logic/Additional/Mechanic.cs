using MathTicTac.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.Logic.Additional
{
    internal static class Mechanic
    {
        internal static bool MoveCoordsAllowed(World world, Move move)
        {
            Coord prevMove = null;

            foreach (var bigCell in world.BigCells)
            {
                for (int x = 0; x < bigCell.Cells.GetLength(0); x++)
                {
                    for (int y = 0; y < bigCell.Cells.GetLength(1); y++)
                    {
                        if (bigCell.Cells[x, y].IsFocus)
                        {
                            prevMove = new Coord(x, y);
                        }
                    }
                }
            }

            
        }

        internal static bool IsBigCellFull(BigCell input)
        {
            foreach (var cell in input.Cells)
            {
                if (cell.State == State.None)
                {
                    return false;
                }
            }

            return true;
        }

        internal static int GetNumberOfMadeMoves(World world)
        {
            int result = 0;

            foreach (var bigCell in world.BigCells)
            {
                for (int x = 0; x < bigCell.Cells.GetLength(0); x++)
                {
                    for (int y = 0; y < bigCell.Cells.GetLength(1); y++)
                    {
                        if (bigCell.Cells[x, y].State != State.None)
                        {
                            result++;
                        }
                    }
                }
            }

            return result;
        }

        internal static void InvertStates(World world)
        {
            if (world.Status == Entities.Enum.GameStatus.Lose)
            {
                world.Status = Entities.Enum.GameStatus.Won;
            }
            else if (world.Status == Entities.Enum.GameStatus.Won)
            {
                world.Status = Entities.Enum.GameStatus.Lose;
            }

            foreach (var bigCell in world.BigCells)
            {
                if (bigCell.State == State.Client)
                {
                    bigCell.State = State.Enemy;
                }
                else if (bigCell.State == State.Enemy)
                {
                    bigCell.State = State.Client;
                }

                for (int x = 0; x < bigCell.Cells.GetLength(0); x++)
                {
                    for (int y = 0; y < bigCell.Cells.GetLength(1); y++)
                    {
                        if (bigCell.Cells[x, y].State == State.Client)
                        {
                            bigCell.Cells[x, y].State = State.Enemy;
                        }
                        else if (bigCell.Cells[x, y].State == State.Enemy)
                        {
                            bigCell.Cells[x, y].State = State.Client;
                        }
                    }
                }
            }
        }

    }
}
