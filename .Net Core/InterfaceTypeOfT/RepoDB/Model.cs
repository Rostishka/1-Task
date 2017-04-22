using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoDB
{
    class Persor
    {
        public string Name { get; set; }
    }

    class Employee : Persor
    {
        public int Id { get; set; }

        public virtual void DoWork()
        {
            Console.WriteLine("Doing some work");
        }
    }

    class Manager : Employee
    {
        public override void DoWork()
        {
            Console.WriteLine("Control sombody's work");
        }
    }

}
