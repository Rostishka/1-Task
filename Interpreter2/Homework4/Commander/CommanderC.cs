using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
    class CommanderC
    {
        public CommanderC() { }

        public string Operation(string @command)
        { 
            switch (@command.ToLower())
            {
                case "add": return "Executing command: " + @command;
                case "delete": return "Executing command: " + @command;
                case "update": return "Executing command: " + @command;
                case "get": return "Executing command: " + @command;
                default: return "Wrong command";
            }
        }

    }
}
