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
        public List<BlockShape> pieces;
        public State(BlockShape container, List<BlockShape> pieces)
        {
            this.container = container;
            this.pieces = pieces;
        }

        public State Clone()
        {
            return new State(container, pieces.Select(bs => bs.Copy()).ToList());
        }

        public List<State> GetNeighbours()
        {
            int containerTop = container.Top();
            int containerBottom = container.Bottom();
            int containerRight = container.Right();
            int containerLeft = container.Left();
            List<State> neighbours = new List<State>();
            for (int i = 0; i < pieces.Count(); i++)
            {
                foreach (BlockShape po in pieces[i].Orientations().Select(o => o.Normalize()))
                {
                    int pieceRight = po.Right();
                    int pieceBottom = po.Bottom();
                    for (int xOffset = containerLeft; pieceRight + xOffset <= containerRight; xOffset++)
                    {
                        for (int yOffset = containerTop; pieceBottom + yOffset <= containerBottom; yOffset++)
                        {
                            State copy = Clone();
                            copy.pieces[i] = po.Normalize(xOffset, yOffset);
                            neighbours.Add(copy);
                        }
                    }
                }
            }
            return neighbours;
        }

    }
}
