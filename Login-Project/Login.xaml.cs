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
using System.Configuration;

namespace Login_Project
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public static MainWindow wnd;

        public Login(MainWindow main)
        {
            InitializeComponent();
            wnd = main;
        }

        private void ButtonAusgang_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            BackendLogin.TryLogin(this, wnd);
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            wnd.content.Content = new Register(wnd);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxBenutzerEmail.Focus();

            if (TextBoxBenutzerEmail.Text != "")
            {
                TextBoxPasswort.Focus();
            }
            else if (TextBoxBenutzerEmail.Text != "" && TextBoxPasswort.Password != "")
            {
                ButtonLogin.Focus();
            }
        }

        private void TextBoxPasswort_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ButtonLogin.IsEnabled = !string.IsNullOrEmpty(TextBoxBenutzerEmail.Text)
                                 && !string.IsNullOrEmpty(TextBoxPasswort.Password);
        }

        private void ButtonLogin_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ButtonLogin.IsEnabled)
            {
                ButtonLogin.Foreground = Brushes.White;
            }
            else
            {
                ButtonLogin.Foreground = Brushes.Black;
            }
        }

        private void labelPasswortVergessen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            wnd.content.Content = new ForgottenPassword(wnd);
        }

        private void TextBoxPasswort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonLogin_Click(sender, e);
            }
        }

        private void TextBoxBenutzerEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonLogin_Click(sender, e);
            }
        }

        private void TextBoxBenutzerEmail_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxBenutzerEmail.Background = Brushes.LightGray;
        }

        private void TextBoxBenutzerEmail_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxBenutzerEmail.Background = Brushes.White;
        }

        private void TextBoxPasswort_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxPasswort.Background = Brushes.LightGray;
        }

        private void TextBoxPasswort_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxPasswort.Background = Brushes.White;
        }
    }
}
