using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockFitter
{
    class Config
    {
        public string Heuristic { get; set; }
        public string HillClimbStrategy { get; set; }
        public long TimeoutMillis { get; set; }
        public State Problem { get; set; }
    }
}
