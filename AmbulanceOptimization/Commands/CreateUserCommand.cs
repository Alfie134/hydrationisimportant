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

        private readonly CreateUserViewModel _viewModel;

        public CreateUserCommand(CreateUserViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public bool CanExecute(object? parameter)
        {
            if (_viewModel.Username.Length <=5) return false;
            if (_viewModel.Password.Length <= 3) return false;
            else if (_viewModel.IsUsernameAvailable()==false) return false;
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

        // Brug dette til at opdatere CanExecute når brugernavnet er valideret
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
