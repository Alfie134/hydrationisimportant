using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using AmbulanceOptimization.ViewModels; //  Bruges til hashing 
using AmbulanceOptimization.Views;

namespace AmbulanceOptimization
{
   
    public partial class MainWindow : Window
    {
        LoginViewModel loginViewModel;

        public MainWindow()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel();
            DataContext = loginViewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                var viewModel = this.DataContext as LoginViewModel;
                if (viewModel != null)
                {
                    viewModel.Password = passwordBox.SecurePassword; // Sætter SecurePassword i ViewModel
                    
                }
            }
        }
    }
}