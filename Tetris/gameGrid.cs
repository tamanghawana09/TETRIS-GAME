using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class gameGrid
    {
        private readonly int[,] grid;
        public int rows { get; }
        public int columns { get; }

        public int this[int r , int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }


        public gameGrid(int r, int c)
        {
            rows = r; columns = c;
            grid = new int[r, c];
        }

        public bool IsInside(int r, int c)
        {
            return r >=0 && r < rows && c >= 0 && c < columns;
        }

        public bool IsEmpty(int r, int c)
        {
            return IsInside(r,c) && grid[r,c] == 0;
        }
        public bool IsRowFull(int r)
        {
            for(int c = 0; c < columns; c++)
            {
                if (grid[r,c] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsRowEmpty(int r)
        {
            for(int c=0;c<columns; c++)
            {
                if (grid[r,c] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void clearRow(int r)
        {
            for(int c=0;c< columns;c++)
            {
                grid[r,c] = 0;
            }
        }

        private void moveRowDown(int r,int numRows)
        {
            for (int c = 0; c < columns; c++)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }

        public int clearFullRows()
        {
            int cleared = 0;
            for(int r = rows-1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    clearRow(r);
                    cleared++;

                }
                else if (cleared>0)
                {
                    moveRowDown(r,cleared);
                }
            }
            return cleared;
        }
    }
}
