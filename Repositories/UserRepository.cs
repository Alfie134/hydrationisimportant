using Models;
using Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Configuration;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = new AppConfig().ConnectionString;
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
        public User GetByUserName(string userName)
        {
            string query = "SELECT * FROM \"User\" WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("UserName", userName);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return SerializeUser(reader);
                    }
                }
            }
            return null;
        }

        public User SerializeUser(SqlDataReader reader)
        {
            return new User(reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("UserName")),
                reader.GetString(reader.GetOrdinal("Password")),
                reader.IsDBNull(reader.GetOrdinal("RegionId")) ? null : reader.GetInt32(reader.GetOrdinal("RegionId")));
        }
    }
}
