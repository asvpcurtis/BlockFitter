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
            for (int i = 0; i < pieces.Count; i++)
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
        public State GetRandomState(Random r)
        {
            int containerTop = container.Top();
            int containerBottom = container.Bottom();
            int containerRight = container.Right();
            int containerLeft = container.Left();
            State copy = Clone();
            for (int i = 0; i < pieces.Count; i++)
            {
                List<BlockShape> orientations = pieces[i].Orientations().Select(o => o.Normalize()).ToList();
                int orientationIndex = r.Next(orientations.Count);
                BlockShape po = orientations[orientationIndex];

                int pieceRight = po.Right();
                int pieceBottom = po.Bottom();
                int xOffset = r.Next(containerRight - containerLeft - pieceRight + 1);
                int yOffset = r.Next(containerBottom - containerTop - pieceBottom + 1);
                copy.pieces[i] = po.Normalize(xOffset, yOffset);
            }
            return copy;
        }
        public State GetRandomNeighbour(Random r)
        {
            int containerTop = container.Top();
            int containerBottom = container.Bottom();
            int containerRight = container.Right();
            int containerLeft = container.Left();
            int pieceIndex = r.Next(pieces.Count);
            List<BlockShape> orientations = pieces[pieceIndex].Orientations().Select(o => o.Normalize()).ToList();
            int orientationIndex = r.Next(orientations.Count);
            BlockShape po = orientations[orientationIndex];
            int pieceRight = po.Right();
            int pieceBottom = po.Bottom();
            int xOffset = r.Next(containerRight - containerLeft - pieceRight + 1);
            int yOffset = r.Next(containerBottom - containerTop - pieceBottom + 1);
            State copy = Clone();
            copy.pieces[pieceIndex] = po.Normalize(xOffset, yOffset);
            return copy;
        }
        public int NumberOfOverlappingPieces()
        {
            int outsideBounds = pieces.Aggregate(0, (total, bs) => 
            {
                if (container.Contains(bs))
                {
                    return total;
                }
                return total + 1;
            });
            int pieceOverlaps = pieces.Aggregate(0, (total, bs1) =>
            {
                return total += pieces.Aggregate(0, (pTotal, bs2) =>
                {
                    if (bs1.Intersects(bs2))
                    {
                        return pTotal + 1;
                    }
                    return pTotal;
                });
            });
            return outsideBounds + pieceOverlaps;
        }
        public int SpaceUncovered()
        {
            throw new NotImplementedException();
        }
    }
}
