using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AmbulanceOptimization.ViewModels;
using AmbulanceOptimization.Views;

namespace AmbulanceOptimization.Commands
{
    internal class LogInCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            // Her kan du tilføje betingelser for at aktivere knappen (f.eks. om brugernavn og password er udfyldt)
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is LoginViewModel livm)
            {
                if (livm.Password == null || livm.Password.Length == 0)
                {
                    MessageBox.Show("Password is required.");
                    return;
                }

                if (livm.Login()) // Hvis login er succesfuld
                {  
                    // Opret og vis menu vinduet
                    MenuWindow menuWindow = new MenuWindow();
                    menuWindow.Show();

                    // Luk det nuværende vindue (loginvinduet)
                    Application.Current.MainWindow.Close(); 
                }
                else
                {
                    MessageBox.Show("Invalid login credentials. Please try again.");
                }
            }
        }
    }
}
