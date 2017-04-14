using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Invoker invoker = new Invoker();
            //CommanderC comm = new CommanderC();
            //Command addCom = new CrudCommand("add", comm);
            //addCom.Execute();

            //invoker.Compute("add", comm);

            //Console.WriteLine(invoker._commands.Capacity);

            Context context = new Context("add");
            Expression exp = new Expression();
            exp.Interpret(context);

            Console.ReadKey();
        }
    }
}
