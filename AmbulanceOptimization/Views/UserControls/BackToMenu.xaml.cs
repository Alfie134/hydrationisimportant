using AmbulanceOptimization.View;
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
    /// Interaction logic for BackToMenu.xaml
    /// </summary>
    public partial class BackToMenu : UserControl
    {
        public BackToMenu()
        {
            InitializeComponent();
        }

        // Event handler for "Vend tilbage til menu"-knappen
        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Opretter og viser MenuWindow-vinduet
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();

            // Lukker det aktuelle vindue (forældre-vinduet, hvor knappen er placeret)
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
    }
}
