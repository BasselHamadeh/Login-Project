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
    /// Interaktionslogik für McLaren_Fahrer.xaml
    /// </summary>
    public partial class McLaren_Fahrer : UserControl
    {
        public McLaren_Fahrer()
        {
            InitializeComponent();
        }

        private void landosteckbrief_Click(object sender, RoutedEventArgs e)
        {
            landosteckbrief.Visibility = Visibility.Collapsed;
            oscarsteckbrief.Visibility = Visibility.Collapsed;
            pngoscar.Visibility = Visibility.Collapsed;
            pnglando.Visibility = Visibility.Collapsed;
            fahrerwahl.Visibility = Visibility.Collapsed;
            name.Visibility = Visibility.Visible;
            alter.Visibility = Visibility.Visible;
            nation.Visibility = Visibility.Visible;
            geburtdatum.Visibility = Visibility.Visible;
            gewicht.Visibility = Visibility.Visible;
            größe.Visibility = Visibility.Visible;
            website.Visibility = Visibility.Visible;
            lando.Visibility = Visibility.Visible;
            zurück.Visibility = Visibility.Visible;
            GBanzeigen.Visibility = Visibility.Visible;
            logoLNanzeigen.Visibility = Visibility.Visible;
        }

        private void oscarsteckbrief_Click(object sender, RoutedEventArgs e)
        {
            landosteckbrief.Visibility = Visibility.Collapsed;
            oscarsteckbrief.Visibility = Visibility.Collapsed;
            pnglando.Visibility = Visibility.Collapsed;
            pngoscar.Visibility = Visibility.Collapsed;
            fahrerwahl.Visibility = Visibility.Collapsed;
            NAME.Visibility = Visibility.Visible;
            ALTER.Visibility = Visibility.Visible;
            NATION.Visibility = Visibility.Visible;
            GEBURTSDATUM.Visibility = Visibility.Visible;
            GEWICHT.Visibility = Visibility.Visible;
            GRÖßE.Visibility = Visibility.Visible;
            WEBSITE.Visibility = Visibility.Visible;
            oscar.Visibility = Visibility.Visible;
            zurück.Visibility = Visibility.Visible;
            AUSanzeigen.Visibility = Visibility.Visible;
            logoOPnzeigen.Visibility = Visibility.Visible;
        }

        private void zurück_Click(object sender, RoutedEventArgs e)
        {
            McLaren_Fahrer Fahrer = new McLaren_Fahrer();
            cont.Content = Fahrer;
        }

        private void GBanzeigen_Click(object sender, RoutedEventArgs e)
        {
            GB.Visibility = Visibility.Visible;
            GBanzeigen.Visibility = Visibility.Collapsed;
        }

        private void AUSanzeigen_Click(object sender, RoutedEventArgs e)
        {
            AUS.Visibility = Visibility.Visible;
            AUSanzeigen.Visibility = Visibility.Collapsed;
        }

        private void logoLNanzeigen_Click(object sender, RoutedEventArgs e)
        {
            logoLN.Visibility = Visibility.Visible;
            logoLNanzeigen.Visibility = Visibility.Collapsed;
        }

        private void logoOPnzeigen_Click(object sender, RoutedEventArgs e)
        {
            logoOP.Visibility = Visibility.Visible;
            logoOPnzeigen.Visibility = Visibility.Collapsed;
        }
    }
}
