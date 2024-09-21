using System.Windows;
using System.Windows.Controls;
using Models;

namespace AmbulanceOptimization.Views
{
    /// <summary>
    /// Interaction logic for AssignVehicle.xaml
    /// </summary>
    public partial class AssignVehicle : Window
    {
        public AssignVehicle()
        {
            InitializeComponent();
        }


        // DER MANGLER TING HER .... 
        private void Assign_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            if (button != null)
            {
                // Hent den valgte ambulance (Vehicle) fra den valgte række i DataGrid
                var selectedVehicle = button?.DataContext as Vehicle;
                // Ved ikke om denne kan virke ....
                var selectedMission = button?.DataContext as Mission;

                if (selectedVehicle != null)
                {

                    string message = $"Er du sikker på, at du vil tildele denne ambulance?\n\n" +
                             $"Ambulance ID: {selectedVehicle.Id}\n" +
                             $"Planlagt rute: {selectedMission.Id}\n";


                    MessageBoxResult result = MessageBox.Show(message, "Bekræft tildeling", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (result == MessageBoxResult.OK)
                    {
                        // Hvis brugeren klikker OK, kan du fortsætte med tildelingen af ambulancen
                        MessageBox.Show("Ambulance tildelt!", "Tildeling bekræftet", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Eventuelt luk vinduet her, hvis det er nødvendigt
                        // this.Close();
                    }
                }
                else
                {
                    // Vis en fejlbesked, hvis ingen ambulance er valgt
                    MessageBox.Show("Vælg venligst en ambulance.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

