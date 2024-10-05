using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbulanceOptimization.Controllers;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class TaskBankViewModel
    {
        public List<Mission> Missions { get; set; }

        private MissionController _missionController;

        public TaskBankViewModel()
        {
            _missionController = new MissionController();
            LoadMissions();

        }

        private void LoadMissions()
        {
            Missions = _missionController.GetAll();
        }

    }
}
