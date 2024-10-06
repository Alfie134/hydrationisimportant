using System;
using System.Collections.Generic;
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
            CUVM.CheckIfUserNameIsTaken();
        }
    }
}
