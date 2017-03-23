using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinqP2
{
    class ReflectionHowTO
    {
        public static void Run()
        {
            Assembly assembly = Assembly.Load("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken= b77a5c561934e089");
            
            var pubTypesQuery = from type in assembly.GetTypes()
                                where type.IsPublic
                                from method in type.GetMethods()
                                where method.ReturnType.IsArray == true
                                    || (method.ReturnType.GetInterface(
                                        typeof(IEnumerable<>).FullName) != null
                                    && method.ReturnType.FullName != "System.String")
                                group method.ToString() by type.ToString();

            foreach (var groupOfMethods in pubTypesQuery)
            {
                Console.WriteLine("Type: {0}", groupOfMethods.Key);
                foreach (var method in groupOfMethods)
                {
                    Console.WriteLine("  {0}", method);
                }
                Console.WriteLine("-------------------------------------------------------------------------");
            }
        }
    }
}
