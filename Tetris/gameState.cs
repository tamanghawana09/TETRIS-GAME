using System;
using System.Collections.Generic;
using System.ComponentModel;
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

                for(int i = 0; i<2; i++)
                {
                    currentBlock.move(1, 0);

                    if (!blockFits())
                    {
                        currentBlock.move(-1, 0);
                    }
                }
            }
        }

        public gameGrid grid { get; }
        public blockQueue queue { get; }
        public bool gameOver { get; private set; }

        public int score { get; private set; }
        public block heldBlock { get; private set; }
        public bool canHold { get; private set; }


        public gameState()
        {
            grid = new gameGrid(22, 10);
            queue = new blockQueue();
            CurrentBlock = queue.getAndUpdate();
            canHold = true;
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

        public void holdBlock()
        {
            if (!canHold)
            {
                return;
            }
            if(heldBlock == null)
            {
                heldBlock = CurrentBlock;
                CurrentBlock = queue.getAndUpdate();
            }
            else
            {
                block temp = CurrentBlock;
                CurrentBlock = heldBlock;
                heldBlock = temp;
            }
            canHold = false;
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
            score += grid.clearFullRows();
            if(IsGameOver())
            {
                gameOver = true;
            }
            else
            {
                CurrentBlock = queue.getAndUpdate();
                canHold = true;
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
        private int TileDropDistance(position p)
        {
            int drop = 0;
            while(grid.IsEmpty(p.row + drop +1, p.column))
            {
                drop++;
            }
            return drop;
        }

        public int blockDropDistance()
        {
            int drop = grid.rows;
            foreach(position p in CurrentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }
            return drop;

        }
        public void dropBlock()
        {
            CurrentBlock.move(blockDropDistance(), 0);
            placeBlock() ;
        }
    }
}
