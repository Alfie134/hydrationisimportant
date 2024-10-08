using System.Windows;

namespace AmbulanceOptimization.Views
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void Open_TaskBank(object sender, RoutedEventArgs e)
        {
            TaskBank taskBank = new TaskBank();
            taskBank.Show();
            this.Close();
        }


        private void Open_Routes(object sender, RoutedEventArgs e)
        {
            RouteAdminWindow routeAdmin = new RouteAdminWindow();
            routeAdmin.Show();
            this.Close();
        }
    }
}
