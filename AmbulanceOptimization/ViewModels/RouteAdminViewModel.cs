using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbulanceOptimization.Controllers;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class RouteAdminViewModel
    {
        public List<Route> Routes { get; set; }
        public Route SelectedRoute { get; set; } 

        private RouteController _routeController {get; set; }

        public RouteAdminViewModel()
        {
            Routes = new List<Route>();
            _routeController = new RouteController();
            LoadRoutes();
        }

        private void LoadRoutes()
        {
            Routes = _routeController.GetAll();
        }
    }
}
