using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly string _connectionString = new AppConfig().ConnectionString;


        public IEnumerable<Region> GetAll()
        {
            var regions = new List<Region>();
            string query = "SELECT * FROM Region";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? itSystem = reader.IsDBNull(2) ? null : reader.GetString(2);
                        regions.Add(new Region((int)reader["Id"], (string)reader["Name"], itSystem));
                    }
                }
            }
            return regions;
        }


        public Region GetById(int id)
        {
            Region region = null;
            string query = "SELECT * FROM Region WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string? itSystem = reader.IsDBNull(2) ? null : reader.GetString(2);
                        region = new Region((int)reader["Id"], (string)reader["Name"], itSystem);
                    }
                }
                return region;
            }
        }
    }
}