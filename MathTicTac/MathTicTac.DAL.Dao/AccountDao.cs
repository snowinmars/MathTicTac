﻿using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MathTicTac.DAL.Dao
{
	public class AccountDao : IAccountDao
	{
		public DateTime? AcceptToken(string token)
		{
			DateTime? result = null;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string query = "SELECT [TimeOfLastAccess] FROM [Token] WHERE Token = @Token";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Token", token);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result = (DateTime)reader["TimeOfLastAccess"];
						}
					}
				}
			}

			return result;
		}

		public bool Add(Account item, byte[] password)
		{
			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string procedureName = "AddUser";

				using (var command = new SqlCommand(procedureName, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					SqlParameter retId = command.Parameters.Add("RetVal", SqlDbType.Int);
					retId.Direction = ParameterDirection.ReturnValue;

					command.Parameters.AddWithValue("@UserName", item.Username);
					command.Parameters.AddWithValue("@Password", password);

					connection.Open();
					command.ExecuteNonQuery();

					item.Id = (int)retId.Value;
				}
			}

			return item.Id != 0;
		}

		public void AddStatus(int id, Enums.GameStatus result)
		{
			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string procedureName = "AddStatusToAccount";

				using (var command = new SqlCommand(procedureName, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@ID", id);
					command.Parameters.AddWithValue("@GameResult", result);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public string CreateToken(int id, string ip)
		{
			string result;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string procedureName = "CreateToken";

				using (var command = new SqlCommand(procedureName, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					SqlParameter retToken = command.Parameters.Add("@Token", SqlDbType.NVarChar);
					retToken.Direction = ParameterDirection.Output;

					command.Parameters.AddWithValue("@UserID", id);
					command.Parameters.AddWithValue("@IP", ip);

					connection.Open();
					command.ExecuteNonQuery();

					result = (string)retToken.Value;
				}
			}

			return result;
		}

		public bool DeleteToken(string token)
		{
			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string query = "DELETE FROM [Tokens] WHERE [Token] = @Token";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Token", token);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}

			return true;
		}

		public Account Get(int id)
		{
			Account result = new Account();

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string query = "SELECT [Id], [Name], [GamesWon], [GamesLose], [GamesDraw] FROM [Accounts] WHERE Id = @ID";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@ID", id);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result.Id = (int)reader["Id"];
							result.Username = (string)reader["Name"];
							result.Won = (int)reader["GamesWon"];
							result.Lose = (int)reader["GamesLose"];
							result.Draw = (int)reader["GamesDraw"];
						}
					}
				}
			}

			return result.Id == 0 ?
                    null : 
                    result;
		}

		public int GetUserIdByIdentifier(string identifier)
		{
			int result = 0;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string query = "SELECT [Id] FROM [Accounts] WHERE [Name] = @Identifier";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Identifier", identifier);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result = (int)reader["Id"];
						}
					}
				}
			}

			return result;
		}

		public int GetUserIdByToken(string token)
		{
			int result = 0;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string query = "SELECT [UserId] FROM [Tokens] WHERE [Token] = @Token";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Token", token);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result = (int)reader["UserId"];
						}
					}
				}
			}

			return result;
		}

		public string GetUserNameById(int id)
		{
			string result = null;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string query = "SELECT [Name] FROM [Accounts] WHERE [Id] = @ID";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@ID", id);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result = (string)reader["Name"];
						}
					}
				}
			}

			return result;
		}

		public IEnumerable<byte> GetUserPassword(int id)
		{
			byte[] result = null;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string query = "SELECT [Password] FROM [Accounts] WHERE [Id] = @ID";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@ID", id);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result = (byte[])reader["Password"];
						}
					}
				}
			}

			return result;
		}

		public string GetUserTokenById(int id)
		{
			string result = null;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string query = "SELECT [Token] FROM [Tokens] WHERE [UserId] = @ID";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@ID", id);

					connection.Open();

					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result = (string)reader["Token"];
						}
					}
				}
			}

			return result;
		}

		public bool IsTokenIpTrusted(string token, string ip)
		{
			bool result = false;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string procedureName = "IsTokenIpTrusted";

				using (var command = new SqlCommand(procedureName, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					SqlParameter retId = command.Parameters.Add("RetVal", SqlDbType.Int);
					retId.Direction = ParameterDirection.ReturnValue;

					command.Parameters.AddWithValue("@Token", token);
					command.Parameters.AddWithValue("@IP", ip);

					connection.Open();
					command.ExecuteNonQuery();

					result = (int)retId.Value == 1;
				}
			}

			return result;
		}

		public bool UpdateTokenDate(string token)
		{
			bool result = false;

			using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
			{
				const string procedureName = "UpdateTokenDate";

				using (var command = new SqlCommand(procedureName, connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					SqlParameter retVal = command.Parameters.Add("RetVal", SqlDbType.Int);
					retVal.Direction = ParameterDirection.Output;

					command.Parameters.AddWithValue("@Token", token);

					connection.Open();
					command.ExecuteNonQuery();

					result = (int)retVal.Value == 1;
				}
			}

			return result;
		}
	}
}