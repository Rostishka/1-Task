using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter2
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            // определяем набор переменных
            int x = 5;
            int y = 8;
            int z = 2;

            // добавляем переменные в контекст
            context.SetVariable("x", x);
            context.SetVariable("y", y);
            context.SetVariable("z", z);
            // создаем объект для вычисления выражения x + y - z
            IExpression expression = new SubtractExpression(
                new AddExpression(
                    new NumberExpression("x"), new NumberExpression("y")),
                new NumberExpression("z"));

            int result = expression.Interpret(context);
            Console.WriteLine("результат: {0}", result);

            Console.Read();
        }
    }
}
