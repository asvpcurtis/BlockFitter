using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    class IntersectingPiecesHeuristic : IBlockFittingHeuristic
    {
        public int Evaluate(State state)
        {
            return state.NumberOfIntersectingPieces();
        }
    }
}
