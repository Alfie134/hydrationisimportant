using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class VehicleRepository: IVehicleRepository
    {

        public IEnumerable<Vehicle> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var vehicles = new List<Vehicle>();
            string query = "SELECT * FROM Vehicle";

            SqlCommand command = new SqlCommand(query, connection, transaction);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    vehicles.Add(new Vehicle((int)reader["Id"], (string)reader["VehicleNumber"], (int)reader["OperatorId"], (int)reader["RegionId"]));
                }
            }
            return vehicles;
        }

        public Vehicle GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            Vehicle? vehicle = null;
            string query = "SELECT * FROM Vehicle WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", id);
           
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vehicle = new Vehicle((int)reader["Id"], (string)reader["VehicleNumber"],(int)reader["OperatorId"], (int)reader["RegionId"]);
                    }
                }
            }
            return vehicle;
        }
    }
}
