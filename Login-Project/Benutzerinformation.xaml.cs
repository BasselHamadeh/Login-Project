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
    /// Interaktionslogik für Benutzerinformation.xaml
    /// </summary>
    public partial class UserInformation : UserControl
    {
        public static MainWindow wnd;

        public UserInformation(MainWindow main)
        {
            InitializeComponent();
        
            wnd = main;

            BackendBenutzerinfo.UserInfo(wnd.adminoverview, this);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            wnd.content.Content = new AdminOverview(wnd);
        }

        private void ButtonHauptmenü_Click(object sender, RoutedEventArgs e)
        {
            wnd.content.Content = new Login(wnd);
        }
    }
}
