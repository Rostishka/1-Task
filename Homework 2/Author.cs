using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NEWLIBRARY
{
    public class Author : ICountingBooks, IComparable
    {

        [XmlElement]
        public string Name { get; set; }

        [XmlAttribute]
        public int Id { get; set; }

        public List<Book> Books { get; set; }

        public Author(string name, int age, int id)
        {
            Name = name;
            Id = id;
            Age = age;
            Books = new List<Book>();
        }

        public Author()
        {
            Books = new List<Book>();
        }

        [XmlAttribute]
        public int Age { get; set; }

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
                Author otherAuthor = obj as Author;
                if (otherAuthor != null)
                {
                    return CountBooks().CompareTo(otherAuthor.CountBooks());
                }
                else
                    throw new ArgumentException("Object doesn't contain any books");
            }
            else return 0;
        }
        public static void ShowComparapble(Author a1, Author a2)
        {
            Console.WriteLine("Department {0} contains more books than Department {1} : {2}", a1.Name, a2.Name, a1.CompareTo(a2));
        }
    }
}
