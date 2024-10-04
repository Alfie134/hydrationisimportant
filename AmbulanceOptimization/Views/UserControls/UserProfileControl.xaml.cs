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
        bool IsUserOptionsDown = false;
        public UserProfileControl()
        {
            InitializeComponent();
        }
        // Event handler for når brugeren klikker på profilknappen
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsUserOptionsDown == false)
            {
                //Opret bruger 
                Button createUserButton = new Button();
                createUserButton.Content = "Opret Profil";
                createUserButton.Name = "CreateUser";
                createUserButton.Click += CreateUserButton_Click;
                UserDropDown.Children.Add(createUserButton);

                //Log ud 
                Button LogOutButton = new Button();
                LogOutButton.Content = "Log ud";
                LogOutButton.Name = "LogOut";
                LogOutButton.Click += LogOutButton_Click;

                UserDropDown.Children.Add(LogOutButton);
                IsUserOptionsDown = true;
            }
            else
            {
                // Find knappen ved dens navn
                Button CreateButton = UserDropDown.Children
                    .OfType<Button>()
                    .FirstOrDefault(b => b.Name == "CreateUser");

                if (CreateButton != null)
                {
                    UserDropDown.Children.Remove(CreateButton);
                }

                // Find knappen ved dens navn
                Button LogOutButton = UserDropDown.Children
                    .OfType<Button>()
                    .FirstOrDefault(b => b.Name == "LogOut");

                if (LogOutButton != null)
                {
                    UserDropDown.Children.Remove(LogOutButton);
                }

                IsUserOptionsDown = false;
            }
        }
        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Handling når knappen klikkes (f.eks. opret bruger)
            CreateUserWindow cuw = new CreateUserWindow();
            cuw.Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Luk alle vinduer undtagen MainWindow
            foreach (Window window in Application.Current.Windows)
            {
                if (!(window is MainWindow))
                {
                    window.Close();
                }
            }
        }
    }
}
