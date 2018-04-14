using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    class State
    {
        public BlockShape container;
        public IEnumerable<BlockShape> pieces;
        public State(BlockShape container, IEnumerable<BlockShape> pieces)
        {
            this.container = container;
            this.pieces = pieces;
        }

        public State Clone()
        {
            return new State(container, pieces.Select(bs => bs.Copy()).ToList());
        }

        public IEnumerable<State> GetNeighbours()
        {
            List<State> neighbours = new List<State>();
            for (int i = 0; i < pieces.Count(); i++)
            {

            }
        }
    }
}
