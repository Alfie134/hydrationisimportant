using Models;
using Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace Repositories
{
    public class ServiceLevelRepository : IServiceLevelRepository
    {
        private readonly string _connectionString;

        public ServiceLevelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ServiceLevel> GetAll()
        {
            var serviceLevels = new List<ServiceLevel>();
            string query = "SELECT * FROM ServiceLevel";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        serviceLevels.Add(SerializeServiceLevel(reader));
                    }
                }
            }
            return serviceLevels;
        }   

        public ServiceLevel GetById(int id)
        {
            ServiceLevel serviceLevel = null;
            string query = "SELECT * FROM ServiceLevel WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        serviceLevel = SerializeServiceLevel(reader);
                    }
                }
            }
            return serviceLevel;
        }

        private ServiceLevel SerializeServiceLevel(SqlDataReader reader)
        {
            return new ServiceLevel(reader.GetInt32(reader.GetOrdinal("Id")), reader.GetString(reader.GetOrdinal("Name")), reader.GetTimeSpan(reader.GetOrdinal("TimeSpan")));
        }
    }
}
