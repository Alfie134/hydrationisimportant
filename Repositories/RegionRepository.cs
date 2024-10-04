using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly string _connectionString;
        public RegionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Region> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var regions = new List<Region>();
            string query = "SELECT * FROM Region";

            SqlCommand command = new SqlCommand(query, connection,transaction);
        

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string? itSystem = reader.IsDBNull(2) ? null : reader.GetString(2);
                    regions.Add(new Region((int)reader["Id"], (string)reader["Name"], itSystem));
                }
            }

            return regions;
        }

        public Region GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            Region? region = null;
            string query = "SELECT * FROM Region WHERE Id = @Id";
          
            SqlCommand command = new SqlCommand(query, connection,transaction);
            command.Parameters.AddWithValue("@Id", id);
         

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