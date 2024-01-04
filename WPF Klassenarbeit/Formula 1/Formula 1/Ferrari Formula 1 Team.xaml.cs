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
    /// Interaktionslogik für Ferrari_Formula_1_Team.xaml
    /// </summary>
    public partial class Ferrari_Formula_1_Team : UserControl
    {
        MainWindow wnd;

        Home_Bildschirm Homen = null;

        public Ferrari_Formula_1_Team(MainWindow mainwindow)
        {
            InitializeComponent();

            Homen = new Home_Bildschirm();

            wnd = mainwindow;

            FahrerCombo.Items.Add("Scuderia Ferrari Fahrer");
            FahrerCombo.Items.Add("Charles Leclerc");
            FahrerCombo.Items.Add("Carlos Sainz");

            FahrerCombo.SelectedIndex = 1;

            SF231.Visibility = Visibility.Visible;
            SF232.Visibility = Visibility.Collapsed;
            SF233.Visibility = Visibility.Collapsed;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            uhrzeit.Content = DateTime.Now.ToLongTimeString();
        }

        private void FahrerCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FahrerCombo.SelectedIndex == 1)
            {
                AnzahlRennenT.Text = "20";
                GesamtSiegeT.Text = "3";
                AnzahlPunkteT.Text = "120";
                PolePositionT.Text = "2";
                SchelleRundenT.Text = "2";

                Name.Text = "Charles Marc Hervé Perceval Leclerc";
                Nation.Text = "Monaco, Monte Carlo";
                fahrerstackbrief.Text = "Charles Leclerc ist ein talentierter Formel-1-Fahrer aus Monaco, der derzeit für das Ferrari-Team fährt. Er hat in " +
                "seiner Karriere bereits mehrere Podestplätze und Siege errungen und wird von vielen Experten als zukünftiger Weltmeister gehandelt.";
                charles.Visibility = Visibility.Visible;
                carlos.Visibility = Visibility.Collapsed;
                monaco.Visibility = Visibility.Visible;
                spanien.Visibility = Visibility.Collapsed;
                carloslogo.Visibility = Visibility.Collapsed;
                charleslogo.Visibility = Visibility.Visible;
                signcharles.Visibility = Visibility.Visible;
                signcarlos.Visibility = Visibility.Collapsed;
            }
            else if (FahrerCombo.SelectedIndex == 2)
            {
                AnzahlRennenT.Text = "50";
                GesamtSiegeT.Text = "1";
                AnzahlPunkteT.Text = "240";
                PolePositionT.Text = "0";
                SchelleRundenT.Text = "1";

                Name.Text = "Carlos Sainz Vázquez de Castro";
                Nation.Text = "Spanien, Madrid";
                fahrerstackbrief.Text = "Carlos Sainz ist ein spanischer Formel-1-Fahrer, der derzeit für das Ferrari-Team fährt. Er hat in seiner " +
                "Karriere mehrere Podestplätze und Siege erzielt und gilt als erfahrener und zuverlässiger Fahrer. Sainz ist bekannt für seine " +
                "Fähigkeit, sich schnell an neue Teams und Autos anzupassen.";
                carlos.Visibility = Visibility.Visible;
                charles.Visibility = Visibility.Collapsed;
                monaco.Visibility = Visibility.Collapsed;
                spanien.Visibility = Visibility.Visible;
                charleslogo.Visibility = Visibility.Collapsed;
                carloslogo.Visibility = Visibility.Visible;
                carloslogo.Visibility = Visibility.Visible;
                signcarlos.Visibility = Visibility.Visible;
                signcharles.Visibility = Visibility.Collapsed;
            }

            else
            {
                MessageBox.Show("Wähle ein Fahrer aus.", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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

        private void Hauptmenü_Click(object sender, RoutedEventArgs e)
        {
            MainWindow teamauswahl = new MainWindow();
            teamauswahl.Show();
        }

        private void rechtsklick_Click(object sender, RoutedEventArgs e)
        {
            if (SF231.Visibility == Visibility.Visible)
            {
                SF231.Visibility = Visibility.Collapsed;
                SF232.Visibility = Visibility.Visible;
                rechtsklick.Visibility = Visibility.Visible;
                linksklick.Visibility = Visibility.Visible;
            }
            else if (SF232.Visibility == Visibility.Visible)
            {
                SF232.Visibility = Visibility.Collapsed;
                SF233.Visibility = Visibility.Visible;
                rechtsklick.Visibility = Visibility.Collapsed;
                linksklick.Visibility = Visibility.Visible;
            }
            else if (SF233.Visibility == Visibility.Visible)
            {
                SF233.Visibility = Visibility.Visible;
                rechtsklick.Visibility = Visibility.Collapsed;
                linksklick.Visibility = Visibility.Visible;
            }
        }

        private void linksklick_Click(object sender, RoutedEventArgs e)
        {
            if (SF232.Visibility == Visibility.Visible)
            {
                SF232.Visibility = Visibility.Collapsed;
                SF231.Visibility = Visibility.Visible;
                rechtsklick.Visibility = Visibility.Visible;
                linksklick.Visibility = Visibility.Collapsed;
            }
            else if (SF233.Visibility == Visibility.Visible)
            {
                SF233.Visibility = Visibility.Collapsed;
                SF232.Visibility = Visibility.Visible;
                rechtsklick.Visibility = Visibility.Visible;
                linksklick.Visibility = Visibility.Visible;
            }
        }

        private void hauptmenü_Click(object sender, RoutedEventArgs e)
        {
            MainWindow hauptmenü = new MainWindow();
            hauptmenü.Show();
        }

        private void Code_Click(object sender, RoutedEventArgs e)
        {
            uhrzeit.Visibility = Visibility.Collapsed;
            Code.Visibility = Visibility.Collapsed;

            eins.Visibility = Visibility.Visible;
            zwei.Visibility = Visibility.Visible;
            drei.Visibility = Visibility.Visible;
            vier.Visibility = Visibility.Visible;
            fünf.Visibility = Visibility.Visible;
            sechs.Visibility = Visibility.Visible;
            sieben.Visibility = Visibility.Visible;
            acht.Visibility = Visibility.Visible;
            neun.Visibility = Visibility.Visible;
            NNull.Visibility = Visibility.Visible;
            richtig.Visibility = Visibility.Visible;
            sechsmaldienull.Visibility = Visibility.Visible;
        }

        string zahl;
        string richtigercode = "000000";
        int count = 0;

        private void eins_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "1";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "1";
            zahlen.Text = zahl;
        }

        private void zwei_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "2";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "2";
            zahlen.Text = zahl;
        }

        private void drei_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "3";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "3";
            zahlen.Text = zahl;
        }

        private void vier_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "4";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "4";
            zahlen.Text = zahl;
        }

        private void fünf_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "5";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "5";
            zahlen.Text = zahl;
        }

        private void sechs_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "6";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "6";
            zahlen.Text = zahl;
        }

        private void sieben_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "7";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "7";
            zahlen.Text = zahl;
        }

        private void acht_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "8";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "8";
            zahlen.Text = zahl;
        }

        private void neun_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "9";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "9";
            zahlen.Text = zahl;
        }

        private void NNull_Click(object sender, RoutedEventArgs e)
        {
            zahlen.Text = "0";
            count += 1;
            if (count >= 6)
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
                NNull.IsEnabled = false;
            }
            zahl = zahl + "0";
            zahlen.Text = zahl;


        }

        private void richtig_Click(object sender, RoutedEventArgs e)
        {
            if (zahlen.Text == "000000")
            {
                CTN.Content = Homen;
                eins.Visibility = Visibility.Collapsed;
                zwei.Visibility = Visibility.Collapsed;
                drei.Visibility = Visibility.Collapsed;
                vier.Visibility = Visibility.Collapsed;
                fünf.Visibility = Visibility.Collapsed;
                sechs.Visibility = Visibility.Collapsed;
                sieben.Visibility = Visibility.Collapsed;
                acht.Visibility = Visibility.Collapsed;
                neun.Visibility = Visibility.Collapsed;
                NNull.Visibility = Visibility.Collapsed;
                richtig.Visibility = Visibility.Collapsed;
                zahlen.Visibility = Visibility.Collapsed;
                code.Visibility = Visibility.Collapsed;
                sechsmaldienull.Visibility = Visibility.Collapsed;
            }
            else if (zahlen.Text != "000000" && count >= 6)
            {
                eins.IsEnabled = true;
                zwei.IsEnabled = true;
                drei.IsEnabled = true;
                vier.IsEnabled = true;
                fünf.IsEnabled = true;
                sechs.IsEnabled = true;
                sieben.IsEnabled = true;
                acht.IsEnabled = true;
                neun.IsEnabled = true;
                NNull.IsEnabled = true;
                zahlen.Text = zahl;
            }

            string eingegebenercode = zahlen.Text;

            if (eingegebenercode == richtigercode)
            {
                MessageBox.Show("Ferrari Handy entsperrt! Viel Spaß");
            }
            else
            {
                MessageBox.Show("Falscher Pin, Versuch nochmal!");
                zahlen.Text = "";
            }
        }
    }
}
