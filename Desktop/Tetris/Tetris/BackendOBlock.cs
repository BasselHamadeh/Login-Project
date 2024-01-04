using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BackendOBlock : BackendBlock
    {
        public readonly BackendPosition[][] tile = new BackendPosition[][]
        {
            new BackendPosition[] { new BackendPosition(0, 0), new BackendPosition(0, 1), new BackendPosition(1, 0), new BackendPosition(1, 1) }
        };

        public override int ID => 4;
        protected override BackendPosition StartOffSet => new BackendPosition(0, 4);
        protected override BackendPosition[][] Tile => tile;
    }
}
