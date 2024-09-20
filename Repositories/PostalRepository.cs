using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class PostalRepository : IPostalRepository
    {
        private readonly string _connectionString = new AppConfig().ConnectionString;
        public IEnumerable<Postal> GetAll()
        {
            var postals = new List<Postal>();
            string query = "SELECT * FROM PostalCode";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        postals.Add(new Postal((int)reader["PostalCode"], (string)reader["City"]));
                    }
                }
            }
            return postals;
        }

        public Postal GetById(int id)
        {
            Postal postal = null;
            string query = "SELECT * FROM PostalCode WHERE PostalCode = @PostalCode";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PostalCode", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        postal = new Postal((int)reader["PostalCode"], (string)reader["City"]);
                    }
                }
            }
            return postal;
        }
    }
}
