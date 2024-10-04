using AmbulanceOptimization.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceOptimization.Commands
{
    internal class CreateUserCommand: ICommand
    {
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is CreateUserViewModel cuvm)
            {
                cuvm.CreatUser();
                // Luk det nuværende vindue
                Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }

        }

        public event EventHandler? CanExecuteChanged;
    }
}
