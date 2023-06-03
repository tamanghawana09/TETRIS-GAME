using System;
using System.Collections.Generic;
using System.Dynamic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative)),
        };
        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-0.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png",UriKind.Relative))
        };
        private readonly Image[,] imageControls;
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 25;
        private gameState gs = new gameState();
        public MainWindow()
        {
            InitializeComponent();
            imageControls = setUpGameCanvas(gs.grid);
        }

        private void drawNextBlock(blockQueue bq)
        {
            block next = bq.nextBlock;
            NextImage.Source = blockImages[next.Id];
        }

        private Image[,] setUpGameCanvas(gameGrid grid)
        {
            Image[,] imageControls = new Image[grid.rows, grid.columns];
            int cellSize = 25;
            for (int r = 0; r < grid.rows; r++)
            {
                for (int c = 0; c < grid.columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    gameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;

                }
            }
            return imageControls;
        }

        private void DrawGrid(gameGrid grid)
        {
            for (int r = 0; r < grid.rows; r++)
            {
                for (int c = 0; c < grid.columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }
        private void drawBlock(block b)
        {
            foreach (position i in b.TilePositions())
            {
                imageControls[i.row, i.column].Opacity = 1;
                imageControls[i.row, i.column].Source = tileImages[b.Id];
            }
        }
        private void draw(gameState gs)
        {
            DrawGrid(gs.grid);
            drawGhostBlock(gs.CurrentBlock);
            drawBlock(gs.CurrentBlock);
            drawNextBlock(gs.queue);
            drawHeldBlock(gs.heldBlock);
            ScoreText.Text = $"Score: {gs.score}";

        }

        private void drawHeldBlock(block h)
        {
            if(h == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[h.Id];
            }
        }
        private void drawGhostBlock(block b)
        {
            int dropDistance = gs.blockDropDistance();
            foreach(position i in b.TilePositions())
            {
                imageControls[i.row + dropDistance, i.column].Opacity = 0.15;
                imageControls[i.row + dropDistance, i.column].Source = tileImages[b.Id];
            }
        }
        private async Task GameLoop()
        {
            draw(gs);
            while (!gs.gameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gs.score * delayDecrease));
                await Task.Delay(500);
                gs.moveBlockDown();
                draw(gs);
            }
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gs.score}";
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gs.gameOver)
            {
                return;
            }
            switch(e.Key)
            {
                case Key.Left:
                    gs.moveBlockLeft();
                    break;
                case Key.Right:
                    gs.moveBlockRight();
                    break;
                case Key.Down:
                    gs.moveBlockDown();
                    break;
                case Key.Up:
                    gs.rotateBlockCW();
                    break;
                case Key.Z:
                    gs.rotateBlockCCW();
                    break;
                case Key.C:
                    gs.holdBlock();
                    break;
                case Key.Space:
                    gs.dropBlock();
                    break;
                default:
                    return;
            }
            draw(gs);
        }

        private async void gameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private  async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gs = new gameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }


    }
}
