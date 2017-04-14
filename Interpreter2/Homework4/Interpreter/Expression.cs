using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
   class Expression
    {
        public void Interpret(Context context)
        {
            if (context.Input.Length == 0)
                return;

            if (context.Input.StartsWith("add"))
            {
                Invoker invoker = new Invoker();
                CommanderC comm = new CommanderC();
                invoker.Compute("add", comm);
            }
            //else if (context.Input.StartsWith(Delete()))
            //{

            //}
            //else if (context.Input.StartsWith(Update()))
            //{

            //}
            //else if (context.Input.StartsWith(Get()))
            //{

        }

            //while (context.Input.StartsWith())
            //{
            //    context.Input = context.Input.Substring(1);
            //}
        }

        //public abstract string Add();
        //public abstract string Delete();
        //public abstract string Update();
        //public abstract string Get();
}
