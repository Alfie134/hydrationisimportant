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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmbulanceOptimization.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfileControl : UserControl
    {
        public UserProfileControl()
        {
            InitializeComponent();
        }
        // Event handler for når brugeren klikker på profilknappen
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Her kan du håndtere, hvad der sker, når der klikkes på profilknappen
            // Du kan f.eks. åbne et nyt vindue med brugeroplysninger eller logge ud
            MessageBox.Show("Profilknappen blev klikket!");
        }
    }
}
