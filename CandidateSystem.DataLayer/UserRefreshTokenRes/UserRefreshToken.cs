using CandidateSystem.Shared;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateSystem.DataLayer.UserRefreshTokenRes
{
    public class UserRefreshToken : IUserRefreshToken

    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseConnector _databaseConnector;

        public UserRefreshToken(IConfiguration configuration, IDatabaseConnector databaseConnector)
        {
            _databaseConnector = databaseConnector;
            _configuration = configuration;
        }

        public bool Delete(string userName, string refreshToken)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                MySqlCommand insert_cmd = new MySqlCommand($"delete from UserRefreshToken where username= @username and refreshToken = @refreshtoken", conn);
                insert_cmd.Parameters.AddWithValue("@userName", userName);
                insert_cmd.Parameters.AddWithValue("@refreshtoken", refreshToken);
                int isChanged = insert_cmd.ExecuteNonQuery();
                _databaseConnector.CloseConnection(conn);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }

        public bool Insert(Shared.Entity.UserRefreshToken user)
        {
            MySqlConnection conn = null;
            try
            {
                conn = _databaseConnector.getConnection();
                _databaseConnector.OpenConnection(conn);
                MySqlCommand insert_cmd = new MySqlCommand($"insert into UserRefreshToken values (@id,@userName,@refreshToken);", conn);
                insert_cmd.Parameters.AddWithValue("@id", 0);
                insert_cmd.Parameters.AddWithValue("@userName", user.UserName);
                insert_cmd.Parameters.AddWithValue("@refreshToken", user.RefreshToken);
                int isChanged = insert_cmd.ExecuteNonQuery();
                _databaseConnector.CloseConnection(conn);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }
        public Shared.Entity.UserRefreshToken Get(string userName,string refreshToken)
        {
            var conn = _databaseConnector.getConnection();
            try
            {
                _databaseConnector.OpenConnection(conn);
                string query = $"select * from  UserRefreshToken where username=@username and refreshtoken=@refreshtoken;";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@username",userName);
                command.Parameters.AddWithValue("@refreshtoken", refreshToken);
                MySqlDataReader reader = command.ExecuteReader();
                var CurrentAssess = new Shared.Entity.UserRefreshToken();
                while (reader.Read())
                {
                    CurrentAssess = new Shared.Entity.UserRefreshToken()
                    {
                        id = reader["id"] is DBNull ? -1 : Convert.ToInt16(reader["id"].ToString()),
                        UserName = reader["username"].ToString(),
                        RefreshToken = reader["refreshToken"].ToString()
                    };
                }
                return CurrentAssess;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _databaseConnector.CloseConnection(conn);
            }
        }
    }

}