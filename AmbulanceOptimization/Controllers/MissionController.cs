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
    }
}
