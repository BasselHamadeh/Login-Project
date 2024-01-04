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
    /// Interaction logic for England.xaml
    /// </summary>
    public partial class England : UserControl
    {

        MainWindow wnd;

        public England(MainWindow mainwindow)
        {
            InitializeComponent();

            wnd = mainwindow;

            for (int i = 2023; i >= 1900; i--)
            {
                Jahr.Items.Add(i);
            }

            for (int i = 1; i <= 31; i++)
            {
                Tag.Items.Add(i);
            }
        }

        private void fertig_Click(object sender, RoutedEventArgs e)
        {
            string vorname = textboxvorname.Text;
            string nachname = textboxvorname.Text;

            if (Nutzungsbedingunen.IsChecked == false)
            {
                MessageBox.Show("You must accept the terms of use.");
            }
            if (männlich.IsChecked == true && Nutzungsbedingunen.IsChecked == true)
            {
                MessageBox.Show($"Hello Mr. {nachname}. Welcome to Red Bull F1 Team.");
                wnd.contentcontrol.Content = wnd.redenglisch;
            }
            if (weiblich.IsChecked == true && Nutzungsbedingunen.IsChecked == true)
            {
                MessageBox.Show($"Hello Mrs. {nachname}. Welcome to Red Bull F1 Team.");
                wnd.contentcontrol.Content = wnd.redenglisch;
            }
        }

        private void sprache_Click(object sender, RoutedEventArgs e)
        {
            wnd.contentcontrol.Content = wnd.RedBull;
        }
    }
}
