using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BackendBlockQueue
    {
        public readonly BackendBlock[] block = new BackendBlock[]
        {
            new BackendIBlock(),
            new BackendJBlock(),
            new BackendLBlock(),
            new BackendOBlock(),
            new BackendSBlock(),
            new BackendTBlock(),
            new BackendZBlock(),
        };

        public readonly Random random = new Random();

        public BackendBlock NextBlock { get; private set; }

        public BackendBlockQueue()
        {
            NextBlock = RandomBlock();
        }

        public BackendBlock RandomBlock()
        {
            return block[random.Next(block.Length)];
        }

        public BackendBlock GetUpdate()
        {
            BackendBlock block = NextBlock;

            do
            {
                NextBlock = RandomBlock();
            } while (block.ID == NextBlock.ID);

            return block;
        }
    }
}
