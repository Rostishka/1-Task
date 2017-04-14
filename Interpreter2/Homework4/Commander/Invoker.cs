using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
    public class Invoker
    {
        public List<Command> _commands = new List<Command>();

        public void Compute(string command, CommanderC commander)
        {
            // Create command operation and execute it
            Command someCommand = new CrudCommand(command, commander);
            //Add command into command list
            someCommand.Execute();
            _commands.Add(someCommand);
                 
        }
    }
}
