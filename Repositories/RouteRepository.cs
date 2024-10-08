using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly string _connectionString;

        public RouteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Route entity, SqlConnection connection, SqlTransaction? transaction = null)
        {
            var command =
                new SqlCommand(
                    @$"INSERT INTO Route (VehicleId) VALUES (@VehicleId); SELECT SCOPE_IDENTITY();", connection,
                    transaction);

            int id = 0;
            using (command)
            {
                command.Parameters.AddWithValue("@VehicleId", entity.VehicleId);
                id = Convert.ToInt32(command.ExecuteScalar());
            }

            return id;
        }

        public IEnumerable<Route> GetAll(SqlConnection connection, SqlTransaction? transaction = null)
        {
            var routes = new List<Route>();
            string query = "SELECT * FROM Route";

            SqlCommand command = new SqlCommand(query, connection, transaction);
           
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    routes.Add(new Route((int)reader["Id"], (int)reader["VehicleId"]));
                }
            }

            return routes;
        }

        public Route? GetById(int id, SqlConnection connection, SqlTransaction? transaction = null)
        {
            Route? route = null;
            string query = "SELECT * FROM Route WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        route = new Route((int)reader["Id"], (int)reader["VehicleId"]);
                    }
                }
            }

            return route;
        }

        public void Update(Route entity, SqlConnection connection, SqlTransaction? transaction = null)
        {
            string query = "UPDATE Route SET VehicleId = @VehicleId WHERE Id = @Id";

            SqlCommand command = new SqlCommand(query, connection, transaction);
            using (command)
            {
                command.Parameters.AddWithValue("@VehicleId", entity.VehicleId);
                command.ExecuteNonQuery();
            }
        }
    }

}
