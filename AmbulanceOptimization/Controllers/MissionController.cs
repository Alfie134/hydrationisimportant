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
        private SuggestRouteService _routeService;

        public MissionController()
        {
            _missionService = new MissionService();
            _routeService = new SuggestRouteService();
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

        public List<Mission> SuggestMissionsByPostal(DateTime date, int postal, bool Arrival)
        {
            return _routeService.SuggestMissionsByPostals(date,new List<int> {postal}, Arrival);
        }

        public List<Mission> SuggestMissionsByMunicipality(DateTime date, int postal, bool arrival)
        {
            return _routeService.SuggestOnMunicality(date, postal, arrival);
        }
    }
}
