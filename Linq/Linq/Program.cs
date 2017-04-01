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

            //var filtredArr = someset.Where<int>(s => s > 60);

            //var filtredArr = someset.Select(s => "*" + s.ToString() + "use");

            //var filtredArr = someset.Where(s => s > 60).Select(s => "*" + s.ToString() + "use");

            //var filtredArr = someset.Single(s => s == 60);

            // var filtredArr = someset.Aggregate(0, (acc, i) => acc + i);

            var filtredArr = someset.Where(i => i < 5).Aggregate(2, (acc, i) => acc * i);

            //foreach (var w in filtredArr)
            Console.WriteLine(filtredArr);



            Console.ReadKey();
        }
    }
}
