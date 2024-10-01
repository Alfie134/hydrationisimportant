using Configuration;
using Models;
using Microsoft.Data.SqlClient;
using Repositories.Interfaces;

namespace Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly string _connectionString;

        public MunicipalityRepository()
        {
            _connectionString = new AppConfig().ConnectionString;
        }
        
        public IEnumerable<Municipality> GetAll()
        {
            var municipalities = new List<Municipality>();
            string query = "SELECT * FROM Municipality";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        municipalities.Add(new Municipality((int)reader["Id"], (string)reader["Name"], (int)reader["RegionId"]));
                    }
                }
            }
            return municipalities;
        }


        public Municipality GetById(int id)
        {
            Municipality municipality = null;
            string query = "SELECT * FROM Municipality WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        municipality = new Municipality((int)reader["Id"], (string)reader["Name"], (int)reader["RegionId"]);
                    }
                }
            }
            return municipality;
        }
    }
}
