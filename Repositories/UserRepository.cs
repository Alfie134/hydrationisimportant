﻿using Models;
using Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

       public IEnumerable<User> GetAll()
       {
             var users = new List<User>();
             string query = "SELECT * FROM \"User\"";

             using (SqlConnection connection = new SqlConnection(_connectionString))
             {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(SerializeUser(reader));
                    }
                }
             }
             return users;   
       }

        public User GetById(int id)
        {
            User user = null;
            string query = "SELECT * FROM \"User\" WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = SerializeUser(reader);
                    }
                }
            }
            return user;
        }

        //get by username 

        public User SerializeUser(SqlDataReader reader)
        {
            return new User(reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("UserName")),
                reader.GetString(reader.GetOrdinal("\"Password\"")),
                reader.IsDBNull(reader.GetOrdinal("RegionId")) ? null : reader.GetInt32(reader.GetOrdinal("RegionId")));
        }
    }
}
