﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToe299
{
    class Spot
    {
        public int row;
        public int col;
        public int score;
        public Spot()
        {
            row   = -1;
            col   = -1;
            score = 0;
        }
    }
    class AI_Player
    {
        public Spot GetBestMove(Game G, int difficulty)
        {
            return Minimax(G, 0, difficulty, G.getTurn());
        }
        private Spot Minimax(Game G, int level, int maxLevel, char turn)
        {
            Spot BestSpot = new Spot();

            //if current AI player
            if (level % 2 == 0)
            {
                BestSpot.score = -1000000;
            }
            //if AI opponent
            else
            {
                BestSpot.score = 1000000;
            }

            //if there is a winner or we are as deep as we are allowed to go
            if(G.checkWinner().Item1 != ' ' || G.BoardIsFull() || level >= maxLevel){
                BestSpot.score = scoreGame(G, level + 1, turn);
                //MessageBox.Show(string.Format("Score is {0}, level is {1}", BestSpot.score, level));
            }
            else{
                //note that consts in C# are static, meaning that to access 
                //them you should use the class name
                for (int i = 0; i < Game.ROWS; i++)
                {
                    for (int j = 0; j < Game.COLS; j++)
                    {
                        if (G.CellIsEmpty(i, j))
                        {
                            Game TmpGame = new Game(G);

                            TmpGame.MarkGameBoard(i, j);

                            Spot TmpSpot = new Spot();
                            TmpSpot.row = i;
                            TmpSpot.col = j;

                            //if (TmpGame.checkWinner().Item1 != ' ')
                            //{
                                TmpSpot.score = Minimax(TmpGame, level + 1, maxLevel, turn).score;

                                //MessageBox.Show(string.Format("Score is {0}, level is {1}", TmpSpot.score, level));
                                //if current AI player
                                if (level % 2 == 0)
                                {
                                    if (TmpSpot.score > BestSpot.score)
                                    {
                                        BestSpot = TmpSpot;
                                    }
                                }
                                //if AI opponent
                                else
                                {
                                    if (TmpSpot.score < BestSpot.score)
                                    {
                                        BestSpot = TmpSpot;
                                    }
                                }
                            //}
                        }
                    }
                }
            }
            //MessageBox.Show(string.Format("Score is {0}, level is {1}", BestSpot.score, level));
            return BestSpot;
        }
        private int scoreGame(Game G, int level, char turn){
            int score = 0;

            score += ScoreThreeInRow(G, turn) / (level * level);
            score += ScoreTwoInRowOpen(G, turn) / (level * level);

            return score;
        }
        private int ScoreThreeInRow(Game G, char turn){
            int score = 0;
            char checkWinner = G.checkWinner().Item1;
            if (checkWinner != ' ')
            {
                if (checkWinner == turn)
                {
                    score += 10000;
                }
                else
                {
                    score -= 10000;
                }
            }
            return score;
        }
        private int ScoreTwoInRowOpen(Game G, char turn)
        {
            int score = 0;
            //check horizontal
            for (int i = 0; i < Game.ROWS; i++)
            {
                if ((G.getGameBoard()[i, 0] == G.getGameBoard()[i, 1] && G.getGameBoard()[i, 2] == ' ') 
                   || (G.getGameBoard()[i, 1] == G.getGameBoard()[i, 2] && G.getGameBoard()[i, 0] == ' ')
                   || (G.getGameBoard()[i, 0] == G.getGameBoard()[i, 2] && G.getGameBoard()[i, 1] == ' '))
                {
                    if (G.getGameBoard()[i, 0] == turn || G.getGameBoard()[i, 1] == turn)
                    {
                        score += 100;
                    }
                    else{
                        score -= 100;
                    }
                }
            }

            //check vertical
            for (int j = 0; j < Game.COLS; j++)
            {
                if ((G.getGameBoard()[0, j] == G.getGameBoard()[1, j] && G.getGameBoard()[2, j] == ' ') 
                   || (G.getGameBoard()[1, j] == G.getGameBoard()[2, j] && G.getGameBoard()[0, j] == ' ')
                   || (G.getGameBoard()[0, j] == G.getGameBoard()[2, j] && G.getGameBoard()[1, j] == ' '))
                {

                    if (G.getGameBoard()[0, j] == turn || G.getGameBoard()[1, j] == turn)
                    {
                        score += 100;
                    }
                    else
                    {
                        score -= 100;
                    }
                }
            }

            //check both diagonals
            if ((G.getGameBoard()[0, 0] == G.getGameBoard()[1, 1] && G.getGameBoard()[2, 2] == ' ') 
                || (G.getGameBoard()[1, 1] == G.getGameBoard()[2, 2] && G.getGameBoard()[0, 0] == ' ')
                || (G.getGameBoard()[0, 0] == G.getGameBoard()[2, 2] && G.getGameBoard()[1, 1] == ' '))
            {
                if (G.getGameBoard()[0, 0] == turn || G.getGameBoard()[1, 1] == turn)
                {
                    score += 100;
                }
                else
                {
                    score -= 100;
                }   
            }
            if ((G.getGameBoard()[0, 2] == G.getGameBoard()[1, 1] && G.getGameBoard()[2, 0] == ' ') 
                || (G.getGameBoard()[1, 1] == G.getGameBoard()[2, 0] && G.getGameBoard()[0, 2] == ' ')
                || (G.getGameBoard()[0, 2] == G.getGameBoard()[2, 0] && G.getGameBoard()[1, 1] == ' '))
            {
                if (G.getGameBoard()[0, 2] == turn || G.getGameBoard()[1, 1] == turn)
                {
                    score += 100;
                }
                else
                {
                    score -= 100;
                }   
            }
            return score;
        }
    }
}

    
