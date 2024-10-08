using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AmbulanceOptimization.Controllers;
using AmbulanceOptimization.Views;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class TaskBankViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Mission> Missions { get; set; }

        private bool _showAllMissions = true;
        public bool ShowAllMissions
        {
            get { return _showAllMissions; }
            set
            {
                _showAllMissions = value;
                OnPropertyChanged();
                LoadMissions();
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
                LoadMissions();
            }
        }

        private Mission _selectedMission;
        public Mission SelectedMission
        {
            get { return _selectedMission; }
            set
            {
                _selectedMission = value;
                OnPropertyChanged();
            }
        }

        private readonly MissionController _missionController;

        public TaskBankViewModel()
        {
            _missionController = new MissionController();
            Missions = new ObservableCollection<Mission>();
            LoadMissions();

            // Initialiser kommandoerne
            OpenMissionDetailsCommand = new RelayCommand(_ => OpenMissionDetails(), _ => CanOpenMissionDetails());
            AssignMissionCommand = new RelayCommand(_ => AssignMission(), _ => CanAssignMission());
        }

        private void LoadMissions()
        {
            var tempMissions = new List<Mission>();
            Missions.Clear();

            if (SelectedDate == null && _showAllMissions)
            {
                tempMissions = _missionController.GetAll(); // Henter ALLE opgaver
            }
            else
            {
                tempMissions = _missionController.GetFilteredMissions(_selectedDate, _showAllMissions);
            }

            foreach (var mission in tempMissions)
            {
                Missions.Add(mission);
            }
        }

        // Kommando til at åbne detaljer
        public ICommand OpenMissionDetailsCommand { get; }

        // Kommando til "Tildel"-knappen
        public ICommand AssignMissionCommand { get; }

        private void AssignMission()
        {
            if (SelectedMission != null)
            {
                // Her kan du implementere logik for at tildele en mission, f.eks. til en rute eller ambulance
                var assignMissionToRouteWindow = new AssignMissionToRouteWindow();
                assignMissionToRouteWindow.DataContext = new MissionDetailsViewModel(SelectedMission);
                assignMissionToRouteWindow.ShowDialog();
            }
        }

        private bool CanAssignMission()
        {
            // Returner true, hvis en mission er valgt
            return SelectedMission != null;
        }

        private void OpenMissionDetails()
        {
            if (SelectedMission != null)
            {
                var assignMissionToRouteWindow = new AssignMissionToRouteWindow();
                assignMissionToRouteWindow.DataContext = new MissionDetailsViewModel(SelectedMission);
                assignMissionToRouteWindow.Show();
            }
        }

        private bool CanOpenMissionDetails()
        {
            return SelectedMission != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
