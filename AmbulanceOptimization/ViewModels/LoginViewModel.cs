using System.ComponentModel;
using System.Security;
using System.Windows.Input;
using AmbulanceOptimization.Commands;
using AmbulanceOptimization.Controllers;
using Models;

namespace AmbulanceOptimization.ViewModels
{
   public class LoginViewModel: INotifyPropertyChanged
   {
        public string UserName { get; set; } //Databindet til username textbox
        private SecureString _password { get; set; }
        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private LoginController _loginController;
        public LoginViewModel()
        {
            _loginController = new LoginController();   
        }

        public ICommand LoginCommand { get; } = new LogInCommand();  //LoginCommand //commandBindet til en loginknappen 
        public bool Login()
        {
            //check om der findes en bruger med kombinationen af brugernavn og adgangskode 
            User user = _loginController.AuthenticateUser(UserName,_password);
           
            CurrentUser.CUser = user;
            if (user != null)
            {
                return true;

            }
            else return false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
