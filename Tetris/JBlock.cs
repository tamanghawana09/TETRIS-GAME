using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class JBlock : block
    {
        private readonly position[][] tiles = new position[][]
        {
            new position[] { new(0, 0), new(1, 0), new(1, 1), new(1, 2) },
            new position[] { new(0, 1), new(0, 2), new(1, 1), new(2, 1) },
            new position[] { new(1, 0), new(1, 1), new(1, 2), new(2, 2) },
            new position[] { new(0, 1), new(1, 1), new(2, 0), new(2, 1) }
        };
        public override int Id => 2;
        protected override position startOffSet => new position(0, 3);
        protected override position[][] Tiles => tiles;
    }
}
