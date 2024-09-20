using Models;
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

namespace AmbulanceOptimization.View
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
                var selectedVehicle =(Vehicle)button.DataContext;

                if (selectedVehicle != null)
                {
                   
                    ConfirmAssignment confirmAssignmentWindow = new ConfirmAssignment();

                    confirmAssignmentWindow.ShowDialog(); // Viser det nye vindue


                    // Luk det nuværende AssignVehicle vindue
                    this.Close();

                }

                else 
                {
                        // Vis en fejlbesked, hvis ingen ambulance er valgt
                        MessageBox.Show("Vælg venligst en ambulance.");
                }
                

            
            }
        }
    }
}

