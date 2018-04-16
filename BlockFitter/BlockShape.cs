using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    public class BlockShape
    {
        public List<Unit> Units { get; set; }
        public BlockShape Normalize()
        {
            return Normalize(0, 0);
        }
        public BlockShape Normalize(int xOffset, int yOffset)
        {
            int left = Left();
            int top = Top();
            return new BlockShape
            {
                Units = this.Units.Select(u => new Unit
                {
                    X = u.X - left + xOffset,
                    Y = u.Y - top + yOffset
                }).ToList()
            };
        }
        public List<BlockShape> Orientations()
        {
            BlockShape r0cw = Copy();
            BlockShape r90cw = Rotate90Clockwise(r0cw);
            BlockShape r180cw = Rotate90Clockwise(r90cw);
            BlockShape r270cw = Rotate90Clockwise(r180cw);
            BlockShape r0cwrx = ReflectXAxis(r0cw);
            BlockShape r90cwrx = ReflectXAxis(r90cw);
            BlockShape r180cwrx = ReflectXAxis(r180cw);
            BlockShape r270cwrx = ReflectXAxis(r270cw);
            return new List<BlockShape>
            {
                r0cw,
                r90cw,
                r180cw,
                r270cw,
                r0cwrx,
                r90cwrx,
                r180cwrx,
                r270cwrx
            };
        }
        public BlockShape Copy()
        {
            return new BlockShape
            {
                Units = this.Units.Select(u => new Unit
                {
                    X = u.X,
                    Y = u.Y
                }).ToList()
            };
        }
        private BlockShape Rotate90Clockwise(BlockShape bs)
        {
            return new BlockShape
            {
                Units = bs.Units.Select(u => new Unit
                {
                    X = -u.Y,
                    Y = u.X
                }).ToList()
            };
        }
        private BlockShape ReflectXAxis(BlockShape bs)
        {
            return new BlockShape
            {
                Units = bs.Units.Select(u => new Unit
                {
                    X = -u.X,
                    Y = u.Y
                }).ToList()
            };
        }
        public int Left()
        {
            return Units.Min(e => e.X);
        }
        public int Right()
        {
            return Units.Max(e => e.X);
        }
        public int Top()
        {
            return Units.Min(e => e.Y);
        }
        public int Bottom()
        {
            return Units.Max(e => e.Y);
        }
        public bool Contains(BlockShape bs)
        {
            return bs.Units.TrueForAll(bsu => 
                Units.Any(u => 
                    bsu.X == u.X && bsu.Y == u.Y));
        }
        public bool Intersects(BlockShape bs)
        {
            return bs.Units.Any(bsu =>
                Units.Any(u =>
                    bsu.X == u.X && bsu.Y == u.Y));
        }
    }
}
