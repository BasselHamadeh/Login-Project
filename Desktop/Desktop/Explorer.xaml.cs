using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

namespace Desktop
{
    /// <summary>
    /// Interaktionslogik für Explorer.xaml
    /// </summary>
    public partial class Explorer : UserControl
    {
        public static MainWindow wnd;

        public static bool ExplorerOpen = false;

        public Explorer(MainWindow main)
        {
            InitializeComponent();

            wnd = main;

            BackendExplorer.Laufwerke(this);
        }

        private void minimeren_Click(object sender, RoutedEventArgs e)
        {
            if (ExplorerOpen)
            {
                ExplorerOpen = false;
                wnd.contentEX.Visibility = Visibility.Collapsed;
                wnd.content.Visibility = Visibility.Visible;

            }
            else
            {
                ExplorerOpen = true;
                wnd.contentEX.Visibility = Visibility.Visible;
            }
        }

        private void maximieren_Click(object sender, RoutedEventArgs e)
        {

        }

        private void schließen_Click(object sender, RoutedEventArgs e)
        {
            if (ExplorerOpen)
            {
                ExplorerOpen = false;
                wnd.contentEX.Visibility = Visibility.Collapsed;
                wnd.content.Visibility = Visibility.Visible;

            }
            else
            {
                ExplorerOpen = true;
                wnd.contentEX.Visibility = Visibility.Visible;
            }
        }
    }
}
