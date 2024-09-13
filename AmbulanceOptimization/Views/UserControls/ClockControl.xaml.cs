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
using System.Windows.Threading;

namespace AmbulanceOptimization.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ClockControl.xaml
    /// </summary>
    public partial class ClockControl : UserControl
    {
        private DispatcherTimer timer;

        public ClockControl()
        {
            InitializeComponent();
            StartClock();
        }

        private void StartClock()
        {
            // Initialize and start the timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Update every second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the time in the TextBlock
            TimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}