using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MathTicTac.BLL.Logic.Additional
{
	internal static class Mechanic
	{
		internal static GameInfo ConvertGameInfo(DetailedGameInfo game, int userId, IAccountDao accDao)
		{
			GameInfo result = new GameInfo()
			{
				ID = game.ID,
				TimeOfCreation = game.TimeOfCreation,
			};

			if (game.ClientId == userId)
            {
                // Extract method
                switch (game.status)
                {
                    case GameStatus.Query:
                        result.status = GameStatus.EnemyTurn;
                        break;

                    case GameStatus.Victory:
                    case GameStatus.Defeat:
                    case GameStatus.Draw:
                    case GameStatus.Rejected:
                    case GameStatus.ClientTurn:
                    case GameStatus.EnemyTurn:
                        result.status = game.status;
                        break;

                    case GameStatus.None:
                    default:
                        throw new InvalidOperationException($"Enum {nameof(GameStatus)} is invalid");
                }

                result.OppositePlayerName = accDao.GetUserNameById(game.EnemyId);
            }
            else if (game.EnemyId == userId)
            {
                // Extract method
                switch (game.status)
                {
                    case GameStatus.Victory:
                        result.status = GameStatus.Defeat;
                        break;

                    case GameStatus.Defeat:
                        result.status = GameStatus.Victory;
                        break;

                    case GameStatus.Query:
                    case GameStatus.Rejected:
                    case GameStatus.Draw:
                        result.status = game.status;
                        break;

                    case GameStatus.ClientTurn:
                        result.status = GameStatus.EnemyTurn;
                        break;

                    case GameStatus.EnemyTurn:
                        result.status = GameStatus.ClientTurn;
                        break;

                    case GameStatus.None:
                    default:
                        throw new InvalidOperationException($"Enum {nameof(GameStatus)} is invalid");
                }

                result.OppositePlayerName = accDao.GetUserNameById(game.ClientId);
            }

            return result;
		}

        internal static World ConvertWorld(DetailedWorld world, int userId)
        {
            if (world.ClientId == userId)
			{
                // Extract method
                World result = new World(world.BigCells)
			    {
			        Id = world.Id,
			        LastBigCellMove = world.LastBigCellMove,
			        LastCellMove = world.LastCellMove
			    };

			    switch (world.Status)
				{
					case GameStatus.Victory:
					case GameStatus.Defeat:
					case GameStatus.Draw:
					case GameStatus.Rejected:
					case GameStatus.ClientTurn:
					case GameStatus.EnemyTurn:
						result.Status = world.Status;
						break;

					case GameStatus.Query:
						result.Status = GameStatus.EnemyTurn;
						break;

					case GameStatus.None:
					default:
						throw new InvalidOperationException($"Enum {nameof(GameStatus)} is invalid");
				}

			    if (result.Status != GameStatus.ClientTurn)
			    {
			        return result;
			    }

                Coord lastCellMove = result.LastCellMove;
                if (!Mechanic.IsBigCellFilled(result.BigCells[lastCellMove.X, lastCellMove.Y]))
			    {
			        result.BigCells[lastCellMove.X, lastCellMove.Y].IsFocus = true;
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

			    return result;
			}

            if (world.EnemyId == userId)
            {
                // Extract method
                World result = new World(world.BigCells)
                {
                    Id = world.Id,
                    LastBigCellMove = world.LastBigCellMove,
                    LastCellMove = world.LastCellMove
                };

                switch (world.Status)
                {
                    case GameStatus.Victory:
                        result.Status = GameStatus.Defeat;
                        break;

                    case GameStatus.Defeat:
                        result.Status = GameStatus.Victory;
                        break;

                    case GameStatus.ClientTurn:
                        result.Status = GameStatus.EnemyTurn;
                        break;

                    case GameStatus.EnemyTurn:
                        result.Status = GameStatus.ClientTurn;
                        break;

                    case GameStatus.Draw:
                    case GameStatus.Rejected:
                    case GameStatus.Query:
                        result.Status = world.Status;
                        break;

                    case GameStatus.None:
                    default:
                        throw new InvalidOperationException($"Enum {nameof(GameStatus)} is invalid");
                }

                Mechanic.InvertWorld(result.BigCells);

                if (result.Status != GameStatus.ClientTurn)
                {
                    return result;
                }

                Coord lastBigCellMove = result.LastBigCellMove;
                if (!Mechanic.IsBigCellFilled(result.BigCells[lastBigCellMove.X, lastBigCellMove.Y]))
                {
                    result.BigCells[lastBigCellMove.X, lastBigCellMove.Y].IsFocus = true;
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

                return result;
            }

            throw new InvalidOperationException();
        }

	    internal static bool IsBigCellFilled(BigCell input)
		{
	        return input.Cells
                .Cast<Cell>()
                .Count(cell => cell.State != State.None) == input.Cells.Length;
		}

		internal static bool IsWorldFilled(DetailedWorld input)
		{
		    return input.BigCells
                .Cast<BigCell>()
                .Count(bigCell => bigCell.State != State.None) == input.BigCells.Length;
		}

	    private static void InvertWorld(BigCell[,] bigCells)
		{
			foreach (var bigCell in bigCells)
			{
				if (true) // wu?
				{
					switch (bigCell.State)
					{
						case State.None:
							break;

						case State.Client:
							bigCell.State = State.Enemy;
							break;

						case State.Enemy:
							bigCell.State = State.Client;
							break;

						default:
							throw new InvalidOperationException($"Enum {nameof(State)} is invalid");
					}
				}

				foreach (var cell in bigCell.Cells)
				{
					switch (cell.State)
					{
						case State.None:
							break;

						case State.Client:
							bigCell.State = State.Enemy;
							break;

						case State.Enemy:
							bigCell.State = State.Client;
							break;

						default:
							throw new InvalidOperationException($"Enum {nameof(State)} is invalid");
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

			for (int i = 0; i < input.Cells.GetLength(0); i++) // wu?
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

                if (Mechanic.IsVictoryRow(currentList2))
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

            if (Mechanic.IsVictoryRow(currentList4))
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

                if (Mechanic.IsVictoryRow(currentList2))
			    {
			        return currentList2.First();
			    }
			}

			if (Mechanic.IsVictoryRow(currentList3))
			{
				return currentList3.First();
			}

            if (Mechanic.IsVictoryRow(currentList4))
		    {
		        return currentList4.First();
		    }

		    return State.None;
		}

	    private static bool IsVictoryRow(IEnumerable<State> input)
		{
			State testState = input.First();
			return testState == State.None || input.All(state => state == testState);
		}
    }
}