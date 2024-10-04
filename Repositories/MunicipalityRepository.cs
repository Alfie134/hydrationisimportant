using Configuration;
using Models;
using Microsoft.Data.SqlClient;
using Repositories.Interfaces;

namespace Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly string _connectionString;

        public MunicipalityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Municipality> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var municipalities = new List<Municipality>();
            string query = "SELECT * FROM Municipality";

            SqlCommand command = new SqlCommand(query, connection,transaction);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    municipalities.Add(new Municipality((int)reader["Id"], (string)reader["Name"], (int)reader["RegionId"]));
                }
            }
            return municipalities;
        }

        public Municipality GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            Municipality? municipality = null;
            string query = "SELECT * FROM Municipality WHERE Id = @Id";
        
            SqlCommand command = new SqlCommand(query, connection,transaction);
            command.Parameters.AddWithValue("Id", id);
            //connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    municipality = new Municipality((int)reader["Id"], (string)reader["Name"], (int)reader["RegionId"]);
                }
            }
            return municipality;
        }
    }
}
