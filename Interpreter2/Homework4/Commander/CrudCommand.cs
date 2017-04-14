using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
     public class CrudCommand : Command
    {
        public string _commandConcrete;
        CommanderC _commander;

        public CrudCommand(string command, CommanderC commander)
        {
            this._commander = commander;
            this._commandConcrete = command;
        }

        public string Commanda
        {
            set { _commandConcrete = value; }
        }

        public override void Execute()
        {
            _commander.Operation(_commandConcrete);
        }
    }
}
