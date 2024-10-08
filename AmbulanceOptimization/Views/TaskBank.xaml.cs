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

      /*  Dette Click er ikke længere en funktion vi benytter
       *  private void AssignVehicle_Click(object sender, RoutedEventArgs e)
        {
            AssignVehicle assignVehicleWindow = new AssignVehicle();

            assignVehicleWindow.ShowDialog();

            this.Close();
        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void TaskDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel != null)
            {
                // Rydder tidligere valgte missioner
                viewModel.SelectedMissions.Clear();

                // Tilføjer de aktuelt valgte missioner
                foreach (Mission mission in TaskDataGrid.SelectedItems)
                {
                    viewModel.SelectedMissions.Add(mission);
                }
            }
        }
    }
}
