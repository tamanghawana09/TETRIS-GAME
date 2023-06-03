﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class OBlock : block
    {
        private readonly position[][] tiles = new position[][]
        {
            new position[]{new(0,0), new(0,1), new (1,0), new(1,1)}
        };

        public override int Id => 4;
        protected override position startOffSet => new position(0, 4);
        protected override position[][] Tiles => tiles;
    }
}
