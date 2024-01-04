using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BackendIBlock : BackendBlock
    {
        public readonly BackendPosition[][] tile = new BackendPosition[][]
        {
            new BackendPosition[] { new BackendPosition(1, 0), new BackendPosition(1, 1), new BackendPosition(1, 2), new BackendPosition(1, 3), },
            new BackendPosition[] { new BackendPosition(0, 2), new BackendPosition(1, 2), new BackendPosition(2, 2), new BackendPosition(3, 2), },
            new BackendPosition[] { new BackendPosition(2, 0), new BackendPosition(2, 1), new BackendPosition(2, 2), new BackendPosition(2, 3), },
            new BackendPosition[] { new BackendPosition(0, 1), new BackendPosition(1, 1), new BackendPosition(2, 1), new BackendPosition(3, 1), },
        };

        public override int ID => 1;
        protected override BackendPosition StartOffSet => new BackendPosition(-1, 3);
        protected override BackendPosition[][] Tile => tile;
    }
}
