using System.Windows;
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
            viewModel = new TaskBankViewModel();
            DataContext = viewModel;

        }

        private void AssignVehicle_Click(object sender, RoutedEventArgs e)
        {
            AssignVehicle assignVehicleWindow = new AssignVehicle();

            assignVehicleWindow.ShowDialog();

            this.Close();
        }

    }
}
