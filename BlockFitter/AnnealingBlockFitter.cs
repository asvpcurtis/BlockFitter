using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    class AnnealingBlockFitter : IBlockFitter
    {
        IBlockFittingHeuristic heuristic;
        Random r;
        public AnnealingBlockFitter(IBlockFittingHeuristic heuristic)
        {
            r = new Random();
            this.heuristic = heuristic;
        }
        public State Climb(State problem, long timeoutMillis)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            State current = problem.GetRandomState(r);
            int currentEval = heuristic.Evaluate(current);
            long elapsed = sw.ElapsedMilliseconds;
            while (elapsed < timeoutMillis)
            {
                State next = current.GetRandomNeighbour(r);
                int nextEval = heuristic.Evaluate(next);
                double temp = Temperature(timeoutMillis, elapsed);
                if (nextEval <= currentEval || Worsen(temp, currentEval, nextEval))
                {
                    current = next;
                    currentEval = nextEval;
                }
                elapsed = sw.ElapsedMilliseconds;
            }
            return current;
        }
        private double Temperature(long timeoutMillis, long elapsed)
        {
            return (double)(timeoutMillis - elapsed) * 100 / (double) timeoutMillis;
        }
        private bool Worsen(double temp, int currentEval, int nextEval)
        {
            double diff = currentEval - nextEval;
            double thresh = Math.Exp(diff / temp);
            return thresh > r.NextDouble();
        }

    }
}
