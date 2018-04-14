using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    class GeneticBlockFitter : IBlockFitter
    {
        int populationSize;
        Random r;
        public GeneticBlockFitter(int populationSize)
        {
            this.populationSize = populationSize;
            r = new Random();
        }
        public State Climb(State problem, long timeoutMillis)
        {
            throw new NotImplementedException();
        }
    }
}
