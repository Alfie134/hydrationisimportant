using Configuration;
using Repositories.Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Microsoft.Data.SqlClient;

namespace Services
{
   public  class RouteService
    {
        private readonly string _connectionString;
        private readonly IRouteRepository _routeRepository;
        public RouteService()
        {
            var config = new AppConfig();
            _connectionString = config.ConnectionString;
            _routeRepository = new RouteRepository(_connectionString);
        }

        public List<Route> GetAll() //TODO vi skal nok have et filter på der styr hvad dato der bliver hentet ruter op for 
        {
            List<Route> tempRoutes = new List<Route>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        tempRoutes = (List<Route>)_routeRepository.GetAll(connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return tempRoutes;
        }
    }
}
