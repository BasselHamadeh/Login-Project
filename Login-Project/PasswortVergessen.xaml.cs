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

namespace Login_Project
{
    /// <summary>
    /// Interaktionslogik für PasswortVergessen.xaml
    /// </summary>
    public partial class ForgottenPassword : UserControl
    {
        public static MainWindow wnd;

        public ForgottenPassword(MainWindow main)
        {
            InitializeComponent();

            wnd = main;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxBenutzerEingabe.Focus();
        }

        private void ButtonBenutzerEmailSuchen_Click(object sender, RoutedEventArgs e)
        {
            string UserInput = TextBoxBenutzerEingabe.Text;

            string password = BackendPasswort.ReadDataCSV(UserInput);

            if (!string.IsNullOrEmpty(password))
            {
                labelNameEmail.Visibility = Visibility.Collapsed;
                TextBoxBenutzerEingabe.Visibility = Visibility.Collapsed;
                ButtonBenutzerEmailSuchen.Visibility = Visibility.Collapsed;
                ButtonHauptmenü.Visibility = Visibility.Collapsed;

                labelPasswort.Visibility = Visibility.Visible;
                labelPasswortBestätigung.Visibility = Visibility.Visible;
                TextBoxPasswort.Visibility = Visibility.Visible;
                TextBoxPasswortBestätigung.Visibility = Visibility.Visible;
                labelPasswortBedingung.Visibility = Visibility.Visible;
                labelGroßbuchstaben.Visibility = Visibility.Visible;
                labelKleinbuchstaben.Visibility = Visibility.Visible;
                labelZahlen.Visibility = Visibility.Visible;
                labelLänge.Visibility = Visibility.Visible;
                labelSonderzeichen.Visibility = Visibility.Visible;
                ButtonPasswort.Visibility = Visibility.Visible;

                TextBoxPasswort.Focus();
            }
            else
            {
                MessageBox.Show("Ungültige E-Mail-Adresse.");
                TextBoxBenutzerEingabe.Clear();
                TextBoxBenutzerEingabe.Focus();
            }
        }

        public void ButtonPasswort_Click(object sender, RoutedEventArgs e)
        {
            BackendPasswort.ResetPassword(this, wnd);
        }

        private void TextBoxPasswort_PasswordChanged(object sender, RoutedEventArgs e)
        {
            BackendPasswort.PasswordCondition(this);
            BackendPasswort.ButtonSearchEnabled(this);
        }

        private void TextBoxPasswortBestätigung_PasswordChanged(object sender, RoutedEventArgs e)
        {
            labelPasswortÜbereinstimmung.Visibility = Visibility.Visible;
            BackendPasswort.PasswordCondition(this);
            BackendPasswort.ButtonSearchEnabled(this);
        }

        private void ButtonPasswort_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ButtonPasswort.IsEnabled)
            {
                ButtonPasswort.Foreground = Brushes.White;
            }
            else
            {
                ButtonPasswort.Foreground = Brushes.Black;
            }
        }

        private void ButtonHauptmenü_Click(object sender, RoutedEventArgs e)
        {
            wnd.content.Content = new Login(wnd);
        }

        private void TextBoxBenutzerEingabe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonBenutzerEmailSuchen_Click(sender, e);
            }
        }

        private void TextBoxPasswortBestätigung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonPasswort_Click(sender, e);
            }
        }

        private void TextBoxPasswort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonPasswort_Click(sender, e);
            }
        }

        private void TextBoxBenutzerEingabe_TextChanged(object sender, TextChangedEventArgs e)
        {
            BackendPasswort.ButtonSearchEnabled(this);
        }

        private void TextBoxBenutzerEingabe_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxBenutzerEingabe.Background = Brushes.LightGray;
        }

        private void TextBoxBenutzerEingabe_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxBenutzerEingabe.Background = Brushes.White;
        }

        private void TextBoxPasswort_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxPasswort.Background = Brushes.LightGray;
        }

        private void TextBoxPasswort_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxPasswort.Background = Brushes.White;
        }

        private void TextBoxPasswortBestätigung_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxPasswortBestätigung.Background = Brushes.LightGray;
        }

        private void TextBoxPasswortBestätigung_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxPasswortBestätigung.Background = Brushes.White;
        }
    }
}
