using MathTicTac.DAL.Interfaces;
using MathTicTac.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.DAL.Dao
{
    public class AccountDao : IAccountDao
    {
        public DateTime? AcceptToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool Add(Account item, byte[] password)
        {
            using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
            {
                const string procedureName = "AddUser";

                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter RetId = command.Parameters.Add ("RetVal", SqlDbType.Int);
                    RetId.Direction = ParameterDirection.ReturnValue;

                    command.Parameters.AddWithValue("@UserName", item.Username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    command.ExecuteNonQuery();

                    item.Id = (int)RetId.Value;
                }
            }

            return item.Id != 0;
        }

        public void AddStatus(int id, MathTicTac.Enums.GameStatusVM result)
        {
            using (SqlConnection connection = new SqlConnection(SqlConfig.ConnectionString))
            {
                const string procedureName = "AddStatusToAccount";

                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

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
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter RetToken = command.Parameters.Add("@Token", SqlDbType.NVarChar);
                    RetToken.Direction = ParameterDirection.ReturnValue;

                    command.Parameters.AddWithValue("@UserID", id);
                    command.Parameters.AddWithValue("@IP", ip);

                    connection.Open();
                    command.ExecuteNonQuery();

                    result = (string)RetToken.Value;
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
            if (result.Id == 0)
            {
                return null;
            }

            return result;
        }

        public int GetUserIdByIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public int GetUserIdByToken(string token)
        {
            throw new NotImplementedException();
        }

        public string GetUserNameById(int id)
        {
            throw new NotImplementedException();
        }

        public byte[] GetUserPassword(int id)
        {
            throw new NotImplementedException();
        }

        public string GetUserTokenById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsTokenIpTrusted(string token, string ip)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTokenDate(string token)
        {
            throw new NotImplementedException();
        }
    }
}
