using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    internal class Heuristic
    {
        int index;
        int value;

        int alpha;
        int beta;

        public int Index { get => index; set => index = value; }
        public int Value { get => value; set => this.value = value; }
        public int Alpha { get => alpha; set => alpha = value; }
        public int Beta { get => beta; set => beta = value; }
    }
}
