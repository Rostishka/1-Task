using LinqP2.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqP2
{
    class Program
    {
        static void Main(string[] args)
        {
            //FindFileByExtension.Run();

            //GroupByExtension.Run();

            //CompareDirs.Run();

            ReflectionHowTO.Run();

            EnumerableApp.TestStreamReaderEnumerable();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
