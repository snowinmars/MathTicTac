using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using MathTicTac.Enums;

namespace MathTicTac.DAL.Dao
{
	public class GameDao : IGameDao
	{
		public bool Add(DetailedWorld item)
		{
			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				string stateString = GameDao.CreateStateString(item);

				const string procedureName = "AddGameWorld";

				using (var command = new SqlCommand(procedureName, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					SqlParameter retId = command.Parameters.Add("RetVal", SqlDbType.Int);
					retId.Direction = ParameterDirection.ReturnValue;

					command.Parameters.AddWithValue("@ClientId", item.ClientId);
					command.Parameters.AddWithValue("@EnemyId", item.EnemyId);
					command.Parameters.AddWithValue("@NumberOfDimensions", item.BigCells.GetLength(0) + 1);
					command.Parameters.AddWithValue("StatusId", item.Status);
					command.Parameters.AddWithValue("States", stateString);

					connection.Open();
					command.ExecuteNonQuery();

					item.Id = (int)retId.Value;
				}
			}

			return item.Id != 0;
		}

		public IEnumerable<DetailedGameInfo> GetAllGames(int userId)
		{
			List<DetailedGameInfo> result = new List<DetailedGameInfo>();

			DetailedGameInfo currentInfo = new DetailedGameInfo();

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string procedureName = "GetGameInfosByUserId";

				using (var command = new SqlCommand(procedureName, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@UserId", userId);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
                        {
                            currentInfo.ID = (int)reader["Id"];
                            currentInfo.ClientId = (int)reader["ClientId"];
                            currentInfo.ClientId = (int)reader["EnemyId"];
                            currentInfo.TimeOfCreation = (DateTime)reader["TimeOfCreation"];
                            currentInfo.status = (GameStatus)reader["StatusId"];


                            result.Add(currentInfo);
                        }
                    }
				}
			}

			return result;
		}

        public DetailedWorld GetGameState(int gameId)
		{
			DetailedWorld result = null;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string query = "SELECT Id, ClientId, EnemyId, NumberOfDimensions, StatusId, TimeOfCreation, States, LastMove FROM [GameWorldsStates] WHERE Id = @Id";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Id", gameId);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
						    result = new DetailedWorld((int) reader["NumberOfDimensions"])
						    {
						        Id = (int) reader["Id"],
						        ClientId = (int) reader["ClientId"],
						        EnemyId = (int) reader["EnemyId"]
						    };

						    GameDao.StringToWorldParse((string)reader["States"],
                                                        (string)reader["LastMove"],
                                                        result);
						}
					}
				}
			}

			return result;
		}

		public bool Update(DetailedWorld gameWorld)
		{
			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string procedureName = "UpdateGameState";

				using (var command = new SqlCommand(procedureName, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@Id", gameWorld.Id);
					command.Parameters.AddWithValue("@StatusId", gameWorld.Status);
					command.Parameters.AddWithValue("@States", GameDao.CreateStateString(gameWorld));
					command.Parameters.AddWithValue("@LastMove", GameDao.WorldToLastMoveString(gameWorld));

					connection.Open();
					command.ExecuteNonQuery();
				}
			}

			return true;
		}

		private static string CreateStateString(DetailedWorld item)
		{
			StringBuilder bigResult = new StringBuilder(),
			    smallResult = new StringBuilder();

			for (int by = 0; by < item.BigCells.GetLength(1); by++)
			{
				for (int bx = 0; bx < item.BigCells.GetLength(0); bx++)
				{
				    switch (item.BigCells[bx, @by].State)
				    {
				        case State.None:
				            bigResult.Append("0");
				            break;
				        case State.Client:
				            bigResult.Append("C");
				            break;
				        case State.Enemy:
				            bigResult.Append("E");
				            break;
				        default:
				            throw new ArgumentOutOfRangeException();
				    }

				    for (int cy = 0; cy < item.BigCells[bx, by].Cells.GetLength(1); cy++)
				    {
				        for (int cx = 0; cx < item.BigCells[bx, by].Cells.GetLength(0); cx++)
				        {
				            switch (item.BigCells[bx, @by].Cells[bx, @by].State)
				            {
				                case State.None:
				                    smallResult.Append("0");
				                    break;
				                case State.Client:
				                    smallResult.Append("C");
				                    break;
				                case State.Enemy:
				                    smallResult.Append("E");
				                    break;
				                default:
				                    throw new ArgumentOutOfRangeException();
				            }
				        }
				    }
				}
			}

		    return bigResult.Append(smallResult).ToString();
		}

	    private static void StringToWorldParse(string inputStates, string inputCoords, DetailedWorld item)
	    {
	        int numberOfDimensions = item.BigCells.GetLength(0);
	        BigCell[,] result = item.BigCells;
	        int iterator = 0;

	        for (int by = 0; by < result.GetLength(1); by++)
	        {
	            for (int bx = 0; bx < result.GetLength(0); bx++)
	            {
	                switch (inputStates[iterator])
	                {
	                    case 'O':
	                        result[bx, by].State = State.None;
	                        break;

	                    case 'E':
	                        result[bx, by].State = State.Enemy;
	                        break;

	                    case 'C':
	                        result[bx, by].State = State.Client;
	                        break;

	                    default:
	                        throw new InvalidCastException();
	                }

	                for (int i = iterator*numberOfDimensions*numberOfDimensions + numberOfDimensions*numberOfDimensions;
                                i < (iterator + 1)*numberOfDimensions*numberOfDimensions + numberOfDimensions*numberOfDimensions;
                                i++)
	                {
	                    for (int cy = 0; cy < result.GetLength(1); cy++)
	                    {
	                        for (int cx = 0; cx < result.GetLength(0); cx++)
	                        {
	                            switch (inputStates[i])
	                            {
	                                case 'O':
	                                    result[bx, by].Cells[cx, cy].State = State.None;
	                                    break;

	                                case 'E':
	                                    result[bx, by].Cells[cx, cy].State = State.Enemy;
	                                    break;

	                                case 'C':
	                                    result[bx, by].Cells[cx, cy].State = State.Client;
	                                    break;

	                                default:
	                                    throw new InvalidCastException();
	                            }
	                        }
	                    }
	                }

	                iterator++;
	            }
	        }

	        var coordArray = inputCoords.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

	        item.LastBigCellMove = new Coord(int.Parse(coordArray[0]), int.Parse(coordArray[1]));

	        item.LastCellMove = new Coord(int.Parse(coordArray[2]), int.Parse(coordArray[3]));
	    }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string WorldToLastMoveString(DetailedWorld item) 
            => $"{item.LastBigCellMove.X},{item.LastBigCellMove.Y},{item.LastCellMove.X},{item.LastCellMove.Y}";
	}
}