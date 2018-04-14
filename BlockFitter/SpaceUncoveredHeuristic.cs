using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    class SpaceUncoveredHeuristic : IBlockFittingHeuristic
    {
        public int Evaluate(State state)
        {
            return state.SpaceUncovered();
        }
    }
}
