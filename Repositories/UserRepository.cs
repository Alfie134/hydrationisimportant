using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public int Add(User entity, SqlConnection connection, SqlTransaction? transaction = null)
        {
            SqlCommand command = new SqlCommand(@$"INSERT INTO [User] (UserName,PasswordHash,RegionId,PasswordSalt) VALUES 
                                        (@UserName,@PasswordHash,@RegionId,@PasswordSalt); SELECT SCOPE_IDENTITY();", connection, transaction);
            int id = 0;
            using (command)
            {
                command.Parameters.AddWithValue("@UserName", entity.UserName);
                command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                command.Parameters.AddWithValue("@RegionId", entity.RegionId);
                command.Parameters.AddWithValue("@PasswordSalt", entity.PasswordSalt);
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            return id;
        }

        public bool Delete(User entity, SqlConnection connection, SqlTransaction? transaction = null)
        {
            return DeleteById(entity.Id,connection, transaction);
        }

        public bool DeleteById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            string query = "DELETE FROM [User] WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", id);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public IEnumerable<User> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var users = new List<User>();
            string query = "SELECT * FROM [User]";

            SqlCommand command = new SqlCommand(query, connection, transaction);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User((int)reader["Id"], (string)reader["UserName"], (string)reader["PasswordHash"], (int)reader["RegionId"],(string)reader["PasswordSalt"]));
                }
            }
            return users;
        }

        public User GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            User? user = null;
            string query = "SELECT * FROM [User] WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User((int)reader["Id"], (string)reader["UserName"], (string)reader["PasswordHash"], (int)reader["RegionId"], (string)reader["PasswordSalt"]);
                    }
                }
            }
            return user;
        }


        public User GetByUsername(string username, SqlConnection connection, SqlTransaction? transaction = null)
        {
            User? user = null;
            string query = "SELECT * FROM [User] WHERE Username = @Username";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Username", username);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User((int)reader["Id"], (string)reader["UserName"], (string)reader["PasswordHash"], (int)reader["RegionId"], (string)reader["PasswordSalt"]);
                    }
                }
            }
            return user;
        }


        public void Update(User entity, SqlConnection connection, SqlTransaction? transaction = null)
        {
            string query = "UPDATE Route SET UserName = @UserName, PasswordHash = @PasswordHash, RegionId = @RegionId WHERE Id = @Id";

            SqlCommand command = new SqlCommand(query, connection, transaction);
            using (command)
            {
                command.Parameters.AddWithValue("@UserName", entity.UserName);
                command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                command.Parameters.AddWithValue("@RegionId", entity.RegionId);
                command.ExecuteNonQuery();
            }
        }
    }
}
