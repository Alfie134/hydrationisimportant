using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AmbulanceOptimization.Controllers;
using AmbulanceOptimization.Views;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class TaskBankViewModel : INotifyPropertyChanged
    {
        // ObservableCollection til alle missioner
        public ObservableCollection<Mission> Missions { get; set; }

        // ObservableCollection til de valgte missioner
        public ObservableCollection<Mission> SelectedMissions { get; set; } = new ObservableCollection<Mission>();

        private bool _showAllMissions = true;
        public bool ShowAllMissions
        {
            get { return _showAllMissions; }
            set
            {
                if (_showAllMissions != value)
                {
                    _showAllMissions = value;
                    OnPropertyChanged();
                    LoadMissions();
                }
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                    LoadMissions();
                }
            }
        }

        private readonly MissionController _missionController;

        public TaskBankViewModel()
        {
            _missionController = new MissionController();
            Missions = new ObservableCollection<Mission>();
            LoadMissions();

            // Initialiser kommandoerne
            AssignMissionCommand = new RelayCommand(_ => AssignMission(), _ => CanAssignMission());
            OpenMissionDetailsCommand = new RelayCommand(_ => OpenMissionDetails(), _ => CanOpenMissionDetails());

            // Overvåg ændringer i SelectedMissions for at opdatere kommandoernes CanExecute
            SelectedMissions.CollectionChanged += (s, e) =>
            {
                (AssignMissionCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (OpenMissionDetailsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            };
        }

        private void LoadMissions()
        {
            Missions.Clear();

            List<Mission> tempMissions;

            if (SelectedDate == null && ShowAllMissions)
            {
                tempMissions = _missionController.GetAll(); // Henter ALLE opgaver
            }
            else
            {
                tempMissions = _missionController.GetFilteredMissions(SelectedDate, ShowAllMissions);
            }

            foreach (var mission in tempMissions)
            {
                Missions.Add(mission);
            }
        }

        // Kommando til "Tildel"-knappen
        public ICommand AssignMissionCommand { get; }

        // Kommando til at åbne mission detaljer
        public ICommand OpenMissionDetailsCommand { get; }

        private void AssignMission()
        {
            if (SelectedMissions != null && SelectedMissions.Count > 0)
            {
                var assignMissionToRouteWindow = new AssignMissionToRouteWindow
                {
                    DataContext = new AssignMissionToRouteViewModel(SelectedMissions)
                };

                assignMissionToRouteWindow.ShowDialog();
            }
        }

        private bool CanAssignMission()
        {
            // Returnerer true, hvis mindst én mission er valgt
            return SelectedMissions != null && SelectedMissions.Count > 0;
        }

        private void OpenMissionDetails()
        {
            if (SelectedMissions != null && SelectedMissions.Count == 1)
            {
                var selectedMission = SelectedMissions.First();
                var assignMissionToRouteWindow = new AssignMissionToRouteWindow
                {
                    DataContext = new MissionDetailsViewModel(selectedMission)
                };

                assignMissionToRouteWindow.ShowDialog();
            }
        }

        private bool CanOpenMissionDetails()
        {
            // Kun aktiveret, når præcis én mission er valgt
            return SelectedMissions != null && SelectedMissions.Count == 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
