using System.Windows;
using System.Windows.Controls;

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

            Benutzerinfo.UserInfo(wnd.adminoverview, this);
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
