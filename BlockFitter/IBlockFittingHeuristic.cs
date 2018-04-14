using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    interface IBlockFittingHeuristic
    {
        int Evaluate(State state);
    }
}
