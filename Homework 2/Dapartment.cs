using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NEWLIBRARY
{
    public class Dapartment : ICountingBooks, IComparable
    {

        [XmlElement]
        public string Name { get; set; }

        [XmlAttribute]
        public int Id { get; set; }

        public List<Book> Books { get; set; }
        public List<Author> Autors { get; set; }

        public Dapartment(string name, int id)
        {
            Name = name;
            Id = id;
            Books = new List<Book>();
            Autors = new List<Author>();
        }

        public Dapartment()
        {
            Autors = new List<Author>();
            Books = new List<Book>();
        }

        public IEnumerable<Book> Where(Func<Book, bool> b)
        {
            foreach (var item in Books)
            {
                if (b(item))
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Author> Where(Func<Author, bool> a)
        {
            foreach (var item in Autors)
            {
                if (a(item))
                {
                    yield return item;
                }
            }
        }

        public int CountBooks()
        {
            int q = 0;
            foreach (var item in Autors)
            {
                q += item.CountBooks();
            }
            return q;
        }

        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                Dapartment otherDepartment = obj as Dapartment;
                if (otherDepartment != null)
                {
                    return CountBooks().CompareTo(otherDepartment.CountBooks());
                }
                else
                    throw new ArgumentException("Object doesn't contain any books");
            }
            else return 0;
        }

        public static void ShowComparapble(Dapartment d1, Dapartment d2)
        {
            Console.WriteLine("Department {0} contains more books than Department {1} : {2}", d1.Name, d2.Name, d1.CompareTo(d2));
        }
    }
}
