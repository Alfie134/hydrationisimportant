using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AmbulanceOptimization.Commands;
using AmbulanceOptimization.ViewModels;

namespace AmbulanceOptimization.Views
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        private CreateUserViewModel CUVM;
        

        public CreateUserWindow()
        {
            InitializeComponent();
            CUVM = new CreateUserViewModel();
            DataContext = CUVM;
        }

        private void LostFokusUsernameFocus(object sender, RoutedEventArgs e)
        {

            if (CUVM.Username.Length < 5)
            {
                UserNameErrors.Visibility = Visibility.Visible;
                UserNameErrors.Content = "Brugernavn er for kort, mindst 5 tegn"; //Fortæl bruger brugernavn er for kort
            }
            else if (CUVM.IsUsernameAvailable() == false)
            {
                UserNameErrors.Visibility = Visibility.Visible;
                UserNameErrors.Content = "Brugernavnet er optaget"; //Fortæl bruger brugernavn er optaget 
            }
            else { UserNameErrors.Visibility = Visibility.Collapsed; }

            ((CreateUserCommand)CUVM.CreateUserCommand).RaiseCanExecuteChanged();
        }

        private void LostFokusPasswordFocus(object sender, RoutedEventArgs e)
        {
            if (CUVM.Password.Length < 3)
            {
                PasswordErrors.Visibility = Visibility.Visible;
                PasswordErrors.Content = "adgangskode er for kort, mindst 3 tegn";
            }
            else PasswordErrors.Visibility = Visibility.Collapsed;
            ((CreateUserCommand)CUVM.CreateUserCommand).RaiseCanExecuteChanged();
        }
    }
}
