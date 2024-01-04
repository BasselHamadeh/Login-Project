using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Desktop
{
    /// <summary>
    /// Interaktionslogik für DesktopOberfläche.xaml
    /// </summary>
    public partial class DesktopOberfläche : UserControl
    {
        public static MainWindow wnd;

        public static bool isStartMenuOpen = false;

        public DesktopOberfläche(MainWindow main)
        {
            InitializeComponent();

            wnd = main;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            labelZeit.Content = DateTime.Now.ToLongTimeString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer Date = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (object s, EventArgs ev) =>
            {
                this.Datum.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }, this.Dispatcher);
            Date.Start();
        }

        private void ButtonExplorer_Click(object sender, RoutedEventArgs e)
        {
            Explorer ex = new Explorer(wnd);
            wnd.contentEX.Content = ex;
        }

        private void TextBoxSuchbegriff_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
        }

        private void TextBoxSuchbegriff_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "  🔍   Suchbegriff hier eingeben";
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            //if (isStartMenuOpen)
            //{
            //    isStartMenuOpen = false;
            //    //Start.Visibility = Visibility.Collapsed;
            //    //Line1.Visibility = Visibility.Collapsed;
            //    //Line2.Visibility = Visibility.Collapsed;
            //    //Line3.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    isStartMenuOpen = true;
            //    //Start.Visibility = Visibility.Visible;
            //    //Line1.Visibility = Visibility.Visible;
            //    //Line2.Visibility = Visibility.Visible;
            //    //Line3.Visibility = Visibility.Visible;
            //}
        }

        private void ButtonSnake_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Snake = "C:\\Desktop/Snake/Snake/bin/Debug/Snake.exe";

                Process.Start(new ProcessStartInfo(Snake));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Öffnen der Website: " + ex.Message);
            }
        }

        private void ButtonTicTacToe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string TicTacToe = "C:\\Desktop/TicTacTo/TicTacTo/bin/Debug/TicTacTo.exe";

                Process.Start(new ProcessStartInfo(TicTacToe));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Öffnen der Website: " + ex.Message);
            }
        }

        private void ButtonTetris_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Tetris = "C:\\Desktop/Tetris/Tetris/bin/Debug/Tetris.exe";
                Process process = new Process();
                process.StartInfo.FileName = Tetris;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Öffnen der Website: " + ex.Message);
            }
        }
    }
}
