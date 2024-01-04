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
    /// Interaction logic for Red_Bull_Englisch.xaml
    /// </summary>
    public partial class Red_Bull_Englisch : UserControl
    {

        MainWindow wnd;

        public Red_Bull_Englisch(MainWindow mainwindow)
        {
            InitializeComponent();

            wnd = mainwindow;
        }

        private void Hauptmenü_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
