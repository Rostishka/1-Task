using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
    class Invoker
    {
        public CommanderC _commander;
        public List<Command> _commands = new List<Command>();

        public void Compute(string @command)
        {
            // Create command operation and execute it
            Command someCommand = new ConcreteCommand(@command);
            someCommand.Execute();

            //Add command into command list
            _commands.Add(someCommand);
        }
    }
}
