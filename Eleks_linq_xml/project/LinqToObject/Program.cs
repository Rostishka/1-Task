using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObject
{
    class Program
    {
        private static List<User> users = new List<User>();
        static void Main(string[] args)
        {
            #region first

            object[] castValues = new object[] { "1", "2", "3", "AAA", "5" };
            IEnumerable<string> result1 = castValues.Cast<string>();
            PrintSequence(result1);

            object[] ofTypeValues = new object[] { "1", "2", "3", "AAA", 5 };
            IEnumerable<string> result2 = ofTypeValues.OfType<string>(); // останній елемент буде опущено
            PrintSequence(result2);

            var distinctValues = new[] { "1", "1", "2", "2", "3", "3", "3" };
            IEnumerable<string> result3 = distinctValues.Distinct(); // "1", "2", "3"
            PrintSequence(result3);

            #endregion

            #region second part
            PrepareCollection();
            foreach(var user in users)
            {
                Console.WriteLine(user);
                Console.WriteLine();
            }
            #endregion

            Console.ReadLine();
            Console.Clear();          

        }

        private static void PrintSequence(IEnumerable<string> sequence)
        {
            foreach(var item in sequence)
            {
                Console.Write("{0}\t", item);
            }
            Console.WriteLine();
        }

        private static void PrepareCollection()
        {
            users.Add(new User { Age = 27, FirstName = "Maksym", LastName = "Muratov", Login = "Maksym.Muratov"});
            users.Add(new User { Age = 19, FirstName = "Taras", LastName = "Soroka", Login = "Taras.Soroka" });
            users.Add(new User { Age = 30, FirstName = "Anna", LastName = "Sokolvska", Login = "Anna.Sokolvska" });
            users.Add(new User { Age = 25, FirstName = "Ivan", LastName = "Fedorchuk", Login = "Ivan.Fedorchuk" });
            users.Add(new User { Age = 28, FirstName = "Lubomyr", LastName = "Mankiv", Login = "Lubomyr.Mankiv" });
            users.Add(new User { Age = 27, FirstName = "Marta", LastName = "Salaban", Login = "Marta.Salaban" });
            users.Add(new User { Age = 15, FirstName = "Ihor", LastName = "Fedorchuk", Login = "Ihor.Fedorchuk" });
            users.Add(new User { Age = 18, FirstName = "Ivan", LastName = "Mankiv", Login = "Ivan.Mankiv" });
        }
    }
}
