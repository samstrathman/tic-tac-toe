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

namespace TicTacToe299
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game TTT_Game = new Game();
        AI_Player AI  = new AI_Player();

        public MainWindow()
        {
            InitializeComponent();
            DrawGridlines();
        }
        void DrawGridlines()
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    Border b = new Border();
                    b.BorderBrush = Brushes.Black;
                    b.BorderThickness = new Thickness(1);

                    PlayGrid.Children.Add(b);
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                }
            }
        }

        void DrawX(int row, int col)
        {
            const int CELL_PADDING = 20;
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.X1 = CELL_PADDING;
            line.X2 = PlayGrid.ActualWidth / 3 - CELL_PADDING;
            line.Y1 = CELL_PADDING;
            line.Y2 = PlayGrid.ActualHeight / 3 - CELL_PADDING;
            line.StrokeThickness = 3;
            PlayGrid.Children.Add(line);
            Grid.SetRow(line, row);
            Grid.SetColumn(line, col);

            Line line2 = new Line();
            line2.Stroke = Brushes.Black;
            line2.X1 = PlayGrid.ActualWidth / 3 - CELL_PADDING;
            line2.X2 = CELL_PADDING;
            line2.Y1 = CELL_PADDING;
            line2.Y2 = PlayGrid.ActualHeight / 3 - CELL_PADDING;
            line2.StrokeThickness = 3;
            PlayGrid.Children.Add(line2);
            Grid.SetRow(line2, row);
            Grid.SetColumn(line2, col);
        }
        void DrawO(int row, int col)
        {
            const int CELL_PADDING = 20;
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.Black;
            ellipse.Height = PlayGrid.ActualHeight / 3 - CELL_PADDING;
            ellipse.Width = PlayGrid.ActualWidth / 3 - CELL_PADDING;
            ellipse.StrokeThickness = 3;
            PlayGrid.Children.Add(ellipse);
            Grid.SetRow(ellipse, row);
            Grid.SetColumn(ellipse, col);
        }

        private void PlayGrid_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            var point = Mouse.GetPosition(PlayGrid);

            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in PlayGrid.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calc col mouse was over
            foreach (var columnDefinition in PlayGrid.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }
            if (TTT_Game.CellIsEmpty(row, col))
            {
                MarkBoard(row, col);
                char winner = TTT_Game.checkWinner().Item1;
                if (winner == ' ')
                {
                    Spot S = AI.GetBestMove(TTT_Game, 3);
                    //MessageBox.Show(string.Format("Score is {0}!", S.score));
                    MarkBoard(S.row, S.col);
                }
            }
        }
        private void MarkBoard(int row, int col)
        {
            if (TTT_Game.getTurn() == 'X' && TTT_Game.CellIsEmpty(row, col))
            {
                DrawX(row, col);
                TTT_Game.MarkGameBoard(row, col);
            }
            else if (TTT_Game.getTurn() == 'O' && TTT_Game.CellIsEmpty(row, col))
            {
                DrawO(row, col);
                TTT_Game.MarkGameBoard(row, col);
            }

            char winner = TTT_Game.checkWinner().Item1;
            if (winner != ' ')
            {
                List<Tuple<int, int>> winningSpots = TTT_Game.checkWinner().Item2;
                foreach (var child in PlayGrid.Children)
                {
                    var shape = child as Shape;

                    if (shape != null)
                    {
                        foreach (var spot in winningSpots)
                        {
                            if (Grid.GetRow(shape) == spot.Item1 && Grid.GetColumn(shape) == spot.Item2)
                            {
                                shape.Stroke = Brushes.Green;
                                shape.StrokeThickness = 6;
                            }
                        }
                    }
                }
                MessageBox.Show(string.Format("Winner is {0}!", winner));
                reset();
            }
            else if (TTT_Game.BoardIsFull())
            {
                MessageBox.Show(string.Format("CAT SCRATCH!"));
                reset();
            }
        }
        void reset()
        {
            PlayGrid.Children.Clear();
            DrawGridlines();
            TTT_Game = new Game(TTT_Game.getTurn());
        }
    }
}
