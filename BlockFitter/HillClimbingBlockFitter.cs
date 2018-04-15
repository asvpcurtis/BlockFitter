using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    class HillClimbingBlockFitter : IBlockFitter
    {
        Random r;
        IBlockFittingHeuristic heuristic;
        public HillClimbingBlockFitter(IBlockFittingHeuristic heuristic)
        {
            r = new Random();
            this.heuristic = heuristic;
        }
        public State Climb(State problem, long timeoutMillis)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (heuristic.Evaluate(problem) == 0)
            {
                return problem;
            }
            State currState = problem.GetRandomState(r);
            int currEval = heuristic.Evaluate(currState);
            if (currEval == 0)
            {
                return problem;
            }
            List<State> neighbours = currState.GetNeighbours();
            while (neighbours.Count != 0 && 
                (sw.ElapsedMilliseconds < timeoutMillis))
            {
                State next = neighbours[0];
                neighbours.RemoveAt(0);
                int nextEval = heuristic.Evaluate(next);
                if (nextEval == 0)
                {
                    return next;
                }
                else if (nextEval < currEval)
                {
                    currState = next;
                    currEval = nextEval;
                    neighbours = currState.GetNeighbours();
                }
            }
            return currState;
        }
    }
}
