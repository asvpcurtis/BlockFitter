using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    class SpaceCohesionHeuristic : IBlockFittingHeuristic
    {
        public int Evaluate(State state)
        {
            return state.SpaceCohesion();
        }
    }
}
