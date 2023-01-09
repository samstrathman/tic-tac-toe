using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe299
{
    class Game
    {
        public const int ROWS = 3;
        public const int COLS = 3;
        private int numFilledCells;
        private char [ , ] GameBoard;

        private char turn;

        public Game(char t = 'X')
        {
            GameBoard = new char[ROWS, COLS];

            turn = t;
            numFilledCells = 0;

            //initialize each cell to empty
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    GameBoard[i, j] = ' ';
                }
            }
        }

        //Copy constructor to make a deep Game copy
        public Game(Game G)
        {
            GameBoard = new char[ROWS, COLS];

            turn = G.turn;
            numFilledCells = G.numFilledCells;

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    GameBoard[i, j] = G.GameBoard[i, j];
                }
            }
        }

        public char[,] getGameBoard()
        {
            return GameBoard;
        }

        public char getTurn()
        {
            return turn;
        }

        public bool CellIsEmpty(int row, int col){
            return (GameBoard[row, col] == ' ');
        }

        public void MarkGameBoard(int row, int col){

            if (!CellIsEmpty(row, col))
            {
                return;
            }
            GameBoard[row, col] = turn;
            if (turn == 'X')
            {
                turn = 'O';
            }
            else
            {
                turn = 'X';
            }
            numFilledCells++;
        }

        public bool BoardIsFull()
        {
            return numFilledCells >= ROWS * COLS;
        }

        public Tuple<char, List<Tuple<int, int>>> checkWinner()
        {
            List<Tuple<int, int>> winningSpots = new List<Tuple<int, int>>();

            //check horizontal
            for (int i = 0; i < ROWS; i++)
            {
                if (GameBoard[i, 0] != ' ' && GameBoard[i, 0] == GameBoard[i, 1] && GameBoard[i, 1] == GameBoard[i, 2])
                {
                    //return GameBoard[i, 0];
                    winningSpots.Add(Tuple.Create(i, 0));
                    winningSpots.Add(Tuple.Create(i, 1));
                    winningSpots.Add(Tuple.Create(i, 2));
                    return Tuple.Create(GameBoard[i, 0], winningSpots);
                }
            }

            //check vertical
            for (int j = 0; j < COLS; j++)
            {
                if (GameBoard[0, j] != ' ' && GameBoard[0, j] == GameBoard[1, j] && GameBoard[1, j] == GameBoard[2, j])
                {
                    //return GameBoard[0, j];
                    winningSpots.Add(Tuple.Create(0, j));
                    winningSpots.Add(Tuple.Create(1, j));
                    winningSpots.Add(Tuple.Create(2, j));
                    return Tuple.Create(GameBoard[0, j], winningSpots);
                }
            }

            //check both diagonals
            if (GameBoard[0, 0] != ' ' && GameBoard[0, 0] == GameBoard[1, 1] && GameBoard[1, 1] == GameBoard[2, 2])
            {
                //return GameBoard[0, 0];
                winningSpots.Add(Tuple.Create(0, 0));
                winningSpots.Add(Tuple.Create(1, 1));
                winningSpots.Add(Tuple.Create(2, 2));
                return Tuple.Create(GameBoard[0, 0], winningSpots);
            }
            if (GameBoard[2, 0] != ' ' && GameBoard[2, 0] == GameBoard[1, 1] && GameBoard[1, 1] == GameBoard[0, 2])
            {
                //return GameBoard[2, 0];
                winningSpots.Add(Tuple.Create(2, 0));
                winningSpots.Add(Tuple.Create(1, 1));
                winningSpots.Add(Tuple.Create(0, 2));
                return Tuple.Create(GameBoard[2, 0], winningSpots);
            }
            //return ' ';
            return Tuple.Create(' ', winningSpots);
        }
    }
}
