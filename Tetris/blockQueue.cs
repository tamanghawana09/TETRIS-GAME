using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class blockQueue
    {
        private readonly block[] blocks = new block[] 
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };

        private readonly Random random = new Random();

        public block nextBlock { get; private set; }
        public blockQueue()
        {
            nextBlock = randomBlock();
        }
        
        private block randomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public block getAndUpdate()
        {
            block b = nextBlock;
            do
            {
                nextBlock = randomBlock();
            }
            while (b.Id == nextBlock.Id);
            return b;

        }

    }
}
