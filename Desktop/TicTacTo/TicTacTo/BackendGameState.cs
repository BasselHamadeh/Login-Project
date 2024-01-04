using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacTo
{
    public class BackendGameState
    {
        public BackendPlayer[,] GameGrid { get; private set; }
        public BackendPlayer CurrentPlayer { get; private set; }
        public int Turn { get; private set; }
        public bool GameOver { get; private set; }

        public event Action<int, int> MoveMade;
        public event Action<BackendGameResult> GameEnded;
        public event Action GameRestarted;

        public BackendGameState()
        {
            GameGrid = new BackendPlayer[3, 3];
            CurrentPlayer = BackendPlayer.X;
            Turn = 0;
            GameOver = false;
        }

        public bool MakeMove(int r, int c)
        {
            if (!GameOver && GameGrid[r, c] == BackendPlayer.None)
            {
                GameGrid[r, c] = CurrentPlayer;
                Turn++;

                if (EndGameMove(r, c, out BackendGameResult game))
                {
                    GameOver = true;
                    GameEnded?.Invoke(game);
                }
                else
                {
                    SwitchPlayer();
                }

                MoveMade(r, c);
                return true;
            }

            return false;
        }

        public bool GridFull()
        {
            return Turn == 9;
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == BackendPlayer.X ? BackendPlayer.O : BackendPlayer.X;
        }

        public bool SquaresMarked((int, int)[] squares, BackendPlayer player)
        {
            foreach ((int r, int c) in squares)
            {
                if (GameGrid[r, c] != player)
                {
                    return false;
                }
            }

            return true;
        }

        public bool WinningMove(int r, int c, out BackendInfo info)
        {
            (int, int)[] row = new[] { (r, 0), (r, 1), (r, 2) };
            (int, int)[] column = new[] { (0, c), (1, c), (2, c) };
            (int, int)[] mainDia = new[] { (0, 0), (1, 1), (2, 2) };
            (int, int)[] antiDia = new[] { (0, 2), (1, 1), (2, 0) };

            if (SquaresMarked(row, CurrentPlayer))
            {
                info = new BackendInfo { Type = BackendWin.Row, Number = r };
                return true;
            }

            if (SquaresMarked(column, CurrentPlayer))
            {
                info = new BackendInfo { Type = BackendWin.Column, Number = c };
                return true;
            }

            if (SquaresMarked(mainDia, CurrentPlayer))
            {
                info = new BackendInfo { Type = BackendWin.MainDiaginal };
                return true;
            }

            if (SquaresMarked(antiDia, CurrentPlayer))
            {
                info = new BackendInfo { Type = BackendWin.AntiDiagonal };
                return true;
            }

            info = null;
            return false;
        }

        public bool EndGameMove(int r, int c, out BackendGameResult game)
        {
            if (WinningMove(r, c, out BackendInfo info))
            {
                game = new BackendGameResult { Winner = CurrentPlayer, BackendInfo = info };
                return true;
            }

            if (GridFull())
            {
                game = new BackendGameResult { Winner = BackendPlayer.None };
                return true;
            }

            game = null;
            return false;
        }

        public void ResetGame()
        {
            GameGrid = new BackendPlayer[3, 3];
            CurrentPlayer = BackendPlayer.X;
            Turn = 0;
            GameOver = false;
            GameRestarted?.Invoke();
        }
    }
}