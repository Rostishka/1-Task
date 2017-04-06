using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
     public class ConcreteCommand : Command
    {
        public string _commandConcrete;
        CommanderC _commander;
        public ConcreteCommand(/*Receiver r,*/ string @command)
        {
            //this._receiver = r;
            this._commandConcrete = @command;
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
