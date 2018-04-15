using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    class GeneticBlockFitter : IBlockFitter
    {
        int populationSize;
        Random r;
        IBlockFittingHeuristic heuristic;
        public GeneticBlockFitter(IBlockFittingHeuristic heuristic, int populationSize)
        {
            this.heuristic = heuristic;
            this.populationSize = populationSize;
            r = new Random();
        }
        public State Climb(State problem, long timeoutMillis)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<State> population = new List<State>();
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(problem.GetRandomState(r));
            }
            while (sw.ElapsedMilliseconds < timeoutMillis)
            {
                population = NextGen(population);
                if (heuristic.Evaluate(population.Take(1).ToList()[0]) == 0)
                {
                    return population.Take(1).ToList()[0];
                }
            }
            return population.Take(1).ToList()[0];
        }

        public State Breed(State state1, State state2)
        {
            State child = state1.Copy();
            for (int i = 0; i < child.pieces.Count; i++)
            {
                if (r.Next(1) == 0)
                {
                    child.pieces[i] = state2.pieces[i].Copy();
                }
            }
            return child.GetRandomNeighbour(r);
        }
        private List<State> NextGen(List<State> population)
        {
            List<State> Children = population.SelectMany(s1 => 
                population.Select(s2 => Breed(s1, s2))).ToList();
            population.AddRange(Children);
            return population.Select(s => (heuristic.Evaluate(s), s))
                .OrderBy(f => f.Item1)
                .Take(populationSize).Select(f => f.Item2)
                .ToList();
        }
    }
}
