using LinqP2.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StreamDemo
{
    class Serializer
    {
        private static string _fileName = "LIbraryData.xml";
        public static void Serialize()
        {
            Department dept = CreateSource();
            XmlSerializer serializer = new XmlSerializer(typeof(Department));
            using (StreamWriter writer = new StreamWriter(_fileName))
            {
                serializer.Serialize(writer, dept);
            }
            DisplayDepartment(dept);
        }
        public static void Deserialize()
        {
            
            XmlSerializer serializer = new XmlSerializer(typeof(Department));
            using (StreamReader reader = new StreamReader(_fileName))
            {
                Department dept = serializer.Deserialize(reader) as Department;
                DisplayDepartment(dept);
            }
        }

        private static void DisplayDepartment(Department dept)
        {
            Console.WriteLine("Department name:{0}", dept.Name);
            foreach (var book in dept.Books)
            {
                Console.WriteLine("Id:{0}", book.Id);
                Console.WriteLine("Title:{0}", book.Title);
                Console.WriteLine("Price:{0}", book.Price);
                Console.WriteLine("Count:{0}", book.Count);
                string autors = String.Join(" ", book.Autors);
                Console.WriteLine("Autors:{0}", autors);
                Console.WriteLine();
            }
        }

        private static Department CreateSource()
        {
            var department = new Department();
            department.Name = "Scientific";

            var book1 = new Book()
            {
                Id = 1,
                Title = "MS Word",
                Price = 10
            };
            book1.Autors.AddRange(new[] { "Autor1", "Autor2" });

            var book2 = new Book()
            {
                Id = 1,
                Title = "MS Excel",
                Price = 10
            };
            book2.Autors.AddRange(new[] { "Autor2", "Autor3" });

            department.Books.AddRange(new[] {
                book1,
                book2
            });
            return department;
        }
    }
}
