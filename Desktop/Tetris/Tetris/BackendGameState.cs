using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BackendGameState
    {
        public BackendBlock currentBlock;
        
        public BackendBlock CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);

                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        public BackendGameGrid gamegrid { get; }
        public BackendBlockQueue blockqueue { get; }
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        public BackendBlock HeldBlock { get; private set; }
        public bool CanHold { get; private set; }


        public BackendGameState()
        {
            gamegrid = new BackendGameGrid(22, 10);
            blockqueue = new BackendBlockQueue();
            CurrentBlock = blockqueue.GetUpdate();
            CanHold = true;
        }

        public void HoldBlock()
        {
            if (!CanHold)
            {
                return;
            }

            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = blockqueue.GetUpdate();
            }
            else
            {
                BackendBlock tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }

            CanHold = false;
        }

        public bool BlockFits()
        {
            foreach (BackendPosition p in CurrentBlock.TilePosition())
            {
                if (!gamegrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        public bool IsGameOver()
        {
            return !(gamegrid.IsRowEmpty(0) && gamegrid.IsRowEmpty(1));
        }

        public void PlaceBlock()
        {
            foreach (BackendPosition p in CurrentBlock.TilePosition())
            {
                gamegrid[p.Row, p.Column] = CurrentBlock.ID;
            }

            Score += gamegrid.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = blockqueue.GetUpdate();
                CanHold = true;
            }
        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }

        public int Tile(BackendPosition p)
        {
            int drop = 0;

            while (gamegrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }

            return drop;
        }

        public int BlockDrop()
        {
            int drop = gamegrid.Rows;

            foreach (BackendPosition p in CurrentBlock.TilePosition())
            {
                drop = System.Math.Min(drop, Tile(p));
            }

            return drop;
        }

        public void DropBlock()
        {
            CurrentBlock.Move(BlockDrop(), 0);
            PlaceBlock();
        }
    }
}
