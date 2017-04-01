using System;
using System.IO;

namespace AnonymMethods
{
    delegate void Anonym(); // сигнатура делегата  
    delegate void Anonym2(int start, int finish); // сигнатура делегата  
    class Program
    {
        static void Main(string[] args)
        {
            #region анонімні методи без параметрів
            // використовуємо анонімний метод   
            Anonym anonym = delegate 
            {
                DirectoryInfo dir = new DirectoryInfo("D:\\");
                foreach (DirectoryInfo d in dir.GetDirectories())
                    Console.WriteLine(d.Name);
            };
            anonym(); // виконання  

            Console.Read();
            #endregion

            Console.Clear();

            #region анонімний метод з параметрами
            Anonym2 anonym2 = delegate (int a, int b) {
                for (int i = a; i <= b; i++)
                    Console.Write("Спортсмен на {0} кiлометрi. До фiнiша {1}\n", i, b - i);

                Console.Read();
            };

            anonym2(1, 10);

            Console.Read();
            #endregion
        }
    }
}
