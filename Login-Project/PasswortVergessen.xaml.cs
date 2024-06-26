﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            TextBoxEmailEingabe.Focus();
        }

        private void ButtonBenutzerEmailSuchen_Click(object sender, RoutedEventArgs e)
        {
            string UserInput = TextBoxEmailEingabe.Text;

            User foundUser = Register.SearchEmailInDatabase(UserInput);

            if (foundUser != null)
            {
                labelNameEmail.Visibility = Visibility.Collapsed;
                TextBoxEmailEingabe.Visibility = Visibility.Collapsed;
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
                TextBoxEmailEingabe.Clear();
                TextBoxEmailEingabe.Focus();
            }
        }

        public void ButtonPasswort_Click(object sender, RoutedEventArgs e)
        {
            Passwort.ResetPassword(this, wnd);
        }

        private void TextBoxPasswort_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Passwort.PasswordCondition(this);
            Passwort.ButtonSearchEnabled(this);
        }

        private void TextBoxPasswortBestätigung_PasswordChanged(object sender, RoutedEventArgs e)
        {
            labelPasswortÜbereinstimmung.Visibility = Visibility.Visible;
            Passwort.PasswordCondition(this);
            Passwort.ButtonSearchEnabled(this);
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
            Passwort.ButtonSearchEnabled(this);
        }

        private void TextBoxBenutzerEingabe_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxEmailEingabe.Background = Brushes.LightGray;
        }

        private void TextBoxBenutzerEingabe_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxEmailEingabe.Background = Brushes.White;
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
