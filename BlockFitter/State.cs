using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    public class State
    {
        public BlockShape container;
        public List<BlockShape> pieces;
        public State(BlockShape container, List<BlockShape> pieces)
        {
            this.container = container;
            this.pieces = pieces;
        }

        public State Copy()
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
                            State copy = Copy();
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
            State copy = Copy();
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
            State copy = Copy();
            copy.pieces[pieceIndex] = po.Normalize(xOffset, yOffset);
            return copy;
        }
        public int NumberOfIntersectingPieces()
        {
            int outsideBounds = pieces
                .Where(p => !container.Contains(p))
                .Count();
            int pieceOverlaps = pieces
                .Where(p1 => pieces
                    .Any(p2 => p1 != p2 && p1.Intersects(p2)))
                .Count();
            return outsideBounds + pieceOverlaps;
        }
        public int SpaceCohesion()
        {
            List<Unit> uncoveredUnits = container.Units
                .Where(cu => pieces
                    .SelectMany(p => p.Units)
                    .All(pu => pu.X != cu.X || pu.Y != cu.Y)).ToList();
            int seperationPenalty = uncoveredUnits.Aggregate(0, (sum1, u1) =>
                    sum1 += uncoveredUnits.Aggregate(0, (sum2, u2) =>
                        sum2 += Math.Abs(u1.X - u2.X) + Math.Abs(u1.Y - u2.Y)));
            return seperationPenalty + SpaceUncovered();
        }
        public int SpaceUncovered()
        {
            int spaceUncovered = container.Units
                .Where(cu => pieces
                    .SelectMany(p => p.Units)
                    .All(pu => pu.X != cu.X || pu.Y != cu.Y))
                .Count();
            return spaceUncovered;
        }
    }
}
