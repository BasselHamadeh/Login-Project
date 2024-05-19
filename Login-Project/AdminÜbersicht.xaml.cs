using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Login_Project
{
    /// <summary>
    /// Interaktionslogik für AdminÜbersicht.xaml
    /// </summary>
    public partial class AdminOverview : UserControl
    {
        public static MainWindow wnd;

        public AdminOverview(MainWindow main)
        {
            InitializeComponent();

            labelBenutzerExistiertNicht.Visibility = Visibility.Collapsed;

            wnd = main;
        }

        private void ButtonBenutzerSuche_Click(object sender, RoutedEventArgs e)
        {
            AdminUebersicht.UserSearch(this);
        }

        private void ButtonKontoLöschen_Click(object sender, RoutedEventArgs e)
        {
            AdminUebersicht.DeleteAccount(this);
        }

        private void ButtonHauptmenü_Click(object sender, RoutedEventArgs e)
        {
            wnd.content.Content = new Login(wnd);
        }

        private void ButtonÜbernehmen_Click(object sender, RoutedEventArgs e)
        {
            AdminUebersicht.NewUsername(this);
        }

        private void ButtonBenutzerInfo_Click(object sender, RoutedEventArgs e)
        {
            wnd.content.Content = wnd.userinformation;
            Benutzerinfo.UserInfo(this, wnd.userinformation);
        }

        private void ButtonÜbernehmenAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminUebersicht.PasswordAdmin(this);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxBenutzerEingabeSuche.Focus();
        }

        private void TextBoxBenutzerEingabeSuche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonBenutzerSuche_Click(sender, e);
            }
        }

        private void ButtonÜbernehmen_Click(object sender, EventArgs e)
        {
            AdminUebersicht.NewUsername(this);
        }

        private void TextBoxNeuerBenutzername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonÜbernehmen_Click(sender, e);
            }
        }

        private void TextBoxPasswortAdmin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonÜbernehmenAdmin_Click(sender, e);
            }
        }

        private void TextBoxBenutzerEingabeSuche_TextChanged(object sender, TextChangedEventArgs e)
        {
            AdminUebersicht.UserCondition(this);

            if (TextBoxBenutzerEingabeSuche.Text != "")
            {
                ButtonBenutzerSuche.IsHitTestVisible = true;
            }
        }

        private void ButtonAdminErnennung_Click(object sender, RoutedEventArgs e)
        {
            AdminUebersicht.AddAdmin(this);
        }

        private void ButtonAdminEntfernung_Click(object sender, RoutedEventArgs e)
        {
            AdminUebersicht.RemoveAdmin(this);
        }

        private void TextBoxBenutzerEingabeSuche_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxBenutzerEingabeSuche.Background = Brushes.LightGray;
        }

        private void TextBoxBenutzerEingabeSuche_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxBenutzerEingabeSuche.Background = Brushes.White;
        }

        private void TextBoxNeuerBenutzername_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxNeuerBenutzername.Background = Brushes.LightGray;
        }

        private void TextBoxNeuerBenutzername_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxNeuerBenutzername.Background = Brushes.White;
        }

        private void TextBoxPasswortAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxPasswortAdmin.Background = Brushes.LightGray;
        }

        private void TextBoxPasswortAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxPasswortAdmin.Background = Brushes.White;
        }
    }
}
