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
    /// Interaktionslogik für MCL60.xaml
    /// </summary>
    public partial class MCL60 : UserControl
    {

        int bildzahl = 1;

        public MCL60()
        {
            InitializeComponent();

            mcl601.Visibility = Visibility.Visible;
            mcl602.Visibility = Visibility.Collapsed;
            mcl603.Visibility = Visibility.Collapsed;
            mcl604.Visibility = Visibility.Collapsed;
        }

        private void rechtsklick1_Click(object sender, RoutedEventArgs e)
        {
            bildzahl++;

            if (bildzahl > 4)


                bildzahl = 1;

            if (bildzahl == 1)
            {
                mcl601.Visibility = Visibility.Visible;
                mcl602.Visibility = Visibility.Collapsed;
                mcl603.Visibility = Visibility.Collapsed;
                mcl604.Visibility = Visibility.Collapsed;
                linksklick1.Visibility = Visibility.Collapsed;
            }
            else if (bildzahl == 2)
            {
                mcl601.Visibility = Visibility.Collapsed;
                mcl602.Visibility = Visibility.Visible;
                mcl603.Visibility = Visibility.Collapsed;
                mcl604.Visibility = Visibility.Collapsed;
                linksklick1.Visibility = Visibility.Visible;
                anzahl.Text = $"Bild: {bildzahl} / 4";
            }
            else if (bildzahl == 3)
            {
                mcl601.Visibility = Visibility.Collapsed;
                mcl602.Visibility = Visibility.Collapsed;
                mcl603.Visibility = Visibility.Visible;
                mcl604.Visibility = Visibility.Collapsed;
                linksklick1.Visibility = Visibility.Visible;
                anzahl.Text = $"Bild: {bildzahl} / 4";
            }
            else if (bildzahl == 4)
            {
                mcl601.Visibility = Visibility.Collapsed;
                mcl602.Visibility = Visibility.Collapsed;
                mcl603.Visibility = Visibility.Collapsed;
                mcl604.Visibility = Visibility.Visible;
                rechtsklick1.Visibility = Visibility.Collapsed;
                linksklick1.Visibility = Visibility.Visible;
                anzahl.Text = $"Bild: {bildzahl} / 4";
            }

        }

        private void linksklick1_Click(object sender, RoutedEventArgs e)
        {
            bildzahl--;

            if (bildzahl < 1)


                bildzahl = 4;

            if (bildzahl == 1)
            {
                mcl601.Visibility = Visibility.Visible;
                mcl602.Visibility = Visibility.Collapsed;
                mcl603.Visibility = Visibility.Collapsed;
                mcl604.Visibility = Visibility.Collapsed;
                linksklick1.Visibility = Visibility.Collapsed;
                anzahl.Text = $"Bild: {bildzahl} / 4";
            }
            else if (bildzahl == 2)
            {
                mcl601.Visibility = Visibility.Collapsed;
                mcl602.Visibility = Visibility.Visible;
                mcl603.Visibility = Visibility.Collapsed;
                mcl604.Visibility = Visibility.Collapsed;
                linksklick1.Visibility = Visibility.Visible;
                rechtsklick1.Visibility = Visibility.Visible;
                anzahl.Text = $"Bild: {bildzahl} / 4";
            }
            else if (bildzahl == 3)
            {
                mcl601.Visibility = Visibility.Collapsed;
                mcl602.Visibility = Visibility.Collapsed;
                mcl603.Visibility = Visibility.Visible;
                mcl604.Visibility = Visibility.Collapsed;
                linksklick1.Visibility = Visibility.Visible;
                rechtsklick1.Visibility = Visibility.Visible;
                anzahl.Text = $"Bild: {bildzahl} / 4";
            }
            else if (bildzahl == 4)
            {
                mcl601.Visibility = Visibility.Collapsed;
                mcl602.Visibility = Visibility.Collapsed;
                mcl603.Visibility = Visibility.Collapsed;
                mcl604.Visibility = Visibility.Visible;
                linksklick1.Visibility = Visibility.Visible;
                rechtsklick1.Visibility = Visibility.Collapsed;
                anzahl.Text = $"Bild: {bildzahl} / 4";
            }
        }
    }
}
