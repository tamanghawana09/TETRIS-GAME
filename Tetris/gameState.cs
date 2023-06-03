using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class gameState
    {
        private block currentBlock;
        public block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.reset();
            }
        }

        public gameGrid grid { get; }
        public blockQueue queue { get; }
        public bool gameOver { get; private set; }

        public gameState()
        {
            grid = new gameGrid(22, 10);
            queue = new blockQueue();
            CurrentBlock = queue.getAndUpdate();
        }
        private bool blockFits()
        {
            foreach (position i in CurrentBlock.TilePositions())
            {
                if (!grid.IsEmpty(i.row, i.column))
                {
                    return false;
                }
            }
            return true;
        }

        public void rotateBlockCW()
        {
            CurrentBlock.rotateCW();
            if(!blockFits())
            {
                CurrentBlock.rotateCCW();
            }
        }
        public void rotateBlockCCW()
        {
            CurrentBlock.rotateCCW();
            if(!blockFits() )
            {
                CurrentBlock.rotateCW();
            }
        }

        public void moveBlockLeft()
        {
            CurrentBlock.move(0, -1);
            if (!blockFits())
            {
                currentBlock.move(0,1);
            }
        }

        public void moveBlockRight()
        {
            CurrentBlock.move(0, 1);
            if (!blockFits())
            {
                currentBlock.move(0, -1);  
            }
        }
        private bool IsGameOver()
        {
            return !(grid.IsRowEmpty(0) && grid.IsRowEmpty(1));
        }
        private void placeBlock()
        {
            foreach(position i in CurrentBlock.TilePositions()){
                grid[i.row, i.column] = CurrentBlock.Id;
            }
            grid.clearFullRows();
            if(IsGameOver())
            {
                gameOver = true;
            }
            else
            {
                CurrentBlock = queue.getAndUpdate();
            }
        }
        public void moveBlockDown()
        {
            currentBlock.move(1, 0);
            if (!blockFits())
            {
                currentBlock.move(-1, 0);
                placeBlock();
            }
        }
    }
}
