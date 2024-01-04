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

namespace Formula_1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public McLaren_Formula_1_Team McLaren = null;

        public Ferrari_Formula_1_Team Ferrari = null;

        public Red_Bull_Formula_1_Team RedBull = null;

        public Deutsch deutschii = null;

        public Syrien syrischii = null;

        public England englandii = null;

        public Red_Bull_Deutsch reddeutsch = null;

        public Red_Bull_Englisch redenglisch = null;

        public Red_Bull_Syrisch redsyrisch = null;

        public MainWindow()
        {
            InitializeComponent();

            McLaren = new McLaren_Formula_1_Team(this);

            Ferrari = new Ferrari_Formula_1_Team(this);

            RedBull = new Red_Bull_Formula_1_Team(this);

            deutschii = new Deutsch(this);

            syrischii = new Syrien(this);

            englandii = new England(this);

            reddeutsch = new Red_Bull_Deutsch(this);

            redenglisch = new Red_Bull_Englisch(this);

            redsyrisch = new Red_Bull_Syrisch(this);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            zeit.Content = DateTime.Now.ToLongTimeString();
        }

        private void minimeren_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void maximieren_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void schließen_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mclaren_Click(object sender, RoutedEventArgs e)
        {
            contentcontrol.Content = McLaren;
        }

        private void ferrari_Click(object sender, RoutedEventArgs e)
        {
            contentcontrol.Content = Ferrari;
        }

        private void redbull_Click(object sender, RoutedEventArgs e)
        {
            contentcontrol.Content = RedBull;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
