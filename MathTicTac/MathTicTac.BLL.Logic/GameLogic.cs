﻿using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic.Additional;
using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using System;
using System.Collections.Generic;

namespace MathTicTac.BLL.Logic
{
	public class GameLogic : IGameLogic
	{
		private const int dimension = 3;

		private IAccountDao accDao;
		private IGameDao gameDao;

		public GameLogic(IGameDao gameDao, IAccountDao accDao)
		{
			if (accDao == null || gameDao == null)
			{
				throw new ArgumentNullException();
			}

			this.accDao = accDao;
			this.gameDao = gameDao;
		}

		public bool Create(string player1Token, string player1Ip, string player2Identifier)
		{
			if (string.IsNullOrWhiteSpace(player1Token) || string.IsNullOrWhiteSpace(player2Identifier))
			{
				throw new ArgumentNullException();
			}

			if (Security.TokenIpPairIsValid(player1Token, player1Ip, accDao))
			{
				int player1Id = accDao.GetUserIdByToken(player1Token);

				int player2Id = accDao.GetUserIdByIdentifier(player2Identifier);

				DetailedWorld world = new DetailedWorld(dimension);

				world.ClientId = player1Id;
				world.EnemyId = player2Id;

				world.Status = Enums.GameStatus.Query;

				return gameDao.Add(world);
			}

			throw new UnauthorizedAccessException();
		}

		public IEnumerable<GameInfo> GetAllActiveGames(string token, string ip)
		{
			if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(ip))
			{
				throw new ArgumentNullException();
			}

			if (!Security.TokenIpPairIsValid(token, ip, accDao))
			{
				throw new UnauthorizedAccessException();
			}

			int userId = accDao.GetUserIdByToken(token);

			var gameList = gameDao.GetAllGames(userId);

			var resultList = new List<GameInfo>();

			foreach (var game in gameList)
			{
				resultList.Add(Mechanic.ConvertGameInfo(game, userId, accDao));
			}

			return resultList;
		}

		public World GetCurrentWorld(string token, string ip, int gameId)
		{
			if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(ip) || gameId == 0)
			{
				throw new ArgumentNullException();
			}

			if (!Security.TokenIpPairIsValid(token, ip, accDao))
			{
				throw new UnauthorizedAccessException();
			}

			int userId = accDao.GetUserIdByToken(token);

			var currentWorld = gameDao.GetGameState(gameId);

			if (userId != currentWorld.ClientId && userId != currentWorld.EnemyId)
			{
				throw new UnauthorizedAccessException();
			}

