using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using AmbulanceOptimization.Controllers;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class RouteAdminViewModel: INotifyPropertyChanged
    {
        public List<Route> Routes { get; set; }
        public ObservableCollection<Mission> MissionsOnRoute { get; set; }
        public ObservableCollection<Mission> MissionSuggetionsByPostal { get; set; }

        private Route _selectedRoute { get; set; } 
        public Route SelectedRoute
        {
            get { return _selectedRoute; }
            set {
                _selectedRoute = value;
                OnPropertyChanged(nameof(SelectedRoute));
                LoadMissionsOnRoute();
                LoadSuggestions();
            }
        }

        private RouteController _routeController {get; set; }
        private MissionController _missionController {get; set; }

        public RouteAdminViewModel()
        {
            Routes = new List<Route>();
            MissionsOnRoute = new ObservableCollection<Mission>();
            MissionSuggetionsByPostal = new ObservableCollection<Mission>();
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
                foreach (Mission mission in tempMissions)
                {
                    MissionsOnRoute.Add(mission); 
                }
            }
        }

        private void LoadSuggestions()
        {
            MissionSuggetionsByPostal.Clear();
            List<Mission> tempMissionsPostal = new List<Mission>();

            foreach (Mission mission in MissionsOnRoute)
            {
                // Hent missioner baseret på afhentning
                var pickupMissions = _missionController.SuggestMissionsByPostal(mission.ExpectedDeparture, mission.FromPostalCode, false);
                tempMissionsPostal.AddRange(pickupMissions);

                // Tilføjer alle missioner fundet ved afsætningsstedet til tempMissionsPostal
                var dropOffMissions = _missionController.SuggestMissionsByPostal(mission.ExpectedArrival, mission.ToPostalCode, true);
                tempMissionsPostal.AddRange(dropOffMissions);
            }
            foreach (Mission mission in tempMissionsPostal)
            {
                MissionSuggetionsByPostal.Add(mission);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
