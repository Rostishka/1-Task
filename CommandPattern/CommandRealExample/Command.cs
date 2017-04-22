using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandRealExample
{
    abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }
}
