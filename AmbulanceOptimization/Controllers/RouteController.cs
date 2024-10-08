using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;

namespace AmbulanceOptimization.Controllers
{
    internal class RouteController
    {
        private RouteService _routeService { get; set; }

        public RouteController()
        {
            _routeService = new RouteService();
        }

        public List<Route> GetAll()
        {
            return _routeService.GetAll();
        }
    }
}
