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
    /// Interaction logic for Red_Bull_Formula_1_Team.xaml
    /// </summary>
    public partial class Red_Bull_Formula_1_Team : UserControl
    {
        MainWindow wnd;

        public Red_Bull_Formula_1_Team(MainWindow mainwindow)
        {
            InitializeComponent();

            wnd = mainwindow;

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

        private void hauptmenü_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Hauptmenü = new MainWindow();
            Hauptmenü.Show();
        }

        private void deutschland_Click(object sender, RoutedEventArgs e)
        {
            wnd.contentcontrol.Content = wnd.deutschii;
        }

        private void england_Click(object sender, RoutedEventArgs e)
        {
            wnd.contentcontrol.Content = wnd.englandii;
        }

        private void syrien_Click(object sender, RoutedEventArgs e)
        {
            wnd.contentcontrol.Content = wnd.syrischii;           
        }
    }
}
