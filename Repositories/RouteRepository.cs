using Configuration;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly string _connectionString = new AppConfig().ConnectionString;
        public IEnumerable<Route> GetAll()
        {
            var routes = new List<Route>();
            string query = "SELECT * FROM Route";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        routes.Add(new Route((int)reader["Id"], (int)reader["VehicleId"]));
                    }
                }
            }
            return routes;
        }

        public Route GetById(int id)
        {
            Route route = null;
            string query = "SELECT * FROM Route WHERE Id = @Id";
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
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
    }
}
