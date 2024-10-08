using System.Windows;
using System.Windows.Controls;
using AmbulanceOptimization.ViewModels;
using Models;

namespace AmbulanceOptimization.Views
{
    /// <summary>
    /// Interaction logic for TaskBank.xaml
    /// </summary>
    public partial class TaskBank : Window
    {
        TaskBankViewModel viewModel;

        public TaskBank()
        {
            InitializeComponent();
            Datepicker.SelectedDate = DateTime.Now;

            viewModel = new TaskBankViewModel();
            DataContext = viewModel;

        }

        private void AssignVehicle_Click(object sender, RoutedEventArgs e)
        {
            AssignVehicle assignVehicleWindow = new AssignVehicle();

            assignVehicleWindow.ShowDialog();

            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
