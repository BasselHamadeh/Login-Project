using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Login_Project
{
    /// <summary>
    /// Interaktionslogik für Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public static MainWindow wnd;

        public Register(MainWindow main)
        {
            InitializeComponent();
            wnd = main;

            labelPasswortÜbereinstimmung.Visibility = Visibility.Collapsed;
            labelBenutzerExistiert.Visibility = Visibility.Collapsed;
            labelEmailExistiert.Visibility = Visibility.Collapsed;
        }

        private void ButtonAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            wnd.content.Content = new Login(wnd);
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            Register.TryRegister(this, wnd);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxBenutzer.Focus();
        }

        private void TextBoxBenutzer_TextChanged(object sender, TextChangedEventArgs e)
        {
            Register.UserCondition(this);
            Register.ButtonRegsiterEnabled(this);
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Register.EmailCondition(this);
            Register.ButtonRegsiterEnabled(this);
        }

        private void TextBoxPasswort_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Register.PasswordCondition(this);
            Register.ButtonRegsiterEnabled(this);
        }

        private void TextBoxPasswortBestätigung_PasswordChanged(object sender, RoutedEventArgs e)
        {
            labelPasswortÜbereinstimmung.Visibility = Visibility.Visible;
            Register.PasswordCondition(this);
            Register.ButtonRegsiterEnabled(this);
        }

        private void ButtonRegister_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Register.ButtonRegsiterEnabled(this);

            if (ButtonRegister.IsEnabled)
            {
                ButtonRegister.Foreground = Brushes.White;
            }
            else
            {
                ButtonRegister.Foreground = Brushes.Black;
            }
        }

        private void TextBoxBenutzer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonRegister_Click(sender, e);
            }
        }

        private void TextBoxEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonRegister_Click(sender, e);
            }
        }

        private void TextBoxPasswort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonRegister_Click(sender, e);
            }
        }

        private void TextBoxPasswortBestätigung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonRegister_Click(sender, e);
            }
        }

        private void TextBoxBenutzer_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxBenutzer.Background = Brushes.LightGray;
        }

        private void TextBoxBenutzer_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxBenutzer.Background = Brushes.White;
        }

        private void TextBoxEmail_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBoxEmail.Background = Brushes.LightGray;
        }

        private void TextBoxEmail_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBoxEmail.Background = Brushes.White;
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
