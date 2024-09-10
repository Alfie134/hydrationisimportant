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
using System.Security.Cryptography; //  Bruges til hashing 

namespace AmbulanceOptimization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BrugerIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) //   Der findes en Password Box, som maskerer input
        {
           throw new NotImplementedException("??");
        }

        private void PasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}