using System;

namespace LambdaExpressions
{
    delegate int Pow(int value);
    delegate int LambdaDelegate(int step);

    class Program
    {
        static void Main(string[] args)
        {
            #region одиночні лямбда вирази
            Pow pow = value => value * value;
            Console.WriteLine(pow(10)); // ризультат будет 100  
            Console.ReadLine();
            #endregion

            Console.Clear();

            #region переписаний приклад про бігуна
            LambdaDelegate lambdaDel = a => ++a;

            int step = 1;
            int finish = 10;

            while (step <= finish)
            {
                Console.Write("Спортсмен на {0} кiлометрi. До фiнiша {1}\n", step, finish - step);
                step = lambdaDel(step);
            }

            Console.ReadLine();
            #endregion

            Console.ReadLine();
            Console.Clear();

            #region блочні лямбда вирази
            Pow pow2 = value =>
            {
                if (value != 0)
                    return value * value;
                return 0;
            };
            Console.WriteLine(pow(10));
            Console.ReadLine();
            #endregion 
        }
    }
}