			return Mechanic.ConvertWorld(currentWorld, userId);
		}

		public bool MakeMove(Move move)
		{
			if (move == null)
			{
				throw new ArgumentNullException();
			}

			if (!Security.TokenIpPairIsValid(move.Token, move.IP, accDao))
			{
				throw new UnauthorizedAccessException();
			}

			int userId = accDao.GetUserIdByToken(move.Token);

			var currentWorld = gameDao.GetGameState(move.GameId);

			// Checking turn availability
			if ((userId == currentWorld.ClientId &&
			    currentWorld.Status != Enums.GameStatus.ClientTurn) ||
			    (userId == currentWorld.EnemyId &&
			    currentWorld.Status != Enums.GameStatus.EnemyTurn) ||
			    (userId != currentWorld.ClientId && userId != currentWorld.EnemyId))
			{
				return false;
			}

			bool moveCoordsValid = false;

			//Checking BigCell coordinates availability
			if (!Mechanic.IsBigCellFilled(currentWorld.BigCells[currentWorld.LastCellMove.X, currentWorld.LastCellMove.Y]) &&
			    move.BigCellCoord.X == currentWorld.LastCellMove.X && move.BigCellCoord.Y == currentWorld.LastCellMove.Y)
			{
				moveCoordsValid = true;
			}
			else
			{
				for (int x = 0; x < currentWorld.BigCells.GetLength(0); x++)
				{
					for (int y = 0; y < currentWorld.BigCells.GetLength(1); y++)
					{
						if (!Mechanic.IsBigCellFilled(currentWorld.BigCells[x, y]) &&
						    move.BigCellCoord.X == x && move.BigCellCoord.Y == y)
						{
							moveCoordsValid = true;
						}
					}
				}
			}

			if (!moveCoordsValid)
			{
				return false;
			}

			//Checking Cell coordinates availability
			if (currentWorld.BigCells[move.BigCellCoord.X, move.BigCellCoord.Y].Cells[move.CellCoord.X, move.CellCoord.Y].State != Enums.State.None)
			{
				return false;
			}

			// Making move and status updating
			if (userId == currentWorld.ClientId)
			{
				currentWorld.BigCells[move.BigCellCoord.X, move.BigCellCoord.Y].Cells[move.CellCoord.X, move.CellCoord.Y].State = Enums.State.Client;
				currentWorld.Status = Enums.GameStatus.EnemyTurn;
			}
			else if (userId == currentWorld.EnemyId)
			{
				currentWorld.BigCells[move.BigCellCoord.X, move.BigCellCoord.Y].Cells[move.CellCoord.X, move.CellCoord.Y].State = Enums.State.Enemy;
				currentWorld.Status = Enums.GameStatus.ClientTurn;
			}
			else
			{
				throw new InvalidOperationException();
			}

			//World result status updating
			foreach (var bigCell in currentWorld.BigCells)
			{
				if (bigCell.State == Enums.State.None)
				{
					var currentState = Mechanic.GetBigCellResult(bigCell);

					if (currentState != Enums.State.None)
					{
						bigCell.State = currentState;
						break;
					}
				}
			}

			var currentWorldState = Mechanic.GetWorldResult(currentWorld);

			switch (currentWorldState)
			{
				case Enums.State.None:
					if (Mechanic.IsWorldFilled(currentWorld))
					{
						currentWorld.Status = Enums.GameStatus.Draw;

						accDao.AddStatus(currentWorld.ClientId, Enums.GameStatus.Draw);
						accDao.AddStatus(currentWorld.EnemyId, Enums.GameStatus.Draw);
					}
					break;

				case Enums.State.Client:
					currentWorld.Status = Enums.GameStatus.Victory;

					accDao.AddStatus(currentWorld.ClientId, Enums.GameStatus.Victory);
					accDao.AddStatus(currentWorld.EnemyId, Enums.GameStatus.Defeat);
					break;

				case Enums.State.Enemy:
					currentWorld.Status = Enums.GameStatus.Defeat;

					accDao.AddStatus(currentWorld.ClientId, Enums.GameStatus.Defeat);
					accDao.AddStatus(currentWorld.EnemyId, Enums.GameStatus.Victory);
					break;

				default:
					throw new InvalidOperationException($"Enum {nameof(Enums.State)} is invalid");
			}

			return gameDao.Update(currentWorld);
		}

		public bool RejectGame(string token, string ip, int gameId)
		{
			if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(ip) || gameId == 0)
			{
				throw new ArgumentNullException();
			}

			if (!Security.TokenIpPairIsValid(token, ip, accDao))
			{
				throw new UnauthorizedAccessException();
			}

			int userId = accDao.GetUserIdByToken(token);

			var currentWorld = gameDao.GetGameState(gameId);

			if (userId != currentWorld.ClientId && userId != currentWorld.EnemyId)
			{
				return false;
			}

			switch (currentWorld.Status)
			{
				case Enums.GameStatus.Victory:
				case Enums.GameStatus.Defeat:
				case Enums.GameStatus.Draw:
				case Enums.GameStatus.Rejected:
					return false;

				case Enums.GameStatus.Query:
					currentWorld.Status = Enums.GameStatus.Rejected;
					return true;

				case Enums.GameStatus.ClientTurn:
				case Enums.GameStatus.EnemyTurn:
					break;

				case Enums.GameStatus.None:
				default:
					throw new InvalidOperationException($"Enum {nameof(Enums.GameStatus)} is invalid");
			}

			if (userId == currentWorld.ClientId)
			{
				currentWorld.Status = Enums.GameStatus.Defeat;
			}
			else
			{
				currentWorld.Status = Enums.GameStatus.Victory;
			}

			return gameDao.Update(currentWorld);
		}
	}
}