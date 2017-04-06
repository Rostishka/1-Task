using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSet someset = new TestSet();

            // var filteredArray = someset.Where<int>(s => s > 60);

            //var filteredArray = someset.Select(s => "*" + s.ToString() + "use");

            //var filteredArray = someset.Where(s => s > 60).Select(s => "*" + s.ToString() + "use");

            //var filteredArray = someset.Single(s => s == 60);

            //var filteredArray = someset.Aggregate(0, (acc, i) => acc + i);

            //var filteredArray = someset.Where(i => i < 5).Aggregate(2, (acc, i) => acc * i);

            //var filteredArray = someset.Where(s => s > 20);

            //foreach (var w in filteredArray)
            //{
            //    Console.WriteLine(w);
            //}
           //Console.WriteLine(filteredArray);



            Console.ReadKey();
        }
    }
}
