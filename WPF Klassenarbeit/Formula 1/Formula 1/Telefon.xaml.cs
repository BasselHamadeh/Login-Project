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
    /// Interaction logic for Telefon.xaml
    /// </summary>
    public partial class Telefon : UserControl
    {
        string zahl;
        int count = 0;

        public Telefon()
        {
            InitializeComponent();
        }

        private void eins_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "1";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "1";
            zahlen.Text = zahl;
        }

        private void zwei_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "2";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "2";
            zahlen.Text = zahl;
        }

        private void drei_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "3";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "3";
            zahlen.Text = zahl;
        }

        private void vier_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "4";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "4";
            zahlen.Text = zahl;
        }

        private void fünf_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "5";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "5";
            zahlen.Text = zahl;
        }

        private void sechs_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "6";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "6";
            zahlen.Text = zahl;
        }

        private void sieben_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "7";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "7";
            zahlen.Text = zahl;
        }

        private void acht_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "8";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "8";
            zahlen.Text = zahl;
        }

        private void neun_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "9";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "9";
            zahlen.Text = zahl;
        }

        private void mal_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "*";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "*";
            zahlen.Text = zahl;
        }

        private void NNull_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "0";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "0";
            zahlen.Text = zahl;
        }

        private void hashtag_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "#";
            count += 1;
            if (count >= 8)
            {
                eins.IsEnabled = false;
                zwei.IsEnabled = false;
                drei.IsEnabled = false;
                vier.IsEnabled = false;
                fünf.IsEnabled = false;
                sechs.IsEnabled = false;
                sieben.IsEnabled = false;
                acht.IsEnabled = false;
                neun.IsEnabled = false;
                mal.IsEnabled = false;
                NNull.IsEnabled = false;
                hashtag.IsEnabled = false;
            }
            zahl = zahl + "#";
            zahlen.Text = zahl;
        }

        private void telefon_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "Telefon defekt.";
        }

        private void löschen_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(zahlen.Text))
            {
                zahlen.Text = zahlen.Text.Remove(zahlen.Text.Length - 1);
            }
        }
    }
}
