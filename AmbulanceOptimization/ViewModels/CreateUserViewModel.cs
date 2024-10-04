using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AmbulanceOptimization.Commands;
using AmbulanceOptimization.Controllers;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class CreateUserViewModel
    {
        public string UserName { get; set; } 
        public string Password { get; set; }
        public List<Region> Regions { get; set; }
        public Region SelectedRegion { get; set; }

        private RegionController _regionController;
        private UserController _userController;

        public ICommand CreateUserCommand { get; set; } = new CreateUserCommand();

        public CreateUserViewModel()
        {
            Regions = new List<Region>();
            _regionController = new RegionController();
            _userController = new UserController();
            LoadRegions();

        }

        private void LoadRegions()
        {
            Regions = _regionController.GetAll();
        }

        internal void CreatUser()
        {
            _userController.Add(UserName,Password,SelectedRegion.RegionId);
        }
    }
}
