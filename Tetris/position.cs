﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class position
    {
        public int row { get; set; }
        public int column { get; set; }
        public position(int r, int c)
        {
            row = r; column = c;
        }
    }
}
