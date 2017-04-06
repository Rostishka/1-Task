using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork4WithAdapter
{
    class Command
    {
        public String _command;

        public Command(String command)
        {
            _command = command;
        }

        public virtual void Execution()
        {
            Console.WriteLine("Executing command: {0}", _command);
        }
    }
}
