using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;

namespace AmbulanceOptimization.Controllers
{
    internal class MissionController
    {
        private MissionService _missionService;

        public MissionController()
        {
            _missionService = new MissionService();
        }

        public List<Mission> GetAll()
        {
            return _missionService.GetAll();
        }
        public List<Mission> GetFilteredMissions(DateTime? selectedDate, bool isChecked)
        {
            return _missionService.GetFilteredMissions(selectedDate, isChecked);
        }

        public List<Mission> GetMissionsByRouteId(int id)
        {
            return _missionService.GetMissionsByRouteId(id);
        }
    }
}
