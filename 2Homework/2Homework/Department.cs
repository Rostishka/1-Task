using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace _2Homework
{
    public class Department : BaseEntity, IComparable
    {
        public List<Book> Books { get; set; }

        public Department(string name, int id)
        {
            Name = name;
            Id = id;
            Books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public int CountBooks()
        {
            int q = 0;
            foreach (var item in Books)
            {
                q += item.Quontaty;
            }
            return q;
        }

        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                Department otherDepartment = obj as Department;
                if (otherDepartment != null)
                {
                    return CountBooks().CompareTo(otherDepartment.CountBooks());
                }
                else
                    throw new ArgumentException("Object doesn't contain any books");
            }
            else return 0;
        }

        public static void ShowComparapble(Department d1, Department d2)
        {
            Console.WriteLine("Department {0} contains more books than Department {1} : {2}", d1.Name, d2.Name, d1.CompareTo(d2));
        }

        public override XElement WriteToXml()
        {
           var departmentRoot = new XElement(GetType().Name,
                            new XAttribute("Id", Id),
                            new XAttribute("Name", Name));

            foreach (var b in Books)
                departmentRoot.Add(b.WriteToXml());

            return departmentRoot;
        }
    }
}