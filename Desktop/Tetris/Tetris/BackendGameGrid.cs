using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BackendGameGrid
    {
        public readonly int[,] grid;

        public int Rows { get; }
        public int Column { get; }

        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public BackendGameGrid(int rows, int columns)
        {
            Rows = rows;
            Column = columns;
            grid = new int[rows, columns];
        }

        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Column;
        }

        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }

        public bool IsRowFull(int r)
        {
            for (int i = 0; i < Column; i++)
            {
                if (grid[r, i] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsRowEmpty(int r)
        {
            for (int i = 0; i < Column; i++)
            {
                if (grid[r, i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void ClearRow(int r)
        {
            for (int i = 0; i < Column; i++)
            {
                grid[r, i] = 0;
            }
        }

        public void MoveRowDown(int r, int numRows)
        {
            for (int i = 0; i < Column; i++)
            {
                grid[r + numRows, i] = grid[r, i];
                grid[r, i] = 0;
            }
        }

        public int ClearFullRows()
        {
            int cleared = 0;

            for (int i = Rows - 1; i >= 0; i--)
            {
                if (IsRowFull(i))
                {
                    ClearRow(i);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(i, cleared);
                }
            }

            return cleared;
        }
    }
}
