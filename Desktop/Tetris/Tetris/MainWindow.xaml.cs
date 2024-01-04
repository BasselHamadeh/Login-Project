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

namespace Tetris
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Tileblue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };

        public readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
        };

        public readonly Image[,] imageControl;
        public readonly int MaxDelay = 1000;
        public readonly int MinDelay = 75;
        public readonly int delayDecrease = 25;
        public BackendGameState gamestate = new BackendGameState();

        public MainWindow()
        {
            InitializeComponent();
            imageControl = SetupGameCanvas(gamestate.gamegrid);
        }

        public Image[,] SetupGameCanvas(BackendGameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Column];
            int cellSize = 25;

            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Column; j++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };

                    Canvas.SetTop(imageControl, (i - 2) * cellSize);
                    Canvas.SetLeft(imageControl, j * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[i, j] = imageControl;
                }
            }

            return imageControls;
        }

        public void DrawGrid(BackendGameGrid grid)
        {
            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Column; j++)
                {
                    int id = grid.grid[i, j];
                    imageControl[i, j].Opacity = 0.99;
                    imageControl[i, j].Source = tileImages[id];
                }
            }
        }

        public void DrawBlock(BackendBlock block)
        {
            foreach (BackendPosition p in block.TilePosition())
            {
                imageControl[p.Row, p.Column].Opacity = 0.97;
                imageControl[p.Row, p.Column].Source = tileImages[block.ID];
            }
        }

        public void Draw(BackendGameState game)
        {
            DrawGrid(game.gamegrid);
            DrawBlock(game.CurrentBlock);
            DrawGhostBlock(gamestate.CurrentBlock);
            DrawNextBoard(gamestate.blockqueue);
            TextBlockScore.Text = $"Score: {gamestate.Score}";
            DrawHeldBlock(gamestate.HeldBlock);
        }

        public void DrawGhostBlock(BackendBlock block)
        {
            int drop = gamestate.BlockDrop();

            foreach (BackendPosition p in block.TilePosition())
            {
                imageControl[p.Row + drop, p.Column].Opacity = 0.24;
                imageControl[p.Row + drop, p.Column].Source = tileImages[block.ID];
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gamestate.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gamestate.MoveBlockLeft();
                    break;
                case Key.Right:
                    gamestate.MoveBlockRight();
                    break;
                case Key.Down:
                    gamestate.MoveBlockDown();
                    break;
                case Key.Up:
                    gamestate.RotateBlockCW();
                    break;
                case Key.Z:
                    gamestate.RotateBlockCCW();
                    break;
                case Key.C:
                    gamestate.HoldBlock();
                    break;
                case Key.Space:
                    gamestate.DropBlock();
                    break;
                default:
                    return;
            }

            Draw(gamestate);
        }

        public void DrawHeldBlock(BackendBlock block)
        {
            if (block == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[block.ID];
            }
        }

        public async Task GameLoop()
        {
            Draw(gamestate);

            while (!gamestate.GameOver)
            {
                int delay = Math.Max(MinDelay, MaxDelay - (gamestate.Score * delayDecrease));
                await Task.Delay(delay);
                gamestate.MoveBlockDown();
                Draw(gamestate);
            }

            GameOverGrid.Visibility = Visibility.Visible;
            FinalResult.Text = $"Score: {gamestate.Score}";
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void ButtonPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gamestate = new BackendGameState();
            GameOverGrid.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        public void DrawNextBoard(BackendBlockQueue block)
        {
            BackendBlock next = block.NextBlock;
            NextImage.Source = blockImages[next.ID];
        }
    }
}
