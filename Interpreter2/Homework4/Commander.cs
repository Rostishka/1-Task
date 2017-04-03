using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
    class Commander
    {
        abstract class Command
        {
            public abstract void Execute();
            public abstract void Undo();
        }
        // конкретная команда
        class ConcreteCommand : Command
        {
            Receiver receiver;
            public ConcreteCommand(Receiver r)
            {
                receiver = r;
            }
            public override void Execute()
            {
                receiver.Operaiton();
            }

            public override void Undo()
            { }
        }

        // получатель команды
        class Receiver
        {
            public void Operaiton()
            { }
        }
        // инициатор команды
        class Invoker
        {
            Command command;
            public void SetCommand(Command c)
            {
                command = c;
            }
            public void Run()
            {
                command.Execute();
            }
            public void Cancel()
            {
                command.Undo();
            }
        }
        class Client
        {
            void Main()
            {
                Invoker invoker = new Invoker();
                Receiver receiver = new Receiver();
                ConcreteCommand command = new ConcreteCommand(receiver);
                invoker.SetCommand(command);
                invoker.Run();
            }
        }
    }
}
