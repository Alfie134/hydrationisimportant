using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbulanceOptimization.Controllers;

namespace AmbulanceOptimization
{
    class LoginViewModel
    {
        public string UserName; //Databindet til username textbox
        public string Password; //Databindet til password textbox
        private LoginController _loginController;

        LoginViewModel()
        {
            _loginController = new LoginController();   
        }

        //LoginCommand //commandBindet til en loginknappen 

        public void Login()
        {
            //check om der findes en bruger med kombinationen af brugernavn og adgangskode 
            // videresend Bruger til MenuWindw 
        }

    }
}
