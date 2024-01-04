using Microsoft.Win32;
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
    /// Interaction logic for mclarenquiz.xaml
    /// </summary>
    public partial class mclarenquiz : UserControl
    {
        public mclarenquiz()
        {
            InitializeComponent();

        }

        int punkte;

        private void auswertung_Click(object sender, RoutedEventArgs e)
        {
            if (f1a2.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung1.Text = "Richtig!";
                wertung1.Foreground = Brushes.DarkGreen;
                wertung1.FontSize = 17;
                f1a1.IsEnabled = false;
                f1a3.IsEnabled = false;
            }
            else if (f1a1.IsChecked == true)
            {
                wertung1.FontSize = 17;
                f1a2.IsEnabled = false;
                f1a3.IsEnabled = false;
                wertung1.Text = "Falsch! Es ist 8.";
                wertung1.Foreground = Brushes.Red;
            }
            else if (f1a3.IsChecked == true)
            {
                wertung1.FontSize = 17;
                f1a1.IsEnabled = false;
                f1a2.IsEnabled = false;
                wertung1.Text = "Falsch! Es ist 8.";
                wertung1.Foreground = Brushes.Red;
            }

            if (f2a3.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung2.Text = "Richtig!";
                wertung2.Foreground = Brushes.DarkGreen;
                wertung2.FontSize = 17;
                f2a1.IsEnabled = false;
                f2a2.IsEnabled = false;
            }
            else if (f2a1.IsChecked == true)
            {
                wertung2.FontSize = 17;
                f2a2.IsEnabled = false;
                f2a3.IsEnabled = false;
                wertung2.Text = "Falsch! Es ist Zak Brown.";
                wertung2.Foreground = Brushes.Red;
            }
            else if (f2a2.IsChecked == true)
            {
                wertung2.FontSize = 17;
                f2a1.IsEnabled = false;
                f2a3.IsEnabled = false;
                wertung2.Text = "Falsch! Es ist Zak Brown.";
                wertung2.Foreground = Brushes.Red;
            }

            if (f3a1.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung3.Text = "Richtig!";
                wertung3.Foreground = Brushes.DarkGreen;
                wertung3.FontSize = 17;
                f3a2.IsEnabled = false;
                f3a3.IsEnabled = false;
            }
            else if (f3a2.IsChecked == true)
            {
                wertung3.FontSize = 17;
                f3a1.IsEnabled = false;
                f3a3.IsEnabled = false;
                wertung3.Text = "Falsch! Es war Ayrton Senna.";
                wertung3.Foreground = Brushes.Red;
            }
            else if (f3a3.IsChecked == true)
            {
                wertung3.FontSize = 17;
                f3a1.IsEnabled = false;
                f3a2.IsEnabled = false;
                wertung3.Text = "Falsch! Es war Ayrton Senna.";
                wertung3.Foreground = Brushes.Red;
            }

            if (f4a2.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung4.Text = "Richtig!";
                wertung4.Foreground = Brushes.DarkGreen;
                wertung4.FontSize = 17;
                f4a1.IsEnabled = false;
                f4a3.IsEnabled = false;
            }
            else if (f4a1.IsChecked == true)
            {
                wertung4.FontSize = 17;
                f4a2.IsEnabled = false;
                f4a3.IsEnabled = false;
                wertung4.Text = "Falsch! Es war 1974.";
                wertung4.Foreground = Brushes.Red;
            }
            else if (f4a3.IsChecked == true)
            {
                wertung4.FontSize = 17;
                f4a1.IsEnabled = false;
                f4a2.IsEnabled = false;
                wertung4.Text = "Falsch! Es war 1974.";
                wertung4.Foreground = Brushes.Red;
            }

            if (f5a1.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung5.Text = "Richtig!";
                wertung5.Foreground = Brushes.DarkGreen;
                wertung5.FontSize = 17;
                f5a2.IsEnabled = false;
                f5a3.IsEnabled = false;
            }
            else if (f5a2.IsChecked == true)
            {
                wertung5.FontSize = 17;
                f5a1.IsEnabled = false;
                f5a3.IsEnabled = false;
                wertung5.Text = "Falsch! Es war Lewis Hamilton.";
                wertung5.Foreground = Brushes.Red;
            }
            else if (f5a3.IsChecked == true)
            {
                wertung5.FontSize = 17;
                f5a1.IsEnabled = false;
                f5a2.IsEnabled = false;
                wertung5.Text = "Falsch! Es war Lewis Hamilton.";
                wertung5.Foreground = Brushes.Red;
            }

            if (f6a3.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung6.Text = "Richtig!";
                wertung6.Foreground = Brushes.DarkGreen;
                wertung6.FontSize = 17;
                f6a1.IsEnabled = false;
                f6a2.IsEnabled = false;
            }
            else if (f6a2.IsChecked == true)
            {
                wertung6.FontSize = 17;
                f6a1.IsEnabled = false;
                f6a3.IsEnabled = false;
                wertung6.Text = "Falsch! Es sind 6 Podien.";
                wertung6.Foreground = Brushes.Red;
            }
            else if (f6a1.IsChecked == true)
            {
                wertung6.FontSize = 17;
                f6a2.IsEnabled = false;
                f6a3.IsEnabled = false;
                wertung6.Text = "Falsch! Es sind 6 Podien.";
                wertung6.Foreground = Brushes.Red;
            }

            if (f7a3.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung7.Text = "Richtig!";
                wertung7.Foreground = Brushes.DarkGreen;
                wertung7.FontSize = 17;
                f7a1.IsEnabled = false;
                f7a2.IsEnabled = false;
            }
            else if (f7a2.IsChecked == true)
            {
                wertung7.FontSize = 17;
                f7a1.IsEnabled = false;
                f7a3.IsEnabled = false;
                wertung7.Text = "Falsch! Es sind 235 Siege.";
                wertung7.Foreground = Brushes.Red;
            }
            else if (f7a1.IsChecked == true)
            {
                wertung7.FontSize = 17;
                f7a2.IsEnabled = false;
                f7a3.IsEnabled = false;
                wertung7.Text = "Falsch! Es sind 235 Siege.";
                wertung7.Foreground = Brushes.Red;
            }

            if (f8a3.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung8.Text = "Richtig!";
                wertung8.Foreground = Brushes.DarkGreen;
                wertung8.FontSize = 17;
                f8a1.IsEnabled = false;
                f8a2.IsEnabled = false;
            }
            else if (f8a2.IsChecked == true)
            {
                wertung8.FontSize = 17;
                f8a1.IsEnabled = false;
                f8a3.IsEnabled = false;
                wertung8.Text = "Falsch! Es sind Lando Norris und Oscar Piastri.";
                wertung8.Foreground = Brushes.Red;
            }
            else if (f8a1.IsChecked == true)
            {
                wertung8.FontSize = 17;
                f8a2.IsEnabled = false;
                f8a3.IsEnabled = false;
                wertung8.Text = "Falsch! Es sind Lando Norris und Oscar Piastri.";
                wertung8.Foreground = Brushes.Red;
            }

            if (f9a2.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung9.Text = "Richtig!";
                wertung9.Foreground = Brushes.DarkGreen;
                wertung9.FontSize = 17;
                f9a1.IsEnabled = false;
                f9a3.IsEnabled = false;
            }
            else if (f9a1.IsChecked == true)
            {
                wertung9.FontSize = 17;
                f9a2.IsEnabled = false;
                f9a3.IsEnabled = false;
                wertung9.Text = "Falsch! Unser Motorenpartner ist Mercedes.";
                wertung9.Foreground = Brushes.Red;
            }
            else if (f9a3.IsChecked == true)
            {
                wertung9.FontSize = 17;
                f9a1.IsEnabled = false;
                f9a2.IsEnabled = false;
                wertung9.Text = "Falsch! Unser Motorenpartner ist Mercedes.";
                wertung9.Foreground = Brushes.Red;
            }

            if (f10a1.IsChecked == true)
            {
                punkte++;
                ergebnis.Text = $"Punkte: {punkte} / 10";
                wertung10.Text = "Richtig!";
                wertung10.Foreground = Brushes.DarkGreen;
                wertung10.FontSize = 17;
                f10a2.IsEnabled = false;
                f10a3.IsEnabled = false;
            }
            else if (f10a2.IsChecked == true)
            {
                f10a1.IsEnabled = false;
                f10a3.IsEnabled = false;
                wertung10.FontSize = 17;
                wertung10.Text = "Falsch! Es ist MCL60.";
                wertung10.Foreground = Brushes.Red;
            }
            else if (f10a3.IsChecked == true)
            {
                wertung10.FontSize = 17;
                f10a1.IsEnabled = false;
                f10a2.IsEnabled = false;
                wertung10.Text = "Falsch! Es ist MCL60.";
                wertung10.Foreground = Brushes.Red;
            }

            if (punkte == 0)
            {
                ergebnis.Text = $"Punkte: {punkte} / 10";
                anzahlpkt.Text = "Miserable Leistung       😭";
                anzahlpkt.FontSize = 25;
                anzahlpkt.Foreground = Brushes.DarkRed;
                traurigesGesicht.Visibility = Visibility.Visible;

                Ellipse gesicht = new Ellipse
                {
                    Width = 140,
                    Height = 140,
                    Fill = Brushes.DarkRed,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };

                Ellipse linkesAuge = new Ellipse
                {
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.Black
                };

                Ellipse rechtesAuge = new Ellipse
                {
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.Black
                };

                Line mund = new Line
                {

                    X1 = 30,
                    Y1 = 80,
                    X2 = 110,
                    Y2 = 100,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(linkesAuge, 30);
                Canvas.SetTop(linkesAuge, 30);

                Canvas.SetLeft(rechtesAuge, 80);
                Canvas.SetTop(rechtesAuge, 30);

                traurigesGesicht.Children.Add(gesicht);
                traurigesGesicht.Children.Add(linkesAuge);
                traurigesGesicht.Children.Add(rechtesAuge);
                traurigesGesicht.Children.Add(mund);
            }

            if (punkte < 5 && punkte >= 1)
            {
                ergebnis.Text = $"Punkte: {punkte} / 10";
                anzahlpkt.Text = "Naja, geht besser.     🙃";
                anzahlpkt.FontSize = 25;
                anzahlpkt.Foreground = Brushes.Black;
                normalesGesicht.Visibility = Visibility.Visible;

                Ellipse gesicht = new Ellipse
                {
                    Width = 140,
                    Height = 140,
                    Fill = Brushes.LightGoldenrodYellow,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };

                Ellipse linkesAuge = new Ellipse
                {
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.Black
                };

                Ellipse rechtesAuge = new Ellipse
                {
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.Black
                };

                Canvas.SetLeft(linkesAuge, 30);
                Canvas.SetTop(linkesAuge, 30);

                Canvas.SetLeft(rechtesAuge, 80);
                Canvas.SetTop(rechtesAuge, 30);

                Line mund = new Line();
                mund.X1 = 30;
                mund.Y1 = 90;
                mund.X2 = 90;
                mund.Y2 = 100;
                mund.Stroke = Brushes.Black;
                mund.StrokeThickness = 5;

                normalesGesicht.Children.Add(gesicht);
                normalesGesicht.Children.Add(linkesAuge);
                normalesGesicht.Children.Add(rechtesAuge);
                normalesGesicht.Children.Add(mund);

            }

            if (punkte >= 5 && punkte <= 8)
            {
                ergebnis.Text = $"Punkte: {punkte} / 10";
                anzahlpkt.Text = "Gut gemacht.      👍";
                anzahlpkt.FontSize = 25;
                anzahlpkt.Foreground = Brushes.Green;
                normalesGesicht2.Visibility = Visibility.Visible;
                gut1.Visibility = Visibility.Visible;
                gut2.Visibility = Visibility.Visible;

                Ellipse gesicht = new Ellipse
                {
                    Width = 140,
                    Height = 140,
                    Fill = Brushes.Yellow,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };

                Ellipse linkesAuge = new Ellipse
                {
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.Black
                };

                Ellipse rechtesAuge = new Ellipse
                {
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.Black
                };

                Canvas.SetLeft(linkesAuge, 30);
                Canvas.SetTop(linkesAuge, 30);

                Canvas.SetLeft(rechtesAuge, 80);
                Canvas.SetTop(rechtesAuge, 30);

                Line mund = new Line();
                mund.X1 = 40;
                mund.Y1 = 100;
                mund.X2 = 100;
                mund.Y2 = 100;
                mund.Stroke = Brushes.Black;
                mund.StrokeThickness = 5;

                normalesGesicht2.Children.Add(gesicht);
                normalesGesicht2.Children.Add(linkesAuge);
                normalesGesicht2.Children.Add(rechtesAuge);
                normalesGesicht2.Children.Add(mund);
            }

            if (punkte == 10)
            {
                ergebnis.Text = $"Punkte: {punkte} / 10";
                anzahlpkt.Text = "Volle Punkte, Tolle Leistung!";
                anzahlpkt.FontSize = 25;
                anzahlpkt.Foreground = Brushes.DarkGreen;
                lachendesGesicht.Visibility = Visibility.Visible;
                wow.Visibility = Visibility.Visible;

                Ellipse gesicht = new Ellipse
                {
                    Width = 140,
                    Height = 140,
                    Fill = Brushes.Yellow,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };

                Ellipse linkesAuge = new Ellipse
                {
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.Black
                };

                Ellipse rechtesAuge = new Ellipse
                {
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.Black
                };

                Canvas.SetLeft(linkesAuge, 30);
                Canvas.SetTop(linkesAuge, 30);

                Canvas.SetLeft(rechtesAuge, 80);
                Canvas.SetTop(rechtesAuge, 30);

                lachendesGesicht.Children.Add(gesicht);
                lachendesGesicht.Children.Add(linkesAuge);
                lachendesGesicht.Children.Add(rechtesAuge);

            }


            auswertung.Background = Brushes.Black;
            auswertung.Foreground = Brushes.White;
            auswertung.Content = "Die Auswertung.";
            auswertung.IsHitTestVisible = false;

        }
    }
}

