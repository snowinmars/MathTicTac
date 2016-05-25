﻿using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.BLL.Logic.Additional
{
    internal static class Mechanic
    {
        internal static GameInfo ConvertGameInfo(DetailedGameInfo game, int userId, IAccountDao accDao)
        {
            GameInfo result = new GameInfo() {
                ID = game.ID,
                TimeOfCreation = game.TimeOfCreation,
            };

            if (game.ClientId == userId)
            {
                switch (game.status)
                {
                    case Enums.GameStatusVM.Query:
                        result.status = Enums.GameStatusVM.EnemyTurn;
                        break;
                    case Enums.GameStatusVM.Victory:
                    case Enums.GameStatusVM.Defeat:
                    case Enums.GameStatusVM.Draw:
                    case Enums.GameStatusVM.Rejected:
                    case Enums.GameStatusVM.ClientTurn:
                    case Enums.GameStatusVM.EnemyTurn:
                        result.status = game.status;
                        break;
                    case Enums.GameStatusVM.None:
                    default:
                        throw new InvalidOperationException($"Enum {nameof (Enums.GameStatusVM)} is invalid");
                }

                result.OppositePlayerName = accDao.GetUserNameById(game.EnemyId);
            }
            else if (game.EnemyId == userId)
            {
                switch (game.status)
                {
                    case Enums.GameStatusVM.Victory:
                        result.status = Enums.GameStatusVM.Defeat;
                        break;
                    case Enums.GameStatusVM.Defeat:
                        result.status = Enums.GameStatusVM.Victory;
                        break;
                    case Enums.GameStatusVM.Query:
                    case Enums.GameStatusVM.Rejected:
                    case Enums.GameStatusVM.Draw:
                        result.status = game.status;
                        break;
                    case Enums.GameStatusVM.ClientTurn:
                        result.status = Enums.GameStatusVM.EnemyTurn;
                        break;
                    case Enums.GameStatusVM.EnemyTurn:
                        result.status = Enums.GameStatusVM.ClientTurn;
                        break;
                    case Enums.GameStatusVM.None:
                    default:
                        throw new InvalidOperationException($"Enum {nameof(Enums.GameStatusVM)} is invalid");
                }

                result.OppositePlayerName = accDao.GetUserNameById(game.ClientId);
            }

            return result;
        }

        internal static World ConvertWorld(DetailedWorld world, int userId)
        {
            if (world.ClientId == userId)
            {
                World result = new World(world.Id, world.BigCells);

                result.LastBigCellMove = world.LastBigCellMove;
                result.LastCellMove = world.LastCellMove;

                switch (world.Status)
                {
                    case Enums.GameStatusVM.Victory:
                    case Enums.GameStatusVM.Defeat:
                    case Enums.GameStatusVM.Draw:
                    case Enums.GameStatusVM.Rejected:
                    case Enums.GameStatusVM.ClientTurn:
                    case Enums.GameStatusVM.EnemyTurn:
                        result.Status = world.Status;
                        break;
                    case Enums.GameStatusVM.Query:
                        result.Status = Enums.GameStatusVM.EnemyTurn;
                        break;
                    case Enums.GameStatusVM.None:
                    default:
                        throw new InvalidOperationException($"Enum {nameof(Enums.GameStatusVM)} is invalid");
                }

                if (result.Status == Enums.GameStatusVM.ClientTurn)
                {
                    if (!Mechanic.IsBigCellFilled(result.BigCells[result.LastCellMove.X, result.LastCellMove.Y]))
                    {
                        result.BigCells[result.LastCellMove.X, result.LastCellMove.Y].IsFocus = true;
                    }
                    else
                    {
                        foreach (var bigCell in result.BigCells)
                        {
                            if (!Mechanic.IsBigCellFilled(bigCell))
                            {
                                bigCell.IsFocus = true;
                            }
                        }
                    }
                }

                return result;
            }
            else if (world.EnemyId == userId)
            {
                World result = new World(world.Id, world.BigCells);

                result.LastBigCellMove = world.LastBigCellMove;
                result.LastCellMove = world.LastCellMove;

                switch (world.Status)
                {
                    case Enums.GameStatusVM.Victory:
                        result.Status = Enums.GameStatusVM.Defeat;
                        break;
                    case Enums.GameStatusVM.Defeat:
                        result.Status = Enums.GameStatusVM.Victory;
                        break;
                    case Enums.GameStatusVM.ClientTurn:
                        result.Status = Enums.GameStatusVM.EnemyTurn;
                        break;
                    case Enums.GameStatusVM.EnemyTurn:
                        result.Status = Enums.GameStatusVM.ClientTurn;
                        break;
                    case Enums.GameStatusVM.Draw:
                    case Enums.GameStatusVM.Rejected:
                    case Enums.GameStatusVM.Query:
                        result.Status = world.Status;
                        break;
                    case Enums.GameStatusVM.None:
                    default:
                        throw new InvalidOperationException($"Enum {nameof(Enums.GameStatusVM)} is invalid");
                }

                Mechanic.InvertWorld(result.BigCells);

                if (result.Status == Enums.GameStatusVM.ClientTurn)
                {
                    if (!Mechanic.IsBigCellFilled(result.BigCells[result.LastBigCellMove.X, result.LastBigCellMove.Y]))
                    {
                        result.BigCells[result.LastBigCellMove.X, result.LastBigCellMove.Y].IsFocus = true;
                    }
                    else
                    {
                        foreach (var bigCell in result.BigCells)
                        {
                            if (!Mechanic.IsBigCellFilled(bigCell))
                            {
                                bigCell.IsFocus = true;
                            }
                        }
                    }
                }

                return result;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        internal static bool IsBigCellFilled(BigCell input)
        {
            int iterator = 0;

            foreach (var cell in input.Cells)
            {
                if (cell.State != Enums.State.None)
                {
                    iterator++;
                }
            }

            return iterator == input.Cells.Length;
        }

        internal static bool IsWorldFilled(DetailedWorld input)
        {
            int iterator = 0;

            foreach (var bigCell in input.BigCells)
            {
                if (bigCell.State != Enums.State.None)
                {
                    iterator++;
                }
            }

            return iterator == input.BigCells.Length;
        }

        internal static void InvertWorld(BigCell[,] bigCells)
        {
            foreach (var bigCell in bigCells)
            {
                if (true)
                {
                    switch (bigCell.State)
                    {
                        case Enums.State.None:
                            break;
                        case Enums.State.Client:
                            bigCell.State = Enums.State.Enemy;
                            break;
                        case Enums.State.Enemy:
                            bigCell.State = Enums.State.Client;
                            break;
                        default:
                            throw new InvalidOperationException($"Enum {nameof(Enums.State)} is invalid");
                    }
                }

                foreach (var cell in bigCell.Cells)
                {
                    switch (cell.State)
                    {
                        case Enums.State.None:
                            break;
                        case Enums.State.Client:
                            bigCell.State = Enums.State.Enemy;
                            break;
                        case Enums.State.Enemy:
                            bigCell.State = Enums.State.Client;
                            break;
                        default:
                            throw new InvalidOperationException($"Enum {nameof(Enums.State)} is invalid");
                    }
                }
            }
        }

        internal static State GetBigCellResult(BigCell input)
        {
            List<State> currentList1 = new List<State>();
            List<State> currentList2 = new List<State>();
            List<State> currentList3 = new List<State>();
            List<State> currentList4 = new List<State>();

            for (int i = 0; i < input.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < input.Cells.GetLength(0); j++)
                {
                    currentList1.Add(input.Cells[i, j].State);
                    currentList2.Add(input.Cells[j, i].State);
                }

                currentList3.Add(input.Cells[i, i].State);
                currentList4.Add(input.Cells[input.Cells.GetLength(0) - 1 - i, input.Cells.GetLength(0) - 1 - i].State);

                if (Mechanic.IsVictoryRow(currentList1))
                {
                    return currentList1.First();
                }
                else if (Mechanic.IsVictoryRow(currentList2))
                {
                    return currentList2.First();
                }

                currentList1.Clear();
                currentList2.Clear();
            }

            if (Mechanic.IsVictoryRow(currentList3))
            {
                return currentList3.First();
            }
            else if (Mechanic.IsVictoryRow(currentList4))
            {
                return currentList4.First();
            }

            return State.None;
        }

        internal static State GetWorldResult(DetailedWorld input)
        {
            List<State> currentList1 = new List<State>();
            List<State> currentList2 = new List<State>();
            List<State> currentList3 = new List<State>();
            List<State> currentList4 = new List<State>();

            for (int i = 0; i < input.BigCells.GetLength(0); i++)
            {
                for (int j = 0; j < input.BigCells.GetLength(0); j++)
                {
                    currentList1.Add(input.BigCells[i, j].State);
                    currentList2.Add(input.BigCells[j, i].State);
                }

                currentList3.Add(input.BigCells[i, i].State);
                currentList4.Add(input.BigCells[input.BigCells.GetLength(0) - 1 - i, input.BigCells.GetLength(0) - 1 - i].State);

                if (Mechanic.IsVictoryRow(currentList1))
                {
                    return currentList1.First();
                }
                else if (Mechanic.IsVictoryRow(currentList2))
                {
                    return currentList2.First();
                }
            }

            if (Mechanic.IsVictoryRow(currentList3))
            {
                return currentList3.First();
            }
            else if (Mechanic.IsVictoryRow(currentList4))
            {
                return currentList4.First();
            }

            return State.None;
        }

        internal static bool IsVictoryRow(IEnumerable<State> input)
        {
            State testState = input.First();

            if (testState != State.None)
            {
                foreach (var state in input)
                {
                    if (state != testState)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}