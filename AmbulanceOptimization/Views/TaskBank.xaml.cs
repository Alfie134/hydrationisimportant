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
using Models;

namespace AmbulanceOptimization.View
{
    /// <summary>
    /// Interaction logic for TaskBank.xaml
    /// </summary>
    public partial class TaskBank : Window
    {
        public TaskBank()
        {
            InitializeComponent();

            // Eksempel på en liste af Mission objekter, hentet fra din Mission klasse i Models
            var tasks = new List<Mission>
{
    new Mission(1, 1, "3456", TaskType.C, "Patient fra Rigshospitalet til Esbjerg",
             new ServiceLevel(1, "Haster", TimeSpan.FromHours(2)), DateTime.Now, 180,
             DateTime.Now.AddHours(2), "Sted: Dronningensgade 15, Esbjerg",
             new Postal(6700, "Esbjerg"), "Sted: Dr. Nielsensgade 34, Odense",
             new Postal(5000, "Odense"), "Eva Jensen"),

    new Mission(2, 1, "4567", TaskType.D, "Patient fra Rigshospitalet til Aalborg",
             new ServiceLevel(2, "Standard", TimeSpan.FromHours(1.5)), DateTime.Now, 90,
             DateTime.Now.AddHours(1.5), "Sted: Ågade 1, Aalborg",
             new Postal(9000, "Aalborg"), "Sted: Strandvejen 10, Esbjerg",
             new Postal(6700, "Esbjerg"), "Jonas Larsen")
};


            // Bind data til DataGrid
            TaskDataGrid.ItemsSource = tasks;
        }

        private void AssignVehicle_Click(object sender, RoutedEventArgs e)
        {
            AssignVehicle assignVehicleWindow = new AssignVehicle();

            assignVehicleWindow.ShowDialog();

            this.Close();
        }

    }
}
