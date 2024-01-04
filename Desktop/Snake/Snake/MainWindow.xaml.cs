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

namespace Snake
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly Dictionary<BackendGrid, ImageSource> GridImage = new Dictionary<BackendGrid, ImageSource>
        {
            {BackendGrid.Empty, new BitmapImage(new Uri("/SnakeAssets/Empty.png", UriKind.Relative)) },
            {BackendGrid.Snake, new BitmapImage(new Uri("/SnakeAssets/Body.png", UriKind.Relative)) },
            {BackendGrid.Food, new BitmapImage(new Uri("/SnakeAssets/Food.png", UriKind.Relative)) }
        };

        public readonly Dictionary<BackendDirection, int> Rotation = new Dictionary<BackendDirection, int>()
        {
            {BackendDirection.Up, 0 },
            {BackendDirection.Right, 90 },
            {BackendDirection.Down, 180 },
            {BackendDirection.Left, 270 }

        };

        public readonly int rows = 15, column = 15;
        public readonly Image[,] gridImages;
        public BackendGameState game;
        public bool GameRunning;

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();

            game = new BackendGameState(rows, column);
        }

        private async Task RunGame()
        {
            Draw();
            await CountDown();
            OverLay.Visibility = Visibility.Hidden;
            await GameLoop();
            await GameOverShow();
            game = new BackendGameState(rows, column);
        }

        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (OverLay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            if (!GameRunning)
            {
                GameRunning = true;
                await RunGame();
                GameRunning = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (game.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    game.SnakeDirection(BackendDirection.Left);
                    break;

                case Key.Right:
                    game.SnakeDirection(BackendDirection.Right);
                    break;

                case Key.Up:
                    game.SnakeDirection(BackendDirection.Up);
                    break;

                case Key.Down:
                    game.SnakeDirection(BackendDirection.Down);
                    break;
            }
        }

        public async Task GameLoop()
        {
            while (!game.GameOver)
            {
                await Task.Delay(100);
                game.SnakeMove();
                Draw();
            }
            game.AddFood();
        }


        public Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, column];
            GameGrid.Rows = rows;
            GameGrid.Columns = column;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    Image image = new Image()
                    {
                        Source = new BitmapImage(new Uri("/SnakeAssets/Empty.png", UriKind.Relative)),
                        RenderTransformOrigin = new Point(0.5, 0.5)                    
                    };

                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }
            return images;
        }

        public void Draw()
        {
            DrawBoard();
            SnakeHeadDraw();
            TextBlockScore.Text = $"Score: {game.Score}";
        }

        public void DrawBoard()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    BackendGrid grid = game.Grid[i, j];
                    gridImages[i, j].Source = GridImage[grid];
                    gridImages[i, j].RenderTransform = Transform.Identity;

                    if (grid == BackendGrid.Food)
                    {
                        gridImages[i, j].Source = new BitmapImage(new Uri("/SnakeAssets/Food.png", UriKind.Relative));
                    }
                }
            }
        }


        public async Task CountDown()
        {
            for (int i = 3; i >= 1; i--)
            {
                TextBlockOverlay.Text = i.ToString();
                await Task.Delay(500);
            }
        }

        public async Task GameOverShow()
        {
            await Task.Delay(1000);
            OverLay.Visibility = Visibility.Visible;
            TextBlockOverlay.Text = "Drücke eine Taste um zu Starten . . .";
        }

        public void SnakeHeadDraw()
        {
            BackendPosition head = game.PositionSnakeHead();
            Image image = gridImages[head.Row, head.Column];
            image.Source = new BitmapImage(new Uri("/SnakeAssets/Head.png", UriKind.Relative));

            int rotation = Rotation[game.Direction];
            image.RenderTransform = new RotateTransform(rotation);
        }
    }
}
