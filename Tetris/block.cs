using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class block 
    {
        protected abstract position[][] Tiles { get; }
        protected abstract position startOffSet { get; }

        public abstract int Id { get; }

        private int rotationState;
        private position offSet;

        public block() 
        {
            offSet = new position(startOffSet.row, startOffSet.column);
        }
        public IEnumerable<position> TilePositions()
        {
            foreach(position i in Tiles[rotationState]) 
            {
                yield return new position(i.row + offSet.row, i.column + offSet.column);
            }
        }
        public void rotateCW()
        {
            rotationState =( rotationState + 1 ) % Tiles.Length; 
        }

        public void rotateCCW()
        {
            if(rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        public void move(int rows, int columns)
        {
            offSet.row += rows;
            offSet.column += columns;
        }
        public void reset()
        {
            rotationState=0;
            offSet.row = startOffSet.row;
            offSet.column = startOffSet.column;
        }
    }

}
