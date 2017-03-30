using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpreterExample
{
    class ThousandExpression : Expression
    {
        public override string One() { return "M"; }
        public override string Four() { return "QM"; }
        public override string Five() { return "Q"; }
        public override string Nine() { return "MK"; }
        public override int Multiplier() { return 1000; }
    }
}
