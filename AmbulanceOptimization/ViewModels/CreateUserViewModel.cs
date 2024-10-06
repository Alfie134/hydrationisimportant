using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AmbulanceOptimization.Commands;
using AmbulanceOptimization.Controllers;
using Models;

namespace AmbulanceOptimization.ViewModels
{
    internal class CreateUserViewModel: INotifyPropertyChanged
    {
        private string _username = "";
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                    ((CreateUserCommand)CreateUserCommand).RaiseCanExecuteChanged(); // Opdater CanExecute, når brugernavnet ændres
                }
            }
        }
        private string _password = "";
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                    ((CreateUserCommand)CreateUserCommand).RaiseCanExecuteChanged(); // Opdater CanExecute, når adgangskoden ændres
                }
            }
        }
        public List<Region> Regions { get; set; }
        public Region SelectedRegion { get; set; }

        private RegionController _regionController;
        private UserController _userController;

        public ICommand CreateUserCommand { get; set; }

        public CreateUserViewModel()
        {
            Regions = new List<Region>();
            _regionController = new RegionController();
            _userController = new UserController();
            LoadRegions();
            
            CreateUserCommand   = new CreateUserCommand(this);
        }

        private void LoadRegions()
        {
            Regions = _regionController.GetAll();
        }

        internal void CreatUser()
        {
            _userController.Add(Username,Password,SelectedRegion.RegionId);
        }

        public bool IsUsernameAvailable()
        {
            if (_userController.GetUserByUserName(Username)==null)
            {
                return true; //Brugeren blev ikke fundet. brugernavn er ledigt
            }
            return false; //brugernavn er optaget.
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
