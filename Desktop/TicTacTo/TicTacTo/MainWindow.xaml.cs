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
using System.Windows.Media.Animation;

namespace TicTacTo
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public readonly Dictionary<BackendPlayer, ImageSource> ImageSource = new Dictionary<BackendPlayer, ImageSource>()
        {
            { BackendPlayer.X, new BitmapImage(new Uri("pack://application:,,,/Assets/X15.png"))},
            { BackendPlayer.O, new BitmapImage(new Uri("pack://application:,,,/Assets/O15.png"))}
        };

        public readonly Dictionary<BackendPlayer, ObjectAnimationUsingKeyFrames> animation = new Dictionary<BackendPlayer, ObjectAnimationUsingKeyFrames>
        {
            { BackendPlayer.X, new ObjectAnimationUsingKeyFrames() },
            { BackendPlayer.O, new ObjectAnimationUsingKeyFrames()}
        };

        public readonly DoubleAnimation fadeOut = new DoubleAnimation()
        {
            Duration = TimeSpan.FromSeconds(1.5),
            From = 1,
            To = 0
        };

        public readonly DoubleAnimation fadeIn = new DoubleAnimation()
        {
            Duration = TimeSpan.FromSeconds(1.5),
            From = 0,
            To = 1
        };

        public readonly Image[,] imageControl = new Image[3, 3];
        public readonly BackendGameState game = new BackendGameState();
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            SetupGrid();
            SetupAnimation();

            game.MoveMade += MoveMade;
            game.GameEnded += GameEnded;
            game.GameRestarted += GameRestart;
        }

        public void SetupAnimation()
        {
            animation[BackendPlayer.X].Duration = TimeSpan.FromSeconds(7);
            animation[BackendPlayer.O].Duration = TimeSpan.FromSeconds(7);

            for (int i = 0; i < 16; i++)
            {
                Uri xUrl = new Uri($"pack://application:,,,/Assets/X{i}.png");
                BitmapImage xImg = new BitmapImage(xUrl);
                DiscreteObjectKeyFrame xKeyFrame = new DiscreteObjectKeyFrame(xImg);
                animation[BackendPlayer.X].KeyFrames.Add(xKeyFrame);

                Uri oUrl = new Uri($"pack://application:,,,/Assets/O{i}.png");
                BitmapImage oImg = new BitmapImage(oUrl);
                DiscreteObjectKeyFrame oKeyFrame = new DiscreteObjectKeyFrame(oImg);
                animation[BackendPlayer.O].KeyFrames.Add(oKeyFrame);
            }
        }

        public async Task FadeOut(UIElement element)
        {
            element.BeginAnimation(OpacityProperty, fadeOut);
            await Task.Delay(fadeOut.Duration.TimeSpan);
            element.Visibility = Visibility.Hidden;
        }

        public async Task FadeIn(UIElement element)
        {
            element.Visibility = Visibility.Visible;
            element.BeginAnimation(OpacityProperty, fadeIn);
            await Task.Delay(fadeIn.Duration.TimeSpan);
        }

        public void SetupGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Image image = new Image();
                    GameGrid.Children.Add(image);
                    imageControl[i, j] = image;
                }
            }
        }

        public async void TransitionEndScreen(string text, ImageSource winner)
        {
            await Task.WhenAll(FadeOut(TurnStack), FadeOut(GameCanvas));
            Result.Text = text;
            Winner.Source = winner;
            await FadeIn(EndScreen);
        }

        public void MoveMade(int r, int c)
        {
            if (game.MakeMove(r, c))
            {
                BackendPlayer player = game.GameGrid[r, c];
                imageControl[r, c].Source = ImageSource[player];
                PlayerImage.Source = ImageSource[game.CurrentPlayer];
            }
        }

        public async void TransitionToGameScreen()
        {
            await FadeOut(EndScreen);
            Line.Visibility = Visibility.Hidden;
            await Task.WhenAll(FadeIn(TurnStack), FadeIn(GameCanvas));
        }

        public async void GameEnded(BackendGameResult game)
        {
            if (game.Winner == BackendPlayer.None)
            {
                TransitionEndScreen("Unentschieden", null);
            }
            else
            {
                ShowLine(game.BackendInfo);
                TransitionEndScreen("Winner: ", ImageSource[game.Winner]);
                await Task.Delay(1000);
            }
        }

        public void GameRestart()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    imageControl[i, j].BeginAnimation(Image.SourceProperty, null);
                    imageControl[i, j].Source = null;
                }
            }

            PlayerImage.Source = ImageSource[game.CurrentPlayer];
            TransitionToGameScreen();
        }

        public (Point, Point) LinePoint(BackendInfo info)
        {
            double size = GameGrid.Width / 3;
            double margin = size / 2;

            if (info.Type == BackendWin.Row)
            {
                double y = info.Number * size + margin;
                return (new Point(0, y), new Point(GameGrid.Width, y));
            }

            if (info.Type == BackendWin.Column)
            {
                double x = info.Number * size + margin;
                return (new Point(x, 0), new Point(x, GameGrid.Width));
            }

            if (info.Type == BackendWin.MainDiaginal)
            {
                double m = info.Number * size + margin;
                return (new Point(0, 0), new Point(GameGrid.Width, GameGrid.Height));
            }

            return (new Point(GameGrid.Width, 0), new Point(0, GameGrid.Height));
        }

        public void ShowLine(BackendInfo info)
        {
            (Point start, Point end) = LinePoint(info);

            Line.X1 = start.X;
            Line.Y1 = start.Y;

            Line.X2 = end.X;
            Line.Y2 = end.Y;

            Line.Visibility = Visibility.Visible;
        }

        private void GameGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (game.GameOver)
            {
                return;
            }

            double size = GameGrid.ActualWidth / 3;
            Point clickPos = e.GetPosition(GameGrid);
            int row = (int)(clickPos.Y / size);
            int column = (int)(clickPos.X / size);

            MoveMade(row, column);
        }


        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            game.ResetGame();
        }
    }
}
