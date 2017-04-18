using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    class Program
    {
        static void Main(string[] args)
        {
            LineManager manager = new LineManager(@"E:\vsyake\laba2.txt");
            manager.DeleteLine(manager.CountLines(), manager._filePath);
           
            Console.ReadKey();
        }
    }
}
