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
    /// Interaktionslogik für Administrator.xaml
    /// </summary>
    public partial class Administrator : UserControl
    {
        public static MainWindow wnd;

        public Administrator(MainWindow main)
        {
            InitializeComponent();

            wnd = main;

            BackendAdministrator.AdminLogin(this);
        }

        private void ButtonEinstellungen_Click(object sender, RoutedEventArgs e)
        {
            wnd.content.Content = new AdminOverview(wnd);
        }

        private void ButtonHauptmenü_Click(object sender, RoutedEventArgs e)
        {
            wnd.content.Content = wnd.login;
            wnd.login.ImageProfilbild.Visibility = Visibility.Visible;
            wnd.login.labelAktuellerBenutzer.Content = "Administrator";
        }
    }
}
