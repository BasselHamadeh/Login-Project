using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class BackendBlock
    {
        protected abstract BackendPosition[][] Tile { get; }
        protected abstract BackendPosition StartOffSet { get; }
        public abstract int ID { get; }

        public int rotation;
        public BackendPosition offset;

        public BackendBlock()
        {
            offset = new BackendPosition(StartOffSet.Row, StartOffSet.Column);
        }

        public IEnumerable<BackendPosition> TilePosition()
        {
            foreach (BackendPosition p in Tile[rotation])
            {
                yield return new BackendPosition(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        public void RotateCW()
        {
            rotation = (rotation + 1) % Tile.Length;
        }

        public void RotateCCW()
        {
            if (rotation == 0)
            {
                rotation = Tile.Length - 1;
            }
            else
            {
                rotation--;
            }
        }

        public void Move(int rows, int column)
        {
            offset.Row += rows;
            offset.Column += column;
        }

        public void Reset()
        {
            rotation = 0;
            offset.Row = StartOffSet.Row;
            offset.Column = StartOffSet.Column;
        }
    }
}
