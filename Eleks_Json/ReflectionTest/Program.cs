using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReflectionGenericTypes.Run();
            //EmitTest.Run();
            
            Assembly a = Assembly.LoadFrom("LinqP2.exe");
            Type[] types2 = a.GetTypes();
            foreach (Type t in types2)
            {
                Console.WriteLine(t.FullName);
            }
            Console.WriteLine();
            var bookType = GetTypeByName(a,"LinqP2.Library.Book");
            if (bookType != null)
            {
                object book = Activator.CreateInstance(bookType);
                SetValue(book, bookType, "Title", "Linq in .Net");
                SetValue(book, bookType, "Id", 1);
                SetValue(book, bookType, "Price", (decimal)150);
                Console.WriteLine(book);
                Console.WriteLine();
                MethodInfo[] methods = bookType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
                var getMethod = methods.FirstOrDefault(m=>m.Name== "GetTitleLength");
                if (getMethod != null)
                {
                    object result = getMethod.Invoke(book,null);
                    Console.WriteLine("Result of GetTitleLength:{0}",result);
                }

                var priceProp = bookType.GetProperty("Price");
                if (priceProp != null)
                {
                    decimal price = (decimal)priceProp.GetValue(book);
                    //var formatAttribute = priceProp.GetCustomAttributes().FirstOrDefault(t => t.GetType().Name == "FormatAttribute");
                    var format = priceProp.GetCustomAttributes<LinqP2.Library.FormatAttribute>().FirstOrDefault();
                    if (format != null)
                    {
                        Console.WriteLine("Formated price: " + price.ToString(format.FormatString));
                    }
                    else
                    {
                        Console.WriteLine("Formated price: " + price);
                    }
                    
                }
            }
            
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        static Type GetTypeByName(Assembly assembly, string className)
        {
            Type[] types = assembly.GetTypes();
            Type type = types.Where(t => t.FullName == className)
                             .FirstOrDefault();
            return type;
        }
        static void SetValue(object obj,Type objType, string propertyName,object value)
        {
            PropertyInfo[] properties = objType.GetProperties();
            PropertyInfo prop = properties.Where(p => p.Name == propertyName)
                                          .FirstOrDefault();
            if (prop != null)
            {
                prop.SetValue(obj, value);
            }
        }
      
    }
}
