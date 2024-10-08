using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using AmbulanceOptimization.Controllers;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class RouteAdminViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Route> Routes { get; set; } // Skiftet fra List til ObservableCollection
        public ObservableCollection<Mission> MissionsOnRoute { get; set; }
        public ObservableCollection<Mission> MissionSuggetionsByPostal { get; set; }
        public ObservableCollection<Mission> MissionsSuggestedByMunicipality { get; set; }

        private Route _selectedRoute;
        public Route SelectedRoute
        {
            get { return _selectedRoute; }
            set
            {
                _selectedRoute = value;
                OnPropertyChanged(nameof(SelectedRoute));
                LoadMissionsOnRoute();
                LoadSuggestions();
            }
        }

        private readonly RouteController _routeController;
        private readonly MissionController _missionController;

        public RouteAdminViewModel()
        {
            Routes = new ObservableCollection<Route>(); // Skiftet til ObservableCollection
            MissionsOnRoute = new ObservableCollection<Mission>();
            MissionSuggetionsByPostal = new ObservableCollection<Mission>();
            MissionsSuggestedByMunicipality = new ObservableCollection<Mission>();
            _routeController = new RouteController();
            _missionController = new MissionController();
            LoadRoutes();
        }

        private void LoadRoutes()
        {
            Routes.Clear();
            var tempRoutes = _routeController.GetAll();

            if (tempRoutes != null)
            {
                foreach (var route in tempRoutes)
                {
                    Routes.Add(route);
                }
            }
        }

        private void LoadMissionsOnRoute()
        {
            if (SelectedRoute == null)
                return;

            MissionsOnRoute.Clear();
            var tempMissions = _missionController.GetMissionsByRouteId(SelectedRoute.Id);

            if (tempMissions != null)
            {
                foreach (var mission in tempMissions)
                {
                    MissionsOnRoute.Add(mission);
                }
            }
        }

        private void LoadSuggestions()
        {
            LoadSuggestionsFromPostal();
            LoadSuggestionsFromMunicipality();
        }

        private void LoadSuggestionsFromMunicipality()
        {
            //Forslag i samme kommune
            MissionsSuggestedByMunicipality.Clear();
            List<Mission> tempMissionsMunipality = new List<Mission>();

            foreach (Mission mission in MissionsOnRoute)
            {
                // Hent missioner baseret på afhentning
                List<Mission> pickupMissions = _missionController.SuggestMissionsByMunicipality(mission.ExpectedDeparture, mission.FromPostalCode, false);
                tempMissionsMunipality.AddRange(pickupMissions);

                // Tilføjer alle missioner fundet ved afsætningsstedet
                List<Mission> dropOffMissions = _missionController.SuggestMissionsByMunicipality(mission.ExpectedArrival, mission.ToPostalCode, true);
                tempMissionsMunipality.AddRange(dropOffMissions);
            }

            foreach (var mission in tempMissionsMunipality)
            {
                MissionsSuggestedByMunicipality.Add(mission);

            }
        }

        private void LoadSuggestionsFromPostal()
        {
            //forslag i sammepostnummer
            MissionSuggetionsByPostal.Clear();
            List<Mission> tempMissionsPostal = new List<Mission>();

            foreach (Mission mission in MissionsOnRoute)
            {
                // Hent missioner baseret på afhentning
                List<Mission> pickupMissions = _missionController.SuggestMissionsByPostal(mission.ExpectedDeparture, mission.FromPostalCode, false);
                tempMissionsPostal.AddRange(pickupMissions);

                // Tilføjer alle missioner fundet ved afsætningsstedet
                List<Mission> dropOffMissions = _missionController.SuggestMissionsByPostal(mission.ExpectedArrival, mission.ToPostalCode, true);
                tempMissionsPostal.AddRange(dropOffMissions);
            }

            // Fjern dubletter (hvis relevant)
            var uniqueMissions = tempMissionsPostal.Distinct().ToList();

            foreach (var mission in uniqueMissions)
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
