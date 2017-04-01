using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSysInfo.Run();
            //ReadFile.RunFile();
            //ReadFile.RunStream();
            ReadFile.RunFileStream();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            
        }

    }
}
