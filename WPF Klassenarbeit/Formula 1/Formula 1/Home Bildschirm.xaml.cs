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
    /// Interaction logic for Home_Bildschirm.xaml
    /// </summary>
    public partial class Home_Bildschirm : UserControl
    {

        Telefon tele = null;

        public Home_Bildschirm()
        {
            InitializeComponent();

            tele = new Telefon();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            zeit.Content = DateTime.Now.ToLongTimeString();
        }

        private void telefon_Click(object sender, RoutedEventArgs e)
        {
            TeleMusGog.Content = tele;
            telefon.Visibility = Visibility.Collapsed;
            google.Visibility = Visibility.Collapsed;
            musik.Visibility = Visibility.Collapsed;
            grid1.Visibility = Visibility.Collapsed;
        }

        private void google_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.ferrari.com/en-EN/formula1");
        }

        private void musik_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Keine Playlist.");
        }
    }
}
