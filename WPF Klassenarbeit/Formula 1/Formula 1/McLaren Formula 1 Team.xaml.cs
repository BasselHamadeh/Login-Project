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

namespace Formula_1
{
    /// <summary>
    /// Interaktionslogik für McLaren_Formula_1_Team.xaml
    /// </summary>
    public partial class McLaren_Formula_1_Team : UserControl
    {
        McLaren_Fahrer fahrer = null;
        mclarenquiz quiz = null;
        MCL60 auto = null;

        MainWindow wnd;
        private McLaren_Formula_1_Team mcLaren_Formula_1_Team;

        public McLaren_Formula_1_Team(MainWindow mainwindow)
        {
            InitializeComponent();

            wnd = mainwindow;

            fahrer = new McLaren_Fahrer();
            quiz = new mclarenquiz();
            auto = new MCL60();
        }

        public McLaren_Formula_1_Team(McLaren_Formula_1_Team mcLaren_Formula_1_Team)
        {
            this.mcLaren_Formula_1_Team = mcLaren_Formula_1_Team;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            McLaren_Formula_1_Team home = new McLaren_Formula_1_Team(this);
            contentcontrol.Content = home;
            home.Background = Brushes.Black;
            System.Diagnostics.Process.Start("file:///H:/WI22Z2/C%23/WPF%20Klassenarbeit/Formula%201/Formula%201/HTML/McLaren/index.html");
        }

        private void minimeren_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].WindowState = WindowState.Minimized;
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

        private void hauptseite_Click(object sender, RoutedEventArgs e)
        {
            MainWindow teamauswahl = new MainWindow();
            teamauswahl.Show();
        }

        private void btnFahrer_Click(object sender, RoutedEventArgs e)
        {
            contentcontrol.Content = fahrer;
        }

        private void mclarenquiz_Click(object sender, RoutedEventArgs e)
        {
            contentcontrol.Content = quiz;
        }

        private void btnauto_Click(object sender, RoutedEventArgs e)
        {
            contentcontrol.Content = auto;
        }
    }
}
