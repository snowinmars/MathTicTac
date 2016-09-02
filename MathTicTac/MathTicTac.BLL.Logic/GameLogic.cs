using MathTicTac.BLL.Interfaces;
using MathTicTac.BLL.Logic.Additional;
using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using MathTicTac.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MathTicTac.BLL.Logic
{
	public class GameLogic : IGameLogic
	{
		private const int Dimension = 3;

		private readonly IAccountDao accDao;
		private readonly IGameDao gameDao;

		public GameLogic(IGameDao gameDao, IAccountDao accDao)
		{
			if (accDao == null || gameDao == null)
			{
				throw new ArgumentNullException();
			}

			this.accDao = accDao;
			this.gameDao = gameDao;
		}

		public ResponseResult Create(string player1Token, string player1Ip, string player2Identifier)
		{
			if (string.IsNullOrWhiteSpace(player1Token) || string.IsNullOrWhiteSpace(player2Identifier))
			{
				throw new ArgumentNullException();
			}

		    if (!Security.TokenIpPairIsValid(player1Token, player1Ip, this.accDao))
		    {
		        return ResponseResult.TokenInvalid;
		    }

		    int player1Id = this.accDao.GetUserIdByToken(player1Token);
		    int player2Id = this.accDao.GetUserIdByIdentifier(player2Identifier);

		    DetailedWorld world = new DetailedWorld(GameLogic.Dimension)
		    {
		        ClientId = player1Id,
		        EnemyId = player2Id,
		        Status = GameStatus.Query
		    };

		    if (!this.gameDao.Add(world))
		    {
		        return ResponseResult.None;
		    }

		    return ResponseResult.Ok;
		}

		public ResponseResult GetAllActiveGames(string token, string ip, out IEnumerable<GameInfo> result)
		{
			if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(ip))
			{
				throw new ArgumentNullException();
			}

			if (!Security.TokenIpPairIsValid(token, ip, this.accDao))
			{
                result = null; // ?

                return ResponseResult.TokenInvalid;
			}

			int userId = this.accDao.GetUserIdByToken(token);

			var gameList = this.gameDao.GetAllGames(userId);

		    result = gameList.Select(game => Mechanic.ConvertGameInfo(game, userId, this.accDao)).ToList();

			return ResponseResult.Ok;
		}

		public ResponseResult GetCurrentWorld(string token, string ip, int gameId, out World result)
		{
			if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(ip) || gameId == 0)
			{
				throw new ArgumentNullException();
			}

			if (!Security.TokenIpPairIsValid(token, ip, this.accDao))
			{
                result = null;

				return ResponseResult.TokenInvalid;
			}

			int userId = this.accDao.GetUserIdByToken(token);

			var currentWorld = this.gameDao.GetGameState(gameId);

			if (userId != currentWorld.ClientId && userId != currentWorld.EnemyId)
			{
                result = null;

                return ResponseResult.AccountDataInvalid;
			}

            result = Mechanic.ConvertWorld(currentWorld, userId);

            return ResponseResult.Ok;
        }

		public ResponseResult MakeMove(Move move)
        {
            if (move == null)
            {
                throw new ArgumentNullException();
            }

            if (!Security.TokenIpPairIsValid(move.Token, move.IP, this.accDao))
            {
                return ResponseResult.TokenInvalid;
            }

            int userId = this.accDao.GetUserIdByToken(move.Token);

            var currentWorld = this.gameDao.GetGameState(move.GameId);

            if (GameLogic.IsTurnAvailable(userId, currentWorld))
            {
                return ResponseResult.TurnUnavailiable;
            }

            bool moveCoordsValid = false;
            if (GameLogic.IsBigCellCoordinatesAvailable(move, currentWorld))
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
                return ResponseResult.TurnUnavailiable;
            }

            if (GameLogic.IsCellCoordinatesAvailable(move, currentWorld))
            {
                return ResponseResult.TurnUnavailiable;
            }

            // Making move and status updating
            if (userId == currentWorld.ClientId)
            {
                currentWorld.BigCells[move.BigCellCoord.X, move.BigCellCoord.Y].Cells[move.CellCoord.X, move.CellCoord.Y].State = State.Client;
                currentWorld.Status = GameStatus.EnemyTurn;
            }
            else if (userId == currentWorld.EnemyId)
            {
                currentWorld.BigCells[move.BigCellCoord.X, move.BigCellCoord.Y].Cells[move.CellCoord.X, move.CellCoord.Y].State = State.Enemy;
                currentWorld.Status = GameStatus.ClientTurn;
            }
            else
            {
                return ResponseResult.AccountDataInvalid;
            }

            //World result status updating
            GameLogic.WorldResultStatusUpdating(currentWorld);

            State currentWorldState = Mechanic.GetWorldResult(currentWorld);

            switch (currentWorldState)
            {
                // Extract method
                case State.None:
                    if (Mechanic.IsWorldFilled(currentWorld))
                    {
                        currentWorld.Status = GameStatus.Draw;

                        this.accDao.AddStatus(currentWorld.ClientId, GameStatus.Draw);
                        this.accDao.AddStatus(currentWorld.EnemyId, GameStatus.Draw);
                    }
                    break;

                case State.Client:
                    currentWorld.Status = GameStatus.Victory;

                    this.accDao.AddStatus(currentWorld.ClientId, GameStatus.Victory);
                    this.accDao.AddStatus(currentWorld.EnemyId, GameStatus.Defeat);
                    break;

                case State.Enemy:
                    currentWorld.Status = GameStatus.Defeat;

                    this.accDao.AddStatus(currentWorld.ClientId, GameStatus.Defeat);
                    this.accDao.AddStatus(currentWorld.EnemyId, GameStatus.Victory);
                    break;

                default:
                    throw new InvalidOperationException($"Enum {nameof(State)} is invalid");
            }

            if (this.gameDao.Update(currentWorld))
            {
                return ResponseResult.Ok;
            }

            return ResponseResult.None;
        }

        public ResponseResult RejectGame(string token, string ip, int gameId)
		{
			if (string.IsNullOrWhiteSpace(token) ||
                string.IsNullOrWhiteSpace(ip) ||
                gameId == 0) // <= ?
			{
				throw new ArgumentNullException();
			}

			if (!Security.TokenIpPairIsValid(token, ip, this.accDao))
			{
				return ResponseResult.TokenInvalid;
			}

			int userId = this.accDao.GetUserIdByToken(token);
            DetailedWorld currentWorld = this.gameDao.GetGameState(gameId);

			if (userId != currentWorld.ClientId && userId != currentWorld.EnemyId)
			{
				return ResponseResult.AccountDataInvalid;
			}

			switch (currentWorld.Status)
			{
				case GameStatus.Victory:
				case GameStatus.Defeat:
				case GameStatus.Draw:
				case GameStatus.Rejected:
					return ResponseResult.TurnUnavailiable;

				case GameStatus.Query:
					currentWorld.Status = GameStatus.Rejected;
					return ResponseResult.Ok;

				case GameStatus.ClientTurn:
				case GameStatus.EnemyTurn:
					break;

				case GameStatus.None:
				default:
					throw new InvalidOperationException($"Enum {nameof(GameStatus)} is invalid");
			}

			currentWorld.Status = userId == currentWorld.ClientId ?
                                    GameStatus.Defeat : 
                                    GameStatus.Victory;

            if (this.gameDao.Update(currentWorld))
            {
                return ResponseResult.Ok;
            }

			return ResponseResult.None;
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WorldResultStatusUpdating(DetailedWorld currentWorld)
        {
            foreach (var bigCell in currentWorld.BigCells)
            {
                if (bigCell.State == State.None)
                {
                    var currentState = Mechanic.GetBigCellResult(bigCell);

                    if (currentState != State.None)
                    {
                        bigCell.State = currentState;
                        break;
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsCellCoordinatesAvailable(Move move, DetailedWorld currentWorld)
        {
            return currentWorld.BigCells[move.BigCellCoord.X, move.BigCellCoord.Y].Cells[move.CellCoord.X, move.CellCoord.Y].State != State.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsBigCellCoordinatesAvailable(Move move, DetailedWorld currentWorld)
        {
            return !Mechanic.IsBigCellFilled(currentWorld.BigCells[currentWorld.LastCellMove.X, currentWorld.LastCellMove.Y]) &&
                            move.BigCellCoord.X == currentWorld.LastCellMove.X && move.BigCellCoord.Y == currentWorld.LastCellMove.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsTurnAvailable(int userId, DetailedWorld currentWorld)
        {
            return (userId == currentWorld.ClientId &&
                            currentWorld.Status != GameStatus.ClientTurn) ||
                            (userId == currentWorld.EnemyId &&
                            currentWorld.Status != GameStatus.EnemyTurn) ||
                            (userId != currentWorld.ClientId && userId != currentWorld.EnemyId);
        }
    }
}