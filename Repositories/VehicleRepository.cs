using Models;
using Repositories;
using Microsoft.Data.SqlClient;
using Repositories.Interfaces;
using Configuration;

namespace Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly string _connectionString;

        public VehicleRepository()
        {
            _connectionString = new AppConfig().ConnectionString;
        }

        public IEnumerable<Vehicle> GetAll()
        {
            var vehicles = new List<Vehicle>();
            string query = "SELECT * FROM Vehicle";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vehicles.Add(SerializeUser(reader));
                    }
                }
            }
            return vehicles;
        }

        public Vehicle GetById(int id)
        {
            Vehicle vehicle = null;
            string query = "SELECT * FROM Vehicle WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vehicle = SerializeUser(reader);
                    }
                }
            }
            return vehicle;
        }

        public Vehicle SerializeUser(SqlDataReader reader)
        {
            return new Vehicle(reader.GetInt32(reader.GetOrdinal("Id")),
                (VehicleType)Enum.Parse(typeof(VehicleType), reader.GetString(reader.GetOrdinal("VehicleType"))),
                reader.GetInt32(reader.GetOrdinal("OperatorId")));
        }
    }
}
