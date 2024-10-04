using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;

namespace AmbulanceOptimization.Controllers
{
    internal class RegionController
    {
        private RegionService _regionService;

        public RegionController()
        {
            _regionService = new RegionService();
        }
        public List<Region> GetAll()
        {
            return _regionService.GetAll();
        }
    }
}
