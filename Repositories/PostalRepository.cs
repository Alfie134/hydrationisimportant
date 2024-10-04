using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class PostalRepository : IPostalRepository
    {
        private readonly string _connectionString;

        public PostalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Postal> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var postals = new List<Postal>();
            string query = "SELECT * FROM PostalCode";

            SqlCommand command = new SqlCommand(query, connection,transaction);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    postals.Add(new Postal((int)reader["PostalCode"], (string)reader["City"]));
                }
            }
        
            return postals;
        }
        public Postal GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            Postal? postal = null;
            string query = "SELECT * FROM PostalCode WHERE PostalCode = @PostalCode";
            SqlCommand command = new SqlCommand(query, connection,transaction);
            command.Parameters.AddWithValue("@PostalCode", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    postal = new Postal((int)reader["PostalCode"], (string)reader["City"]);
                }
            }
            return postal;
        }
    }
}
