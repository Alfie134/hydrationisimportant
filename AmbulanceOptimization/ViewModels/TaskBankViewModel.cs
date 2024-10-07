using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using AmbulanceOptimization.Controllers;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class TaskBankViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Mission> Missions { get; set; }
        private bool _showAllMissions { get; set; } = true;
        public bool ShowAllMissions
        {
            get {return _showAllMissions; }
            set
            {
                _showAllMissions = value;
                OnPropertyChanged(nameof(ShowAllMissions));
                LoadMissions();
            }
        }

        private DateTime? _selectedDate { get; set; }

        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                LoadMissions();
            }
        }

private MissionController _missionController;

        public TaskBankViewModel()
        {
            _missionController = new MissionController();
            Missions = new ObservableCollection<Mission>();
            LoadMissions();
          
        }

        private void LoadMissions()
        {
            List <Mission>  tempMissions = new List<Mission>();
            Missions.Clear();

            if (SelectedDate == null && _showAllMissions == true)
            {
                tempMissions = _missionController.GetAll(); // henter ALLE Opgaver
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

     
    }
}
