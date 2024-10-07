using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AmbulanceOptimization.Controllers;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class RouteAdminViewModel: INotifyPropertyChanged
    {
        public List<Route> Routes { get; set; }
        public ObservableCollection<Mission> MissionsOnRoute { get; set; }

        private Route _selectedRoute { get; set; } 
        public Route SelectedRoute
        {
            get { return _selectedRoute; }
            set {
                _selectedRoute = value;
                OnPropertyChanged(nameof(SelectedRoute));
                LoadMissionsOnRoute();
            }
        }

        private RouteController _routeController {get; set; }
        private MissionController _missionController {get; set; }

        public RouteAdminViewModel()
        {
            Routes = new List<Route>();
            MissionsOnRoute = new ObservableCollection<Mission>();
            _routeController = new RouteController();
            _missionController = new MissionController();
            LoadRoutes();
        }

        private void LoadRoutes()
        {
            Routes = _routeController.GetAll();
        }

        private void LoadMissionsOnRoute()
        { 
            MissionsOnRoute.Clear();
            List<Mission> tempMissions = _missionController.GetMissionsByRouteId(SelectedRoute.Id);
            if (tempMissions != null)
            {
                foreach (var mission in tempMissions)
                {
                    MissionsOnRoute.Add(mission); 
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
